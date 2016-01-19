Imports System.IO
Imports System.Reactive.Linq
Imports System.Text
Imports System.Threading
Imports Common.Logging
Imports GBD.IO.Reactive.Diagnostics

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

#Region "Functions - Setup"

        ''' <summary> Main dialog load. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub MainDialog_Load(sender As Object, e As EventArgs) Handles Me.Load

            Logger.Debug("Test123")

            ' Example values
            'ExePathTb.Text = ""
            'ArgumentsTb.Text = ""
            'WorkingDirTb.Text = ""

            '' Ping Values
            'ExePathTb.Text = "C:\Windows\System32\PING.EXE"
            'ArgumentsTb.Text = "192.168.111.1 -t"
            'WorkingDirTb.Text = "C:\Windows\System32"

            ' Gdb values
            Dim gdbdir As String = "C:\Program Files (x86)\Arduino\hardware\tools\gcc-arm-none-eabi-4.8.3-2014q1\bin"
            Dim gdbexe As String = "arm-none-eabi-gdb.exe"
            Dim gdbpath As String = Path.Combine(gdbdir, gdbexe)

            ExePathTb.Text = gdbpath
            ArgumentsTb.Text = ""
            WorkingDirTb.Text = gdbdir
        End Sub

        ''' <summary> Browse for an exe. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ExeBrowseButt_Click(sender As Object, e As EventArgs) Handles ExeBrowseButt.Click
            Dim dia As New OpenFileDialog
            Dim result As DialogResult = dia.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                ExePathTb.Text = dia.FileName
                WorkingDirTb.Text = Path.GetDirectoryName(dia.FileName)
            End If
        End Sub

        ''' <summary> Launches the exe. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub LaunchExeButt_Click(sender As Object, e As EventArgs) Handles LaunchExeButt.Click

            ' Check Exe Parameters
            If File.Exists(ExePathTb.Text) = False Then MsgBox("Exe Not Found") : Exit Sub
            If LCase(Path.GetExtension(ExePathTb.Text)) <> ".exe" Then MsgBox("FIle is not an exe") : Exit Sub

            ' Create a new process
            RxProc = New RxProcess(ExePathTb.Text, ArgumentsTb.Text, WorkingDirTb.Text)

            ' Subscribe to the output
            RxProc.RxStdOut.ObserveOn(SynchronizationContext.Current).Subscribe( _
                Sub(item)
                    If item.Length > 0 Then
                        Dim tmpstr As String = Encoding.ASCII.GetString(item).Replace(vbCr, vbCrLf)
                        StdOutTb.Text &= tmpstr
                        Logger.Info(tmpstr)
                    End If
                End Sub)
            RxProc.RxStdErr.ObserveOn(SynchronizationContext.Current).Subscribe( _
                Sub(item)
                    If item.Length > 0 Then
                        Dim tmpstr As String = Encoding.ASCII.GetString(item).Replace(vbCr, vbCrLf)
                        StdErrorTb.Text &= tmpstr
                        Logger.Error(tmpstr)
                    End If
                End Sub)

            ' Start the process
            RxProc.Start()
        End Sub

#End Region

#Region "Functions - Standard In / Out"

        ''' <summary> Clears the standard out text box. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ClearStdOutButt_Click(sender As Object, e As EventArgs) Handles ClearStdOutButt.Click
            StdOutTb.Text = ""
        End Sub

        ''' <summary> Clears the standard error text box. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ClearStdErrorButt_Click(sender As Object, e As EventArgs) Handles ClearStdErrorButt.Click
            StdErrorTb.Text = ""
        End Sub

        ''' <summary> Send text data to standard input. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub StdInSendButt_Click(sender As Object, e As EventArgs) Handles StdInSendButt.Click
            If RxProc IsNot Nothing Then

                ' TODO this works, but the stream async calls don't
                'RxProc.Process.StandardInput.WriteLine(StdInputTb.Text)

                'RxProc.Process.StandardInput.Write(StdInputTb.Text)
                'RxProc.Process.StandardInput.Write(RxProc.Process.StandardInput.NewLine)

                Dim tmparr = Encoding.ASCII.GetBytes(StdInputTb.Text & RxProc.Process.StandardInput.NewLine)
                RxProc.Process.StandardInput.BaseStream.Write(tmparr, 0, tmparr.Length)
                RxProc.Process.StandardInput.BaseStream.Flush()



                ' This should work but doesn't
                'RxProc.Uart.RxStdIn.Send(StdInputTb.Text)
            End If
        End Sub

#End Region

    End Class

End Namespace
