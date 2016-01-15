Imports GBD.IO.Serial.Net

Namespace App

    ''' <summary> Example Serial Port Comms Control. </summary>
    Public Class SerialPortCtrl

#Region "Properties"

        ''' <summary> Serial Port for Access </summary>
        ''' <value> TSerial Port for Access </value>
        Public Property SPort As SerialPort

#End Region

#Region "Functions - Display"

        ''' <summary> Control Loaded. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SerialPortConsoleCtrl_Load(sender As Object, e As EventArgs) Handles Me.Load
            SPort = New SerialPort

            SerialConnectCtrl1.SetSerialPort(SPort)
            SerialRxCtrl1.SetSerialPort(SPort)
            SerialTxCtrl1.SetSerialPort(SPort)
            ' TODO Settings / Pin States
        End Sub

#End Region

    End Class

End Namespace
