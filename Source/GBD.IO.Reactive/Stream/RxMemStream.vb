Imports System.Text
Imports GBD.IO.Reactive.Base

Namespace Stream

    ''' <summary> Acts as a observable memory stream for buffering and storing data. </summary>
    Public Class RxMemStream
        Inherits System.IO.MemoryStream
        Implements IObservable(Of ArraySegment(Of Byte))
        Implements IObserver(Of Byte())

#Region "Properties"

        ''' <summary> An Observable used for monitoring data written to the stream. </summary>
        ''' <value> An Observable used for monitoring data written to the stream. </value>
        Protected Property _ObWrite As New RxObservableBase(Of ArraySegment(Of Byte))

        ''' <summary> Gets the byte buffer. </summary>
        ''' <value> The byte buffer. </value>
        Public ReadOnly Property ByteBuffer As Byte()
            Get
                Return GetBuffer()
            End Get
        End Property

        ''' <summary> Gets a value indicating whether to flush on write. </summary>
        ''' <value> true if automatic flush, false if not. </value>
        Public Property AutoFlush As Boolean = True

#End Region

#Region "Constructors"

        ''' <summary> Default constructor. </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary> Default constructor. </summary>
        ''' <param name="buffer"> The buffer. </param>
        Public Sub New(buffer() As Byte)
            MyBase.New()
            MyBase.Write(buffer, 0, buffer.Length)
        End Sub

        ''' <summary> Default constructor. </summary>
        ''' <param name="capacity"> The capacity. </param>
        Public Sub New(capacity As Integer)
            MyBase.New(capacity)
        End Sub

#End Region

#Region "Functions - IObservable CallBacks - Reading"

        ''' <summary> Subscribes the given observer. </summary>
        ''' <param name="observer"> The observer. </param>
        ''' <returns> An IDisposable. </returns>
        Public Function Subscribe(observer As IObserver(Of ArraySegment(Of Byte))) As IDisposable Implements IObservable(Of ArraySegment(Of Byte)).Subscribe
            Dim ret As IDisposable = _ObWrite.Subscribe(observer)
            If ByteBuffer.Length > 0 Then
                Dim segment As New ArraySegment(Of Byte)(ByteBuffer, 0, ByteBuffer.Length)
                _ObWrite.ObserverClient.OnNext(segment)
            End If
            Return ret
        End Function

#End Region

#Region "Functions - IObserver CallBacks - Writing"

        ''' <summary> Outbound Write Traffic. </summary>
        ''' <param name="value"> The data to write to the stream. </param>
        Public Sub OnNext(value As Byte()) Implements IObserver(Of Byte()).OnNext
            Write(value, 0, value.Length)
        End Sub

        ''' <summary>
        ''' Notifies the observer that the provider has experienced an error condition.
        ''' </summary>
        ''' <param name="error"> An object that provides additional information about the error. </param>
        Public Sub OnError(ByVal [error] As Exception) Implements IObserver(Of Byte()).OnError
            ' Pass on Errors to any subscribed clients, but ignore for the stream
            If _ObWrite.ObserverClient IsNot Nothing Then _ObWrite.ObserverClient.OnError([error])
        End Sub

        ''' <summary>
        ''' Notifies the observer that the provider has finished sending push-based notifications.
        ''' </summary>
        Public Sub OnCompleted() Implements IObserver(Of Byte()).OnCompleted
            ' Pass on Completions to any subscribed clients, but ignore for the stream
            If _ObWrite.ObserverClient IsNot Nothing Then _ObWrite.ObserverClient.OnCompleted()
        End Sub


#End Region

#Region "Functions - Write Overrides"

        ''' <summary> Writes a byte to the current stream at the current position. </summary>
        ''' <param name="value"> The byte to write. </param>
        Public Overrides Sub WriteByte(value As Byte)
            MyBase.WriteByte(value)
            Dim segment As New ArraySegment(Of Byte)(GetBuffer, GetBuffer.Length, 1)
            _ObWrite.ObserverClient.OnNext(segment)
        End Sub

        ''' <summary>
        ''' Writes a sequence of bytes to the current stream and advances the current position within
        ''' this stream by the number of bytes written.
        ''' </summary>
        ''' <param name="buffer">   An array of bytes. This method copies <paramref name="count" /> bytes
        '''                         from <paramref name="buffer" /> to the current stream. </param>
        ''' <param name="offset">   The zero-based byte offset in <paramref name="buffer" /> at which to
        '''                         begin copying bytes to the current stream. </param>
        ''' <param name="count">  The number of bytes to be written to the current stream. </param>
        Public Overrides Sub Write(buffer As Byte(), offset As Integer, count As Integer)
            MyBase.Write(buffer, offset, count)
            If _ObWrite.ObserverClient IsNot Nothing Then
                Dim segment As New ArraySegment(Of Byte)(buffer, offset, count)
                _ObWrite.ObserverClient.OnNext(segment)
            End If
            If AutoFlush Then Flush()
        End Sub

        ''' <summary> Begins a async write. </summary>
        ''' <param name="buffer">   The buffer to write data from. </param>
        ''' <param name="offset">   The byte offset in <paramref name="buffer" /> from which to begin
        '''                         writing. </param>
        ''' <param name="count">    The maximum number of bytes to write. </param>
        ''' <param name="callback"> An optional asynchronous callback, to be called when the write is
        '''                         complete. </param>
        ''' <param name="state">    A user-provided object that distinguishes this particular
        '''                         asynchronous write request from other requests. </param>
        ''' <returns> An IAsyncResult. </returns>
        Public Overrides Function BeginWrite(buffer() As Byte, offset As Integer, count As Integer, callback As AsyncCallback, state As Object) As IAsyncResult
            Dim result = MyBase.BeginWrite(buffer, offset, count, callback, state)
            If _ObWrite.ObserverClient IsNot Nothing Then
                Dim segment As New ArraySegment(Of Byte)(buffer, offset, count)
                _ObWrite.ObserverClient.OnNext(segment)
            End If
            Return result
        End Function

        ''' <summary> Override the main ToString method, assume ASCII encoding. </summary>
        ''' <returns> A String that represents this object. </returns>
        Public Overrides Function ToString() As String
            Dim tmpstr As String = Encoding.ASCII.GetString(ByteBuffer)
            Return tmpstr
        End Function

        ''' <summary>
        ''' Convert Buffer to String for different encoding types
        ''' </summary>
        Public Overloads Function ToString(encode As Encoding) As String
            Dim tmpstr As String = encode.GetString(ByteBuffer)
            Return tmpstr
        End Function

        ''' <summary> Clears the memory stream to it's initial state. </summary>
        Public Sub Clear()
            Dim buffer As Byte() = GetBuffer()
            Array.Clear(buffer, 0, buffer.Length)
            Position = 0
            SetLength(0)
        End Sub

