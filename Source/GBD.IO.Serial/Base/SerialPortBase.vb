Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports GBD.IO.Reactive.Stream

Namespace Base

    ''' <summary> Base Class for a Serial Port. </summary>
    Public MustInherit Class SerialPortBase
        Implements IDisposable
        Implements INotifyPropertyChanged

#Region "Properties"

        ''' <summary> Underlying Port Name. </summary>
        ''' <value> Underlying Port Name. </value>
        Public MustOverride Property Name As String

        ''' <summary> If the Serial Port is Open. </summary>
        ''' <value> true if this object is open, false if not. </value>
        Public MustOverride Property IsOpen As Boolean

        ''' <summary> Serial Port Settings. </summary>
        ''' <value> Serial Port Settings. </value>
        Public MustOverride Property Settings As SettingsBase

        ''' <summary> Serial Port Buffer Settings. </summary>
        ''' <value> Serial Port Buffer Settings. </value>
        Public MustOverride Property BufferSettings As BufferSettingsBase

        ''' <summary> Pin States. </summary>
        ''' <value> Pin States. </value>
        Public MustOverride Property PinStates As PinStatesBase

        ''' <summary> Gets the BaseStream for reading / writing data. </summary>
        ''' <value> The base stream for reading / writing data. </value>
        Public MustOverride ReadOnly Property BaseStream As RxStream

#End Region

#Region "Destructors"

        ''' <summary> Disposal. </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Close()
        End Sub

        ''' <summary> Destructor. </summary>
        Protected Overrides Sub Finalize()
            Try
                Close()
            Catch ex As Exception
            End Try
            MyBase.Finalize()
        End Sub

#End Region

#Region "Events - Property Changed"

        ''' <summary>
        ''' Property Changed Event
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ''' <summary> Property Changed Event. </summary>
        ''' <param name="propertyName">  Name of the caller member name property. </param>
        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = "none passed")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

#End Region

#Region "Functions - Open / Close"

        ''' <summary> Opens a new serial port connection. </summary>
        Public MustOverride Sub ApplySettings()

        ''' <summary> Opens a new serial port connection. </summary>
        Public MustOverride Sub Open()

        ''' <summary> Closes the port connection, and disposes the internal stream object. </summary>
        Public MustOverride Sub Close()

#End Region

    End Class

End Namespace
