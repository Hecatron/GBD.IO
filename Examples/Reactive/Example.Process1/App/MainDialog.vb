Imports System.Reactive.Linq
Imports System.Text
Imports System.Threading
Imports Common.Logging
Imports GBD.IO.Reactive.Diagnostics
Imports Gdk
Imports Gtk

' TODO
' 1. Test under Linux
' 2. Create a C# Copy

Namespace App

    ''' <summary> Main Dialog for testing RxProcess. </summary>
    Public Class MainDialog

#Region "Properties"

        ''' <summary> Log Output. </summary>
        ''' <value> NLog Instance. </value>
        Private Property Logger As ILog = LogManager.GetLogger(Me.GetType)

        ''' <summary> Rx Process. </summary>
        ''' <value> Rx Process. </value>
        Public Property RxProc As RxProcess

#End Region

#Region "Form Load / Close"

        ''' <summary> Main dialog loaded. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub MainDialog_Loaded(sender As Object, e As EventArgs) Handles Me.Loaded
            DefaultSize = New Size(700, 500)

            ExeCombo1.AppendText("Clear")
            ExeCombo1.AppendText("Console Test App")
            ExeCombo1.AppendText("Ping")
            ExeCombo1.AppendText("Gdb")
            ExeCombo1.Active = 1

            Dim filter1 As New FileFilter 
            filter1.Name = "Exe Files"
            filter1.AddPattern("*.exe")
            ExeChooserButt.AddFilter(filter1)
            Dim filter2 As New FileFilter
            filter2.Name = "All Files"
            filter2.AddPattern("*.*")
            ExeChooserButt.AddFilter(filter2)

        End Sub

        ''' <summary> Handle Close of Form, Quit Application. </summary>
        ''' <param name="o">    Source of the event. </param>
        ''' <param name="args"> Event information to send to registered event handlers. </param>
        Private Sub MainDialog_DeleteEvent(o As Object, args As DeleteEventArgs) Handles Me.DeleteEvent
            Application.Quit()
            args.RetVal = True
        End Sub

        ''' <summary> Gtk message box. </summary>
        ''' <param name="Msg"> The message. </param>
        Private Sub GtkMsgBox(Msg As String)
            Dim md As New MessageDialog(Nothing, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, Msg)
            md.Run()
            md.Destroy()
        End Sub

#End Region

#Region "Form Options"

        ''' <summary> Executable combo 1 changed. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ExeCombo1_Changed(sender As Object, e As EventArgs) Handles ExeCombo1.Changed

            ' Example values
            Select Case ExeCombo1.ActiveText
                Case "Clear"
                    ExePathTb.Text = ""
                    ArgumentsTb.Text = ""
                    WorkingDirTb.Text = ""

                Case "Console Test App"
                    ExePathTb.Text = "..\..\..\Example.ConsoleApp1\bin\Debug\Example.ConsoleApp1.exe"
                    ArgumentsTb.Text = ""
                    WorkingDirTb.Text = "..\..\..\Example.ConsoleApp1\bin\Debug\"

                Case "Ping"
                    ExePathTb.Text = "C:\Windows\System32\PING.EXE"
                    ArgumentsTb.Text = "192.168.111.1 -t"
                    WorkingDirTb.Text = "C:\Windows\System32"

                Case "Gdb"
                    Dim gdbdir As String = "C:\Program Files (x86)\Arduino\hardware\tools\gcc-arm-none-eabi-4.8.3-2014q1\bin"
                    Dim gdbexe As String = "arm-none-eabi-gdb.exe"
                    Dim gdbpath As String = IO.Path.Combine(gdbdir, gdbexe)

                    ExePathTb.Text = gdbpath
                    ArgumentsTb.Text = ""
                    WorkingDirTb.Text = gdbdir

            End Select

        End Sub

        ''' <summary> Selection of different exe. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ExeChooserButt_SelectionChanged(sender As Object, e As EventArgs) Handles ExeChooserButt.SelectionChanged
            ExePathTb.Text = ExeChooserButt.File.Path
            WorkingDirTb.Text = IO.Path.GetDirectoryName(ExePathTb.Text)
        End Sub

#End Region

#Region "Process Launch / Close"

        ''' <summary> Launch the Exe. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub LaunchExeButt_Clicked(sender As Object, e As EventArgs) Handles LaunchExeButt.Clicked

            ' Check Exe Parameters
            If IO.File.Exists(ExePathTb.Text) = False Then GtkMsgBox("Exe Not Found") : Exit Sub
            If LCase(IO.Path.GetExtension(ExePathTb.Text)) <> ".exe" Then GtkMsgBox("FIle is not an exe") : Exit Sub

            ' Create a new process
            If RxProc IsNot Nothing Then RxProc.Close()
            RxProc = New RxProcess(ExePathTb.Text, ArgumentsTb.Text, WorkingDirTb.Text)
            AddHandler RxProc.StateChange, AddressOf StateChange

            ' Subscribe to the output
            RxProc.RxStdOut.ObserveOn(SynchronizationContext.Current).Subscribe(
                Sub(inbound_item)
                    If inbound_item.Length > 0 Then
                        Dim tmpstr As String = Encoding.ASCII.GetString(inbound_item).Replace(vbCr, vbCrLf)
                        StdOutTb.Buffer.Text &= tmpstr
                        Logger.Info(tmpstr)
                    End If
                End Sub)
            RxProc.RxStdErr.ObserveOn(SynchronizationContext.Current).Subscribe(
                Sub(inbound_item)
                    If inbound_item.Length > 0 Then
                        Dim tmpstr As String = Encoding.ASCII.GetString(inbound_item).Replace(vbCr, vbCrLf)
                        StdErrorTb.Buffer.Text &= tmpstr
                        Logger.Error(tmpstr)
                    End If
                End Sub)

            ' Start the process
            RxProc.Start()

        End Sub

        ''' <summary> State change Handler. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub StateChange(sender As Object, e As EventArgs)
            If RxProc IsNot Nothing Then
                lblState.Text = RxProc.State.ToString
            Else
                lblState.Text = ""
            End If
        End Sub

        ''' <summary> Closes executable. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub CloseExeButt_Clicked(sender As Object, e As EventArgs) Handles CloseExeButt.Clicked
            If RxProc IsNot Nothing Then RxProc.Close()
        End Sub

