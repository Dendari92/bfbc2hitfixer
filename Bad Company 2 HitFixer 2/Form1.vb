Imports System.IO

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ping As String
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ping = Val(TextBox1.Text)
        If CheckBox1.Checked = True Then
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "BC2HF2 - Error!")
            Else
                Dim latcomp As Double
                Dim intertime As Integer
                Dim extratime As Integer
                latcomp = (ping + 10) / 1000
                intertime = ping + 10
                extratime = intertime * 12
                Dim settings As New IO.FileStream(DocFolder + "\BFBC2\settings.ini", IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.None)
                Dim myFileWriter As New System.IO.StreamWriter(settings)
                Dim latcomps As String = CType(latcomp, String)
                Dim extratimes As String = CType(extratime, String)
                Dim intertimes As String = CType(intertime, String)
                myFileWriter.WriteLine("SettingsManager.floatSet GSDefaultLatencyCompensation " + latcomps.Replace(",", "."))
                myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 1")
                myFileWriter.WriteLine("SettingsManager.U32Set GSExtrapolationTime " + extratimes)
                myFileWriter.WriteLine("SettingsManager.U32Set GSInterpolationTime " + intertimes)
                myFileWriter.Close()
                settings.Close()
                MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF2 - Done!")
            End If
        Else
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "BC2HF2 - Error!")
            Else
                Dim linesList As New List(Of String)(File.ReadAllLines(DocFolder + "\BFBC2\settings.ini"))
                Dim i As Integer = 0
                While i <> 4
                    linesList.RemoveAt(33)
                    File.WriteAllLines(DocFolder + "\BFBC2\settings.ini", linesList.ToArray())
                    i = i + 1
                End While
                Dim latcomp As Double
                Dim intertime As Integer
                Dim extratime As Integer
                latcomp = (ping + 10) / 1000
                intertime = ping + 10
                extratime = intertime * 12
                Dim settings As New IO.FileStream(DocFolder + "\BFBC2\settings.ini", IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.None)
                Dim myFileWriter As New System.IO.StreamWriter(settings)
                Dim latcomps As String = CType(latcomp, String)
                Dim extratimes As String = CType(extratime, String)
                Dim intertimes As String = CType(intertime, String)
                myFileWriter.WriteLine("SettingsManager.floatSet GSDefaultLatencyCompensation " + latcomps.Replace(",", "."))
                myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 1")
                myFileWriter.WriteLine("SettingsManager.U32Set GSExtrapolationTime " + extratimes)
                myFileWriter.WriteLine("SettingsManager.U32Set GSInterpolationTime " + intertimes)
                myFileWriter.Close()
                settings.Close()
                MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF2 - Done!")
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim linesList As New List(Of String)(File.ReadAllLines(DocFolder + "\BFBC2\settings.ini"))
        Dim i As Integer = 0
        While i <> 4
            linesList.RemoveAt(33)
            File.WriteAllLines(DocFolder + "\BFBC2\settings.ini", linesList.ToArray())
            i = i + 1
        End While
        MsgBox("Bad Company 2 hit registration restored to default!", MsgBoxStyle.Information, "BC2HF2 - Done!")
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Dim pingoffset As Integer
        pingoffset = TrackBar1.Value
        Label12.Text = pingoffset
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        Dim latslide As Integer
        latslide = TrackBar2.Value
        Label13.Text = latslide
    End Sub

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        Dim extraslide As Integer
        extraslide = TrackBar3.Value
        Label14.Text = extraslide
    End Sub

    Private Sub TrackBar4_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar4.Scroll
        Dim interslide As Integer
        interslide = TrackBar4.Value
        Label15.Text = interslide
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim ping As String
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        ping = Val(TextBox2.Text)
        If CheckBox3.Checked = True Then
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "BC2HF2 - Error!")
            Else
                Dim latcomp As Double
                Dim intertime As Integer
                Dim extratime As Integer
                Dim pingoffset As Integer
                Dim latslide As Double
                Dim extraslide As Double
                Dim interslide As Double
                pingoffset = TrackBar1.Value
                latslide = TrackBar2.Value
                extraslide = TrackBar3.Value
                interslide = TrackBar4.Value
                latcomp = ((ping + pingoffset) / 1000) * (latslide / 100)
                intertime = (ping + pingoffset) * (interslide / 100)
                If RadioButton1.Checked = True Then
                    extratime = ((ping + pingoffset) * 12) * (extraslide / 100)
                Else
                    extratime = (((ping + pingoffset) * 5.5) + 489) * (extraslide / 100)
                End If
                Dim settings As New IO.FileStream(DocFolder + "\BFBC2\settings.ini", IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.None)
                Dim myFileWriter As New System.IO.StreamWriter(settings)
                Dim latcomps As String = CType(latcomp, String)
                Dim extratimes As String = CType(extratime, String)
                Dim intertimes As String = CType(intertime, String)
                myFileWriter.WriteLine("SettingsManager.floatSet GSDefaultLatencyCompensation " + latcomps.Replace(",", "."))
                If CheckBox2.Checked = True Then
                    myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 1")
                Else
                    myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 0")
                End If
                myFileWriter.WriteLine("SettingsManager.U32Set GSExtrapolationTime " + extratimes)
                myFileWriter.WriteLine("SettingsManager.U32Set GSInterpolationTime " + intertimes)
                myFileWriter.Close()
                settings.Close()
                MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF2 - Done!")
            End If
        Else
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "BC2HF2 - Error!")
            Else
                Dim linesList As New List(Of String)(File.ReadAllLines(DocFolder + "\BFBC2\settings.ini"))
                Dim i As Integer = 0
                While i <> 4
                    linesList.RemoveAt(33)
                    File.WriteAllLines(DocFolder + "\BFBC2\settings.ini", linesList.ToArray())
                    i = i + 1
                End While
                Dim latcomp As Double
                Dim intertime As Integer
                Dim extratime As Integer
                Dim pingoffset As Integer
                Dim latslide As Double
                Dim extraslide As Double
                Dim interslide As Double
                pingoffset = TrackBar1.Value
                latslide = TrackBar2.Value
                extraslide = TrackBar3.Value
                interslide = TrackBar4.Value
                latcomp = ((ping + pingoffset) / 1000) * (latslide / 100)
                intertime = (ping + pingoffset) * (interslide / 100)
                If RadioButton1.Checked = True Then
                    extratime = ((ping + pingoffset) * 12) * (extraslide / 100)
                Else
                    extratime = (((ping + pingoffset) * 5.5) + 489) * (extraslide / 100)
                End If
                Dim settings As New IO.FileStream(DocFolder + "\BFBC2\settings.ini", IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.None)
                Dim myFileWriter As New System.IO.StreamWriter(settings)
                Dim latcomps As String = CType(latcomp, String)
                Dim extratimes As String = CType(extratime, String)
                Dim intertimes As String = CType(intertime, String)
                myFileWriter.WriteLine("SettingsManager.floatSet GSDefaultLatencyCompensation " + latcomps.Replace(",", "."))
                If CheckBox2.Checked = True Then
                    myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 1")
                Else
                    myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 0")
                End If
                myFileWriter.WriteLine("SettingsManager.U32Set GSExtrapolationTime " + extratimes)
                myFileWriter.WriteLine("SettingsManager.U32Set GSInterpolationTime " + intertimes)
                myFileWriter.Close()
                settings.Close()
                MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF2 - Done!")
            End If
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            ContextMenuStrip1.Enabled = True
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub ShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem.Click
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class
