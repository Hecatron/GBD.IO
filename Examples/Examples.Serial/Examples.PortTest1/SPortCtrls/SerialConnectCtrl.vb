Imports System.ComponentModel
Imports GBD.IO.Serial.Net

' TODO Can we get the serial port description similar to the way visual micro does?

Namespace SPortCtrls

    ''' <summary> A Control for handling connect / disconnects. </summary>
    Public Class SerialConnectCtrl

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

            ' Refresh list of Serial Ports
            RefreshSerialPortList()
        End Sub

        ''' <summary> Event handler. Called by SerialPort for i notify handler events. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub SPort_INotifyHandler(sender As Object, e As EventArgs)
            If TypeOf e Is PropertyChangedEventArgs Then
                Dim evargs As PropertyChangedEventArgs = e
                If evargs.PropertyName = "IsOpen" Then RefreshEnabledStates()
            End If
        End Sub

        ''' <summary> Refresh visible states. </summary>
        Private Sub RefreshEnabledStates()
            ConnectButt.Enabled = True
            RefreshButt.Enabled = Not SPort.IsOpen
            SerialPortCbo.Enabled = Not SPort.IsOpen
        End Sub

#End Region

#Region "Functions - Refresh Port List"

        ''' <summary> Refresh serial port list. </summary>
        Private Sub RefreshSerialPortList()
            Dim selectedport As String = SerialPortCbo.SelectedItem
            SerialPortCbo.Items.Clear()
            For Each item In SerialPort.GetPortNames()
                SerialPortCbo.Items.Add(item)
            Next
            If SerialPortCbo.Items.Count > 0 Then SerialPortCbo.SelectedIndex = 0
            If String.IsNullOrEmpty(selectedport) = False Then
                If SerialPortCbo.Items.Contains(selectedport) Then SerialPortCbo.SelectedItem = selectedport
            End If
        End Sub

        ''' <summary> Refresh the list of available Serial Ports. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub RefreshButt_Click(sender As Object, e As EventArgs) Handles RefreshButt.Click
            RefreshSerialPortList()
        End Sub

#End Region

#Region "Functions - Connect"

        ''' <summary> Connect / Disconnect to a serial port. </summary>
        ''' <param name="sender"> Source of the event. </param>
        ''' <param name="e">      Event information. </param>
        Private Sub ConnectButt_Click(sender As Object, e As EventArgs) Handles ConnectButt.Click

            If SPort.IsOpen = False Then
                ' Connect
                If SerialPort.GetPortNames().Contains(SerialPortCbo.SelectedItem) Then
                    SPort.Name = SerialPortCbo.SelectedItem
                    SPort.Open()
                    ConnectButt.Text = "DisConnect"
                End If
            Else
                ' Disconnect
                SPort.Close()
                ConnectButt.Text = "Connect"
            End If

        End Sub

#End Region

    End Class

End Namespace
