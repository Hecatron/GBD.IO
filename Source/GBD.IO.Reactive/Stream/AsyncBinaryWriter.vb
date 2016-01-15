Imports System.IO
Imports System.Text
Imports System.Threading

Namespace Stream

    ''' <summary> Asynchronous binary writer. </summary>
    Public Class AsyncBinaryWriter
        Inherits BinaryWriter

#Region "Properties"

        ''' <summary> If to automatically flush the stream on a binary write. </summary>
        ''' <value> true if automatic flush, false if not. </value>
        Public Property AutoFlush As Boolean = True

        ''' <summary> Underlying Stream used for Writing data. </summary>
        ''' <value> The write stream. </value>
        Public Overrides ReadOnly Property Basestream As System.IO.Stream
            Get
                If StreamFunc IsNot Nothing Then OutStream = StreamFunc.Invoke
                OutStream.Flush()
                Return OutStream
            End Get
        End Property

        ''' <summary> Delegate Function for referencing the write Stream. </summary>
        ''' <value> Delegate Function for referencing the write Stream. </value>
        Protected Overridable Property StreamFunc As Func(Of System.IO.Stream)

        ''' <summary>
        ''' Byte Buffer for data conversions
        ''' </summary>
        Private _buffer() As Byte = New Byte(&H10) {}

        ''' <summary> The encoding used for String Formatting. </summary>
        ''' <value> The encoding used for String Formatting. </value>
        Public ReadOnly Property Encoding As Encoding
            Get
                Return _Encoding
            End Get
        End Property
        Protected Property _Encoding As Encoding

        ''' <summary> The encoder used for String Formatting. </summary>
        ''' <value> The encoder used for String Formatting. </value>
        Public ReadOnly Property Encoder As Encoder
            Get
                Return _Encoder
            End Get
        End Property
        Protected Property _Encoder As Encoder

#End Region

#Region "Constructors"

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class that writes to a stream.
        ''' </summary>
        Public Sub New()
            MyBase.New()
            _Encoding = New UTF8Encoding(False, True)
            _Encoder = _Encoding.GetEncoder
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class based on the specified stream and
        ''' using UTF-8 encoding.
        ''' </summary>
        ''' <param name="output"> The output stream. </param>
        Public Sub New(output As System.IO.Stream)
            MyBase.New(output)
            _Encoding = New UTF8Encoding(False, True)
            _Encoder = _Encoding.GetEncoder
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class based on the specified stream and
        ''' character encoding.
        ''' </summary>
        ''' <param name="output">   The output stream. </param>
        ''' <param name="encoding"> The character encoding to use. </param>
        Public Sub New(output As System.IO.Stream, encoding As Encoding)
            MyBase.New(output, encoding)
            _Encoding = New UTF8Encoding(False, True)
            _Encoder = _Encoding.GetEncoder
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class based on the specified stream and
        ''' character encoding, and optionally leaves the stream open.
        ''' </summary>
        ''' <param name="output">    The output stream. </param>
        ''' <param name="encoding">  The character encoding to use. </param>
        ''' <param name="leaveOpen">    true to leave the stream open after the RxBinaryWriter class is
        '''                             disposed, otherwise false. </param>
        Public Sub New(output As System.IO.Stream, encoding As Encoding, leaveOpen As Boolean)
            MyBase.New(output, encoding, leaveOpen)
            _Encoding = New UTF8Encoding(False, True)
            _Encoder = _Encoding.GetEncoder
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class based on the specified stream and
        ''' using UTF-8 encoding.
        ''' </summary>
        ''' <param name="streamfunc"> Delegate Function for writing the Stream. </param>
        Public Sub New(streamfunc As Func(Of System.IO.Stream))
            MyBase.New()
            _Encoding = New UTF8Encoding(False, True)
            _Encoder = _Encoding.GetEncoder
            Me.StreamFunc = streamfunc
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class based on the specified stream and
        ''' character encoding.
        ''' </summary>
        ''' <param name="streamfunc"> Delegate Function for writing the Stream. </param>
        ''' <param name="encoding"> The character encoding to use. </param>
        Public Sub New(streamfunc As Func(Of System.IO.Stream), encoding As Encoding)
            MyBase.New(New MemoryStream, encoding)
            _Encoding = encoding
            _Encoder = _Encoding.GetEncoder
            Me.StreamFunc = streamfunc
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the RxBinaryWriter class based on the specified stream and
        ''' character encoding, and optionally leaves the stream open.
        ''' </summary>
        ''' <param name="streamfunc"> Delegate Function for writing the Stream. </param>
        ''' <param name="encoding">  The character encoding to use. </param>
        ''' <param name="leaveOpen">    true to leave the stream open after the RxBinaryWriter class is
        '''                             disposed, otherwise false. </param>
        Public Sub New(streamfunc As Func(Of System.IO.Stream), encoding As Encoding, leaveOpen As Boolean)
            MyBase.New(New MemoryStream, encoding, leaveOpen)
            _Encoding = encoding
            _Encoder = _Encoding.GetEncoder
            Me.StreamFunc = streamfunc
        End Sub

#End Region

#Region "Write - Buffer with Offset"

        ''' <summary> Writes a region of a byte array to the current stream. </summary>
        ''' <param name="buffer"> A byte array containing the data to write. </param>
        ''' <param name="offset"> The offset. </param>
        ''' <param name="count">  The number of bytes to write. </param>
        Public Overrides Sub Write(buffer() As Byte, offset As Integer, count As Integer)
            OutStream.Write(buffer, offset, count)
            If AutoFlush Then Flush()
        End Sub

        ''' <summary> Writes a region of a byte array to the current stream. </summary>
        ''' <param name="buffer"> A byte array containing the data to write. </param>
        ''' <param name="offset"> The offset. </param>
        ''' <param name="count">  The number of bytes to write. </param>
        ''' <returns> A Task. </returns>
        Public Overridable Function WriteAsync(buffer() As Byte, offset As Integer, count As Integer) As Task
            Return WriteAsync(buffer, offset, count, CancellationToken.None)
        End Function

        ''' <summary> Writes a region of a byte array to the current stream. </summary>
        ''' <param name="buffer"> A byte array containing the data to write. </param>
        ''' <param name="offset"> The offset. </param>
        ''' <param name="count">  The number of bytes to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task. </returns>
        Public Overridable Function WriteAsync(buffer() As Byte, offset As Integer, count As Integer, cancellationToken As CancellationToken) As Task
            Return OutStream.WriteAsync(buffer, offset, count, cancellationToken).ContinueWith( _
                Sub()
                    If AutoFlush Then Flush()
                End Sub)
        End Function

