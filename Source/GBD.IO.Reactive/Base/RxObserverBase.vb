Imports System.Reactive
Imports System.Threading
Imports Common.Logging

Namespace Base

    ''' <summary>
    ''' Base Class for Observers. This is just a convenience class where inheritance is prefered.
    ''' </summary>
    Public Class RxObserverBase(Of T)
        Inherits ObserverBase(Of T)

#Region "Properties"

        ''' <summary> Current State of the Observer. </summary>
        ''' <value> Current State of the Observer. </value>
        Public Property State As RunningState

        ''' <summary> Log Output. </summary>
        ''' <value> NLog Instance. </value>
        Private Property Logger As ILog = LogManager.GetLogger(Me.GetType)

#End Region

#Region "Types"

        ''' <summary> State of the Observer. </summary>
        Public Enum RunningState
            Active = 1
            Stopped = 2
            Errored = 3
        End Enum

#End Region

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        Public Sub New()
            Interlocked.Exchange(State, RunningState.Active)
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="observ"> The Observable to subscribe to. </param>
        Public Sub New(observ As IObservable(Of T))
            Interlocked.Exchange(State, RunningState.Active)
            observ.Subscribe(Me)
        End Sub

#End Region

#Region "Functions - Observer callbacks"

        ''' <summary> Sequence Next Value. </summary>
        ''' <param name="value"> Next element in the sequence. </param>
        Protected Overrides Sub OnNextCore(value As T)
            Logger.Debug("Next Value: " & value.ToString)
        End Sub

        ''' <summary> Sequence Errored. </summary>
        ''' <param name="error"> The error that has occurred. </param>
        Protected Overrides Sub OnErrorCore([error] As Exception)
            Logger.Debug("Inbound Error: " & [error].ToString)
            Interlocked.Exchange(State, RunningState.Errored)
        End Sub

        ''' <summary> Sequence Complted / Closed. </summary>
        Protected Overrides Sub OnCompletedCore()
            Logger.Debug("Inbound Complete")
            Interlocked.Exchange(State, RunningState.Stopped)
        End Sub

#End Region

    End Class

End Namespace
