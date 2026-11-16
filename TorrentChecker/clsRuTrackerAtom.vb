Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Xml.Linq

Friend NotInheritable Class clsRuTrackerAtom
    Friend Const DefaultForum As String = "486"
    Friend Const FeedBase As String = "https://feed.rutracker.cc/atom/f/"

    Private Shared ReadOnly Atom As XNamespace = "http://www.w3.org/2005/Atom"
    Private Shared ReadOnly TopicPattern As New Regex("/t/([0-9]+)$")
    Private Shared ReadOnly SizePattern As New Regex("\s+\[(\d+(?:\.\d+)?)\s+([KMGTPE]?B)\]$", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)
    Private Shared ReadOnly HashPattern As New Regex("(?:\?|&)xt=urn:btih:([0-9a-f]{40})(?=&|$)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant)

    Friend Property TopicId As String
    Friend Property Title As String
    Friend Property Forum As String
    Friend Property Author As String
    Friend Property Magnet As String
    Friend Property InfoHash As String
    Friend Property Size As Single
    Friend Property SizeUnit As String

    Friend Shared Function ForumIds(value As String) As String()
        If String.IsNullOrWhiteSpace(value) OrElse value.Trim() = "-1" Then value = DefaultForum
        Return value.Split(","c).Select(Function(id) id.Trim()).Distinct().ToArray()
    End Function

    Friend Shared Function IsForumId(value As String) As Boolean
        Dim id As Integer
        Return Integer.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, id) AndAlso id > 0
    End Function

    Friend Shared Function MagnetHash(value As String) As String
        If Not value.StartsWith("magnet:?", StringComparison.Ordinal) OrElse
            value.Any(Function(c) Char.IsControl(c) OrElse Char.IsWhiteSpace(c) OrElse c = """"c) Then Return ""
        Dim magnetUri As Uri = Nothing
        If Not Uri.TryCreate(value, UriKind.Absolute, magnetUri) OrElse magnetUri.Fragment.Length > 0 Then Return ""
        Return HashPattern.Match(value).Groups(1).Value.ToLowerInvariant()
    End Function

    Friend Shared Function Read(xml As String) As List(Of clsRuTrackerAtom)
        Dim feed As XElement = XDocument.Parse(xml).Root
        If feed Is Nothing OrElse feed.Name <> Atom + "feed" Then
            Throw New FormatException("Сервер вернул ответ без Atom-ленты RuTracker.")
        End If

        Dim defaultForumName As String = If(CStr(feed.Element(Atom + "title")), "")
        Dim entries As New List(Of clsRuTrackerAtom)
        For Each item As XElement In feed.Elements(Atom + "entry")
            Dim topicId As String = TopicPattern.Match(If(CStr(item.Element(Atom + "id")), "")).Groups(1).Value
            Dim title As String = If(CStr(item.Element(Atom + "title")), "").Trim()
            If topicId = "" OrElse title = "" Then Continue For

            Dim magnet = item.Elements(Atom + "link").
                Where(Function(link) CStr(link.Attribute("rel")) = "enclosure").
                Select(Function(link) If(CStr(link.Attribute("href")), "")).
                FirstOrDefault(Function(value) MagnetHash(value) <> "")
            Dim sizeMatch = SizePattern.Match(title)
            Dim size As Single
            Dim unit As String = ""
            If sizeMatch.Success AndAlso Single.TryParse(sizeMatch.Groups(1).Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, size) Then
                title = title.Substring(0, sizeMatch.Index)
                unit = sizeMatch.Groups(2).Value.ToUpperInvariant()
            End If
            Dim category = item.Element(Atom + "category")
            Dim author = item.Element(Atom + "author")
            entries.Add(New clsRuTrackerAtom With {
                .TopicId = topicId,
                .Title = title,
                .Forum = If(category Is Nothing, defaultForumName, If(CStr(category.Attribute("label")), defaultForumName)),
                .Author = If(author Is Nothing, "", If(CStr(author.Element(Atom + "name")), "")),
                .Magnet = If(magnet, ""),
                .InfoHash = If(magnet IsNot Nothing, MagnetHash(If(magnet, "")),
                               "atom:" & If(CStr(item.Element(Atom + "updated")), "")),
                .Size = size,
                .SizeUnit = unit
            })
        Next
        Return entries
    End Function

    Friend Function Matches(phrase As String) As Boolean
        Return Title.IndexOf(phrase.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
    End Function
End Class
