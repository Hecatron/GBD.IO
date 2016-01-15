Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Text

Namespace Base

    ''' <summary> Base class for Buffer Settings. </summary>
    Public Class BufferSettingsBase
        Implements ICloneable
        Implements INotifyPropertyChanged

#Region "Properties"

        ''' <summary>
        ''' Gets or sets a value indicating whether null bytes are ignored when transmitted between the
        ''' port and the receive buffer.
        ''' </summary>
        ''' <value> true if discard null, false if not. </value>
        Public Overridable Property DiscardNull As Boolean
            Get
                Return _DiscardNull
            End Get
            Set(value As Boolean)
                _DiscardNull = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _DiscardNull As Boolean

        ''' <summary>
        ''' Gets or sets the byte that replaces invalid bytes in a data stream when a parity error occurs.
        ''' </summary>
        ''' <value> The parity replace byte. </value>
        Public Overridable Property ParityReplace As Byte
            Get
                Return _ParityReplace
            End Get
            Set(value As Byte)
                _ParityReplace = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _ParityReplace As Byte

        ''' <summary> Gets or sets the size of the SerialPort input buffer. </summary>
        ''' <value> The size of the read buffer. </value>
        Public Overridable Property ReadBufferSize As Integer
            Get
                Return _ReadBufferSize
            End Get
            Set(value As Integer)
                _ReadBufferSize = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _ReadBufferSize As Integer

        ''' <summary> Gets or sets the size of the serial port output buffer. </summary>
        ''' <value> The size of the write buffer. </value>
        Public Overridable Property WriteBufferSize As Integer
            Get
                Return _WriteBufferSize
            End Get
            Set(value As Integer)
                _WriteBufferSize = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _WriteBufferSize As Integer

        ''' <summary>
        ''' Gets or sets the number of milliseconds before a time-out occurs when a read operation does
        ''' not finish.
        ''' </summary>
        ''' <value> The read timeout. </value>
        Public Overridable Property ReadTimeout As Integer
            Get
                Return _ReadTimeout
            End Get
            Set(value As Integer)
                _ReadTimeout = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _ReadTimeout As Integer

        ''' <summary>
        ''' Gets or sets the number of milliseconds before a time-out occurs when a write operation does
        ''' not finish.
        ''' </summary>
        ''' <value> The write timeout. </value>
        Public Overridable Property WriteTimeout As Integer
            Get
                Return _WriteTimeout
            End Get
            Set(value As Integer)
                _WriteTimeout = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _WriteTimeout As Integer

        ''' <summary>
        ''' Gets or sets the value used to interpret the end of a call to the ReadLine and
        ''' WriteLine(System.String) methods.
        ''' </summary>
        ''' <value> The new line. </value>
        Public Overridable Property NewLine As String
            Get
                Return _NewLine
            End Get
            Set(value As String)
                _NewLine = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _NewLine As String

        ''' <summary>
        ''' Gets or sets the byte encoding for pre- and post-transmission conversion of text.
        ''' </summary>
        ''' <value> The string encoding to use. </value>
        Public Overridable Property Encoding As Encoding
            Get
                Return _Encoding
            End Get
            Set(value As Encoding)
                _Encoding = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _Encoding As Encoding

        ''' <summary>
        ''' Gets or sets the number of bytes in the internal input buffer before a
        ''' SerialPort.DataReceived event occurs.
        ''' </summary>
        ''' <value> The received bytes threshold. </value>
        Public Overridable Property ReceivedBytesThreshold As Integer
            Get
                Return _ReceivedBytesThreshold
            End Get
            Set(value As Integer)
                _ReceivedBytesThreshold = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _ReceivedBytesThreshold As Integer

#End Region

#Region "Public Events - Property Changed"

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

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
        End Sub

        ''' <summary> Clone Constructor. </summary>
        ''' <param name="inp"> The input settings to clone from. </param>
        Public Sub New(inp As BufferSettingsBase)
            Import(inp)
        End Sub

#End Region

#Region "Public Functions"

        ''' <summary> Copy / Import Port Settings into underlying Serial Port Object. </summary>
        ''' <param name="import"> The settings class to import / copy from. </param>
        Public Overridable Sub Import(import As BufferSettingsBase)
            DiscardNull = import.DiscardNull
            ParityReplace = import.ParityReplace
            ReadBufferSize = import.ReadBufferSize
            WriteBufferSize = import.WriteBufferSize
            ReadTimeout = import.ReadTimeout
            WriteTimeout = import.WriteTimeout
            NewLine = import.NewLine
            Encoding = import.Encoding
            ReceivedBytesThreshold = import.ReceivedBytesThreshold
        End Sub

        ''' <summary> Setup Default Port Settings. </summary>
        Public Overridable Sub SetDefaults()
            DiscardNull = False
            ParityReplace = 63
            ReadBufferSize = 4096
            WriteBufferSize = 2048
            ReadTimeout = -1
            WriteTimeout = -1
            NewLine = vbLf
            Encoding = New ASCIIEncoding
            ReceivedBytesThreshold = 1
        End Sub

        ''' <summary> Clone. </summary>
        ''' <returns> Create a copy of this object. </returns>
        Public Overridable Function Clone() As Object Implements ICloneable.Clone
            Dim ret As New BufferSettingsBase
            ret.Import(Me)
            Return ret
        End Function

        ''' <summary> Copy settings from an existing class into this one. </summary>
        ''' <param name="inp"> The input class to copy from. </param>
        ''' <returns> A BufferSettingsBase class. </returns>
        Public Shared Function Copy(inp As BufferSettingsBase) As BufferSettingsBase
            Dim ret As New BufferSettingsBase(inp)
            Return ret
        End Function

#End Region

    End Class

End Namespace
