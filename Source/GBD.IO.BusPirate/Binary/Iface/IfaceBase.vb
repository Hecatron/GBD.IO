'' IfaceBase.vb
''
'' Base Class for Bus Pirate Interface

'Imports System.Reactive.Linq
'Imports GBD.Port.Common.Buffer
'Imports GBD.Port.Serial.Net
'Imports NLog
'Imports System.Threading

'Namespace Binary.Iface

'    ''' <summary>
'    ''' Base Class for Bus Pirate Interface
'    ''' </summary>
'    Public Class IfaceBase

'#Region "Properties - General"

'        ''' <summary>
'        ''' NLog Logger
'        ''' </summary>
'        Private Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

'        ''' <summary>
'        ''' Timeout in miliseconds
'        ''' </summary>
'        Public Property TimeOut As TimeSpan = New TimeSpan(0, 0, 0, 0, 3000)

'        ''' <summary>
'        ''' Cancelation Token Source
'        ''' </summary>
'        Public ReadOnly Property CancelSource As CancellationTokenSource
'            Get
'                Return _CancelSource
'            End Get
'        End Property
'        Protected Property _CancelSource As CancellationTokenSource

'#End Region

'#Region "Properties - IO"

'        ''' <summary>
'        ''' Bytes sent to the Bus Pirate via Buffered
'        ''' </summary>
'        Protected Property BufferTx As ObByteOutput

'        ''' <summary>
'        ''' Bytes recieved from the Bus Pirate via Buffered
'        ''' </summary>
'        Protected Property BufferRx As ObByteInput

'#End Region

'#Region "Types"

'        ''' <summary>
'        ''' Binary Mode Enums
'        ''' </summary>
'        Public Enum BinaryModeEnum
'            BitBang = 0
'            SPI = 1
'            I2C = 2
'            Uart = 3
'            OneWire = 4
'            RawWire = 5
'            OpenOCD_JTAG = 6
'            HWReset = 15
'        End Enum

'#End Region

'#Region "Constructors"

'        ''' <summary>
'        ''' Default Constructor
'        ''' </summary>
'        Public Sub New()
'            BufferRx = New ObByteInput
'            BufferTx = New ObByteOutput
'        End Sub

'        ''' <summary>
'        ''' Default Constructor
'        ''' </summary>
'        Public Sub New(input As IObservable(Of Byte()), output As IObserver(Of Byte()))
'            BufferRx = New ObByteInput(input)
'            BufferTx = New ObByteOutput(output)
'        End Sub

'        ''' <summary>
'        ''' Default Constructor
'        ''' </summary>
'        Public Sub New(sport As SerialPort)
'            BufferRx = New ObByteInput(sport.DataRx)
'            BufferTx = New ObByteOutput(sport.DataTx)
'        End Sub

'#End Region

'#Region "Public Functions"

'        ''' <summary>
'        ''' Send a reset to make sure we're at the correct point in the menu
'        ''' </summary>
'        Public Function ExitMenu_Async() As Task(Of String)
'            _CancelSource = New CancellationTokenSource
'            Dim ret As New TaskCompletionSource(Of String)()

'            ' Reset Buffer
'            BufferRx.Clear()

'            ' Begin sending Command, send a max of 25 times
'            Task.Factory.StartNew( _
'                Sub()
'                    For i = 0 To 9
'                        If _CancelSource.IsCancellationRequested = True Then Exit Sub
'                        BufferTx.Send(vbCr.ToString)
'                    Next
'                    If _CancelSource.IsCancellationRequested = True Then Exit Sub
'                    BufferTx.Send("#")
'                End Sub, _CancelSource.Token)

'            Return ret.Task
'        End Function

'        ''' <summary>
'        ''' Enter Binary Mode
'        ''' </summary>
'        Public Function EnterBinaryMode_Async() As Task(Of String)
'            _CancelSource = New CancellationTokenSource
'            Dim ret As New TaskCompletionSource(Of String)

'            ' Reset Buffer
'            BufferRx.Clear()

'            ' Begin sending Command, send a max of 25 times
'            Task.Factory.StartNew( _
'                Sub()
'                    Dim count1 As Integer = 0
'                    While _CancelSource.IsCancellationRequested = False And count1 < 25
'                        BufferTx.Send(CType(BinaryModeEnum.BitBang, Byte))
'                        Logger.Debug("Tx: Enter Binary Mode")
'                        Thread.Sleep(100)
'                        count1 += 1
'                    End While
'                End Sub, _CancelSource.Token)