#End Region

#Region "Write - Boolean"

        ''' <summary>
        ''' Writes a one-byte Boolean value to the current stream, with 0 representing false and 1
        ''' representing true.
        ''' </summary>
        ''' <param name="value"> The Boolean value to write (0 or 1). </param>
        Public Overrides Sub Write(value As Boolean)
            _buffer(0) = If(value, (CByte(1)), (CByte(0)))
            Write(_buffer, 0, 1)
        End Sub

        ''' <summary>
        ''' Writes a one-byte Boolean value to the current stream, with 0 representing false and 1
        ''' representing true.
        ''' </summary>
        ''' <param name="value"> The Boolean value to write (0 or 1). </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Boolean) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a one-byte Boolean value to the current stream, with 0 representing false and 1
        ''' representing true.
        ''' </summary>
        ''' <param name="value"> The Boolean value to write (0 or 1). </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Boolean, cancellationToken As CancellationToken) As Task
            _buffer(0) = If(value, (CByte(1)), (CByte(0)))
            Return WriteAsync(_buffer, 0, 1)
        End Function

#End Region

#Region "Write - Byte"

        ''' <summary>
        ''' Writes an unsigned byte to the current stream and advances the stream position by one byte.
        ''' </summary>
        ''' <param name="value"> The unsigned byte to write. </param>
        Public Overrides Sub Write(value As Byte)
            _buffer(0) = value
            Write(_buffer, 0, 1)
        End Sub

        ''' <summary>
        ''' Writes an unsigned byte to the current stream and advances the stream position by one byte.
        ''' </summary>
        ''' <param name="value"> The unsigned byte to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Byte) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes an unsigned byte to the current stream and advances the stream position by one byte.
        ''' </summary>
        ''' <param name="value"> The unsigned byte to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Byte, cancellationToken As CancellationToken) As Task
            _buffer(0) = value
            Return WriteAsync(_buffer, 0, 1)
        End Function

#End Region

#Region "Write - Buffer"

        ''' <summary> Writes a byte array to the underlying stream. </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="buffer"> A byte array containing the data to write. </param>
        Public Overrides Sub Write(buffer() As Byte)
            If buffer Is Nothing Then Throw New ArgumentNullException("buffer")
            Write(buffer, 0, buffer.Length)
        End Sub

        ''' <summary> Writes a byte array to the underlying stream. </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="buffer"> A byte array containing the data to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(buffer() As Byte) As Task
            Return WriteAsync(buffer, CancellationToken.None)
        End Function

        ''' <summary> Writes a byte array to the underlying stream. </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="buffer"> A byte array containing the data to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(buffer() As Byte, cancellationToken As CancellationToken) As Task
            If buffer Is Nothing Then Throw New ArgumentNullException("buffer")
            Return WriteAsync(buffer, 0, buffer.Length)
        End Function

