Imports Gtk

Namespace App
    Partial Public Class MainDialog
        Inherits Window

#Region "Properties"

        ''' <summary> Used to load in the glade file resource as a window. </summary>
        Private _builder As Builder

        Friend WithEvents ExeCombo1 As ComboBoxText
        Friend WithEvents ExeChooserButt As FileChooserButton
        Friend WithEvents ExePathTb As Entry
        Friend WithEvents WorkingDirTb As Entry
        Friend WithEvents ArgumentsTb As Entry
        Friend WithEvents LaunchExeButt As Button
        Friend WithEvents CloseExeButt As Button

        Friend WithEvents ClearStdOutButt As Button
        Friend WithEvents StdOutTb As TextView
        Friend WithEvents ClearStdErrorButt As Button
        Friend WithEvents StdErrorTb As TextView

        Friend WithEvents SendButton As Button
        Friend WithEvents StdInputTxt As Entry

        ''' <summary> Event queue for all listeners interested in Loaded events. </summary>
        Public Event Loaded As EventHandler

#End Region

#Region "Constructors / Destructors"

        ''' <summary> Default Shared Constructor. </summary>
        ''' <returns> A MainDialog. </returns>
        Public Shared Function Create() As MainDialog
            Dim builder As New Builder(Nothing, "Example.Process1.MainDialog.glade", Nothing)
            Return New MainDialog(builder, builder.GetObject("window1").Handle)
        End Function

        ''' <summary> Specialised constructor for use only by derived class. </summary>
        ''' <param name="builder"> The builder. </param>
        ''' <param name="handle">  The handle. </param>
        Protected Sub New(builder As Builder, handle As IntPtr)
            MyBase.New(handle)
            _builder = builder
            builder.Autoconnect(Me)

            ' Link the Controls here instead of using Attributes
            ExeCombo1 = builder.GetObject("ExeCombo1")
            ExeChooserButt = builder.GetObject("ExeChooserButt")
            ExePathTb = builder.GetObject("ExePathTb")
            WorkingDirTb = builder.GetObject("WorkingDirTb")
            ArgumentsTb = builder.GetObject("ArgumentsTb")
            LaunchExeButt = builder.GetObject("LaunchExeButt")
            CloseExeButt = builder.GetObject("CloseExeButt")
            ClearStdOutButt = builder.GetObject("ClearStdOutButt")
            StdOutTb = builder.GetObject("StdOutTb")
            ClearStdErrorButt = builder.GetObject("ClearStdErrorButt")
            StdErrorTb = builder.GetObject("StdErrorTb")
            SendButton = builder.GetObject("SendButton")
            StdInputTxt = builder.GetObject("StdInputTxt")

            ' Form Loaded
            RaiseEvent Loaded(Me, Nothing)
        End Sub

#End Region

    End Class

End Namespace
