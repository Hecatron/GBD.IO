Imports System.Reactive.Disposables
Imports System.Threading

Namespace Base

    ''' <summary>
    ''' Base class for Observables that require a Task to be run during subscription to read data
    ''' from a source like a stream.
    ''' </summary>
    Public Class RxObservableTaskBase(Of T)
        Inherits RxObservableBase(Of T)

#Region "Properties"

        ''' <summary> The Task Initiated as part of the read process. </summary>
        ''' <value> The read task. </value>
        Public ReadOnly Property ReadTask As Task
            Get
                Return _ReadTask
            End Get
        End Property

        Protected Property _ReadTask As Task

        ''' <summary> Token Source for Cancelation of the read task. </summary>
        ''' <value> Token Source for Cancelation of the read task. </value>
        Public ReadOnly Property TokenSource As CancellationTokenSource
            Get
                Return _TokenSource
            End Get
        End Property
        Protected Property _TokenSource As CancellationTokenSource

        ''' <summary> Delegate Function for writing the Stream. </summary>
        ''' <value> Delegate Function for writing the Stream. </value>
        Protected Overridable Property TaskReadDataFunc As Action(Of IObserver(Of T))

#End Region

#Region "Constructors"

        ''' <summary> Default constructor. </summary>
        Public Sub New()
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="readDataAction"> The action to perform when reading data. </param>
        Public Sub New(readDataAction As Action(Of IObserver(Of T)))
            TaskReadDataFunc = readDataAction
        End Sub

#End Region

#Region "Destructors"

        ''' <summary> Disposal. </summary>
        ''' <param name="disposing">    true to release both managed and unmanaged resources; false to
        '''                             release only unmanaged resources. </param>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If _TokenSource IsNot Nothing Then _TokenSource.Cancel()
            MyBase.Dispose(disposing)
        End Sub

#End Region

#Region "Function - Observable callbacks"

        ''' <summary> Subscribe. </summary>
        ''' <param name="observ"> The observer to subscribe. </param>
        ''' <returns> An IDisposable to unsubscribe. </returns>
        Protected Overrides Function SubscribeCore(observ As IObserver(Of T)) As IDisposable

            ' Setup the subscription
            _observerClient = observ

            SyncLock _observerClient

                ' Get rid of the old subscription
                If _TokenSource IsNot Nothing Then _TokenSource.Cancel()

                ' Start up the read of serial data
                _TokenSource = New CancellationTokenSource
                _ReadTask = Task.Factory.StartNew(Sub() TaskReadDataFunc.Invoke(observ))

                ' Return a Disposable
                Return Disposable.Create( _
                    Sub()
                        SyncLock _observerClient
                            _TokenSource.Cancel()
                            _observerClient = Nothing
                        End SyncLock
                    End Sub)

            End SyncLock

        End Function

#End Region

    End Class

End Namespace
