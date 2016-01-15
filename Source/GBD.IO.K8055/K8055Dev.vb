'
' K8055Dev.vb
'
' Author:       Richard Westwell
' Date:         06/03/2013
'
' KWrapper class for access to the K8055
Imports GBD.IO.K8055.SVLIB

''' <summary>
''' Wrapper class for access to the K8055
''' </summary>
Public Class K8055Dev
    Implements IDisposable

#Region "Public Properties"

    ''' <summary>
    ''' Underlying Access to the PIC library
    ''' </summary>
    Public Property PIC As New SVLIBWrapper

    ''' <summary>
    ''' Default Vendor ID
    ''' </summary>
    Public Property VendorID As Integer = &H10CF

    ''' <summary>
    ''' Default Device ID
    ''' </summary>
    Public Property DeviceID As Integer = &H5500

    ''' <summary>
    ''' If the Device is Open / Connected
    ''' </summary>
    Public ReadOnly Property IsOpen As Boolean
        Get
            Return PIC.IsConnected
        End Get
    End Property

    ''' <summary>
    ''' Return the Firmware Version of the PIC
    ''' </summary>
    Public ReadOnly Property Firmwareversion As String
        Get
            If IsOpen = False Then Return -1
            Dim tmpver As Integer = PIC.FirmwareVersion()
            Dim ret As String = "Firmware version: 1.x.x.x"
            If tmpver >= 0 Then ret = "Firmware version: " + Hex((tmpver >> 16) And &HFF) + "." _
                    + Hex((tmpver >> 8) And &HFF) + "." + Hex(tmpver And &HFF)
            Return ret
        End Get
    End Property

    ''' <summary>
    ''' Return the Version of the Board
    ''' </summary>
    Public ReadOnly Property Version As Integer
        Get
            If IsOpen = False Then Return -1
            Return PIC.Version()
        End Get
    End Property

    ''' <summary>
    ''' Return the Error Status of the Board
    ''' </summary>
    Public ReadOnly Property ErrorStatus As UInteger
        Get
            If IsOpen = False Then Return 0
            Return PIC.ErrorStatus
        End Get
    End Property

#End Region

#Region "Public Constructors"

    ''' <summary>
    ''' Default Constructor
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Default Constructor
    ''' </summary>
    Public Sub New(VendorID As Integer, DeviceID As Integer)
        Me.DeviceID = DeviceID
        Me.VendorID = VendorID
    End Sub

#End Region

#Region "Public Functions - Open / Close"

    ''' <summary>
    ''' Open the Device
    ''' </summary>
    Public Sub Open()
        If IsOpen = True Then Exit Sub
        Dim ret As Integer = PIC.Open(VendorID, DeviceID)
        If ret <> 0 Then PIC.Close() : Throw New K8055Exception("Unable to Connect to Device")
        ' Set Legacy Mode for K8055
        ret = PIC.SetK8055LegacyMode(True)
        If ret <> 0 Then PIC.Close() : Throw New K8055Exception("Unable to set Device to K8055 Legacy Mode")
    End Sub

    ''' <summary>
    ''' Close the Device
    ''' </summary>
    Public Sub Close()
        If IsOpen = False Then Exit Sub
        PIC.Close()
    End Sub

    ''' <summary>
    ''' Dispose of the object
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Close()
        PIC.Dispose()
    End Sub

#End Region

#Region "Public Functions - Digital Output"

    ''' <summary>
    ''' Clear All Digital IO Pins
    ''' </summary>
    Public Sub DigitalOut_ClearAll()
        PIC.ClearAllDigital()
    End Sub

    ''' <summary>
    ''' Set All Digital IO Pins
    ''' </summary>
    Public Sub DigitalOut_SetAll()
        PIC.SetAllDigital()
    End Sub

    ''' <summary>
    ''' Set a Digital Channel
    ''' </summary>
    Public Sub DigitalOut_Set(channel As Integer)
        If channel > 8 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        PIC.SetDigitalChannel(channel)
    End Sub

    ''' <summary>
    ''' Clear a Digital Channel
    ''' </summary>
    Public Sub DigitalOut_Clear(channel As Integer)
        If channel > 8 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        PIC.ClearDigitalChannel(channel)
    End Sub

    ''' <summary>
    ''' Write Data to Digital Output
    ''' </summary>
    Public Sub DigitalOut_Write(input As Byte)
        If PIC.SetDigitalOutputs(input) <> 0 Then Throw New K8055Exception("Unable to write to Digital Output")
    End Sub

    ''' <summary>
    ''' Read Data from the Digital Output
    ''' </summary>
    Public Function DigitalOut_Read() As Byte
        Dim ret As Byte = PIC.ReadBackDigitalOut
        Return ret
    End Function

    ''' <summary>
    ''' Read Data from the Digital Output Channel
    ''' </summary>
    Public Function DigitalOut_Get(channel As Integer) As Boolean
        If channel > 8 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        Dim ret As Boolean = (DigitalOut_Read() And 1 << channel) > 0
        Return ret
    End Function

#End Region

#Region "Public Functions - Digital Input"

    ''' <summary>
    ''' Read Data from the Digital Input in the form of a single Byte
    ''' </summary>
    Public Function DigitalIn_Read() As Byte
        Dim tmpval As Integer = PIC.ReadAllDigital()
        ' Swap over bits 4 and 5
        Dim ret As Integer = tmpval And &H7
        If tmpval And &H8 Then ret = ret Or &H10
        If tmpval And &H10 Then ret = ret Or &H8
        Return ret
    End Function

    ''' <summary>
    ''' Read Digital Input channel / bit
    ''' </summary>
    Public Function DigitalIn_ReadChannel(channel As Integer) As Boolean
        If channel > 5 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        Dim usedchannel As Integer = channel
        ' Swap over channels 4 and 5
        If channel = 4 Then usedchannel = 5
        If channel = 5 Then usedchannel = 4
        Return PIC.ReadDigitalChannel(usedchannel)
    End Function

