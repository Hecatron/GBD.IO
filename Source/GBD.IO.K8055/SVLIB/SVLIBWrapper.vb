'
' SVLIBWrapper.vb
'
' Author:       Richard Westwell
' Date:         06/03/2013
'
' K8055 - wrapper around the SVLIB library

' Note the underlying heavy lifting is done via a cpp dll
' SVLIB https://sites.google.com/site/pcusbprojects/about
' which is dynamically loaded at runtime
' http://stackoverflow.com/questions/2359918/net-dynamically-load-dll

Imports System.Reflection
Imports System.IO

Namespace SVLIB

    ''' <summary>
    ''' K8055 - Base Class
    ''' </summary>
    Public Class SVLIBWrapper

#Region "Public Properties"

        ''' <summary>
        ''' Type used for the SVLIB Class
        ''' </summary>
        Public ReadOnly Property SVLIBType As Type
            Get
                Return _SVLIBType
            End Get
        End Property
        Protected Property _SVLIBType As Type = Nothing

        ''' <summary>
        ''' Assembly for the SVLIB
        ''' </summary>
        Public ReadOnly Property SVLIBAssembly As Assembly
            Get
                Return _SVLIBAssembly
            End Get
        End Property
        Protected Property _SVLIBAssembly As Assembly = Nothing

        ''' <summary>
        ''' Represents the class instance of SVPICAPI within SVLIB
        ''' </summary>
        Public ReadOnly Property SVLIBFactory As Object
            Get
                Return _SVLIBFactory
            End Get
        End Property
        Protected Property _SVLIBFactory As Object = Nothing

        ''' <summary>
        ''' If the SVLIB Assembly / library has been loaded
        ''' </summary>
        Public ReadOnly Property IsLoaded As Boolean
            Get
                Return _IsLoaded
            End Get
        End Property
        Protected Property _IsLoaded As Boolean = False

        ''' <summary>
        ''' Name of the Dll to be loaded
        ''' </summary>
        Public ReadOnly Property DLLFileName As String
            Get
                If IntPtr.Size = 8 Then Return "SVLIB_PIC18F24J50 v2.7.st.NET4x64.dll" ' 8 = x64
                Return "SVLIB_PIC18F24J50 v2.7.st.NET4.dll" ' 4 = x32
            End Get
        End Property

        ''' <summary>
        ''' Path to the DLL to be loaded
        ''' </summary>
        Public ReadOnly Property DllPath As String
            Get
                Dim basedir As String = AppDomain.CurrentDomain.BaseDirectory
                Dim tmppath As String = Path.Combine(basedir, "SVLIB\" & DLLFileName)
                Return tmppath
            End Get
        End Property

#End Region

#Region "Public Constructors"

        ''' <summary>
        ''' Default Constructor
        ''' </summary>
        Public Sub New()
            LoadAssembly()
        End Sub

#End Region

