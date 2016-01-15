Imports Examples.PortTest1.SPortCtrls

Namespace App
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SerialPortCtrl
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.TabControl1 = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.SerialConnectCtrl1 = New SerialConnectCtrl()
            Me.SerialTxCtrl1 = New SerialTxCtrl()
            Me.SerialRxCtrl1 = New SerialRxCtrl()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.TabControl1.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            Me.SuspendLayout()
            '
            'TabControl1
            '
            Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TabControl1.Controls.Add(Me.TabPage1)
            Me.TabControl1.Controls.Add(Me.TabPage2)
            Me.TabControl1.Location = New System.Drawing.Point(3, 12)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(701, 454)
            Me.TabControl1.TabIndex = 11
            '
            'TabPage1
            '
            Me.TabPage1.Controls.Add(Me.SerialConnectCtrl1)
            Me.TabPage1.Controls.Add(Me.SerialTxCtrl1)
            Me.TabPage1.Controls.Add(Me.SerialRxCtrl1)
            Me.TabPage1.Location = New System.Drawing.Point(4, 22)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(693, 428)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Tx / Rx Data"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'SerialConnectCtrl1
            '
            Me.SerialConnectCtrl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SerialConnectCtrl1.Location = New System.Drawing.Point(6, 6)
            Me.SerialConnectCtrl1.Name = "SerialConnectCtrl1"
            Me.SerialConnectCtrl1.Size = New System.Drawing.Size(681, 63)
            Me.SerialConnectCtrl1.SPort = Nothing
            Me.SerialConnectCtrl1.TabIndex = 13
            '
            'SerialTxCtrl1
            '
            Me.SerialTxCtrl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SerialTxCtrl1.Location = New System.Drawing.Point(6, 307)
            Me.SerialTxCtrl1.Name = "SerialTxCtrl1"
            Me.SerialTxCtrl1.Size = New System.Drawing.Size(681, 115)
            Me.SerialTxCtrl1.SPort = Nothing
            Me.SerialTxCtrl1.TabIndex = 12
            '
            'SerialRxCtrl1
            '
            Me.SerialRxCtrl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SerialRxCtrl1.Location = New System.Drawing.Point(6, 75)
            Me.SerialRxCtrl1.Name = "SerialRxCtrl1"
            Me.SerialRxCtrl1.Size = New System.Drawing.Size(681, 226)
            Me.SerialRxCtrl1.SPort = Nothing
            Me.SerialRxCtrl1.TabIndex = 11
            '
            'TabPage2
            '
            Me.TabPage2.Location = New System.Drawing.Point(4, 22)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(693, 428)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "Settings"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'SerialPortCtrl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.TabControl1)
            Me.Name = "SerialPortCtrl"
            Me.Size = New System.Drawing.Size(707, 469)
            Me.TabControl1.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents SerialRxCtrl1 As SerialRxCtrl
        Friend WithEvents SerialTxCtrl1 As SerialTxCtrl
        Friend WithEvents SerialConnectCtrl1 As SerialConnectCtrl

    End Class
End Namespace