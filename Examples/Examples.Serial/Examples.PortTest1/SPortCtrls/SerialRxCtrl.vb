Imports System.ComponentModel
Imports System.Reactive.Linq
Imports System.Text
Imports System.Threading
Imports GBD.IO.Serial.Net

Namespace SPortCtrls

    ''' <summary> A Control for showing data received from the serial port. </summary>
    Public Class SerialRxCtrl

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

            SPort.BaseStream.ObRead.ObserveOn(SynchronizationContext.Current).Subscribe( _
                Sub(item)
                    RxTb.Text &= Encoding.ASCII.GetString(item).Replace(vbCr, vbCrLf)
                End Sub)
        End Sub

        ''' <summary> Event handler. Called by SerialPort for i notify handler events. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SPort_INotifyHandler(sender As Object, ByVal e As EventArgs)
            If TypeOf e Is PropertyChangedEventArgs Then
                Dim evargs As PropertyChangedEventArgs = e
                If evargs.PropertyName = "IsOpen" Then RxGroupBox.Enabled = SPort.IsOpen
            End If
        End Sub

#End Region

#Region "Functions - Inbound Data"

        ''' <summary> Clears the receive text window. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ClearRxButt_Click(sender As Object, e As EventArgs) Handles ClearRxButt.Click
            RxTb.Text = ""
        End Sub

#End Region

    End Class

End Namespace
