Imports Gtk

Namespace App

    Public Class Program

        Public Shared Sub Main()
            Application.Init()
            Dim win As MainForm = MainForm.Create()
            win.Show()
            Application.Run()
        End Sub

    End Class
End Namespace