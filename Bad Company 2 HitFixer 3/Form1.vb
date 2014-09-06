Imports System.IO

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        'Reading HitFixer Settings File
        Dim HFSettingsFile As String = DocFolder & "\BFBC2\BC2HF\settings.ini"
        If System.IO.File.Exists(HFSettingsFile) Then
            Dim lines = IO.File.ReadAllLines(HFSettingsFile)
            Dim sysTray As Integer = Array.FindIndex(lines, Function(s) s.Contains("sys_tray"))
            Dim autoSave As Integer = Array.FindIndex(lines, Function(s) s.Contains("auto_save"))
            Dim autoRemove As Integer = Array.FindIndex(lines, Function(s) s.Contains("auto_remove"))
            Me.CheckBox2.Checked = CDbl(lines(sysTray).Split(" "c)(1)) = 1
            Me.CheckBox3.Checked = CDbl(lines(autoSave).Split(" "c)(1)) = 1
            Me.CheckBox4.Checked = CDbl(lines(autoRemove).Split(" "c)(1)) = 1
        End If

        'Reading Advanced Fix Values File
        Dim ValuesFile As String = DocFolder & "\BFBC2\BC2HF\values.ini"
        If System.IO.File.Exists(ValuesFile) Then
            Dim lines1 = IO.File.ReadAllLines(ValuesFile)
            Dim pingOffset As Integer = Array.FindIndex(lines1, Function(s) s.Contains("ping_offset"))
            Dim latComp As Integer = Array.FindIndex(lines1, Function(s) s.Contains("latency_comp"))
            Dim extraPol As Integer = Array.FindIndex(lines1, Function(s) s.Contains("extrapolation"))
            Dim interPol As Integer = Array.FindIndex(lines1, Function(s) s.Contains("interpolation"))
            Dim extraPolFrame As Integer = Array.FindIndex(lines1, Function(s) s.Contains("extrapol_frame"))
            Dim hitregFormula As Integer = Array.FindIndex(lines1, Function(s) s.Contains("hitreg_formula"))
            Me.TrackBar1.Value = Val(lines1(pingOffset).Split(" "c)(1))
            Me.TrackBar2.Value = Val(lines1(latComp).Split(" "c)(1))
            Me.TrackBar3.Value = Val(lines1(extraPol).Split(" "c)(1))
            Me.TrackBar4.Value = Val(lines1(interPol).Split(" "c)(1))
            Me.CheckBox1.Checked = CDbl(lines1(extraPolFrame).Split(" "c)(1)) = 1
            If lines1(hitregFormula).Split(" "c)(1) = "0" Then
                Me.RadioButton1.Checked = True
            ElseIf lines1(hitregFormula).Split(" "c)(1) = "1" Then
                Me.RadioButton2.Checked = True
            End If
        End If

        'Transform Label in Values
        Me.Label11.Text = Me.TrackBar1.Value & " ms"
        Me.Label12.Text = Me.TrackBar2.Value & " %"
        Me.Label13.Text = Me.TrackBar3.Value & " %"
        Me.Label14.Text = Me.TrackBar4.Value & " %"
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.CheckBox2.Checked = True Then
            If Me.WindowState = FormWindowState.Minimized Then
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub Form1_Close(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.FormClosing
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        'Creating Folder
        System.IO.Directory.CreateDirectory(DocFolder & "\BFBC2\BC2HF\")

        'Creating HitFixer Settings File
        Dim SettingsFile As New IO.FileStream(DocFolder & "\BFBC2\BC2HF\settings.ini", IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
        Dim mySettingsWriter As New System.IO.StreamWriter(SettingsFile)
        If Me.CheckBox2.Checked = True Then
            mySettingsWriter.WriteLine("sys_tray 1")
        ElseIf Me.CheckBox2.Checked = False Then
            mySettingsWriter.WriteLine("sys_tray 0")
        End If
        If Me.CheckBox3.Checked = True Then
            mySettingsWriter.WriteLine("auto_save 1")
        ElseIf Me.CheckBox3.Checked = False Then
            mySettingsWriter.WriteLine("auto_save 0")
        End If
        If Me.CheckBox4.Checked = True Then
            mySettingsWriter.WriteLine("auto_remove 1")
        ElseIf Me.CheckBox4.Checked = False Then
            mySettingsWriter.WriteLine("auto_remove 0")
        End If
        mySettingsWriter.Close()
        SettingsFile.Close()

        'Creating Advanced Fix Values File
        If Me.CheckBox3.Checked = True Then
            Dim ValuesFile As New IO.FileStream(DocFolder & "\BFBC2\BC2HF\values.ini", IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
            Dim myValuesWriter As New System.IO.StreamWriter(ValuesFile)
            myValuesWriter.WriteLine("ping_offset " & TrackBar1.Value)
            myValuesWriter.WriteLine("latency_comp " & TrackBar2.Value)
            myValuesWriter.WriteLine("extrapolation " & TrackBar3.Value)
            myValuesWriter.WriteLine("interpolation " & TrackBar4.Value)
            If Me.CheckBox1.Checked = True Then
                myValuesWriter.WriteLine("extrapol_frame 1")
            ElseIf Me.CheckBox1.Checked = False Then
                myValuesWriter.WriteLine("extrapol_frame 0")
            End If
            If Me.RadioButton1.Checked = True Then
                myValuesWriter.WriteLine("hitreg_formula 0")
            ElseIf Me.RadioButton2.Checked = True Then
                myValuesWriter.WriteLine("hitreg_formula 1")
            End If
            myValuesWriter.Close()
            ValuesFile.Close()
        ElseIf Me.CheckBox3.Checked = False Then
            Dim ValuesFile As String = DocFolder & "\BFBC2\BC2HF\values.ini"
            If System.IO.File.Exists(ValuesFile) = True Then
                System.IO.File.Delete(ValuesFile)
            End If
        End If

        'Remove Fix From Bad Company 2 Settings File
        If Me.CheckBox4.Checked = True Then
            Dim BC2SettingsFile As String = DocFolder & "\BFBC2\settings.ini"
            If System.IO.File.Exists(BC2SettingsFile) Then
                Dim lines = IO.File.ReadAllLines(BC2SettingsFile)
                Dim latComp As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.floatSet GSDefaultLatencyCompensation"))
                Dim extraFrame As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.boolSet GSExtrapolateFrame"))
                Dim extraTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSExtrapolationTime"))
                Dim interTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSInterpolationTime"))

                If latComp > 0 And extraFrame > 0 And extraTime > 0 And interTime > 0 Then
                    Dim tempList As New List(Of String)(lines)
                    tempList.RemoveAt(latComp)
                    tempList.RemoveAt(extraFrame - 1)
                    tempList.RemoveAt(extraTime - 2)
                    tempList.RemoveAt(interTime - 3)
                    lines = tempList.ToArray
                    IO.File.WriteAllLines(BC2SettingsFile, lines)
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        'Reading Bad Company 2 Settings File
        Dim SettingsFile As String = DocFolder & "\BFBC2\settings.ini"
        If System.IO.File.Exists(SettingsFile) Then
            Dim lines = IO.File.ReadAllLines(SettingsFile)
            Dim latComp As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.floatSet GSDefaultLatencyCompensation"))
            Dim extraFrame As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.boolSet GSExtrapolateFrame"))
            Dim extraTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSExtrapolationTime"))
            Dim interTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSInterpolationTime"))

            'Writing Bad Company 2 Settings File
            Dim ping As String = Val(TextBox1.Text)
            Dim valLatComp As Double = (ping + 10) / 1000
            Dim valInterTime As Integer = ping + 10
            Dim valExtraTime As Integer = valInterTime * 12
            Dim valLatComps As String = CType(valLatComp, String)
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "BC2HF3 - Error!")
            Else
                'Lines already exist so replace them
                If latComp > 0 And extraFrame > 0 And extraTime > 0 And interTime > 0 Then
                    Dim tempList As New List(Of String)(lines)
                    tempList.RemoveAt(latComp)
                    tempList.RemoveAt(extraFrame - 1)
                    tempList.RemoveAt(extraTime - 2)
                    tempList.RemoveAt(interTime - 3)
                    tempList.Insert(latComp, "SettingsManager.floatSet GSDefaultLatencyCompensation " & valLatComps.Replace(",", "."))
                    tempList.Insert(extraFrame, "SettingsManager.boolSet GSExtrapolateFrame 1")
                    tempList.Insert(extraTime, "SettingsManager.U32Set GSExtrapolationTime " & valExtraTime)
                    tempList.Insert(interTime, "SettingsManager.U32Set GSInterpolationTime " & valInterTime)
                    lines = tempList.ToArray
                    IO.File.WriteAllLines(SettingsFile, lines)
                    MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF3 - Done!")
                Else
                    'Lines don't exist so add them
                    Dim Settings As New IO.FileStream(SettingsFile, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.None)
                    Dim myFileWriter As New System.IO.StreamWriter(Settings)
                    myFileWriter.WriteLine("SettingsManager.floatSet GSDefaultLatencyCompensation " & valLatComps.Replace(",", "."))
                    myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 1")
                    myFileWriter.WriteLine("SettingsManager.U32Set GSExtrapolationTime " & valExtraTime)
                    myFileWriter.WriteLine("SettingsManager.U32Set GSInterpolationTime " & valInterTime)
                    myFileWriter.Close()
                    Settings.Close()
                    MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF3 - Done!")
                End If
            End If
        Else
            MsgBox("Cannot find the Bad Company 2 Settings file!", MsgBoxStyle.Critical, "BC2HF3 - Error!")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        'Reading Bad Company 2 Settings File
        Dim SettingsFile As String = DocFolder & "\BFBC2\settings.ini"
        If System.IO.File.Exists(SettingsFile) Then
            Dim lines = IO.File.ReadAllLines(SettingsFile)
            Dim latComp As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.floatSet GSDefaultLatencyCompensation"))
            Dim extraFrame As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.boolSet GSExtrapolateFrame"))
            Dim extraTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSExtrapolationTime"))
            Dim interTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSInterpolationTime"))

            'Restoring Bad Company 2 Settings File
            If latComp > 0 And extraFrame > 0 And extraTime > 0 And interTime > 0 Then
                Dim tempList As New List(Of String)(lines)
                tempList.RemoveAt(latComp)
                tempList.RemoveAt(extraFrame - 1)
                tempList.RemoveAt(extraTime - 2)
                tempList.RemoveAt(interTime - 3)
                lines = tempList.ToArray
                IO.File.WriteAllLines(SettingsFile, lines)
                MsgBox("Bad Company 2 has been restored at its default state!", MsgBoxStyle.Information, "BC2HF3 - Done!")
            Else
                MsgBox("It seems Bad Company 2 is already at its default state!", MsgBoxStyle.Critical, "BC2HF3 - Error!")
            End If
        Else
            MsgBox("Cannot find the Bad Company 2 Settings file!", MsgBoxStyle.Critical, "BC2HF3 - Error!")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TabControl1.SelectedTab = TabPage3
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        TabControl1.SelectedTab = TabPage4
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.WindowState = FormWindowState.Minimized
        If Me.CheckBox2.Checked = True Then
            If Me.WindowState = FormWindowState.Minimized Then
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        'Reading Bad Company 2 Settings File
        Dim SettingsFile As String = DocFolder & "\BFBC2\settings.ini"
        If System.IO.File.Exists(SettingsFile) Then
            Dim lines = IO.File.ReadAllLines(SettingsFile)
            Dim latComp As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.floatSet GSDefaultLatencyCompensation"))
            Dim extraFrame As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.boolSet GSExtrapolateFrame"))
            Dim extraTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSExtrapolationTime"))
            Dim interTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSInterpolationTime"))

            'Writing Bad Company 2 Settings File
            Dim ping As String = Val(TextBox2.Text)
            Dim pingOffset As Integer = TrackBar1.Value
            Dim slideLatComp As Double = TrackBar2.Value
            Dim slideExtraTime As Double = TrackBar3.Value
            Dim slideInterTime As Double = TrackBar4.Value
            Dim valLatComp As Double = ((ping + pingOffset) / 1000) * (slideLatComp / 100)
            Dim valInterTime As Integer = (ping + pingOffset) * (slideInterTime / 100)
            Dim valExtraTime As Integer
            If Me.RadioButton1.Checked = True Then
                valExtraTime = ((ping + pingOffset) * 12) * (slideExtraTime / 100)
            ElseIf Me.RadioButton2.Checked = True Then
                valExtraTime = (((ping + pingOffset) * 5.5) + 489) * (slideExtraTime / 100)
            End If
            Dim valLatComps As String = CType(valLatComp, String)
            If ping <= 0 Then
                MsgBox("Error, invalid ping value. Must be higher than 0.", MsgBoxStyle.Critical, "BC2HF3 - Error!")
            Else
                'Lines already exist so replace them
                If latComp > 0 And extraFrame > 0 And extraTime > 0 And interTime > 0 Then
                    Dim tempList As New List(Of String)(lines)
                    tempList.RemoveAt(latComp)
                    tempList.RemoveAt(extraFrame - 1)
                    tempList.RemoveAt(extraTime - 2)
                    tempList.RemoveAt(interTime - 3)
                    tempList.Insert(latComp, "SettingsManager.floatSet GSDefaultLatencyCompensation " & valLatComps.Replace(",", "."))
                    If Me.CheckBox1.Checked = True Then
                        tempList.Insert(extraFrame, "SettingsManager.boolSet GSExtrapolateFrame 1")
                    Else
                        tempList.Insert(extraFrame, "SettingsManager.boolSet GSExtrapolateFrame 0")
                    End If
                    tempList.Insert(extraTime, "SettingsManager.U32Set GSExtrapolationTime " & valExtraTime)
                    tempList.Insert(interTime, "SettingsManager.U32Set GSInterpolationTime " & valInterTime)
                    lines = tempList.ToArray
                    IO.File.WriteAllLines(SettingsFile, lines)
                    MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF3 - Done!")
                Else
                    'Lines don't exist so add them
                    Dim Settings As New IO.FileStream(SettingsFile, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.None)
                    Dim myFileWriter As New System.IO.StreamWriter(Settings)
                    myFileWriter.WriteLine("SettingsManager.floatSet GSDefaultLatencyCompensation " & valLatComps.Replace(",", "."))
                    If Me.CheckBox1.Checked = True Then
                        myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 1")
                    Else
                        myFileWriter.WriteLine("SettingsManager.boolSet GSExtrapolateFrame 0")
                    End If
                    myFileWriter.WriteLine("SettingsManager.U32Set GSExtrapolationTime " & valExtraTime)
                    myFileWriter.WriteLine("SettingsManager.U32Set GSInterpolationTime " & valInterTime)
                    myFileWriter.Close()
                    Settings.Close()
                    MsgBox("Bad Company 2 hit registration should has been fixed!", MsgBoxStyle.Information, "BC2HF3 - Done!")
                End If
                End If
        Else
            MsgBox("Cannot find the Bad Company 2 Settings file!", MsgBoxStyle.Critical, "BC2HF3 - Error!")
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        'Reading Bad Company 2 Settings File
        Dim SettingsFile As String = DocFolder & "\BFBC2\settings.ini"
        If System.IO.File.Exists(SettingsFile) Then
            Dim lines = IO.File.ReadAllLines(SettingsFile)
            Dim latComp As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.floatSet GSDefaultLatencyCompensation"))
            Dim extraFrame As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.boolSet GSExtrapolateFrame"))
            Dim extraTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSExtrapolationTime"))
            Dim interTime As Integer = Array.FindIndex(lines, Function(s) s.Contains("SettingsManager.U32Set GSInterpolationTime"))

            'Restoring Bad Company 2 Settings File
            If latComp > 0 And extraFrame > 0 And extraTime > 0 And interTime > 0 Then
                Dim tempList As New List(Of String)(lines)
                tempList.RemoveAt(latComp)
                tempList.RemoveAt(extraFrame - 1)
                tempList.RemoveAt(extraTime - 2)
                tempList.RemoveAt(interTime - 3)
                lines = tempList.ToArray
                IO.File.WriteAllLines(SettingsFile, lines)
                MsgBox("Bad Company 2 has been restored at its default state!", MsgBoxStyle.Information, "BC2HF3 - Done!")
            Else
                MsgBox("It seems Bad Company 2 is already at its default state!", MsgBoxStyle.Critical, "BC2HF3 - Error!")
            End If
        Else
            MsgBox("Cannot find the Bad Company 2 Settings file!", MsgBoxStyle.Critical, "BC2HF3 - Error!")
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim DocFolder As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        'Creating Advanced Fix Values File
        Dim ValuesFile As New IO.FileStream(DocFolder & "\BFBC2\BC2HF\values.ini", IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
        Dim myValuesWriter As New System.IO.StreamWriter(ValuesFile)
        myValuesWriter.WriteLine("ping_offset " & TrackBar1.Value)
        myValuesWriter.WriteLine("latency_comp " & TrackBar2.Value)
        myValuesWriter.WriteLine("extrapolation " & TrackBar3.Value)
        myValuesWriter.WriteLine("interpolation " & TrackBar4.Value)
        If Me.CheckBox1.Checked = True Then
            myValuesWriter.WriteLine("extrapol_frame 1")
        ElseIf Me.CheckBox1.Checked = False Then
            myValuesWriter.WriteLine("extrapol_frame 0")
        End If
        If Me.RadioButton1.Checked = True Then
            myValuesWriter.WriteLine("hitreg_formula 0")
        ElseIf Me.RadioButton2.Checked = True Then
            myValuesWriter.WriteLine("hitreg_formula 1")
        End If
        myValuesWriter.Close()
        ValuesFile.Close()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Me.TrackBar1.Value = 10
        Me.Label11.Text = "10 ms"
        Me.TrackBar2.Value = 100
        Me.Label12.Text = "100 %"
        Me.TrackBar3.Value = 100
        Me.Label13.Text = "100 %"
        Me.TrackBar4.Value = 100
        Me.Label14.Text = "100 %"
        Me.CheckBox1.Checked = True
        Me.RadioButton1.Checked = True
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        Me.Label11.Text = Me.TrackBar1.Value & " ms"
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        Me.Label12.Text = Me.TrackBar2.Value & " %"
    End Sub

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        Me.Label13.Text = Me.TrackBar3.Value & " %"
    End Sub

    Private Sub TrackBar4_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar4.Scroll
        Me.Label14.Text = Me.TrackBar4.Value & " %"
    End Sub
End Class
