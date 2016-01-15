Imports System.IO.Ports
Imports GBD.IO.Reactive.Base
Imports GBD.IO.Serial.Base

Namespace Net

    ''' <summary> Represents the state of pins on the port. </summary>
    Public Class PinStates
        Inherits PinStatesBase
        Implements IDisposable

#Region "Properties - Bound"

        ''' <summary> Binding to a Serial Port Implementation. </summary>
        ''' <value> The binding port. </value>
        Public Property BindingPort As SerialPort

        ''' <summary> If these settings are bound to a serial port implementation. </summary>
        ''' <value> true if this object is bound, false if not. </value>
        Public ReadOnly Property IsBound As Boolean
            Get
                If BindingPort Is Nothing Then Return False Else Return True
            End Get
        End Property

#End Region

#Region "Properties - Pin States"

        ''' <summary> OUTPUT: Gets or sets the break signal state. </summary>
        ''' <value> true if break state, false if not. </value>
        Protected Overrides Property _BreakState As Boolean
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.BreakState
                Return False
            End Get
            Set(value As Boolean)
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.BreakState = value
            End Set
        End Property

        ''' <summary> INPUT: Gets the state of the Carrier Detect line for the port. </summary>
        ''' <value> true if CD holding, false if not. </value>
        Public Overrides ReadOnly Property CDHolding As Boolean
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.CDHolding
                Return False
            End Get
        End Property

        ''' <summary>
        ''' INPUT: Gets the state of the Clear-to-Send line Typically set via RTS on the other end of the
        ''' serial port.
        ''' </summary>
        ''' <value> true if cts holding, false if not. </value>
        Public Overrides ReadOnly Property CTSHolding As Boolean
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.CtsHolding
                Return False
            End Get
        End Property

        ''' <summary>
        ''' INPUT: Gets the state of the Data Set Ready (DSR) signal Typically set via Dtr on the other
        ''' end of the serial port.
        ''' </summary>
        ''' <value> true if dsr holding, false if not. </value>
        Public Overrides ReadOnly Property DsrHolding As Boolean
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.DsrHolding
                Return False
            End Get
        End Property

        ''' <summary>
        ''' OUTPUT: Gets or sets a value that enables the Data Terminal Ready (DTR) signal during serial
        ''' communication.
        ''' </summary>
        ''' <value> true if dtr enable, false if not. </value>
        Protected Overrides Property _DtrEnable As Boolean
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.DtrEnable
                Return False
            End Get
            Set(value As Boolean)
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.DtrEnable = value
            End Set
        End Property

        ''' <summary>
        ''' OUTPUT: Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled
        ''' during serial communication.
        ''' </summary>
        ''' <value> true if RTS enable, false if not. </value>
        Protected Overrides Property _RtsEnable As Boolean
            Get
                If BindingPort IsNot Nothing Then Return BindingPort.IOSerialPort.RtsEnable
                Return False
            End Get
            Set(value As Boolean)
                If BindingPort IsNot Nothing Then BindingPort.IOSerialPort.RtsEnable = value
            End Set
        End Property

#End Region

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
            SetDefaults()
        End Sub

        ''' <summary> Binding Constructor. </summary>
        ''' <param name="port"> The port. </param>
        Public Sub New(port As SerialPort)
            SetupPort(port)
        End Sub

        ''' <summary> Clone Constructor. </summary>
        ''' <param name="inp"> The inp. </param>
        Public Sub New(inp As PinStatesBase)
            MyBase.New(inp)
        End Sub

        ''' <summary> Setup Port. </summary>
        ''' <param name="port"> The port. </param>
        Protected Friend Sub SetupPort(port As SerialPort)
            BindingPort = port
            ' Capture Pin State changes
            AddHandler BindingPort.IOSerialPort.PinChanged, AddressOf PinChangedHandler
            _RxPinState = New RxObservableBase(Of SerialPinChange)
        End Sub

#End Region

#Region "Destructors"

        ''' <summary> Destructor. </summary>
        Protected Overrides Sub Finalize()
            Dispose()
            MyBase.Finalize()
        End Sub

        ''' <summary> Dispose of Class. </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Stop Capture Pin State changes
            If BindingPort IsNot Nothing Then
                RemoveHandler BindingPort.IOSerialPort.PinChanged, AddressOf PinChangedHandler
            End If
            GC.SuppressFinalize(Me)
        End Sub
#End Region

#Region "Pin Changed Handler"

        ''' <summary> Handle pin changed event. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Serial pin changed event information. </param>
        Protected Friend Sub PinChangedHandler(sender As Object, e As System.IO.Ports.SerialPinChangedEventArgs)
            Select Case e.EventType
                Case SerialPinChange.Break
                    OnPropertyChanged("BreakState")
                Case SerialPinChange.CDChanged
                    OnPropertyChanged("CDHolding")
                Case SerialPinChange.CtsChanged
                    OnPropertyChanged("CTSHolding")
                Case SerialPinChange.DsrChanged
                    OnPropertyChanged("DsrHolding")
                    ' TODO implement a property for this pin, we can't read it directly with the inbuild .Net implementation
                    'Case SerialPinChange.Ring
                    '    OnPropertyChanged("Ring")
            End Select
            OnPinChangedEvent(e)
            If _RxPinState.ObserverClient IsNot Nothing Then _RxPinState.ObserverClient.OnNext(e.EventType)
        End Sub

#End Region

    End Class

End Namespace
