Imports GBD.IO.Reactive.Base

Namespace Stream

    ''' <summary> An observable Stream Wrapper. </summary>
    Public Class RxStream
        Inherits StreamWrapperBase
        Implements IObserver(Of Byte())
        Implements IObservable(Of Byte())

#Region "Properties - Observable Write"

        ''' <summary> An Observable used for monitoring data written to the stream. </summary>
        ''' <value> An Observable used for monitoring data written to the stream. </value>
        Public ReadOnly Property ObWrite As RxObservableBase(Of Byte())
            Get
                Return _ObWrite
            End Get
        End Property
        Protected Property _ObWrite As New RxObservableBase(Of Byte())

        ''' <summary>
        ''' Gets or sets a value indicating whether the asynchronous mode should be used for OnNext.
        ''' </summary>
        ''' <value> true if write asynchronous, false if not. </value>
        Public Property ObWriteAsync As Boolean = True

        ''' <summary> Gets the write task used for writing async data. </summary>
        ''' <value> Gets the write task used for writing async data. </value>
        Public ReadOnly Property ObWriteAsyncTask As Task
            Get
                Return _ObWriteAsyncTask
            End Get
        End Property
        Protected Property _ObWriteAsyncTask As Task

#End Region

#Region "Properties - Observable Read"

        ''' <summary>
        ''' An Observable used for reading data automatically when ready from the stream.
        ''' </summary>
        ''' <value> An Observable used for reading data automatically when ready from the stream. </value>
        Public ReadOnly Property ObRead As RxObservableTaskBase(Of Byte())
            Get
                Return _ObRead
            End Get
        End Property
        Protected Property _ObRead As RxObservableTaskBase(Of Byte())

        ''' <summary> The size of the buffer used for reading data. </summary>
        ''' <value> The size of the buffer used for reading data. </value>
        Public Property ObRead_BufferSize As Integer = 81920

        ''' <summary> Gets the read task status used for streaming data via the observable. </summary>
        ''' <value> Gets the read task status used for streaming data via the observable. </value>
        Public ReadOnly Property ObReadTaskStatus As TaskStatus
            Get
                If _ObReadTask Is Nothing Then Return TaskStatus.Created
                Return _ObReadTask.Status
            End Get
        End Property
        Protected Property _ObReadTask As Task(Of Integer)

        ''' <summary>
        ''' Optional Condition to test when reading fromt the stream during a Subscribe.
        ''' </summary>
        ''' <value> Optional Condition to test when reading fromt the stream during a Subscribe. </value>
        Protected Property _ObReadCondition As Func(Of Boolean)

#End Region

#Region "Public Constructors"

        ''' <summary> Default Constructor. </summary>
        ''' <param name="stream"> The stream to reference / intercept. </param>
        Public Sub New(stream As System.IO.Stream)
            MyBase.New(stream)
            _ObRead = New RxObservableTaskBase(Of Byte())(AddressOf TaskReadData)
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="streamfunc"> Delegate Function for reading the Stream. </param>
        Public Sub New(streamfunc As Func(Of System.IO.Stream))
            MyBase.New(streamfunc)
            _ObRead = New RxObservableTaskBase(Of Byte())(AddressOf TaskReadData)
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="stream">    The stream to reference / intercept. </param>
        ''' <param name="condition"> The condition to test during subscription reads. </param>
        Public Sub New(stream As System.IO.Stream, condition As Func(Of Boolean))
            MyBase.New(stream)
            _ObRead = New RxObservableTaskBase(Of Byte())(AddressOf TaskReadData)
            _ObReadCondition = condition
        End Sub

        ''' <summary> Default Constructor. </summary>
        ''' <param name="streamfunc"> Delegate Function for reading the Stream. </param>
        ''' <param name="condition">  The condition to test during subscription reads. </param>
        Public Sub New(streamfunc As Func(Of System.IO.Stream), condition As Func(Of Boolean))
            MyBase.New(streamfunc)
            _ObRead = New RxObservableTaskBase(Of Byte())(AddressOf TaskReadData)
            _ObReadCondition = condition
        End Sub

#End Region

#Region "Functions - Base Overrides - Read / Write"

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
            If _ObWrite.ObserverClient IsNot Nothing Then
                Dim ret(count - offset) As Byte
                System.Buffer.BlockCopy(buffer, offset, ret, 0, count)
                _ObWrite.ObserverClient.OnNext(ret)
            End If
            MyBase.Write(buffer, offset, count)
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
            If _ObWrite.ObserverClient IsNot Nothing Then
                Dim ret(count - offset) As Byte
                System.Buffer.BlockCopy(buffer, offset, ret, 0, count)
                _ObWrite.ObserverClient.OnNext(ret)
            End If
            Return MyBase.BeginWrite(buffer, offset, count, callback, state)
        End Function

#End Region

#Region "Functions - IObserver CallBacks - Writing"

        ''' <summary> Outbound Write Traffic. </summary>
        ''' <param name="value"> The data to write to the stream. </param>
        Public Sub OnNext(value As Byte()) Implements IObserver(Of Byte()).OnNext
            If ObWriteAsync Then
                If _ObWriteAsyncTask IsNot Nothing Then _ObWriteAsyncTask.Wait()
                _ObWriteAsyncTask = WriteAsync(value, 0, value.Length)
            Else
                Write(value, 0, value.Length)
            End If
        End Sub

        ''' <summary>
        ''' Notifies the observer that the provider has experienced an error condition.
        ''' </summary>
        ''' <param name="error"> An object that provides additional information about the error. </param>
        Public Sub OnError([error] As Exception) Implements IObserver(Of Byte()).OnError
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

#Region "Functions - IObservable CallBacks - Reading"

        ''' <summary> Subscribes the given observer to the stream for reading data / outbound. </summary>
        ''' <param name="observer"> The observer to subscribe. </param>
        ''' <returns> An IDisposable. </returns>
        Public Function Subscribe(observer As IObserver(Of Byte())) As IDisposable Implements IObservable(Of Byte()).Subscribe
            Return _ObRead.Subscribe(observer)
        End Function

        ''' <summary>
        ''' Start Reading Data. This should call a function that should loop while
        ''' _TokenSource.Token.IsCancellationRequested = False. Once true, observ.OnCompleted() should be
        ''' called then the class can be re-subscribed to, but any prior subscriptions will be invalid As
        ''' they are wrapped via AutoDetachObserver, prior to SubscribeCore as part of ObservableBase.
        ''' </summary>
        ''' <param name="observ"> The observer to stream data to. </param>
        Protected Sub TaskReadData(observ As IObserver(Of Byte()))

            ' Setup the Buffer
            Dim usedbuffsize As Integer = ObRead_BufferSize
            Dim serialbuffer(usedbuffsize - 1) As Byte

            ' Run until canceled or complete
            While _ObRead.TokenSource.Token.IsCancellationRequested = False

                ' If the Optional condition is specified, then check to see if we should continue
                If _ObReadCondition IsNot Nothing And _ObReadCondition.Invoke = False Then Continue While

                ' If the Base Stream is setup later on
                If BaseStream Is Nothing Then Continue While

                _ObReadTask = BaseStream.ReadAsync(serialbuffer, 0, usedbuffsize, _ObRead.TokenSource.Token)
                _ObReadTask.Wait(_ObRead.TokenSource.Token)

                ' Handle returned data
                If _ObReadTask.IsCompleted Then
                    Dim ret(_ObReadTask.Result - 1) As Byte
                    Buffer.BlockCopy(serialbuffer, 0, ret, 0, _ObReadTask.Result)
                    observ.OnNext(ret)
                End If

                ' Handle Error of recieve data
                If _ObReadTask.IsFaulted Then
                    observ.OnError(_ObReadTask.Exception)
                    _ObRead.TokenSource.Cancel()
                    Exit While
                End If

                ' Resize buffer if the size has changed
                If ObRead_BufferSize <> usedbuffsize Then
                    usedbuffsize = ObRead_BufferSize
                    ReDim serialbuffer(usedbuffsize - 1)
                End If

            End While

            ' Completed sequence
            observ.OnCompleted()
        End Sub

#End Region

    End Class

End Namespace