#End Region

#Region "Public Functions - PWM"

    ''' <summary>
    ''' Set the PMW Value output
    ''' </summary>
    Public Sub PMWOut_Set(channel As Integer, value As Byte)
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        Dim ch1 As Byte = PMWOut_Get(1)
        Dim ch2 As Byte = PMWOut_Get(2)
        If channel = 1 Then ch1 = value
        If channel = 2 Then ch2 = value
        If PIC.SetPWM(ch1, ch2) <> 0 Then Throw New K8055Exception("Unable to set PMW Output")
    End Sub

    ''' <summary>
    ''' Get the PMW Value output
    ''' </summary>
    Public Function PMWOut_Get(channel As Integer) As Byte
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        If channel = 1 Then Return PIC.K8055_legacy_PWM0_value
        If channel = 2 Then Return PIC.K8055_legacy_PWM1_value
        Return 0
    End Function

#End Region

#Region "Public Functions - Analouge DAC Output"

    ''' <summary>
    ''' Clear All Analouge DAC Output Pins
    ''' </summary>
    Public Sub AnalougeOut_ClearAll()
        PIC.ClearAllAnalog()
    End Sub

    ''' <summary>
    ''' Set All Analouge DAC Output Pins
    ''' </summary>
    Public Sub AnalougeOut_SetAll()
        PIC.SetAllAnalog()
    End Sub

    ''' <summary>
    ''' Set Analouge DAC Outputs - Specific value
    ''' </summary>
    Public Sub AnalougeOut_SetAll(value1 As Byte, value2 As Byte)
        PIC.OutputAllAnalog(value1, value2)
    End Sub

    ''' <summary>
    ''' Set the Analouge DAC Output - Max value
    ''' </summary>
    Public Sub AnalougeOut_Set(channel As Integer)
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        PIC.SetAnalogChannel(channel)
    End Sub

    ''' <summary>
    ''' Set the Analouge DAC Output - Specific value
    ''' </summary>
    Public Sub AnalougeOut_Set(channel As Integer, value As Byte)
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        PIC.OutputAnalogChannel(channel, value)
    End Sub

    ''' <summary>
    ''' </summary>
    ''' Clear the Analouge DAC Output - Minimum value
    Public Sub AnalougeOut_Clear(channel As Integer)
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        PIC.ClearAnalogChannel(channel)
    End Sub

#End Region

#Region "Public Functions - Analouge ADC Input"

    ''' <summary>
    ''' Clear the Analouge DAC Output - Minimum value
    ''' </summary>
    Public Function AnalougeIn_Get(channel As Integer)
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        Return PIC.ReadAnalogInput(channel)
    End Function

#End Region

#Region "Public Functions - Counter"

    ''' <summary>
    ''' Reset the counter
    ''' </summary>
    Public Sub CounterIn_Reset(channel As Integer)
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        PIC.ResetCounter(channel)
    End Sub

    ''' <summary>
    ''' Read the counter
    ''' </summary>
    Public Function CounterIn_Read(channel As Integer) As Integer
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        Return PIC.ReadCounter(channel)
    End Function

    ''' <summary>
    ''' Set the Debounce time of the counter
    ''' </summary>
    Public Sub CounterIn_SetDebounceTime(channel As Integer, DebounceTime As Integer)
        If channel > 2 Or channel < 1 Then Throw New ArgumentException("Channel out of range: " & channel)
        channel -= 1
        PIC.SetCounterDebounceTime(channel, DebounceTime)
    End Sub

#End Region

#Region "Unused Functions"

    ' Open / Close
    ' We already know this as part of the Open Routine
    'PIC.deviceID
    'PIC.vendorID

    ' Digital
    'PIC.ReadDigitalInputs(0) - Not sure what this does but seems to have no effect
    'PIC.WriteAllDigital(input) - Appears to be the same as SetDigitalOutputs

    ' PMW
    'PIC.GetPWMDutyCycle - Doesn't work with the K8055 with the default firmware

    ' Unknown
    'PIC.SearchDevices()
    'PIC.K8055_legacy_DOut_value()
    'PIC.K8055_legacy_mode()
    'PIC.K8055LegacyMode()
    'PIC.GetDefaultOutputValues()
    'PIC.SetDefaultOutputValue()

    'PIC.OpenDevice()
    'PIC.CloseDevice()
    'PIC.ClearDataMemBit()
    'PIC.DataMemRead()
    'PIC.DataMemWrite()
    'PIC.GetPortMemAdr()
    'PIC.ProgMemRead()
    'PIC.SetDataMemBit()

    'PIC.ReadBuffer()
    'PIC.ReadBufferWORD()
    'PIC.WriteBuffer()
    'PIC.WriteBufferWORD()

    'PIC.SetCurrentDevice()
    'PIC.ClearPin(1, 0)
    'PIC.SetPin(0, 0)

    'PIC.CommandEEPROM()
    'PIC.CommandResultEEPROM()
    'PIC.EnterICSPEEPROM()
    'PIC.ReadEEPROM()
    'PIC.WriteEEPROM()

    'PIC.StartThermostaticControl()
    'PIC.StopThermostaticControl()
    'PIC.LoadThermostaticControlValues()
    'PIC.ReadBackThermostaticControlValues()

#End Region

End Class
