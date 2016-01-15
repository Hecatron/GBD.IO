Imports System.Reactive
Imports System.Reactive.Disposables
Imports Common.Logging

Namespace Base

    ''' <summary>
    ''' Base class for Observables uses Rx ObservableBase which is the same as the Observable.Create
    ''' method factory call inheriting from this class is just a convenience, note we only support a
    ''' single subscription at a time here although you can distribute to many via .Publish /
    ''' .Multicast.
    ''' </summary>
    Public Class RxObservableBase(Of T)
        Inherits ObservableBase(Of T)
        Implements IDisposable

#Region "Properties"

        ''' <summary> Log Output. </summary>
        ''' <value> NLog Instance. </value>
        Private Property Logger As ILog = LogManager.GetLogger(Me.GetType)

        ''' <summary> Outbound Observer / Client we throw messages out to. </summary>
        ''' <value> The observer client. </value>
        Public ReadOnly Property ObserverClient As IObserver(Of T)
            Get
                Return _observerClient
            End Get
        End Property
        Protected Property _observerClient As IObserver(Of T)

        ''' <summary> If disposed. </summary>
        ''' <value> true if disposed, false if not. </value>
        Protected Property Disposed As Boolean = False

        ''' <summary> If this class has been disposed. </summary>
        ''' <value> If this class has been disposed. </value>
        Public ReadOnly Property IsDisposed
            Get
                Return Disposed
            End Get
        End Property

#End Region

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary> Default Constructor with subscription. </summary>
        ''' <param name="observ"> An observer to subscribe during creation. </param>
        Public Sub New(observ As IObserver(Of T))
            MyBase.New()
            Subscribe(observ)
        End Sub

#End Region

#Region "Destructors"

        ''' <summary> Disposal. </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary> Disposal. </summary>
        ''' <param name="disposing">    true to release both managed and unmanaged resources; false to
        '''                             release only unmanaged resources. </param>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Disposed Then
                If disposing Then
                    ' Free other state (managed objects).
                    If _observerClient IsNot Nothing Then _observerClient.OnCompleted()
                    _observerClient = Nothing
                End If
                ' Free your own state (unmanaged objects), Set large fields to null.
                Disposed = True
            End If
        End Sub

        ''' <summary> Destructor. </summary>
        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub

#End Region

#Region "Function - Observable callbacks"

        ''' <summary> Subscribe. </summary>
        ''' <param name="observ"> The observer to subscribe. </param>
        ''' <returns> An IDisposable to unsubscribe. </returns>
        Protected Overrides Function SubscribeCore(observ As IObserver(Of T)) As IDisposable
            _observerClient = observ
            Dim disp = Disposable.Create( _
                Sub()
                    _observerClient = Nothing
                End Sub)
            Logger.Debug("Observer Subscribed")
            Return disp
        End Function

#End Region

    End Class

End Namespace
