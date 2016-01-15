Imports System.IO

Namespace Base

    ''' <summary>
    ''' A stream wrapper class, desgined to wrap read and writes to a underlying stream class.
    ''' </summary>
    Public Class StreamWrapperBase
        Inherits System.IO.Stream

#Region "Properties"

        ''' <summary> Delegate Function for reading / writing the Stream. </summary>
        ''' <value> Delegate Function for reading / writing the Stream. </value>
        Protected Overridable Property StreamFunc As Func(Of System.IO.Stream)

        ''' <summary> Gets the base stream that this stream references. </summary>
        ''' <value> Gets the base stream that this stream references. </value>
        Public ReadOnly Property BaseStream As System.IO.Stream
            Get
                If StreamFunc IsNot Nothing Then Return StreamFunc.Invoke()
                Return _BaseStream
            End Get
        End Property

        Protected Property _BaseStream As System.IO.Stream

        ''' <summary> Gets a value indicating whether to flush on write. </summary>
        ''' <value> true if automatic flush, false if not. </value>
        Public Property AutoFlush As Boolean

#End Region

#Region "Properties - Overrides"

        ''' <summary> Gets a value indicating whether the current stream supports reading. </summary>
        ''' <value> true if the stream supports reading; otherwise, false. </value>
        Public Overrides ReadOnly Property CanRead() As Boolean
            Get
                Return BaseStream.CanRead
            End Get
        End Property

        ''' <summary> Gets a value indicating whether the current stream supports seeking. </summary>
        ''' <value> true if the stream supports seeking; otherwise, false. </value>
        Public Overrides ReadOnly Property CanSeek() As Boolean
            Get
                Return BaseStream.CanSeek
            End Get
        End Property

        ''' <summary> Gets a value indicating whether the current stream supports writing. </summary>
        ''' <value> true if the stream supports writing; otherwise, false. </value>
        Public Overrides ReadOnly Property CanWrite() As Boolean
            Get
                Return BaseStream.CanWrite
            End Get
        End Property

        ''' <summary> Gets the length in bytes of the stream. </summary>
        ''' <value> A long value representing the length of the stream in bytes. </value>
        Public Overrides ReadOnly Property Length() As Long
            Get
                Return BaseStream.Length
            End Get
        End Property

        ''' <summary> Gets or sets the position within the current stream. </summary>
        ''' <value> The current position within the stream. </value>
        Public Overrides Property Position() As Long
            Get
                Return BaseStream.Position
            End Get
            Set(value As Long)
                BaseStream.Position = value
            End Set
        End Property

#End Region

#Region "Public Constructors"

        ''' <summary> Default Constructor. </summary>
        ''' <param name="stream"> The stream to reference / intercept. </param>
        Public Sub New(stream As System.IO.Stream)
            _BaseStream = stream
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="streamfunc"> Delegate Function for reading the Stream. </param>
        Public Sub New(streamfunc As Func(Of System.IO.Stream))
            Me.StreamFunc = streamfunc
        End Sub

#End Region

#Region "Function - Overrides"

        ''' <summary>
        ''' Clears all buffers for this stream and causes any buffered data to be written to the
        ''' underlying device.
        ''' </summary>
        Public Overrides Sub Flush()
            BaseStream.Flush()
        End Sub

        ''' <summary> Sets the position within the current stream. </summary>
        ''' <param name="offset">   A byte offset relative to the <paramref name="origin" /> parameter. </param>
        ''' <param name="origin">   A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the
        '''                         reference point used to obtain the new position. </param>
        ''' <returns> The new position within the current stream. </returns>
        Public Overrides Function Seek(offset As Long, origin As SeekOrigin) As Long
            Return BaseStream.Seek(offset, origin)
        End Function

        ''' <summary> Sets the length of the current stream. </summary>
        ''' <param name="value"> The desired length of the current stream in bytes. </param>
        Public Overrides Sub SetLength(value As Long)
            BaseStream.SetLength(value)
        End Sub

        ''' <summary>
        ''' Reads a sequence of bytes from the current stream and advances the position within the stream
        ''' by the number of bytes read.
        ''' </summary>
        ''' <param name="buffer">   An array of bytes. When this method returns, the buffer contains the
        '''                         specified byte array with the values between
        '''                         <paramref name="offset" /> and (<paramref name="offset" /> +
        '''                         <paramref name="count" /> - 1) replaced by the bytes read from the
        '''                         current source. </param>
        ''' <param name="offset">   The zero-based byte offset in <paramref name="buffer" /> at which to
        '''                         begin storing the data read from the current stream. </param>
        ''' <param name="count">  The maximum number of bytes to be read from the current stream. </param>
        ''' <returns>
        ''' The total number of bytes read into the buffer. This can be less than the number of bytes
        ''' requested if that many bytes are not currently available, or zero (0) if the end of the
        ''' stream has been reached.
        ''' </returns>
        Public Overrides Function Read(buffer As Byte(), offset As Integer, count As Integer) As Integer
            Return BaseStream.Read(buffer, offset, count)
        End Function

        ''' <summary> Begins a async read. </summary>
        ''' <param name="buffer">   The buffer to read the data into. </param>
        ''' <param name="offset">   The byte offset in <paramref name="buffer" /> at which to begin
        '''                         writing data read from the stream. </param>
        ''' <param name="count">    The maximum number of bytes to read. </param>
        ''' <param name="callback"> An optional asynchronous callback, to be called when the read is
        '''                         complete. </param>
        ''' <param name="state">    A user-provided object that distinguishes this particular
        '''                         asynchronous read request from other requests. </param>
        ''' <returns> An IAsyncResult. </returns>
        Public Overrides Function BeginRead(buffer() As Byte, offset As Integer, count As Integer, callback As AsyncCallback, state As Object) As IAsyncResult
            Return BaseStream.BeginRead(buffer, offset, count, callback, state)
        End Function

        ''' <summary>
        ''' Writes a sequence of bytes to the current stream and
        ''' advances the current position within this stream by the number of bytes written.
        ''' </summary>
        ''' <param name="buffer">   An array of bytes. This method copies <paramref name="count" /> bytes
        '''                         from <paramref name="buffer" /> to the current stream. </param>
        ''' <param name="offset">   The zero-based byte offset in <paramref name="buffer" /> at which to
        '''                         begin copying bytes to the current stream. </param>
        ''' <param name="count">  The number of bytes to be written to the current stream. </param>
        Public Overrides Sub Write(buffer As Byte(), offset As Integer, count As Integer)
            BaseStream.Write(buffer, offset, count)
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
            Return BaseStream.BeginWrite(buffer, offset, count, callback, state)
        End Function

        ''' <summary>
        ''' Asynchronously writes a sequence of bytes to the current stream, advances the current
        ''' position within this stream by the number of bytes written, and monitors cancellation
        ''' requests.
        ''' </summary>
        ''' <param name="buffer">            The buffer to write data from. </param>
        ''' <param name="offset">               The zero-based byte offset in <paramref name="buffer" />
        '''                                     from which to begin copying bytes to the stream. </param>
        ''' <param name="count">             The maximum number of bytes to write. </param>
        ''' <param name="cancellationToken">    The token to monitor for cancellation requests. The
        '''                                     default value is
        '''                                     <see cref="P:System.Threading.CancellationToken.None" />. </param>
        ''' <returns> A task that represents the asynchronous write operation. </returns>
        Public Overrides Function WriteAsync(buffer() As Byte, offset As Integer, count As Integer, cancellationToken As Threading.CancellationToken) As Task
            Return BaseStream.WriteAsync(buffer, offset, count, cancellationToken).ContinueWith( _
                Sub()
                    If AutoFlush Then Flush()
                End Sub)
        End Function

#End Region

    End Class

End Namespace
