Imports GBD.IO.Reactive.Stream

' TODO handle additional values for state and events
' TODO implement INotifyPropertyChanged for state and StartInfo
' TODO after the process terminates externally we need to stop the read process task via changing the state

''' <summary>
''' Wrapper around the system process / exe, to stream std out / std in / std err via reactive
''' extensions.
''' </summary>
Public Class RxProcess
    Implements IProcessStream

#Region "Types"

    ''' <summary> Represents the current state of the process. </summary>
    Public Enum ProcessState
        Stopped = 0
        Started = 1
        Completed = 2
        Errored = 3
    End Enum

#End Region

#Region "Properties - Process"

    ''' <summary> Underlying System Diagnostics Process. </summary>
    ''' <value> Underlying System Diagnostics Process. </value>
    Public ReadOnly Property Process As Process
        Get
            Return _Process
        End Get
    End Property
    Protected Property _Process As Process

    ''' <summary> Gets or sets the properties to pass to the Start method of the Process. </summary>
    ''' <value> Gets or sets the properties to pass to the Start method of the Process. </value>
    Public Property StartInfo As ProcessStartInfo
        Get
            Return _Process.StartInfo
        End Get
        Set(value As ProcessStartInfo)
            _Process.StartInfo = value
        End Set
    End Property

    ''' <summary> Gets the state of the process. </summary>
    ''' <value> Gets the state of the process. </value>
    Public ReadOnly Property State As ProcessState
        Get
            Return _State
        End Get
    End Property
    Protected Property _State As ProcessState

#End Region

#Region "Properties - Stream"

    ''' <summary> Gets the standard output for the process. </summary>
    ''' <value> The standard output as an Observable. </value>
    Public ReadOnly Property RxStdOut As RxStream
        Get
            Return _RxStdOut
        End Get
    End Property
    Protected Property _RxStdOut As RxStream

    ''' <summary> Gets the standard error output for the process. </summary>
    ''' <value> The standard error output as an observable. </value>
    Public ReadOnly Property RxStdErr As RxStream
        Get
            Return _RxStdErr
        End Get
    End Property
    Protected Property _RxStdErr As RxStream

    ''' <summary> Gets the standard input for the process. </summary>
    ''' <value> The standard input as an Observer. </value>
    Public ReadOnly Property RxStdIn As RxStream
        Get
            Return _RxStdIn
        End Get
    End Property
    Protected Property _RxStdIn As RxStream

#End Region

#Region "Interface - Stream"

    ''' <summary> Gets the standard output stream. </summary>
    ''' <returns> The standard output stream. </returns>
    Public Function GetStdOutput() As RxStream Implements IProcessStream.GetStdOutput
        Return _RxStdOut
    End Function

    ''' <summary> Gets the standard input stream. </summary>
    ''' <returns> The standard input stream. </returns>
    Public Function GetStdInput() As RxStream Implements IProcessStream.GetStdInput
        Return _RxStdIn
    End Function

    ''' <summary> Gets the standard error stream. </summary>
    ''' <returns> The standard error stream. </returns>
    Public Function GetStdError() As RxStream Implements IProcessStream.GetStdError
        Return _RxStdErr
    End Function

#End Region

#Region "Constructors"

    ''' <summary> Default Constructor. </summary>
    Public Sub New()
        _Process = New Process
        CommonSetup()
    End Sub

    ''' <summary> Default Constructor. </summary>
    ''' <param name="startinfo"> The startinfo to use with the process. </param>
    Public Sub New(startinfo As ProcessStartInfo)
        Me.StartInfo = startinfo
        _Process = New Process
        CommonSetup()
    End Sub

    ''' <summary> Default Constructor. </summary>
    ''' <param name="exepath">    The path to the executable. </param>
    ''' <param name="args">       The arguments to pass to the executable. </param>
    ''' <param name="workingdir"> The working directory for the executable. </param>
    Public Sub New(exepath As String, args As String, workingdir As String)
        _Process = New Process
        StartInfo.FileName = exepath
        StartInfo.Arguments = args
        StartInfo.WorkingDirectory = workingdir
        CommonSetup()
    End Sub

    ''' <summary> Common setup between constructors. </summary>
    Private Sub CommonSetup()
        _State = ProcessState.Stopped

        _RxStdOut = New RxStream(
                Function()
                    Return _Process.StandardOutput.BaseStream
                End Function,
                Function()
                    If _State = ProcessState.Started AndAlso (_Process.HasExited = False) Then Return True
                    Return False
                End Function)
        _RxStdErr = New RxStream(
                Function()
                    Return _Process.StandardError.BaseStream
                End Function,
                Function()
                    If _State = ProcessState.Started AndAlso (_Process.HasExited = False) Then Return True
                    Return False
                End Function)
        _RxStdIn = New RxStream(
                Function()
                    Return _Process.StandardInput.BaseStream
                End Function,
                Function()
                    If _State = ProcessState.Started AndAlso (_Process.HasExited = False) Then Return True
                    Return False
                End Function)

        AddHandler _Process.Exited, AddressOf ExitHandler
    End Sub

#End Region

#Region "Functions - Handlers"

    ''' <summary> Handler, called when the process exits. </summary>
    ''' <param name="sender"> Source of the event. </param>
    ''' <param name="e">      Event information. </param>
    Protected Sub ExitHandler(sender As Object, e As EventArgs)
        _State = ProcessState.Stopped
    End Sub

#End Region

#Region "Functions - Start / Stop"

    ''' <summary> Starts the process. </summary>
    Public Sub Start()
        ' In order to observe / interact with the standard input / output we need to set the following
        StartInfo.RedirectStandardOutput = True
        StartInfo.RedirectStandardError = True
        StartInfo.RedirectStandardInput = True
        StartInfo.UseShellExecute = False
        _Process.Start()
        _State = ProcessState.Started
    End Sub

    ''' <summary> Closes this process. </summary>
    Public Sub Close()
        _State = ProcessState.Stopped
        If _Process IsNot Nothing Then _Process.Close()
    End Sub

#End Region

End Class
