Imports System.Text
Imports System.Threading
Imports GBD.IO.Serial
Imports GBD.IO.Serial.Net

Public Class MainDialog1


    Private Sub test2()
        Dim list1 = SerialPort.GetPortNames

        'Dim sport2 As New SerialPort("COM24")
        'sport2.Open()
        'sport2.RxDataOut.Subscribe( _
        '    Sub(item)
        '        'Debug.WriteLine("Data Output:" & Encoding.ASCII.GetString(item))
        '        Dim tmparr = Encoding.UTF8.GetBytes("BBIO1")
        '        sport2.RxDataIn.OnNext(tmparr)
        '    End Sub)


        'Dim setts As New Settings()
        'setts.BaudRate = GBD.Port.Serial.Base.SettingsBase.BaudRates.B115200
        'setts.DataBits = GBD.Port.Serial.Base.SettingsBase.DataBitsType.D8
        'setts.HandShake = IO.Ports.Handshake.None
        'setts.Parity = IO.Ports.Parity.None
        'setts.StopBits = IO.Ports.StopBits.One

        'Dim sport1 As New SerialPort("COM17", BPDevice.DefaultSerialSettings)
        'Dim sport1 As New SerialPort("COM23")
        'sport1.Open()


        'sport1.RxDataOut.Subscribe( _
        '    Sub(item)
        '        Debug.WriteLine("OUT: " & Encoding.ASCII.GetString(item))
        '    End Sub)
        'Thread.Sleep(3000)

        'sport1.RxDataIn.OnNext(Encoding.ASCII.GetBytes(vbCr))
        'Thread.Sleep(3000)





        ' TODO This now works !!
        ' Now move to awaiting observables instead of tasks on EnterBinaryModeAsync
        ' Experiment with FirstAsync()

        'Dim iface As New IfaceBase(sport1)
        'iface.ExitMenu_Async().Wait()
        'iface.EnterBinaryMode_Async.Wait()
        'Dim i2ciface As New IfaceI2C(sport1)
        'i2ciface.StartMode().Wait()


        'Dim result = iface.EnterMode_Async(IfaceBase.BinaryModeEnum.I2C, "I2C")

        Try
            'result.Wait()
        Catch ex As Exception
            Debug.WriteLine("Timeout")
        End Try



        Thread.Sleep(10000)


        'sport1.Close()
        ''sport2.Close()


    End Sub

    Private Sub test1()
        Dim list1 = SerialPort.GetPortNames

        Dim sport1 As New SerialPort("COM23")
        sport1.Open()

        Dim sport2 As New SerialPort("COM24")
        sport2.Open()



        ' TODO if the buffer is empty and we call readasync then I think it causes a problem

        ' Write some data
        Dim tmparr = Encoding.UTF8.GetBytes("test1")
        'sport1.BaseStream.WriteAsync(tmparr, 0, tmparr.Length)
        sport1.BaseStream.OnNext(tmparr)

        Thread.Sleep(10000)

        sport2.PinStates.RxPinState.Subscribe( _
            Sub(item)
                Debug.WriteLine("Pin Change:" & item.ToString)
            End Sub)

        sport2.BaseStream.ObRead.Subscribe( _
            Sub(item)
                Debug.WriteLine("Data Read:" & Encoding.ASCII.GetString(item))
            End Sub)

        sport1.BaseStream.ObWrite.Subscribe( _
            Sub(item)
                Debug.WriteLine("Data Written:" & Encoding.ASCII.GetString(item))
            End Sub)

        ' Change Pin State
        'sport1.PinStates.DtrEnable = True

        System.Threading.Thread.Sleep(10000)

        'For i = 0 To 1000
        'Dim x As Integer = 3
        'Next

        ' Write some data
        Dim tmparr2 = Encoding.UTF8.GetBytes("test2")
        'sport1.BaseStream.WriteAsync(tmparr2, 0, tmparr2.Length)
        sport1.BaseStream.OnNext(tmparr2)

        System.Threading.Thread.Sleep(10000)

        'Dim blocksize As Integer = 30
        'Dim buffer(blocksize - 1) As Byte
        'Dim tmptask As Task(Of Integer) = sport2.BaseStream.ReadAsync(buffer, 0, blocksize)



        ''Dim rxobserv As IObservable(Of Byte())

        'Dim src As New CancellationTokenSource


        'Dim x1 = Observable.Create(Of String)(Function(observer As IObserver(Of String))



        '                                          'or can return an Action like 
        '                                          'return () => Console.WriteLine("Observer has unsubscribed"); 
        '                                          observer.OnNext("a")
        '                                          observer.OnNext("b")
        '                                          observer.OnCompleted()
        '                                          Return Disposable.Create(Sub() Debug.WriteLine("Observer has unsubscribed"))
        '                                      End Function)



        'Debug.WriteLine(tmptask.Result.ToString)
        'Debug.WriteLine(Encoding.ASCII.GetString(buffer.ToArray))

        'sport1.Close()
        'sport2.Close()


    End Sub


End Class
