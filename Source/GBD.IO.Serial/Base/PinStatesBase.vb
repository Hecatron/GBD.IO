Imports System.ComponentModel
Imports System.IO.Ports
Imports System.Runtime.CompilerServices
Imports GBD.IO.Reactive.Base

Namespace Base

    ''' <summary> Base Class for Pin States on the serial port. </summary>
    Public MustInherit Class PinStatesBase
        Implements INotifyPropertyChanged

#Region "Properties"

        ''' <summary> OUTPUT: Gets or sets the break signal state. </summary>
        ''' <value> true if break state, false if not. </value>
        Public Overridable Property BreakState As Boolean
            Get
                Return _BreakState
            End Get
            Set(value As Boolean)
                _BreakState = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Overridable Property _BreakState As Boolean

        ''' <summary> INPUT: Gets the state of the Carrier Detect line for the port. </summary>
        ''' <value> true if CD holding, false if not. </value>
        Public MustOverride ReadOnly Property CDHolding As Boolean

        ''' <summary>
        ''' INPUT: Gets the state of the Clear-to-Send line Typically set via RTS on the other end of the
        ''' serial port.
        ''' </summary>
        ''' <value> true if cts holding, false if not. </value>
        Public MustOverride ReadOnly Property CTSHolding As Boolean

        ''' <summary>
        ''' INPUT: Gets the state of the Data Set Ready (DSR) signal Typically set via Dtr on the other
        ''' end of the serial port.
        ''' </summary>
        ''' <value> true if dsr holding, false if not. </value>
        Public MustOverride ReadOnly Property DsrHolding As Boolean

        ''' <summary>
        ''' OUTPUT: Gets or sets a value that enables the Data Terminal Ready (DTR) signal during serial
        ''' communication.
        ''' </summary>
        ''' <value> true if dtr enable, false if not. </value>
        Public Overridable Property DtrEnable As Boolean
            Get
                Return _DtrEnable
            End Get
            Set(value As Boolean)
                _DtrEnable = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Overridable Property _DtrEnable As Boolean

        ''' <summary>
        ''' OUTPUT: Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled
        ''' during serial communication.
        ''' </summary>
        ''' <value> true if RTS enable, false if not. </value>
        Public Overridable Property RtsEnable As Boolean
            Get
                Return _RtsEnable
            End Get
            Set(value As Boolean)
                _RtsEnable = value
                OnPropertyChanged()
            End Set
        End Property
        Protected Overridable Property _RtsEnable As Boolean

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

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
        End Sub

        ''' <summary> Clone Constructor. </summary>
        ''' <param name="inp"> The input state to clone from. </param>
        Public Sub New(inp As PinStatesBase)
            Import(inp)
        End Sub

#End Region

#Region "PinState Handlers"

        ''' <summary> Observable output. </summary>
        ''' <value> The rx data output. </value>
        Public ReadOnly Property RxPinState As IObservable(Of SerialPinChange)
            Get
                Return _RxPinState
            End Get
        End Property
        Protected Property _RxPinState As RxObservableBase(Of SerialPinChange)

        ''' <summary>
        ''' Event for Pin State change events
        ''' </summary>
        Public Event PinChangedEvent(sender As Object, e As System.IO.Ports.SerialPinChangedEventArgs)

        ''' <summary> Event for Pin State change events. </summary>
        ''' <param name="e"> Event information to send to registered event handlers. </param>
        Protected Sub OnPinChangedEvent(e As System.IO.Ports.SerialPinChangedEventArgs)
            RaiseEvent PinChangedEvent(Me, e)
        End Sub

#End Region

#Region "Functions"

        ''' <summary> Import pin states into this class. </summary>
        ''' <param name="importobj"> The states to import. </param>
        Public Sub Import(importobj As PinStatesBase)
            BreakState = importobj.BreakState
            DtrEnable = importobj.DtrEnable
            RtsEnable = importobj.RtsEnable
        End Sub

        ''' <summary> Setup Default Port Settings. </summary>
        Public Overridable Sub SetDefaults()
            BreakState = False
            DtrEnable = False
            RtsEnable = False
        End Sub

#End Region

    End Class

End Namespace
