Imports GBD.IO.Reactive.Stream
Imports GBD.IO.Serial.Base

''' <summary> Represents a Singluar Bus Pirate Device. </summary>
Public Class BPDevice

#Region "Public Properties"

    ''' <summary> Serial Port used to access the bus pirate. </summary>
    ''' <value> Serial Port used to access the bus pirate. </value>
    Public ReadOnly Property SPort As SerialPortBase
        Get
            Return _SPort
        End Get
    End Property
    Protected Property _SPort As SerialPortBase

    ''' <summary> Serial Port Stream used for sending / receiving data. </summary>
    ''' <value> Serial Port Stream used for sending / receiving data. </value>
    Public ReadOnly Property SPortStream As RxStream
        Get
            Return _SPortStream
        End Get
    End Property
    Protected Property _SPortStream As RxStream

    ''' <summary> Current Mode of the Device. </summary>
    ''' <value> Current Mode of the Device. </value>
    Public ReadOnly Property Mode As BPMode
        Get
            Return _Mode
        End Get
    End Property
    Protected Property _Mode As BPMode = BPMode.Unknown

    ''' <summary> Current Binary Mode of the Device. </summary>
    ''' <value> Current Binary Mode of the Device. </value>
    Public ReadOnly Property BinaryMode As BPBinaryMode
        Get
            Return _BinaryMode
        End Get
    End Property
    Protected Property _BinaryMode As BPBinaryMode = BPBinaryMode.Unknown

#End Region

#Region "Types"

    ''' <summary> Current Mode the Bus Pirate is in. </summary>
    Public Enum BPMode
        Unknown = -1
        Test = 0
        Binary = 1
    End Enum

    ''' <summary> Values that represent bp binary modes for the bus pirate. </summary>
    Public Enum BPBinaryMode
        Unknown = -1
        BitBang = 0
        SPI = 1
        I2C = 2
        Uart = 3
        OneWire = 4
        RawWire = 5
        JTagOpenOCD = 6
    End Enum

#End Region

#Region "Constructors"

    ''' <summary> Default Constructor. </summary>
    ''' <param name="sport"> The serial port to attach to. </param>
    Public Sub New(sport As SerialPortBase)
        _SPort = sport
        _SPortStream = sport.BaseStream
    End Sub

    ''' <summary> Default Constructor. </summary>
    ''' <param name="sportstream"> The serial port stream to use. </param>
    Public Sub New(sportstream As RxStream)
        _SPort = Nothing
        _SPortStream = sportstream
    End Sub

#End Region

#Region "Functions"

    ''' <summary> Sets the serial port settings to defaults for the bus pirate. </summary>
    Public Sub SetDefaultSerialSettings()
        Dim defsetts = DefaultSerialSettings()
        _SPort.Settings.Import(defsetts)
    End Sub




    ' ''' <summary>
    ' ''' Get a Binary I2C Interface
    ' ''' </summary>
    'Public Function Get_Bin_Iface_I2C() As IfaceI2C
    '    'Dim tmprx = Rx.Where(Function(t) Mode = BPMode.Binary_I2C)
    '    'Dim tmptx = Tx
    '    ' TODO
    '    Return Nothing
    'End Function




    ''' <summary> Get the Default Serial Port Settings. </summary>
    ''' <returns> A Serial Port Settings class. </returns>
    Public Shared Function DefaultSerialSettings() As SettingsBase
        Dim setts As New SettingsBase()
        setts.BaudRate = Serial.Base.SettingsBase.BaudRates.B115200
        setts.DataBits = Serial.Base.SettingsBase.DataBitsType.D8
        setts.HandShake = System.IO.Ports.Handshake.None
        setts.Parity = System.IO.Ports.Parity.None
        setts.StopBits = System.IO.Ports.StopBits.One
        Return setts
    End Function

#End Region

End Class