#Region "Public Functions - Assembly"

        ''' <summary>
        ''' Load the Assembly into memory
        ''' </summary>
        Public Sub LoadAssembly()
            If _IsLoaded = True Then Exit Sub
            _SVLIBAssembly = Assembly.LoadFrom(DllPath)
            Dim typelist As Type() = SVLIBAssembly.GetExportedTypes
            For Each item As Type In typelist
                If item.Name = "SVPICAPI" Then _SVLIBType = item
            Next
            If SVLIBType Is Nothing Then Throw New K8055Exception("Unable to locate SVPICAPI Type when loading the SVLIB")
            _SVLIBFactory = SVLIBAssembly.CreateInstance(SVLIBType.FullName, True)
            _IsLoaded = True
        End Sub

#End Region

#Region "Public Properties"

        ''' <summary>
        ''' If the Device is Connected
        ''' </summary>
        Public Function IsConnected() As Boolean
            Dim method As MethodInfo = SVLIBType.GetMethod("IsConnected", New Type() {})
            Dim params() As Object = New Object() {}
            Dim ret As Boolean = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Firmware Version
        ''' </summary>
        Public Function FirmwareVersion() As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("FirmwareVersion", New Type() {})
            Dim params() As Object = New Object() {}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Board Version
        ''' </summary>
        Public Function Version() As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("Version", New Type() {})
            Dim params() As Object = New Object() {}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Error Status
        ''' </summary>
        Public Property ErrorStatus As UInteger
            Get
                Dim prop As PropertyInfo = SVLIBType.GetProperty("ErrorStatus")
                Dim ret As Byte = prop.GetValue(_SVLIBFactory, Nothing)
                Return ret
            End Get
            Set(value As UInteger)
                Dim prop As PropertyInfo = SVLIBType.GetProperty("ErrorStatus")
                prop.SetValue(_SVLIBFactory, value, Nothing)
            End Set
        End Property

#End Region

#Region "Public Functions - Open / Close"

        ''' <summary>
        ''' Open the Device
        ''' </summary>
        Public Function Open(vendorID As Integer, devID As Integer) As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("Open", New Type() {GetType(Integer), GetType(Integer)})
            Dim params() As Object = New Object() {vendorID, devID}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Close the Device
        ''' </summary>
        Public Function Close() As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("Close")
            Dim params() As Object = New Object() {}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Set the board to K8055Legacy Mode
        ''' </summary>
        Public Function SetK8055LegacyMode(legacy_FW_mode As Boolean) As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("SetK8055LegacyMode", New Type() {GetType(Boolean)})
            Dim params() As Object = New Object() {legacy_FW_mode}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Dispose of the Class
        ''' </summary>
        Public Sub Dispose()
            Dim method As MethodInfo = SVLIBType.GetMethod("Dispose")
            Dim params() As Object = New Object() {}
            method.Invoke(_SVLIBFactory, params)
        End Sub

#End Region

#Region "Public Functions - Digital Output"

        ''' <summary>
        ''' Clear All Digital Outputs
        ''' </summary>
        Public Sub ClearAllDigital()
            Dim method As MethodInfo = SVLIBType.GetMethod("ClearAllDigital")
            Dim params() As Object = New Object() {}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Set All Digital Outputs
        ''' </summary>
        Public Sub SetAllDigital()
            Dim method As MethodInfo = SVLIBType.GetMethod("SetAllDigital")
            Dim params() As Object = New Object() {}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Set Digital Channel
        ''' </summary>
        Public Sub SetDigitalChannel(Channel As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("SetDigitalChannel", New Type() {GetType(Integer)})
            Dim params() As Object = New Object() {Channel}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Clear Digital Channel
        ''' </summary>
        Public Sub ClearDigitalChannel(Channel As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("ClearDigitalChannel", New Type() {GetType(Integer)})
            Dim params() As Object = New Object() {Channel}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Set Digital Outputs
        ''' </summary>
        Public Function SetDigitalOutputs(portB As Byte) As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("SetDigitalOutputs", New Type() {GetType(Byte)})
            Dim params() As Object = New Object() {portB}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Read Digital Output
        ''' </summary>
        Public Function ReadBackDigitalOut() As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("ReadBackDigitalOut")
            Dim params() As Object = New Object() {}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

#End Region

#Region "Public Functions - Digital Input"

        ''' <summary>
        ''' Read All Digital Inputs
        ''' </summary>
        Public Function ReadAllDigital() As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("ReadAllDigital")
            Dim params() As Object = New Object() {}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Read Digital Channel
        ''' </summary>
        Public Function ReadDigitalChannel(Channel As Integer) As Boolean
            Dim method As MethodInfo = SVLIBType.GetMethod("ReadDigitalChannel", New Type() {GetType(Integer)})
            Dim params() As Object = New Object() {Channel}
            Dim ret As Boolean = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

#End Region

#Region "Public Functions - PWM"

        ''' <summary>
        ''' Set the PMW Value output
        ''' </summary>
        Public Function SetPWM(dutyCycle1 As UShort, dutyCycle2 As UShort) As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("SetPWM", New Type() {GetType(UShort), GetType(UShort)})
            Dim params() As Object = New Object() {dutyCycle1, dutyCycle2}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Get the PWM Value for PWM0
        ''' </summary>
        Public Property K8055_legacy_PWM0_value As Byte
            Get
                Dim finfo As FieldInfo = SVLIBType.GetField("K8055_legacy_PWM0_value")
                Dim ret As Byte = finfo.GetValue(_SVLIBFactory)
                Return ret
            End Get
            Set(value As Byte)
                Dim finfo As FieldInfo = SVLIBType.GetField("K8055_legacy_PWM0_value")
                finfo.SetValue(_SVLIBFactory, value)
            End Set
        End Property

        ''' <summary>
        ''' Get the PWM Value for PWM1
        ''' </summary>
        Public Property K8055_legacy_PWM1_value As Byte
            Get
                Dim finfo As FieldInfo = SVLIBType.GetField("K8055_legacy_PWM1_value")
                Dim ret As Byte = finfo.GetValue(_SVLIBFactory)
                Return ret
            End Get
            Set(value As Byte)
                Dim finfo As FieldInfo = SVLIBType.GetField("K8055_legacy_PWM1_value")
                finfo.SetValue(_SVLIBFactory, value)
            End Set
        End Property

#End Region

#Region "Public Functions - Analouge DAC Output"

        ''' <summary>
        ''' Clear all Analog Outputs
        ''' </summary>
        Public Sub ClearAllAnalog()
            Dim method As MethodInfo = SVLIBType.GetMethod("ClearAllAnalog")
            Dim params() As Object = New Object() {}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Set all Analog Outputs
        ''' </summary>
        Public Sub SetAllAnalog()
            Dim method As MethodInfo = SVLIBType.GetMethod("SetAllAnalog")
            Dim params() As Object = New Object() {}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Output to all Analog Outputs
        ''' </summary>
        Public Sub OutputAllAnalog(Data1 As Integer, Data2 As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("OutputAllAnalog", New Type() {GetType(Integer), GetType(Integer)})
            Dim params() As Object = New Object() {Data1, Data2}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Set an Analog Channel
        ''' </summary>
        Public Sub SetAnalogChannel(Channel As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("SetAnalogChannel", New Type() {GetType(Integer)})
            Dim params() As Object = New Object() {Channel}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Output to an Analog Channel
        ''' </summary>
        Public Sub OutputAnalogChannel(Channel As Integer, Data As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("OutputAnalogChannel", New Type() {GetType(Integer), GetType(Integer)})
            Dim params() As Object = New Object() {Channel, Data}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Clear Analog Channel
        ''' </summary>
        Public Sub ClearAnalogChannel(Channel As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("ClearAnalogChannel", New Type() {GetType(Integer)})
            Dim params() As Object = New Object() {Channel}
            method.Invoke(_SVLIBFactory, params)
        End Sub

#End Region

#Region "Public Functions - Analouge ADC Input"

        ''' <summary>
        ''' Read Analog Input
        ''' </summary>
        Public Function ReadAnalogInput(ADchannel As Byte) As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("ReadAnalogInput", New Type() {GetType(Byte)})
            Dim params() As Object = New Object() {ADchannel}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

#End Region

#Region "Public Functions - Counter"

        ''' <summary>
        ''' Reset a Counter
        ''' </summary>
        Public Sub ResetCounter(CounterNr As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("ResetCounter", New Type() {GetType(Integer)})
            Dim params() As Object = New Object() {CounterNr}
            method.Invoke(_SVLIBFactory, params)
        End Sub

        ''' <summary>
        ''' Read a Counter
        ''' </summary>
        Public Function ReadCounter(CounterNr As Integer) As Integer
            Dim method As MethodInfo = SVLIBType.GetMethod("ReadCounter", New Type() {GetType(Integer)})
            Dim params() As Object = New Object() {CounterNr}
            Dim ret As Integer = method.Invoke(_SVLIBFactory, params)
            Return ret
        End Function

        ''' <summary>
        ''' Set Counter Debounce Time
        ''' </summary>
        Public Sub SetCounterDebounceTime(CounterNr As Integer, DebounceTime As Integer)
            Dim method As MethodInfo = SVLIBType.GetMethod("SetCounterDebounceTime", New Type() {GetType(Integer), GetType(Integer)})
            Dim params() As Object = New Object() {CounterNr, DebounceTime}
            method.Invoke(_SVLIBFactory, params)
        End Sub

#End Region

#Region "TODO Clear"

        ' ''' <summary>
        ' ''' Dynamically Load the DLL
        ' ''' </summary>
        'Public Sub Test1()
        '    Dim tmppath As String = AppDomain.CurrentDomain.BaseDirectory
        '    tmppath = Path.Combine(tmppath, "SVLIB\" & DLL32Bit)
        '    Dim testasm As Assembly = Assembly.LoadFrom(tmppath)
        '    Dim typelist As Type() = testasm.GetExportedTypes
        '    Dim SVLIBType As Type = Nothing
        '    For Each item As Type In typelist
        '        If item.Name = "SVPICAPI" Then SVLIBType = item
        '    Next
        '    If SVLIBType Is Nothing Then Exit Sub

        '    ' Create a new instance
        '    Dim factory As Object = testasm.CreateInstance(SVLIBType.FullName, True)

        '    ' Open the Device

        '    ' Grab the method
        '    Dim method As MethodInfo = SVLIBType.GetMethod("Open", New Type() {GetType(Integer), GetType(Integer)})
        '    ' Call the method
        '    'Dim params() As Object = New Object() {"", ""}
        '    Dim ret As Integer = method.Invoke(factory, New Object() {&H10CF, &H5500})

        '    ' Set Legacy Mode
        '    Dim method2 As MethodInfo = SVLIBType.GetMethod("SetK8055LegacyMode", New Type() {GetType(Boolean)})
        '    method2.Invoke(factory, New Object() {True})

        '    ' Set Digital Outputs
        '    Dim ret3 As Integer
        '    Dim method3 As MethodInfo = SVLIBType.GetMethod("SetDigitalOutputs", New Type() {GetType(Byte)})
        '    ret3 = method3.Invoke(factory, New Object() {CByte(1)})
        '    ret3 = method3.Invoke(factory, New Object() {CByte(2)})
        '    ret3 = method3.Invoke(factory, New Object() {CByte(3)})

        'End Sub

#End Region

    End Class
End Namespace