#End Region

#Region "Write - Character"

        ''' <summary>
        ''' Writes a Unicode character to the current stream and advances the current position of the
        ''' stream in accordance with the Encoding used and the specific characters being written to the
        ''' stream.
        ''' </summary>
        ''' <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
        '''                                         illegal values. </exception>
        ''' <param name="ch"> The non-surrogate, Unicode character to write. </param>
        Public Overrides Sub Write(ch As Char)
            If (Char.IsSurrogate(ch)) Then Throw New ArgumentException("Surrogates Not Allowed As a SingleChar")
            Dim chars() As Char = {ch}
            Dim count As Integer = _Encoder.GetBytes(chars, 0, 1, _buffer, &H10, True)
            Write(_buffer, 0, count)
        End Sub

        ''' <summary>
        ''' Writes a Unicode character to the current stream and advances the current position of the
        ''' stream in accordance with the Encoding used and the specific characters being written to the
        ''' stream.
        ''' </summary>
        ''' <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
        '''                                         illegal values. </exception>
        ''' <param name="ch"> The non-surrogate, Unicode character to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(ch As Char) As Task
            Return WriteAsync(ch, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a Unicode character to the current stream and advances the current position of the
        ''' stream in accordance with the Encoding used and the specific characters being written to the
        ''' stream.
        ''' </summary>
        ''' <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
        '''                                         illegal values. </exception>
        ''' <param name="ch"> The non-surrogate, Unicode character to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(ch As Char, cancellationToken As CancellationToken) As Task
            If (Char.IsSurrogate(ch)) Then Throw New ArgumentException("Surrogates Not Allowed As a SingleChar")
            Dim chars() As Char = {ch}
            Dim count As Integer = _Encoder.GetBytes(chars, 0, 1, _buffer, &H10, True)
            Return WriteAsync(_buffer, 0, count)
        End Function

#End Region

#Region "Write - Character Array"

        ''' <summary> Writes a byte array to the underlying stream. </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="chars"> The characters. </param>
        Public Overrides Sub Write(chars() As Char)
            If chars Is Nothing Then Throw New ArgumentNullException("chars")
            Dim buffer() As Byte = _Encoding.GetBytes(chars, 0, chars.Length)
            Write(buffer, 0, buffer.Length)
        End Sub

        ''' <summary> Writes a byte array to the underlying stream. </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="chars"> The characters. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(chars() As Char) As Task
            Return WriteAsync(chars, CancellationToken.None)
        End Function

        ''' <summary> Writes a byte array to the underlying stream. </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="chars"> The characters. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(chars() As Char, cancellationToken As CancellationToken) As Task
            If chars Is Nothing Then Throw New ArgumentNullException("chars")
            Dim buffer() As Byte = _Encoding.GetBytes(chars, 0, chars.Length)
            Return WriteAsync(buffer, 0, buffer.Length)
        End Function

#End Region

#Region "Write - Character Array with index"

        ''' <summary> Writes a region of a byte array to the current stream. </summary>
        ''' <param name="chars"> The characters. </param>
        ''' <param name="index">    The starting point in <paramref name="chars" /> at which to begin
        '''                         writing. </param>
        ''' <param name="count"> The number of bytes to write. </param>
        Public Overrides Sub Write(chars() As Char, index As Integer, count As Integer)
            Dim buffer() As Byte = _Encoding.GetBytes(chars, index, count)
            Write(buffer, 0, buffer.Length)
        End Sub

        ''' <summary> Writes a region of a byte array to the current stream. </summary>
        ''' <param name="chars"> The characters. </param>
        ''' <param name="index">    The starting point in <paramref name="chars" /> at which to begin
        '''                         writing. </param>
        ''' <param name="count"> The number of bytes to write. </param>
        ''' <returns> A Task. </returns>
        Public Function WriteAsync(chars() As Char, index As Integer, count As Integer) As Task
            Return WriteAsync(chars, index, count, CancellationToken.None)
        End Function

        ''' <summary> Writes a region of a byte array to the current stream. </summary>
        ''' <param name="chars"> The characters. </param>
        ''' <param name="index">    The starting point in <paramref name="chars" /> at which to begin
        '''                         writing. </param>
        ''' <param name="count"> The number of bytes to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task. </returns>
        Public Function WriteAsync(chars() As Char, index As Integer, count As Integer, cancellationToken As CancellationToken) As Task
            If chars Is Nothing Then Throw New ArgumentNullException("chars")
            Dim buffer() As Byte = _Encoding.GetBytes(chars, index, count)
            Return WriteAsync(buffer, 0, buffer.Length)
        End Function

#End Region

#Region "Write - Decimal"

        ''' <summary>
        ''' Writes a decimal value to the current stream and advances the stream position by sixteen
        ''' bytes.
        ''' </summary>
        ''' <param name="value"> The decimal value to write. </param>
        Public Overrides Sub Write(value As Decimal)
            Dim bits As Int32() = Decimal.GetBits(value)
            Dim bytes As New List(Of Byte)
            For Each i As Int32 In bits
                bytes.AddRange(BitConverter.GetBytes(i))
            Next
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes a decimal value to the current stream and advances the stream position by sixteen
        ''' bytes.
        ''' </summary>
        ''' <param name="value"> The decimal value to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Decimal) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a decimal value to the current stream and advances the stream position by sixteen
        ''' bytes.
        ''' </summary>
        ''' <param name="value"> The decimal value to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Decimal, cancellationToken As CancellationToken) As Task
            Dim bits As Int32() = Decimal.GetBits(value)
            Dim bytes As New List(Of Byte)
            For Each i As Int32 In bits
                bytes.AddRange(BitConverter.GetBytes(i))
            Next
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - Double"

        ''' <summary>
        ''' Writes an eight-byte floating-point value to the current stream and advances the stream
        ''' position by eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte floating-point value to write. </param>
        Public Overrides Sub Write(value As Double)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes an eight-byte floating-point value to the current stream and advances the stream
        ''' position by eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte floating-point value to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Double) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes an eight-byte floating-point value to the current stream and advances the stream
        ''' position by eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte floating-point value to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Double, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - Integer"

        ''' <summary>
        ''' Writes a four-byte signed integer to the current stream and advances the stream position by
        ''' four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte signed integer to write. </param>
        Public Overrides Sub Write(value As Integer)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes a four-byte signed integer to the current stream and advances the stream position by
        ''' four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte signed integer to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Integer) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a four-byte signed integer to the current stream and advances the stream position by
        ''' four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte signed integer to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Integer, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - Long"

        ''' <summary>
        ''' Writes an eight-byte signed integer to the current stream and advances the stream position by
        ''' eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte signed integer to write. </param>
        Public Overrides Sub Write(value As Long)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes an eight-byte signed integer to the current stream and advances the stream position by
        ''' eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte signed integer to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Long) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes an eight-byte signed integer to the current stream and advances the stream position by
        ''' eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte signed integer to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Long, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - SByte"

        ''' <summary>
        ''' Writes a signed byte to the current stream and advances the stream position by one byte.
        ''' </summary>
        ''' <param name="value"> The signed byte to write. </param>
        Public Overrides Sub Write(value As SByte)
            _buffer(0) = value
            Write(_buffer, 0, 1)
        End Sub

        ''' <summary>
        ''' Writes a signed byte to the current stream and advances the stream position by one byte.
        ''' </summary>
        ''' <param name="value"> The signed byte to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As SByte) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a signed byte to the current stream and advances the stream position by one byte.
        ''' </summary>
        ''' <param name="value"> The signed byte to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As SByte, cancellationToken As CancellationToken) As Task
            _buffer(0) = value
            Return WriteAsync(_buffer, 0, 1)
        End Function

#End Region

#Region "Write - Short"

        ''' <summary>
        ''' Writes a two-byte signed integer to the current stream and advances the stream position by
        ''' two bytes.
        ''' </summary>
        ''' <param name="value"> The two-byte signed integer to write. </param>
        Public Overrides Sub Write(value As Short)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes a two-byte signed integer to the current stream and advances the stream position by
        ''' two bytes.
        ''' </summary>
        ''' <param name="value"> The two-byte signed integer to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Short) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a two-byte signed integer to the current stream and advances the stream position by
        ''' two bytes.
        ''' </summary>
        ''' <param name="value"> The two-byte signed integer to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Short, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - Single"

        ''' <summary>
        ''' Writes a four-byte floating-point value to the current stream and advances the stream
        ''' position by four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte floating-point value to write. </param>
        Public Overrides Sub Write(value As Single)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes a four-byte floating-point value to the current stream and advances the stream
        ''' position by four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte floating-point value to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Single) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a four-byte floating-point value to the current stream and advances the stream
        ''' position by four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte floating-point value to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As Single, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - String"

        ''' <summary>
        ''' Writes a length-prefixed string to this stream in the current encoding of the
        ''' <see cref="T:System.IO.BinaryWriter" />, and advances the current position of the stream in
        ''' accordance with the encoding used and the specific characters being written to the stream.
        ''' </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="value"> The value to write. </param>
        Public Overrides Sub Write(value As String)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            Dim buffer() As Byte = _Encoding.GetBytes(value)
            Write(buffer, 0, buffer.Length)
        End Sub

        ''' <summary>
        ''' Writes a length-prefixed string to this stream in the current encoding of the
        ''' <see cref="T:System.IO.BinaryWriter" />, and advances the current position of the stream in
        ''' accordance with the encoding used and the specific characters being written to the stream.
        ''' </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="value"> The value to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As String) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a length-prefixed string to this stream in the current encoding of the
        ''' <see cref="T:System.IO.BinaryWriter" />, and advances the current position of the stream in
        ''' accordance with the encoding used and the specific characters being written to the stream.
        ''' </summary>
        ''' <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        '''                                             null. </exception>
        ''' <param name="value"> The value to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As String, cancellationToken As CancellationToken) As Task
            If value Is Nothing Then Throw New ArgumentNullException("value")
            Dim buffer() As Byte = _Encoding.GetBytes(value)
            Return WriteAsync(buffer, 0, buffer.Length)
        End Function

#End Region

#Region "Write - UInteger"

        ''' <summary>
        ''' Writes a four-byte unsigned integer to the current stream and advances the stream position by
        ''' four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte unsigned integer to write. </param>
        Public Overrides Sub Write(value As UInteger)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes a four-byte unsigned integer to the current stream and advances the stream position by
        ''' four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte unsigned integer to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As UInteger) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a four-byte unsigned integer to the current stream and advances the stream position by
        ''' four bytes.
        ''' </summary>
        ''' <param name="value"> The four-byte unsigned integer to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As UInteger, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - ULong"

        ''' <summary>
        ''' Writes an eight-byte unsigned integer to the current stream and advances the stream position
        ''' by eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte unsigned integer to write. </param>
        Public Overrides Sub Write(value As ULong)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes an eight-byte unsigned integer to the current stream and advances the stream position
        ''' by eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte unsigned integer to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As ULong) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes an eight-byte unsigned integer to the current stream and advances the stream position
        ''' by eight bytes.
        ''' </summary>
        ''' <param name="value"> The eight-byte unsigned integer to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As ULong, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

#Region "Write - UShort"

        ''' <summary>
        ''' Writes a two-byte unsigned integer to the current stream and advances the stream position by
        ''' two bytes.
        ''' </summary>
        ''' <param name="value"> The two-byte unsigned integer to write. </param>
        Public Overrides Sub Write(value As UShort)
            Dim bytes = BitConverter.GetBytes(value)
            Write(bytes.ToArray, 0, bytes.Count)
        End Sub

        ''' <summary>
        ''' Writes a two-byte unsigned integer to the current stream and advances the stream position by
        ''' two bytes.
        ''' </summary>
        ''' <param name="value"> The two-byte unsigned integer to write. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As UShort) As Task
            Return WriteAsync(value, CancellationToken.None)
        End Function

        ''' <summary>
        ''' Writes a two-byte unsigned integer to the current stream and advances the stream position by
        ''' two bytes.
        ''' </summary>
        ''' <param name="value"> The two-byte unsigned integer to write. </param>
        ''' <param name="cancellationToken"> The cancellation token. </param>
        ''' <returns> A Task for Async Operations. </returns>
        Public Function WriteAsync(value As UShort, cancellationToken As CancellationToken) As Task
            Dim bytes = BitConverter.GetBytes(value)
            Return WriteAsync(bytes.ToArray, 0, bytes.Count)
        End Function

#End Region

    End Class

End Namespace
