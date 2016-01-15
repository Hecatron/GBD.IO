Imports System.Reactive.Subjects
Imports System.Reactive.Threading.Tasks
Imports System.Reactive.Linq
Imports System.Text
Imports Common.Logging

Namespace Probes.Binary

    ''' <summary> A bit bang probe. </summary>
    Public Class BitBangProbe
        Inherits BaseProbe

#Region "Properties"

        ''' <summary>
        ''' NLog Logger
        ''' </summary>
        Private Property Logger As ILog = LogManager.GetLogger(Me.GetType)

#End Region

#Region "Constructors"

        ''' <summary> Default constructor. </summary>
        Public Sub New(device As BPDevice)
            MyBase.New(device)
        End Sub

#End Region


        Public Function SelectBitBang() As Integer
            ReadCache.Clear() ' Clear the Read Cache
            Dim portdisp = Device.SPortStream.Subscribe(ReadCache) ' Start Reading from serial port to cache
            Device.SPortStream.WriteByte(&H0) ' Write Bitbang select mode
            Dim retversion As Integer

            ' This is the response we're expecting from the device
            Dim cmdresponse = Encoding.ASCII.GetBytes("BBIO")

            ' TODO timeout

            ' Read from the cache into the analysing function
            Dim rcdisp As IDisposable
            rcdisp = ReadCache.Subscribe( _
                Sub()

                    Logger.Debug("OnNext")
                    If ReadCache.ByteBuffer.Length >= cmdresponse.Length + 1 _
                        And ReadCache.StartsWith(cmdresponse) Then

                        Dim tmpstr As String = ReadCache.ToString.Replace("BBIO", "")
                        Integer.TryParse(tmpstr, retversion)

                        Logger.Debug("Response Found")

                        portdisp.Dispose()

                        ' TODO

                    End If
                End Sub, _
                Sub(ex As Exception)
                    Logger.Debug("OnError")
                End Sub, _
                Sub()
                    Logger.Debug("On Completed")
                End Sub)

            ' TODO wait

            ' SerialPort.TakeWhile.AsReadCache.Timeout
            ' .Subscribe( code to analyse data and set takewhile condition to false)
            ' .ToTask.ContinueWith(clear read flag)

            ' TODO need to check the population of Completions and Errors all the way through
            ' and move testbool to a global property




            Dim testbool1 As Boolean = True
            Dim x1 = ReadCache _
                .TakeWhile( _
                Function(x As ArraySegment(Of Byte))
                    Return testbool1
                End Function) _
                .Timeout(New TimeSpan(0, 0, 2)).ToTask


            ' Stop reading from the read cache
            rcdisp.Dispose()
            ' Stop reading from the serial port
            portdisp.Dispose()

            Return retversion
        End Function

    End Class

End Namespace
