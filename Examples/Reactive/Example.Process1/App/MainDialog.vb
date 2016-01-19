Imports System.Reactive.Linq
Imports System.Text
Imports System.Threading
Imports Common.Logging
Imports GBD.IO.Reactive.Diagnostics
Imports Gdk
Imports Gtk

' TODO
' 1. Test under Linux
' 2. Remove old example
' 3. Create a C# Copy
' 4. Make TextView Control scrollable

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

#Region "Handlers"

        ''' <summary> Main dialog loaded. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub MainDialog_Loaded(sender As Object, e As EventArgs) Handles Me.Loaded
            SendButton.Sensitive = False
            StdInputTxt.Sensitive = False
            StdErrorTb.Editable = False
            StdOutTb.Editable = False

            DefaultSize = New Size(700, 500)
            ' TODO set file chooser filter

            Logger.Debug("Test123")

            ' Example values
            'ExePathTb.Text = ""
            'ArgumentsTb.Text = ""
            'WorkingDirTb.Text = ""

            ' Ping Values
            ExePathTb.Text = "C:\Windows\System32\PING.EXE"
            ArgumentsTb.Text = "192.168.111.1 -t"
            WorkingDirTb.Text = "C:\Windows\System32"

            '' Gdb values
            'Dim gdbdir As String = "C:\Program Files (x86)\Arduino\hardware\tools\gcc-arm-none-eabi-4.8.3-2014q1\bin"
            'Dim gdbexe As String = "arm-none-eabi-gdb.exe"
            'Dim gdbpath As String = IO.Path.Combine(gdbdir, gdbexe)

            'ExePathTb.Text = gdbpath
            'ArgumentsTb.Text = ""
            'WorkingDirTb.Text = gdbdir

        End Sub

        ''' <summary> Handle Close of Form, Quit Application. </summary>
        ''' <param name="o">    Source of the event. </param>
        ''' <param name="args"> Event information to send to registered event handlers. </param>
        Private Sub MainDialog_DeleteEvent(o As Object, args As DeleteEventArgs) Handles Me.DeleteEvent
            Application.Quit()
            args.RetVal = True
        End Sub

        ''' <summary> Selection of different exe. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ExeChooserButt_SelectionChanged(sender As Object, e As EventArgs) Handles ExeChooserButt.SelectionChanged
            ExePathTb.Text = ExeChooserButt.File.Path
            WorkingDirTb.Text = System.IO.Path.GetDirectoryName(ExePathTb.Text)
        End Sub

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

        ''' <summary> Launch the Exe. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub LaunchExeButt_Clicked(sender As Object, e As EventArgs) Handles LaunchExeButt.Clicked

            ' Check Exe Parameters
            If IO.File.Exists(ExePathTb.Text) = False Then GtkMsgBox("Exe Not Found") : Exit Sub
            If LCase(IO.Path.GetExtension(ExePathTb.Text)) <> ".exe" Then GtkMsgBox("FIle is not an exe") : Exit Sub

            ' Create a new process
            RxProc = New RxProcess(ExePathTb.Text, ArgumentsTb.Text, WorkingDirTb.Text)

            ' Subscribe to the output
            RxProc.RxStdOut.ObserveOn(SynchronizationContext.Current).Subscribe(
                Sub(item)
                    If item.Length > 0 Then
                        Dim tmpstr As String = Encoding.ASCII.GetString(item).Replace(vbCr, vbCrLf)
                        StdOutTb.Buffer.Text &= tmpstr
                        Logger.Info(tmpstr)
                    End If
                End Sub)
            RxProc.RxStdErr.ObserveOn(SynchronizationContext.Current).Subscribe(
                Sub(item)
                    If item.Length > 0 Then
                        Dim tmpstr As String = Encoding.ASCII.GetString(item).Replace(vbCr, vbCrLf)
                        StdErrorTb.Buffer.Text &= tmpstr
                        Logger.Error(tmpstr)
                    End If
                End Sub)

            ' Start the process
            RxProc.Start()

        End Sub

        ''' <summary> Send to Standard Input. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SendButton_Clicked(sender As Object, e As EventArgs) Handles SendButton.Clicked
            ' TODO

            'If RxProc IsNot Nothing Then

            '    ' TODO this works, but the stream async calls don't
            '    'RxProc.Process.StandardInput.WriteLine(StdInputTb.Text)

            '    'RxProc.Process.StandardInput.Write(StdInputTb.Text)
            '    'RxProc.Process.StandardInput.Write(RxProc.Process.StandardInput.NewLine)

            '    Dim tmparr = Encoding.ASCII.GetBytes(StdInputTb.Text & RxProc.Process.StandardInput.NewLine)
            '    RxProc.Process.StandardInput.BaseStream.Write(tmparr, 0, tmparr.Length)
            '    RxProc.Process.StandardInput.BaseStream.Flush()



            '    ' This should work but doesn't
            '    'RxProc.Uart.RxStdIn.Send(StdInputTb.Text)
            'End If

        End Sub

        ''' <summary> Gtk message box. </summary>
        ''' <param name="Msg"> The message. </param>
        Private Sub GtkMsgBox(Msg As String)
            Dim md As New MessageDialog(Nothing, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, Msg)
            md.Run()
            md.Destroy()
        End Sub

#End Region

    End Class

End Namespace
