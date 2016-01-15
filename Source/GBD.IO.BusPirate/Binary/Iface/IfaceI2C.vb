'' IfaceI2C.vb
''
'' Bus Pirate I2C Interface / Binary

'Imports System.Threading
'Imports GBD.Port.Common.Buffer
'Imports GBD.Port.Serial.Net

'Namespace Binary.Iface

'    ''' <summary>
'    ''' Bus Pirate I2C Interface / Binary
'    ''' </summary>
'    Public Class IfaceI2C
'        Inherits IfaceBase

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
'        ''' Enter I2C Mode
'        ''' </summary>
'        Public Function StartMode() As Task(Of String)
'            Return EnterMode_Async(BinaryModeEnum.I2C, "I2C")
'        End Function




'        ''' <summary>
'        ''' Sends a I2C Start Bit
'        ''' </summary>
'        Public Sub Send_StartBit()
'            ' TODO
'        End Sub

'        ''' <summary>
'        ''' Sends a I2C Stop Bit
'        ''' </summary>
'        Public Sub Send_StopBit()
'            ' TODO
'        End Sub

'        ''' <summary>
'        ''' Read a Byte from the I2C Interface
'        ''' </summary>
'        Public Function Get_Byte() As Byte
'            ' TODO
'            Return Nothing
'        End Function

'        ''' <summary>
'        ''' Send an I2C Acknowledge bit after reading a byte. Tells a slave device that you will read another byte.
'        ''' </summary>
'        Public Sub Send_AckBit()
'            ' TODO
'        End Sub

'        ''' <summary>
'        ''' Send an I2C Non Acknowledge bit after reading a byte. Tells a slave device that you will stop reading, next bit should be an I2C stop bit.
'        ''' </summary>
'        Public Sub Send_NAckBit()
'            ' TODO
'        End Sub

'        ''' <summary>
'        ''' Starts the I2C Bus Sniffer
'        ''' </summary>
'        Public Sub Send_StartSniffer()
'            ' TODO
'        End Sub

'        ''' <summary>
'        ''' Stops the I2C Bus Sniffer
'        ''' </summary>
'        Public Sub Send_StopSniffer()
'            ' TODO
'        End Sub

'        ''' <summary>
'        ''' Send a Bulk Packet 1 - 16 bytes
'        ''' </summary>
'        Public Sub Send_BulkWrite(buffer As Byte())
'            If buffer.Length > 16 Then Throw New ArgumentException("Buffer length too long")

'            ' TODO
'        End Sub

'        ''' <summary>
'        ''' Send a command to configure the peripherals
'        ''' </summary>
'        Public Sub Send_ConfigPeripherals()

'        End Sub

'        ''' <summary>
'        ''' Select the pullup voltage
'        ''' </summary>
'        Public Sub Send_PullupVoltageSelect()

'        End Sub

'        ''' <summary>
'        ''' Select the I2C Speed to use
'        ''' </summary>
'        Public Sub Send_I2CSpeed()

'        End Sub

'        ''' <summary>
'        ''' Write then read
'        ''' </summary>
'        Public Sub Send_Read(sendbuffer As Byte(), readcount As Integer)
'            If sendbuffer.Count > 4096 Then Throw New ArgumentException("Buffer Send Size Exceeded")
'            If readcount > 4096 Then Throw New ArgumentException("Buffer Recieve Size Exceeded")

'            ' TODO

'        End Sub

'        ' TODO Aux

'#End Region

'    End Class

'End Namespace
