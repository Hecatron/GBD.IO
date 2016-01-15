'
' K8055Exception.vb
'
' Author:       Richard Westwell
' Date:         06/03/2013
'
' K8055 Exception

''' <summary>
''' Exception Generated from the Bus Pirate Device
''' </summary>
Public Class K8055Exception
    Inherits Exception

#Region "Public Constructors"

    ''' <summary>
    ''' Default Constructor
    ''' </summary>
    <DebuggerStepThrough()> _
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Default Constructor, text string
    ''' </summary>
    <DebuggerStepThrough()> _
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Default Constructor, text string and inner exception
    ''' </summary>
    <DebuggerStepThrough()> _
    Public Sub New(message As String, inner As Exception)
        MyBase.New(message, inner)
    End Sub

#End Region

End Class
