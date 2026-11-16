param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release'
)

# Run with Windows PowerShell 5.1: the application targets .NET Framework 4.8.
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
Add-Type -AssemblyName System.Windows.Forms
$assemblyPath = Join-Path $PSScriptRoot "..\TorrentChecker\bin\$Configuration\TorrentChecker.exe"
$assembly = [Reflection.Assembly]::LoadFrom((Resolve-Path $assemblyPath))
$atom = $assembly.GetType('TorrentChecker.clsRuTrackerAtom', $true)
$staticFlags = [Reflection.BindingFlags]'Static,NonPublic'
$instanceFlags = [Reflection.BindingFlags]'Instance,NonPublic'
$script:checks = 0

function Assert($condition, [string]$message) {
    if (-not $condition) { throw $message }
    $script:checks++
}

function Invoke-Atom([string]$name, $value) {
    # Preserve returned collections, including empty lists.
    return ,$atom.GetMethod($name, $staticFlags).Invoke($null, @($value))
}

function Get-EntryValue($entry, [string]$name) {
    return $atom.GetProperty($name, $instanceFlags).GetValue($entry)
}

foreach ($value in @('', '  ', '-1', $null)) {
    Assert ((Invoke-Atom ForumIds $value) -join ',' -eq '486') 'Legacy/default forum must map to 486'
}
Assert (((Invoke-Atom ForumIds '486, 123,486') -join ',') -eq '486,123') 'Trim and deduplicate forums'
foreach ($value in @('486', '1', '2147483647')) {
    Assert (Invoke-Atom IsForumId $value) "Valid forum rejected: $value"
}
foreach ($value in @('', '0', '-1', '1.5', 'abc', '../486', '2147483648')) {
    Assert (-not (Invoke-Atom IsForumId $value)) "Invalid forum accepted: $value"
}

$hash = '0123456789ABCDEF0123456789ABCDEF01234567'
$magnet = "magnet:?xt=urn:btih:$hash&dn=Alpine%20skiing"
Assert ((Invoke-Atom MagnetHash $magnet) -ceq $hash.ToLowerInvariant()) 'Normalize info hash'
foreach ($value in @('', 'https://example.org/', "magnet:?xt=urn:btih:abc", "$magnet#fragment", "$magnet bad", "$magnet`n", "$magnet`"")) {
    Assert ((Invoke-Atom MagnetHash $value) -eq '') "Unsafe magnet accepted: $value"
}

$xml = @"
<feed xmlns="http://www.w3.org/2005/Atom">
  <title>Winter sports</title>
  <entry>
    <id>https://rutracker.org/forum/t/123</id>
    <title>Alpine Skiing World Cup [1.25 GB]</title>
    <category label="Alpine skiing"/><author><name>Uploader</name></author>
    <link rel="alternate" href="https://example.org/"/>
    <link rel="enclosure" href="https://example.org/file.torrent"/>
    <link rel="enclosure" href="magnet:?xt=urn:btih:$hash&amp;dn=Alpine%20skiing"/>
    <updated>2026-11-16T00:00:00Z</updated>
  </entry>
  <entry><id>https://rutracker.org/forum/t/124</id><title>No magnet</title>
    <updated>2026-11-15T00:00:00Z</updated></entry>
  <entry><id>invalid</id><title>Skip invalid ID</title></entry>
  <entry><id>https://rutracker.org/forum/t/125</id><title> </title></entry>
</feed>
"@
$originalCulture = [Threading.Thread]::CurrentThread.CurrentCulture
try {
    [Threading.Thread]::CurrentThread.CurrentCulture = [Globalization.CultureInfo]'ru-RU'
    $entries = Invoke-Atom Read $xml
    Assert ($entries.Count -eq 2) 'Skip entries without topic IDs or titles'
    $entry = $entries[0]
    Assert ((Get-EntryValue $entry TopicId) -eq '123') 'Read topic ID'
    Assert ((Get-EntryValue $entry Title) -eq 'Alpine Skiing World Cup') 'Strip size from title'
    Assert ((Get-EntryValue $entry Size) -eq 1.25) 'Parse size independently of locale'
    Assert ((Get-EntryValue $entry SizeUnit) -eq 'GB') 'Read size unit'
    Assert ((Get-EntryValue $entry Forum) -eq 'Alpine skiing') 'Read category'
    Assert ((Get-EntryValue $entry Author) -eq 'Uploader') 'Read author'
    Assert ((Get-EntryValue $entry Magnet) -ceq $magnet) 'Choose valid enclosure and decode XML entities'
    Assert ((Get-EntryValue $entry InfoHash) -ceq $hash.ToLowerInvariant()) 'Use info hash for update detection'
    $matches = $atom.GetMethod('Matches', $instanceFlags)
    Assert ($matches.Invoke($entry, @('  SKIING world  '))) 'Match phrase without case or surrounding spaces'
    Assert ($matches.Invoke($entry, @(''))) 'Empty phrase matches every entry'
    Assert (-not $matches.Invoke($entry, @('skiing.*cup'))) 'Search phrase is literal, not a regex'
    $missing = $entries[1]
    Assert ((Get-EntryValue $missing Forum) -eq 'Winter sports') 'Fall back to feed title'
    Assert ((Get-EntryValue $missing Author) -eq '') 'Allow missing author'
    Assert ((Get-EntryValue $missing Magnet) -eq '') 'Allow missing magnet'
    Assert ((Get-EntryValue $missing SizeUnit) -eq '') 'Do not invent missing size'
    Assert ((Get-EntryValue $missing InfoHash) -eq 'atom:2026-11-15T00:00:00Z') 'Use updated timestamp without magnet'
} finally {
    [Threading.Thread]::CurrentThread.CurrentCulture = $originalCulture
}
Assert ((Invoke-Atom Read '<feed xmlns="http://www.w3.org/2005/Atom"/>').Count -eq 0) 'Accept empty feed'
foreach ($invalidXml in @('<html/>', '<feed/>', '<feed')) {
    $rejected = $false
    try { $null = Invoke-Atom Read $invalidXml } catch { $rejected = $true }
    Assert $rejected 'Reject non-Atom or malformed responses'
}

# Construct windows without showing them or running Load handlers. This verifies
# compiled WinForms resources without network requests or user settings writes.
foreach ($name in @('frmMain', 'frmKeyWord', 'frmCredentials', 'frmForums', 'frmAppOptions')) {
    $form = [Activator]::CreateInstance($assembly.GetType("TorrentChecker.$name", $true))
    try {
        Assert ($form.Controls.Count -gt 0) "Window resources did not load: $name"
        if ($name -eq 'frmMain') {
            $menu = $form.GetType().GetField('openMagnetMenu', $instanceFlags).GetValue($form)
            Assert ($null -ne $menu.Owner) 'Magnet action must be attached to the context menu'
        }
    } finally { $form.Dispose() }
}
Assert ($assembly.EntryPoint.DeclaringType.FullName -eq 'TorrentChecker.mdlStartup') 'Use startup error logging entry point'
Write-Host "PASS: $script:checks checks ($Configuration)"
