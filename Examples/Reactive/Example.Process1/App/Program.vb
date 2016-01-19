Imports Gtk

Namespace App

    ''' <summary> Main Program Launcher. </summary>
    Public Class Program

        ''' <summary> Main entry-point for this application. </summary>
        Public Shared Sub Main()
            Application.Init()
            Dim win As MainDialog = MainDialog.Create()
            win.Show()
            Application.Run()
        End Sub

    End Class
End Namespace