'            ' Wait for a device response
'            AwaitResponse(ret, _
'                Function(val)
'                    Dim tmpstr = BufferRx.ToString
'                    If tmpstr.StartsWith("BBIO") And IsNumeric(tmpstr.Replace("BBIO", "")) Then
'                        Logger.Debug("Rx: " & tmpstr)
'                        Return True
'                    End If
'                    Return False
'                End Function)

'            '' Wait for a device response
'            'BufferRx.Timeout(TimeOut).Subscribe( _
'            '    Sub(val As Byte())
'            '        ' OnNext
'            '        Dim tmpstr = BufferRx.ToString
'            '        If tmpstr.StartsWith("BBIO") And IsNumeric(tmpstr.Replace("BBIO", "")) Then
'            '            ret.TrySetResult(tmpstr)
'            '            Logger.Debug("Rx: Enter Binary Mode: " & tmpstr)
'            '            cancelsrc.Cancel()
'            '        End If
'            '    End Sub, _
'            '    Sub(ex As Exception)
'            '        ' OnError
'            '        Logger.Debug("Rx: Device Timeout")
'            '        ret.TrySetException(ex)
'            '        cancelsrc.Cancel()
'            '    End Sub, cancelsrc.Token)

'            Return ret.Task
'        End Function


'        ''' <summary>
'        ''' Enter Specific Binary Mode
'        ''' </summary>
'        Protected Function EnterMode_Async(mode As BinaryModeEnum, response As String) As Task(Of String)
'            _CancelSource = New CancellationTokenSource
'            Dim ret As New TaskCompletionSource(Of String)()

'            ' Reset Buffer
'            BufferRx.Clear()

'            ' Begin sending Command, send a max of 25 times
'            Task.Factory.StartNew( _
'                Sub()
'                    If _CancelSource.IsCancellationRequested = True Then Exit Sub
'                    BufferTx.Send(CType(mode, Byte))
'                End Sub, _CancelSource.Token)

'            ' Wait for a device response
'            AwaitResponse(ret, _
'                Function(val)
'                    Dim tmpstr = BufferRx.ToString
'                    If tmpstr.StartsWith(response) And IsNumeric(tmpstr.Replace(response, "")) Then
'                        Logger.Debug("Rx: " & tmpstr)
'                        Return True
'                    End If
'                    Return False
'                End Function)

'            'BufferRx.Timeout(TimeOut).Subscribe( _
'            '    Sub(val As Byte())
'            '        ' OnNext
'            '        Dim tmpstr = BufferRx.ToString
'            '        If tmpstr.StartsWith(response) And IsNumeric(tmpstr.Replace(response, "")) Then
'            '            ret.TrySetResult(tmpstr)
'            '            Logger.Debug("Rx: Enter Binary Mode: " & tmpstr)
'            '            _CancelSource.Cancel()
'            '        End If
'            '    End Sub, _
'            '    Sub(ex As Exception)
'            '        ' OnError
'            '        Logger.Debug("Rx: Device Timeout")
'            '        ret.TrySetException(ex)
'            '        _CancelSource.Cancel()
'            '    End Sub, _CancelSource.Token)

'            Return ret.Task
'        End Function



'        ''' <summary>
'        ''' Await a response from the Device
'        ''' </summary>
'        Protected Sub AwaitResponse(tcs As TaskCompletionSource(Of String), callbackcheck As Func(Of Byte(), Boolean))

'            ' Wait for a device response
'            BufferRx.Timeout(TimeOut).Subscribe( _
'                Sub(val As Byte())
'                    ' OnNext
'                    Dim tmpstr = BufferRx.ToString
'                    If callbackcheck(val) Then
'                        tcs.TrySetResult(tmpstr)
'                        _CancelSource.Cancel()
'                    End If
'                End Sub, _
'                Sub(ex As Exception)
'                    ' OnError
'                    Logger.Debug("Rx: Device Timeout")
'                    tcs.TrySetException(ex)
'                    _CancelSource.Cancel()
'                End Sub, _CancelSource.Token)
'        End Sub

'#End Region

'    End Class

'End Namespace
