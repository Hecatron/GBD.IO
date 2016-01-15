Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace Base

    ''' <summary> Base class for Serial Port Settings. </summary>
    Public Class SettingsBase
        Implements ICloneable
        Implements INotifyPropertyChanged

#Region "Public Properties - General Port Settings"

        ''' <summary> Number of Databits used within a single packet. </summary>
        ''' <value> Number of Databits used within a single packet. </value>
        Public Overridable Property DataBits As DataBitsType
            Get
                Return _DataBits
            End Get
            Set(value As DataBitsType)
                _DataBits = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _DataBits As DataBitsType

        ''' <summary> Type of Parity used. </summary>
        ''' <value> Type of Parity used. </value>
        Public Overridable Property Parity As System.IO.Ports.Parity
            Get
                Return _Parity
            End Get
            Set(value As System.IO.Ports.Parity)
                _Parity = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _Parity As System.IO.Ports.Parity

        ''' <summary> Number of Stop Bits used within a single packet. </summary>
        ''' <value> Number of Stop Bits used within a single packet. </value>
        Public Overridable Property StopBits As System.IO.Ports.StopBits
            Get
                Return _StopBits
            End Get
            Set(value As System.IO.Ports.StopBits)
                _StopBits = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _StopBits As System.IO.Ports.StopBits

        ''' <summary> Handshake Flow Control used for the serial port. </summary>
        ''' <value> Handshake Flow Control used for the serial port. </value>
        Public Overridable Property HandShake As System.IO.Ports.Handshake
            Get
                Return _HandShake
            End Get
            Set(value As System.IO.Ports.Handshake)
                _HandShake = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _HandShake As System.IO.Ports.Handshake

        ''' <summary> Speed or BaudRate of the Serial Port, Expressed as an Integer Value. </summary>
        ''' <value> The baud rate as an integer. </value>
        Public Overridable Property BaudRateInt As Integer
            Get
                Return _BaudRateInt
            End Get
            Set(value As Integer)
                _BaudRateInt = value
                OnPropertyChanged("BaudRate")
                OnPropertyChanged("BaudRateInt")
            End Set
        End Property
        Protected Property _BaudRateInt As Integer

        ''' <summary> Speed or BaudRate of the Serial Port, Expressed as an Enum Value. </summary>
        ''' <value> The baud rate as an enum. </value>
        Public Overridable Property BaudRate As BaudRates
            Get
                Dim tmpenum As BaudRates = BaudRates.Unknown
                [Enum].TryParse(BaudRateInt, tmpenum)
                Return tmpenum
            End Get
            Set(value As BaudRates)
                BaudRateInt = value
                OnPropertyChanged("BaudRate")
                OnPropertyChanged("BaudRateInt")
            End Set
        End Property

#End Region

#Region "Public Types"

        ''' <summary> Number of Databits used for a single packet. </summary>
        Public Enum DataBitsType
            D5 = 5
            D6 = 6
            D7 = 7
            D8 = 8
        End Enum

        ''' <summary> Standard Baud Rate Values. </summary>
        Public Enum BaudRates

            ''' <summary> Invalid Baud Rate</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Unknown = -1
            B75 = 75
            B110 = 110
            B134 = 134
            B150 = 150
            B300 = 300
            B600 = 600
            B1200 = 1200
            B1800 = 1800
            B2400 = 2400
            B4800 = 4800
            B7200 = 7200
            B9600 = 9600
            B14400 = 14400
            B19200 = 19200
            B38400 = 38400
            B57600 = 57600
            B115200 = 115200
            B128000 = 128000
            B230400 = 230400
            B256000 = 256000
            B460800 = 460800
            B512000 = 512000
        End Enum

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

#Region "Public Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
        End Sub

        ''' <summary> Clone Constructor. </summary>
        ''' <param name="inp"> The input settings to clone from. </param>
        Public Sub New(inp As SettingsBase)
            Import(inp)
        End Sub

#End Region

#Region "Public Functions"

        ''' <summary> Copy / Import Port Settings into underlying Serial Port Object. </summary>
        ''' <param name="import"> The settings to import / copy from. </param>
        Public Overridable Sub Import(import As SettingsBase)
            DataBits = import.DataBits
            Parity = import.Parity
            StopBits = import.StopBits
            HandShake = import.HandShake
            BaudRateInt = import.BaudRateInt
        End Sub

        ''' <summary> Setup Default Port Settings. </summary>
        Public Overridable Sub SetDefaults()
            _DataBits = DataBitsType.D8
            _Parity = System.IO.Ports.Parity.None
            _StopBits = System.IO.Ports.StopBits.One
            _HandShake = System.IO.Ports.Handshake.None
            _BaudRateInt = BaudRates.B9600
        End Sub

        ''' <summary> Clone. </summary>
        ''' <returns> A copy of this object. </returns>
        Public Overridable Function Clone() As Object Implements ICloneable.Clone
            Dim ret As New SettingsBase
            ret.Import(Me)
            Return ret
        End Function

        ''' <summary> Copy. </summary>
        ''' <param name="inp"> The input settings to copy. </param>
        ''' <returns> A SettingsBase class. </returns>
        Public Shared Function Copy(inp As SettingsBase) As SettingsBase
            Dim ret As New SettingsBase(inp)
            Return ret
        End Function

#End Region

    End Class

End Namespace
