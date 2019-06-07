Public Class clsListViewExt
    Inherits ListView

    Private Const WM_KILLFOCUS As Integer = &H8

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg >= &H201 And m.Msg <= &H209 Then 'events between WM_LBUTTONDOWN and WM_MBUTTONDBLCLK
            'suppress mouse messages that are OUTSIDE of the items area
            Dim pt As Point = New Point(m.LParam.ToInt32())
            Dim ht As ListViewHitTestInfo = Me.HitTest(pt)
            If (ht.Item Is Nothing) Then
                m.Result = IntPtr.Zero
            Else
                MyBase.WndProc(m)
            End If
        ElseIf m.Msg = WM_KILLFOCUS Then
            'ignore WM_KILLFOCUS
            m.Result = IntPtr.Zero
        Else
            MyBase.WndProc(m)
        End If

    End Sub
End Class
