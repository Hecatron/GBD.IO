
Namespace Base

    ''' <summary> Exception class for errors specific to the GBD.IO.Serial library. </summary>
    Public Class SerialException
        Inherits Exception

#Region "Constructors"

        ''' <summary> Default Constructor, text string. </summary>
        ''' <param name="message"> The exception message. </param>
        <DebuggerStepThrough()> _
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        ''' <summary> Default Constructor, text string and inner exception. </summary>
        ''' <param name="message"> The exception message. </param>
        ''' <param name="inner">   The inner exception. </param>
        <DebuggerStepThrough()> _
        Public Sub New(message As String, inner As Exception)
            MyBase.New(message, inner)
        End Sub

#End Region

    End Class

End Namespace
