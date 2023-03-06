Imports System.IO
Imports System.IO.Compression

Module mdlSettings
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(frmMain)
    End Sub

    Private AppSettingsPath As String = Path.Combine(Application.StartupPath, "settings.dat")
    Public LogPath As String = Path.Combine(Application.StartupPath, "log.txt")
    Private formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()

    Public AppAllSettings As New Dictionary(Of String, Object) From
    {
        {"frmMain", MainFormSettings},
        {"rutracker_credentials", TrackerParams(Trackers.rutracker)("credentials")},
        {"kinozal_credentials", TrackerParams(Trackers.kinozal)("credentials")},
        {"proxy", ProxySettings},
        {"dtFoundTorrents", dtFoundTorrents},
        {"dgvTorrentsColumns", dgvTorrentsColumns},
        {"dgvTorrentsSorting", dgvTorrentsSorting},
        {"KwListToParamsMapping", KwListToParamsMapping},
        {"KeyWordsParams", KeyWordsParams},
        {"dtStoredTorrents", dtStoredTorrents},
        {"CHECK_INTERVAL", CHECK_INTERVAL},
        {"TORRENTS_MAX_STORETIME", TORRENTS_MAX_STORETIME},
        {"TORRENTS_MAX_RESULTS", TORRENTS_MAX_RESULTS},
        {"AppOptions", AppOptions}
    }

    Public Sub SaveSettings()
        Try
            'save datagridview's scroll position (AcceptChanges updates the datagridview) 
            Dim dgv_idx As Integer = frmMain.dgvTorrents.FirstDisplayedScrollingRowIndex

            'commit all changes
            dtFoundTorrents.AcceptChanges()
            dtStoredTorrents.AcceptChanges()

            'main form settings
            AppAllSettings("frmMain") = MainFormSettings

            'credentials
            AppAllSettings("rutracker_credentials") = TrackerParams(Trackers.rutracker)("credentials")
            AppAllSettings("kinozal_credentials") = TrackerParams(Trackers.kinozal)("credentials")

            'proxy
            AppAllSettings("proxy") = ProxySettings

            'keywords
            AppAllSettings("dtFoundTorrents") = dtFoundTorrents
            AppAllSettings("KwListToParamsMapping") = KwListToParamsMapping
            AppAllSettings("KeyWordsParams") = KeyWordsParams

            'columns
            For Each col As KeyValuePair(Of String, Object) In dgvTorrentsColumns 'AppSettings("dgvTorrentsColumns")
                dgvTorrentsColumns(col.Key)("index") = frmMain.dgvTorrents.Columns(col.Key).DisplayIndex
                dgvTorrentsColumns(col.Key)("width") = frmMain.dgvTorrents.Columns(col.Key).Width
                dgvTorrentsColumns(col.Key)("visibility") = frmMain.dgvTorrents.Columns(col.Key).Visible
            Next
            AppAllSettings("dgvTorrentsColumns") = dgvTorrentsColumns
            If frmMain.dgvTorrents.SortedColumn IsNot Nothing AndAlso frmMain.dgvTorrents.SortOrder <> SortOrder.None Then
                dgvTorrentsSorting("column_name") = frmMain.dgvTorrents.SortedColumn.Name
                dgvTorrentsSorting("sort_order") = frmMain.dgvTorrents.SortOrder
            End If
            AppAllSettings("dgvTorrentsSorting") = dgvTorrentsSorting

            'all found torrents
            AppAllSettings("dtStoredTorrents") = dtStoredTorrents

            'check interval
            AppAllSettings("CHECK_INTERVAL") = CHECK_INTERVAL

            'torrents max store time
            AppAllSettings("TORRENTS_MAX_STORETIME") = TORRENTS_MAX_STORETIME

            'max visible results
            AppAllSettings("TORRENTS_MAX_RESULTS") = TORRENTS_MAX_RESULTS

            'options
            AppAllSettings("AppOptions") = AppOptions

            Using stream As Stream = File.Open(AppSettingsPath, FileMode.Create)
                Using gzipstream = New GZipStream(stream, CompressionMode.Compress)
                    formatter.Serialize(gzipstream, AppAllSettings)
                End Using
            End Using

            'restore datagridview's scroll
            If dgv_idx >= 0 Then frmMain.dgvTorrents.FirstDisplayedScrollingRowIndex = dgv_idx
        Catch ex As Exception
            MsgBox("Ошибка при записи конфигурационного файла!" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Public Sub ReadSettings()
        Try
            Using stream As Stream = File.Open(AppSettingsPath, FileMode.OpenOrCreate)
                If stream.Length > 0 Then
                    Using gzipstream = New GZipStream(stream, CompressionMode.Decompress)
                        AppAllSettings = formatter.Deserialize(gzipstream)
                    End Using
                End If
            End Using
        Catch ex As Exception
            MsgBox("Ошибка чтения конфигурационного файла!" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        Finally
            'main form settings
            If AppAllSettings.ContainsKey("frmMain") Then
                MainFormSettings = AppAllSettings("frmMain")
            End If

            'credentials
            If AppAllSettings.ContainsKey("rutracker_credentials") Then
                TrackerParams(Trackers.rutracker)("credentials") = AppAllSettings("rutracker_credentials")
            End If
            If AppAllSettings.ContainsKey("kinozal_credentials") Then
                TrackerParams(Trackers.kinozal)("credentials") = AppAllSettings("kinozal_credentials")
            End If

            'proxy
            If AppAllSettings.ContainsKey("proxy") Then
                ProxySettings = AppAllSettings("proxy")
            End If

            'keywords
            If AppAllSettings.ContainsKey("dtFoundTorrents") Then
                dtFoundTorrents = AppAllSettings("dtFoundTorrents")
            End If
            If dtFoundTorrents.Columns.Count = 0 Then
                'create default columns
                With dtFoundTorrents.Columns
                    .Add(Columns.tracker_name.ToString)
                    .Add(Columns.tracker_id.ToString, GetType(Trackers))
                    .Add(Columns.found_datetime.ToString)
                    .Add(Columns.found_unix_datetime.ToString, GetType(Long))
                    .Add(Columns.torrent_status.ToString)
                    .Add(Columns.forum_name.ToString)
                    .Add(Columns.topic_id.ToString)
                    .Add(Columns.topic_name.ToString)
                    .Add(Columns.topic_author.ToString)
                    .Add(Columns.torrent_dl.ToString)
                    .Add(Columns.torrent_size_dimension.ToString)
                    .Add(Columns.torrent_unique_identifier.ToString)
                    .Add(Columns.keyword_id.ToString, GetType(Integer))
                    .Add(Columns.marked_as_new.ToString, GetType(Boolean))
                    .Add(Columns.torrent_size_bytes.ToString, GetType(Double))
                End With
            End If
            If AppAllSettings.ContainsKey("KwListToParamsMapping") AndAlso AppAllSettings.ContainsKey("KeyWordsParams") Then
                KwListToParamsMapping = AppAllSettings("KwListToParamsMapping")
                Dim tmp_kwParams As Dictionary(Of Integer, Dictionary(Of String, String)) = AppAllSettings("KeyWordsParams")
                For params_id As Integer = 1 To UBound(KwListToParamsMapping)
                    Dim kw_id As Integer = KwListToParamsMapping(params_id)
                    For Each dr As DataRow In dtFoundTorrents.Select(Columns.keyword_id.ToString & "=" & kw_id)
                        dr(Columns.keyword_id.ToString) = params_id
                    Next
                    KeyWordsParams.Add(params_id, tmp_kwParams(kw_id))
                    KwListToParamsMapping(params_id) = params_id

                    If CBool(KeyWordsParams(params_id)("paused")) Then
                        frmMain.dgvKeyWords.Rows.Add(IMAGE_PAUSE, KeyWordsParams(params_id)("label"))
                    Else
                        frmMain.dgvKeyWords.Rows.Add(Nothing, KeyWordsParams(params_id)("label"))
                    End If
                    frmMain.dgvKeyWords.Rows(params_id).Cells("kw_item").Style.Padding = ITEM_PADDING
                Next
                frmMain.grpKWBox.Text = "Ключевые фразы: " & UBound(KwListToParamsMapping)
            End If
            dtFoundTorrents.AcceptChanges()

            'columns
            If AppAllSettings.ContainsKey("dgvTorrentsColumns") AndAlso AppAllSettings.ContainsKey("dgvTorrentsSorting") Then
                dgvTorrentsColumns = AppAllSettings("dgvTorrentsColumns")
                Dim col_array(dgvTorrentsColumns.Count - 1) As DataGridViewTextBoxColumn
                For Each col As KeyValuePair(Of String, Object) In dgvTorrentsColumns
                    'the only correct way to add columns with DisplayIndex - using array with AutoGenerateColumns=False
                    col_array(col.Value("index")) = CreateColumn(col.Key, col.Value("name"), col.Value("index"), col.Value("width"), col.Value("visibility"))
                Next
                frmMain.dgvTorrents.Columns.AddRange(col_array)
                Array.Clear(col_array, 0, col_array.Length)
                dgvTorrentsSorting = AppAllSettings("dgvTorrentsSorting")
            End If

            'all found torrents
            If AppAllSettings.ContainsKey("dtStoredTorrents") Then
                dtStoredTorrents = AppAllSettings("dtStoredTorrents")
            End If
            If dtStoredTorrents.Columns.Count = 0 Then
                'create default columns
                With dtStoredTorrents.Columns
                    .Add(Columns.tracker_id.ToString, GetType(Trackers))
                    .Add(Columns.found_unix_datetime.ToString, GetType(Long))
                    .Add(Columns.topic_id.ToString)
                    .Add(Columns.torrent_unique_identifier.ToString)
                End With
            End If
            dtStoredTorrents.AcceptChanges()

            'check interval
            If AppAllSettings.ContainsKey("CHECK_INTERVAL") Then
                CHECK_INTERVAL = AppAllSettings("CHECK_INTERVAL")
            End If

            'torrents max store time
            If AppAllSettings.ContainsKey("TORRENTS_MAX_STORETIME") Then
                TORRENTS_MAX_STORETIME = AppAllSettings("TORRENTS_MAX_STORETIME")
            End If

            'max visible results
            If AppAllSettings.ContainsKey("TORRENTS_MAX_RESULTS") Then
                TORRENTS_MAX_RESULTS = AppAllSettings("TORRENTS_MAX_RESULTS")
            End If

            'options
            If AppAllSettings.ContainsKey("AppOptions") Then
                AppOptions = AppAllSettings("AppOptions")
            End If

            'new keys in AppOptions after 3.0.6
            'added 20190829
            If Not AppOptions.ContainsKey("play_sound") Then
                AppOptions.Add("play_sound", False)
            End If

            'fonts
            If AppOptions.ContainsKey("dgv_font_bold") AndAlso AppOptions.ContainsKey("dgv_font_normal") Then
                DGV_BOLD = New DataGridViewCellStyle With {.Font = New Font(DirectCast(AppOptions("dgv_font_bold"), Font), FontStyle.Bold)}
                DGV_NORMAL = New DataGridViewCellStyle With {.Font = New Font(DirectCast(AppOptions("dgv_font_normal"), Font), FontStyle.Regular)}
            End If
        End Try
    End Sub

    Private Function CreateColumn(column_id As String, column_name As String, column_index As Integer, column_width As Integer, column_visibility As Boolean) As DataGridViewTextBoxColumn
        Dim col As New DataGridViewTextBoxColumn

        With col
            .HeaderText = column_name
            .DataPropertyName = column_id
            .Name = column_id
            .Width = column_width
            .Visible = column_visibility
            .DisplayIndex = column_index
        End With

        Return col
    End Function
End Module
