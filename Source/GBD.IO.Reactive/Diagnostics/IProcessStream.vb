Imports GBD.IO.Reactive.Stream

Namespace Diagnostics

    ''' <summary> Interface for a collection of streams exported by a process. </summary>
    Public Interface IProcessStream

        ''' <summary> Gets the standard output stream. </summary>
        ''' <returns> The standard output stream. </returns>
        Function GetStdOutput() As RxStream

        ''' <summary> Gets the standard input stream. </summary>
        ''' <returns> The standard input stream. </returns>
        Function GetStdInput() As RxStream

        ''' <summary> Gets the standard error stream. </summary>
        ''' <returns> The standard error stream. </returns>
        Function GetStdError() As RxStream

    End Interface

End Namespace
