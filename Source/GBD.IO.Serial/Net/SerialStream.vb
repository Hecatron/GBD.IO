Imports GBD.IO.Reactive.Stream
Imports GBD.IO.Serial.Base

Namespace Net

    ''' <summary> A serial stream. </summary>
    Public Class SerialStream
        Inherits RxStream

#Region "Properties"

        ''' <summary> Binding to a Serial Port Implementation. </summary>
        ''' <value> Binding to a Serial Port Implementation. </value>
        Public ReadOnly Property BindingPort As SerialPort
            Get
                Return _BindingPort
            End Get
        End Property
        Protected Property _BindingPort As SerialPort

#End Region

#Region "Public Constructors"

        ''' <summary> Default Constructor. </summary>
        ''' <param name="port"> The serial port to bind to. </param>
        Public Sub New(port As SerialPort)
            MyBase.New( _
                Function()
                    ' The underlying Serial port stream is not set untill the port is open
                    ' This allows us to subscribe to the read data before the port is opened
                    If port.IsOpen Then Return port.IOSerialPort.BaseStream
                    Return Nothing
                End Function, _
                Function()
                    If port.IsOpen = False Then Return False
                    ' This is a more reliable method to check if data is available for reading
                    If port.IOSerialPort.BytesToRead <= 0 Then Return False
                    Return True
                End Function)
            _BindingPort = port
            AutoFlush = True
        End Sub

#End Region

#Region "Function - Overrides"

        ''' <summary>
        ''' Reads a sequence of bytes from the current stream and advances the position within the stream
        ''' by the number of bytes read.
        ''' </summary>
        ''' <exception cref="SerialException"> Thrown when a Serial error condition occurs. </exception>
        ''' <param name="buffer">   An array of bytes. When this method returns, the buffer contains the
        '''                         specified byte array with the values between. </param>
        ''' <param name="offset"> and (<paramref name="offset" /> +. </param>
        ''' <param name="count">  - 1) replaced by the bytes read from the current source. </param>
        ''' <returns>
        ''' The total number of bytes read into the buffer. This can be less than the number of bytes
        ''' requested if that many bytes are not currently available, or zero (0) if the end of the
        ''' stream has been reached.
        ''' </returns>
        Public Overrides Function Read(buffer() As Byte, offset As Integer, count As Integer) As Integer
            If BindingPort.IsOpen = False Then Throw New SerialException("Serial Port is Closed")
            If BaseStream Is Nothing Then Throw New SerialException("Serial Port stream is null")
            Return MyBase.Read(buffer, offset, count)
        End Function

        ''' <summary> Begins a async read. </summary>
        ''' <exception cref="SerialException"> Thrown when a Serial error condition occurs. </exception>
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
            If BindingPort.IsOpen = False Then Throw New SerialException("Serial Port is Closed")
            If BaseStream Is Nothing Then Throw New SerialException("Serial Port stream is null")
            Return MyBase.BeginRead(buffer, offset, count, callback, state)
        End Function

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
            If BindingPort.IsOpen = False Then Throw New SerialException("Serial Port is Closed")
            If BaseStream Is Nothing Then Throw New SerialException("Serial Port stream is null")
            MyBase.Write(buffer, offset, count)
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
            If BindingPort.IsOpen = False Then Throw New SerialException("Serial Port is Closed")
            If BaseStream Is Nothing Then Throw New SerialException("Serial Port stream is null")
            Return MyBase.BeginWrite(buffer, offset, count, callback, state)
        End Function

#End Region

    End Class

End Namespace
