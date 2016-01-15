'' CmdRawWire.vb
''
'' Represents a Raw Wire Command to the Bus Pirate

'Namespace Binary.Cmd

'    ''' <summary>
'    ''' Represents a Raw Wire Command to the Bus Pirate
'    ''' </summary>
'    Public Class CmdRawWire
'        Inherits CmdBase

'#Region "Public Properties"

'        ''' <summary>
'        ''' Initial Command Byte
'        ''' </summary>
'        Public Property CmdByte As RawWireEnum

'        ''' <summary>
'        ''' 
'        ''' </summary>
'        Public Property ReturnValue As Byte

'#End Region

'#Region "Public Types"

'        ''' <summary>
'        ''' List of Available Raw-Wire Commands
'        ''' </summary>
'        Public Enum RawWireEnum As Byte

'            ''' <summary>
'            ''' Exit to bitbang mode, responds “BBIOx”
'            ''' </summary>
'            ExitToBitbang = 0

'            ''' <summary>
'            ''' Display mode version string, responds “RAWx”
'            ''' </summary>
'            DisplayModeVersion = 1

'            ''' <summary>
'            ''' I2C-style start (0) / stop (1) bit
'            ''' </summary>
'            I2CStart = 2

'            ''' <summary>
'            ''' Set the CS Pin
'            ''' </summary>
'            CSSet = 4

'        End Enum

'#End Region

'    End Class

'End Namespace