#End Region

#Region "Functions - Comparison"

        ''' <summary> Tests if this Byte() is considered equal to the underlying buffer. </summary>
        ''' <param name="destarray"> The byte() to compare to this object. </param>
        ''' <returns> true if the objects are considered equal, false if they are not. </returns>
        Public Overloads Function Equals(destarray As Byte()) As Boolean
            Return ByteBuffer.SequenceEqual(destarray)
        End Function

        ''' <summary> Tests if this Rx memory stream is considered equal to the underlying buffer. </summary>
        ''' <param name="stream"> The Rx memory stream to compare to this object. </param>
        ''' <returns> true if the objects are considered equal, false if they are not. </returns>
        Public Overloads Function Equals(stream As RxMemStream) As Boolean
            Return ByteBuffer.SequenceEqual(stream.ByteBuffer)
        End Function

        ''' <summary> Tests if this ArraySegment is considered equal to the underlying buffer. </summary>
        ''' <param name="arrseg"> The array segment( of byte) to compare to this object. </param>
        ''' <returns> true if the objects are considered equal, false if they are not. </returns>
        Public Overloads Function Equals(arrseg As ArraySegment(Of Byte)) As Boolean
            Return ByteBuffer.SequenceEqual(arrseg.Array.Skip(arrseg.Offset))
        End Function

        ''' <summary> Starts with byte array. </summary>
        ''' <param name="destarray"> The byte() to compare to this object. </param>
        ''' <returns> true if it succeeds, false if it fails. </returns>
        Public Function StartsWith(destarray As Byte()) As Boolean
            If ByteBuffer.Length < destarray.Length Then Return False
            Return ByteBuffer.Take(destarray.Length).SequenceEqual(destarray)
        End Function

        ''' <summary> Starts with byte array from RxMemStream. </summary>
        ''' <param name="stream"> The byte() to compare to this object. </param>
        ''' <returns> true if it succeeds, false if it fails. </returns>
        Public Function StartsWith(stream As RxMemStream) As Boolean
            If ByteBuffer.Length < stream.ByteBuffer.Length Then Return False
            Return ByteBuffer.Take(stream.ByteBuffer.Length).SequenceEqual(stream.ByteBuffer)
        End Function

        ''' <summary> Starts with byte array from ArraySegment. </summary>
        ''' <param name="arrseg"> The byte() to compare to this object. </param>
        ''' <returns> true if it succeeds, false if it fails. </returns>
        Public Function StartsWith(arrseg As ArraySegment(Of Byte)) As Boolean
            If ByteBuffer.Length < arrseg.Count Then Return False
            Return ByteBuffer.Take(arrseg.Count).SequenceEqual(arrseg.Array.Skip(arrseg.Offset))
        End Function

#End Region

    End Class

End Namespace
