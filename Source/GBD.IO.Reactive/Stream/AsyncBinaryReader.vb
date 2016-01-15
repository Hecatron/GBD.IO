Imports System.IO
Imports System.Text

' TODO

Namespace Stream

    ''' <summary> Asynchronous binary reader. </summary>
    Public Class AsyncBinaryReader
        Inherits BinaryReader

#Region "Properties"

        ''' <summary> Underlying Stream used for Reading data. </summary>
        ''' <value> The read stream. </value>
        Public Overrides ReadOnly Property Basestream As System.IO.Stream
            Get
                If StreamFunc IsNot Nothing Then Return StreamFunc.Invoke
                Return MyBase.BaseStream
            End Get
        End Property

        ''' <summary> Delegate Function for referencing the read stream. </summary>
        ''' <value> Delegate Function for referencing the read stream. </value>
        Protected Overridable Property StreamFunc As Func(Of System.IO.Stream)

#End Region

#Region "Constructors"

        ''' <summary> Default Constructor. </summary>
        ''' <param name="input"> The input stream. </param>
        Public Sub New(input As System.IO.Stream)
            MyBase.New(input)
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="input">    The input stream. </param>
        ''' <param name="encoding"> The encoding. </param>
        Public Sub New(input As System.IO.Stream, encoding As Encoding)
            MyBase.New(input, encoding)
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="input">     The input stream. </param>
        ''' <param name="encoding">  The encoding. </param>
        ''' <param name="leaveOpen"> true to leave open. </param>
        Public Sub New(input As System.IO.Stream, encoding As Encoding, leaveOpen As Boolean)
            MyBase.New(input, encoding, leaveOpen)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class based on the specified stream and
        ''' using UTF-8 encoding.
        ''' </summary>
        ''' <param name="streamfunc"> Delegate Function for writing the Stream. </param>
        Public Sub New(streamfunc As Func(Of System.IO.Stream))
            MyBase.New(Nothing) ' TODO
            'MyBase.New(Basestream)
            'Me.StreamFunc = streamfunc
        End Sub

#End Region

    End Class

End Namespace
