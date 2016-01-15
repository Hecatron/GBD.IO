Imports System.ComponentModel
Imports System.IO
Imports GBD.IO.Reactive.Stream
Imports GBD.IO.Serial.Base

Namespace Net

    ''' <summary> .Net Implementation of a Serial Port. </summary>
    Public Class SerialPort
        Inherits SerialPortBase
        Implements INotifyPropertyChanged

#Region "Properties"

        ''' <summary> Internal Representation of the .Net Serial Port. </summary>
        ''' <value> The internal i/o serial port. </value>
        Public Overridable ReadOnly Property IOSerialPort As Ports.SerialPort
            Get
                Return _IOSerialPort
            End Get
        End Property
        Protected Property _IOSerialPort As Ports.SerialPort

        ''' <summary> Underlying Serial Port Name. </summary>
        ''' <value> Underlying Serial Port Name. </value>
        Public Overrides Property Name() As String
            Get
                Return _IOSerialPort.PortName
            End Get
            Set(value As String)
                If IsOpen Then Close()
                IOSerialPort.PortName = value
                OnPropertyChanged()
            End Set
        End Property

        ''' <summary> If the Serial Port is Open. </summary>
        ''' <value> true if this object is open, false if not. </value>
        Public Overrides Property IsOpen() As Boolean
            Get
                Return _IOSerialPort.IsOpen
            End Get
            Set(value As Boolean)
                If IsOpen And value = False Then Close() : OnPropertyChanged()
                If Not IsOpen And value = True Then Open() : OnPropertyChanged()
            End Set
        End Property

        ''' <summary> Serial Port Settings. </summary>
        ''' <value> Serial Port Settings. </value>
        Public Overrides Property Settings() As SettingsBase
            Get
                Return _Settings
            End Get
            Set(value As SettingsBase)
                _Settings = New Settings(value)
                _Settings.BindingPort = Me
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _Settings As Settings

        ''' <summary> Serial Port Buffer Settings. </summary>
        ''' <value> Serial Port Buffer Settings. </value>
        Public Overrides Property BufferSettings As BufferSettingsBase
            Get
                Return _BufferSettings
            End Get
            Set(value As BufferSettingsBase)
                _BufferSettings = New BufferSettings(value)
                _BufferSettings.BindingPort = Me
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _BufferSettings As BufferSettings

        ''' <summary> Serial Port Pin States. </summary>
        ''' <value> Serial Port Pin States. </value>
        Public Overrides Property PinStates As PinStatesBase
            Get
                Return _PinStates
            End Get
            Set(value As PinStatesBase)
                _PinStates = New PinStates(value)
                _PinStates.SetupPort(Me)
                OnPropertyChanged()
            End Set
        End Property
        Protected Property _PinStates As PinStates

        ''' <summary> Gets the BaseStream for reading / writing data. </summary>
        ''' <value> The base stream for reading / writing data. </value>
        Public Overrides ReadOnly Property BaseStream As RxStream
            Get
                Return _BaseStream
            End Get
        End Property
        Protected Property _BaseStream As SerialStream

#End Region

#Region "Public Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
            _IOSerialPort = New Ports.SerialPort()
            Setup()
            _Settings.BindingPort = Me
            _BufferSettings.BindingPort = Me
            _PinStates.BindingPort = Me
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="portName"> Name of the port to use. </param>
        Public Sub New(portName As String)
            _IOSerialPort = New Ports.SerialPort(portName)
            Setup()
            _Settings.BindingPort = Me
            _BufferSettings.BindingPort = Me
            _PinStates.BindingPort = Me
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="portName"> Name of the port to use. </param>
        ''' <param name="settings"> The serial port settings. </param>
        Public Sub New(portName As String, settings As SettingsBase)
            _IOSerialPort = New Ports.SerialPort(portName)
            Setup()
            _Settings = settings
            _Settings.BindingPort = Me
            _BufferSettings.BindingPort = Me
            _PinStates.BindingPort = Me
        End Sub

#End Region

#Region "Functions"

        ''' <summary> Apply Settings to the serial port. </summary>
        Public Overrides Sub ApplySettings()
            _Settings.BindingPort = Me
            _BufferSettings.BindingPort = Me
            _PinStates.BindingPort = Me

            Dim tmpsetts As Settings = _Settings.Clone
            Dim tmpbuffsetts As BufferSettings = _BufferSettings.Clone

            _Settings.Import(tmpsetts)
            _BufferSettings.Import(tmpbuffsetts)
        End Sub

        ''' <summary> Opens a new serial port connection. </summary>
        Public Overrides Sub Open()
            ' Open Port
            IOSerialPort.Open()
            OnPropertyChanged("IsOpen")
        End Sub

        ''' <summary> Closes the port connection, and disposes the internal stream object. </summary>
        Public Overrides Sub Close()
            ' Close Port
            If IOSerialPort IsNot Nothing Then IOSerialPort.Close()
            OnPropertyChanged("IsOpen")
        End Sub

        ''' <summary> Common Setup. </summary>
        Private Sub Setup()
            _Settings = New Settings()
            _BufferSettings = New BufferSettings()
            _PinStates = New PinStates()
            _BaseStream = New SerialStream(Me)
        End Sub

#End Region

#Region "Shared Functions"

        ''' <summary> Get a Full List of Serial Port Names for the computer. </summary>
        ''' <returns> The available port names. </returns>
        Public Shared Function GetPortNames() As String()
            Return Ports.SerialPort.GetPortNames()
        End Function

#End Region

    End Class

End Namespace
