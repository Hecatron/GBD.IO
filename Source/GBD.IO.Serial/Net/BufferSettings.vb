Imports System.Text
Imports GBD.IO.Serial.Base

Namespace Net

    ''' <summary> Settings for the Serial Buffer / Stream. </summary>
    Public Class BufferSettings
        Inherits BufferSettingsBase

#Region "Properties - Bound"

        ''' <summary> Binding to a Serial Port Implementation. </summary>
        ''' <value> The binding to a serial port. </value>
        Public Property BindingPort As SerialPort

        ''' <summary> If these settings are bound to a serial port implementation. </summary>
        ''' <value> true if this object is bound, false if not. </value>
        Public ReadOnly Property IsBound As Boolean
            Get
                If BindingPort Is Nothing Then Return False Else Return True
            End Get
        End Property

#End Region

#Region "Properties - Buffer Settings"

        ''' <summary>
        ''' Gets or sets a value indicating whether null bytes are ignored when transmitted between the
        ''' port and the receive buffer.
        ''' </summary>
        ''' <value> true if discard null, false if not. </value>
        Public Overrides Property DiscardNull As Boolean
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.DiscardNull
                Return _DiscardNull
            End Get
            Set(value As Boolean)
                _DiscardNull = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.DiscardNull = _DiscardNull
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the byte that replaces invalid bytes in a data stream when a parity error occurs.
        ''' </summary>
        ''' <value> The parity replace byte. </value>
        Public Overrides Property ParityReplace As Byte
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.ParityReplace
                Return _ParityReplace
            End Get
            Set(value As Byte)
                _ParityReplace = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.ParityReplace = _ParityReplace
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary> Gets or sets the size of the SerialPort input buffer. </summary>
        ''' <value> The size of the read buffer. </value>
        Public Overrides Property ReadBufferSize As Integer
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.ReadBufferSize
                Return _ReadBufferSize
            End Get
            Set(value As Integer)
                _ReadBufferSize = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.ReadBufferSize = _ReadBufferSize
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary> Gets or sets the size of the serial port output buffer. </summary>
        ''' <value> The size of the write buffer. </value>
        Public Overrides Property WriteBufferSize As Integer
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.WriteBufferSize
                Return _WriteBufferSize
            End Get
            Set(value As Integer)
                _WriteBufferSize = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.WriteBufferSize = _WriteBufferSize
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the number of milliseconds before a time-out occurs when a read operation does
        ''' not finish.
        ''' </summary>
        ''' <value> The read timeout. </value>
        Public Overrides Property ReadTimeout As Integer
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.ReadTimeout
                Return _ReadTimeout
            End Get
            Set(value As Integer)
                _ReadTimeout = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.ReadTimeout = _ReadTimeout
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the number of milliseconds before a time-out occurs when a write operation does
        ''' not finish.
        ''' </summary>
        ''' <value> The write timeout. </value>
        Public Overrides Property WriteTimeout As Integer
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.WriteTimeout
                Return _WriteTimeout
            End Get
            Set(value As Integer)
                _WriteTimeout = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.WriteTimeout = _WriteTimeout
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the value used to interpret the end of a call to the ReadLine and
        ''' WriteLine(System.String) methods.
        ''' </summary>
        ''' <value> The new line. </value>
        Public Overrides Property NewLine As String
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.NewLine
                Return _NewLine
            End Get
            Set(value As String)
                _NewLine = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.NewLine = _NewLine
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the byte encoding for pre- and post-transmission conversion of text.
        ''' </summary>
        ''' <value> The string encoding. </value>
        Public Overrides Property Encoding As Encoding
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.Encoding
                Return _Encoding
            End Get
            Set(value As Encoding)
                _Encoding = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.Encoding = _Encoding
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the number of bytes in the internal input buffer before a
        ''' SerialPort.DataReceived event occurs.
        ''' </summary>
        ''' <value> The received bytes threshold. </value>
        Public Overrides Property ReceivedBytesThreshold As Integer
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.ReceivedBytesThreshold
                Return _ReceivedBytesThreshold
            End Get
            Set(value As Integer)
                _ReceivedBytesThreshold = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.ReceivedBytesThreshold = _ReceivedBytesThreshold
                OnPropertyChanged()
            End Set
        End Property

#End Region

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
            SetDefaults()
        End Sub

        ''' <summary> Binding Constructor. </summary>
        ''' <param name="port"> The serial port to bind to. </param>
        Public Sub New(port As SerialPort)
            BindingPort = port
        End Sub

        ''' <summary> Clone Constructor. </summary>
        ''' <param name="inp"> The input class to clone from. </param>
        Public Sub New(inp As BufferSettings)
            MyBase.New(inp)
        End Sub

        ''' <summary> Clone. </summary>
        ''' <returns> A copy of this object. </returns>
        Public Overrides Function Clone() As Object
            Dim ret As New BufferSettings
            ret.Import(Me)
            Return ret
        End Function

#End Region

    End Class

End Namespace