#End Region

#Region "Process StdIn / Out"

        ''' <summary> Clears the standard out Text Box. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ClearStdOutButt_Clicked(sender As Object, e As EventArgs) Handles ClearStdOutButt.Clicked
            StdOutTb.Buffer.Text = ""
        End Sub

        ''' <summary> Clears the standard error Text Box. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ClearStdErrorButt_Clicked(sender As Object, e As EventArgs) Handles ClearStdErrorButt.Clicked
            StdErrorTb.Buffer.Text = ""
        End Sub

        ''' <summary> Send to Standard Input. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SendButton_Clicked(sender As Object, e As EventArgs) Handles SendButton.Clicked
            If RxProc IsNot Nothing Then

                ' Working Example 1
                'Dim tmparr = Encoding.ASCII.GetBytes(StdInputTxt.Buffer.Text & RxProc.Process.StandardInput.NewLine)
                'RxProc.Process.StandardInput.BaseStream.Write(tmparr, 0, tmparr.Length)
                'RxProc.Process.StandardInput.BaseStream.Flush()

                ' Working Example 2
                RxProc.Process.StandardInput.WriteLine(StdInputTxt.Buffer.Text)

                ' Working Example 3
                'RxProc.Process.StandardInput.Write(StdInputTxt.Buffer.Text)
                'RxProc.Process.StandardInput.Write(RxProc.Process.StandardInput.NewLine)

            End If
        End Sub

#End Region

    End Class

End Namespace
