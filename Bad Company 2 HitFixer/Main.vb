Imports System.IO.File
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Form2

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ping As String
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        ping = Val(TextBox1.Text)
        If CheckBox1.Checked = True Then
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "[BC2HF] Error")
            Else
                Dim latcomp As Double
                Dim intertime As Integer
                Dim extratime As Integer
                latcomp = ((ping + 10) / 1000)
                intertime = (ping + 10)
                extratime = (intertime * 12)
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
                MsgBox("Bad Company 2 should have been fixed!", MsgBoxStyle.Information, "[BC2HF] Done")
            End If
        Else
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "[BC2HF] Error")
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
                latcomp = ((ping + 10) / 1000)
                intertime = (ping + 10)
                extratime = (intertime * 12)
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
                MsgBox("Bad Company 2 should have been fixed!", MsgBoxStyle.Information, "[BC2HF] Done")
            End If
        End If
    End Sub

    Private Sub Form2_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Form1.Close()
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
        MsgBox("Bad Company 2 restore to default!", MsgBoxStyle.Information, "[BC2HF] Done")
    End Sub
End Class