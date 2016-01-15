Imports GBD.IO.Serial.Base

Namespace Net

    ''' <summary> Bound Serial Port Settings. </summary>
    Public Class Settings
        Inherits SettingsBase

#Region "Properties - Bound"

        ''' <summary> Binding to a Serial Port Implementation. </summary>
        ''' <value> The binding serial port. </value>
        Public Property BindingPort As SerialPort

        ''' <summary> If these settings are bound to a serial port implementation. </summary>
        ''' <value> true if this object is bound, false if not. </value>
        Public ReadOnly Property IsBound As Boolean
            Get
                If BindingPort Is Nothing Then Return False Else Return True
            End Get
        End Property

#End Region

#Region "Properties - Port Settings"

        ''' <summary> Number of Databits used within a single packet. </summary>
        ''' <value> Number of Databits used within a single packet. </value>
        Public Overrides Property DataBits As DataBitsType
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.DataBits
                Return _DataBits
            End Get
            Set(value As DataBitsType)
                _DataBits = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.DataBits = _DataBits
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary> Type of Parity used. </summary>
        ''' <value> Type of Parity used. </value>
        Public Overrides Property Parity As System.IO.Ports.Parity
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.Parity
                Return _Parity
            End Get
            Set(value As System.IO.Ports.Parity)
                _Parity = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.Parity = _Parity
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary> Number of Stop Bits used within a single packet. </summary>
        ''' <value> Number of Stop Bits used within a single packet. </value>
        Public Overrides Property StopBits As System.IO.Ports.StopBits
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.StopBits
                Return _StopBits
            End Get
            Set(value As System.IO.Ports.StopBits)
                _StopBits = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.StopBits = _StopBits
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary> Handshake Flow Control used for the serial port. </summary>
        ''' <value> Handshake Flow Control used for the serial port. </value>
        Public Overrides Property HandShake As System.IO.Ports.Handshake
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.Handshake
                Return _HandShake
            End Get
            Set(value As System.IO.Ports.Handshake)
                _HandShake = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.Handshake = _HandShake
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary> Speed or BaudRate of the Serial Port, Expressed as an Integer Value. </summary>
        ''' <value> Speed or BaudRate of the Serial Port, Expressed as an Integer Value. </value>
        Public Overrides Property BaudRateInt As Integer
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.BaudRate
                Return _BaudRateInt
            End Get
            Set(value As Integer)
                _BaudRateInt = value
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.BaudRate = _BaudRateInt
                OnPropertyChanged("BaudRate")
                OnPropertyChanged("BaudRateInt")
            End Set
        End Property

#End Region

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
            SetDefaults()
        End Sub

        ''' <summary> Binding Constructor. </summary>
        ''' <param name="port"> The serial port. </param>
        Public Sub New(port As SerialPort)
            BindingPort = port
        End Sub

        ''' <summary> Clone Constructor. </summary>
        ''' <param name="inp"> The settings to clone from. </param>
        Public Sub New(inp As Settings)
            MyBase.New(inp)
        End Sub

        ''' <summary> Clone. </summary>
        ''' <returns> A copy of this object. </returns>
        Public Overrides Function Clone() As Object
            Dim ret As New Settings
            ret.Import(Me)
            Return ret
        End Function

#End Region

    End Class

End Namespace
