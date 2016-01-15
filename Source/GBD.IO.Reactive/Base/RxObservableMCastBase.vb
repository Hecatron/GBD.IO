Imports System.Reactive
Imports System.Reactive.Disposables
Imports Common.Logging

Namespace Base

    ''' <summary>
    ''' Base class for Observables uses Rx ObservableBase which is the same as the Observable.Create
    ''' method factory call inheriting from this class is just a convenience, this is a variant that
    ''' supports multiple subscribers.
    ''' </summary>
    Public Class RxObservableMCastBase(Of T)
        Inherits ObservableBase(Of T)
        Implements IDisposable

#Region "Properties"

        ''' <summary> Log Output. </summary>
        ''' <value> NLog Instance. </value>
        Private Property Logger As ILog = LogManager.GetLogger(Me.GetType)

        ''' <summary> List of Outbound Observers / Clients we throw messages out to. </summary>
        ''' <value> The list of observers. </value>
        Public ReadOnly Property ObserverClient As IReadOnlyList(Of IObserver(Of T))
            Get
                Return _observerClient.AsReadOnly
            End Get
        End Property
        Protected Property _observerClient As List(Of IObserver(Of T))

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
            _observerClient = New List(Of IObserver(Of T))
        End Sub

        ''' <summary> Default Constructor with subscription. </summary>
        ''' <param name="observ"> An observer to subscribe during creation. </param>
        Public Sub New(observ As IObserver(Of T))
            MyBase.New()
            _observerClient = New List(Of IObserver(Of T))
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
                    If _observerClient IsNot Nothing Then OnCompletedCore()
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

#Region "Functions - Observable callbacks"

        ''' <summary> Subscribe. </summary>
        ''' <param name="observ"> The observer to subscribe. </param>
        ''' <returns> An IDisposable to unsubscribe. </returns>
        Protected Overrides Function SubscribeCore(observ As IObserver(Of T)) As IDisposable
            SyncLock _observerClient
                _observerClient.Add(observ)
            End SyncLock
            Dim disp = Disposable.Create( _
                Sub()
                    SyncLock _observerClient
                        _observerClient.Remove(observ)
                    End SyncLock
                End Sub)
            Logger.Debug("Observer Subscribed")
            Return disp
        End Function

        ''' <summary> On Next Callback. </summary>
        ''' <param name="value"> The value to pass on. </param>
        Protected Overridable Sub OnNextCore(value As T)
            SyncLock _observerClient
                For Each obitem In _observerClient
                    obitem.OnNext(value)
                Next
            End SyncLock
        End Sub

        ''' <summary> On Error Callback. </summary>
        ''' <param name="error"> an error / exception to pass on. </param>
        Protected Overridable Sub OnErrorCore([error] As Exception)
            SyncLock _observerClient
                For Each obitem In _observerClient
                    obitem.OnError([error])
                Next
                _observerClient.Clear()
            End SyncLock
        End Sub

        ''' <summary> On Completed Callback. </summary>
        Protected Overridable Sub OnCompletedCore()
            SyncLock _observerClient
                For Each obitem In _observerClient
                    obitem.OnCompleted()
                Next
                _observerClient.Clear()
            End SyncLock
        End Sub

#End Region

    End Class

End Namespace
