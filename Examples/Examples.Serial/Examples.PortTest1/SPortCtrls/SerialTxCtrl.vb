Imports System.ComponentModel
Imports System.IO
Imports GBD.IO.Serial.Net

' TODO Hex / Encoding options

Namespace SPortCtrls

    ''' <summary> A Control for handling data transmitted to the serial port. </summary>
    Public Class SerialTxCtrl

#Region "Properties"

        ''' <summary> Serial Port for Access </summary>
        ''' <value> TSerial Port for Access </value>
        Public Property SPort As SerialPort

#End Region

#Region "Functions - Setup"

        ''' <summary> Sets the serial port to be used for inbound data. </summary>
        ''' <param name="port"> Serial Port for Access. </param>
        Public Sub SetSerialPort(port As SerialPort)
            ' Change allocated serial port
            SPort = port

            ' Setup Handler for Serial Port Property Changes
            If SPort IsNot Nothing Then RemoveHandler SPort.PropertyChanged, AddressOf SPort_INotifyHandler
            AddHandler SPort.PropertyChanged, AddressOf SPort_INotifyHandler
        End Sub

        ''' <summary> Event handler. Called by SerialPort for i notify handler events. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SPort_INotifyHandler(sender As Object, ByVal e As EventArgs)
            If TypeOf e Is PropertyChangedEventArgs Then
                Dim evargs As PropertyChangedEventArgs = e
                If evargs.PropertyName = "IsOpen" Then TxGroupBox.Enabled = SPort.IsOpen
            End If
        End Sub

#End Region

#Region "Functions - Outbound Data"

        ''' <summary> Sends data via the serial port. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SendButt_Click(sender As Object, e As EventArgs) Handles SendButt.Click
            Dim strwrt As New BinaryWriter(SPort.BaseStream)



            strwrt.Write(SendTb.Text)
        End Sub

        ''' <summary> Sends data via the serial port X10. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SendX10Butt_Click(sender As Object, e As EventArgs) Handles SendX10Butt.Click
            For i = 1 To 10
                Dim strwrt As New BinaryWriter(SPort.BaseStream)
                strwrt.Write(SendTb.Text)
            Next
        End Sub

#End Region

    End Class

End Namespace
