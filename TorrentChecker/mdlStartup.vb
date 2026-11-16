Imports System.IO

Friend Module mdlStartup
    <STAThread>
    Public Sub Main()
        Try
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(My.Forms.frmMain)
        Catch ex As Exception
            Environment.ExitCode = 1
            Dim message As String = ex.GetBaseException().Message
            Try
                Dim logPath As String = Path.Combine(Path.GetTempPath(), "TorrentChecker-startup.log")
                File.WriteAllText(logPath, ex.ToString())
                message &= vbCrLf & vbCrLf & logPath
            Catch
                'The error dialog is still available if the temporary directory is unwritable.
            End Try
            MessageBox.Show(message, "Ошибка запуска TorrentChecker", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Module
