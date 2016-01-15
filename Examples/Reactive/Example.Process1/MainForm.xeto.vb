﻿Imports Eto.Forms
Imports Eto.Drawing
Imports Eto.Serialization.Xaml

Public Class MainForm
    Inherits Form

    Public Sub New()
        XamlReader.Load(Me)
    End Sub

    Sub HandleQuit(ByVal sender As Object, ByVal e As EventArgs)
        Application.Instance.Quit()
    End Sub

    Sub HandleClickMe(ByVal sender As Object, ByVal e As EventArgs)
        MessageBox.Show(Me, "I was clicked!")
    End Sub

End Class
