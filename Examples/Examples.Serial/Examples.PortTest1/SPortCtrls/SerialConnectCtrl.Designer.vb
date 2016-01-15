Namespace SPortCtrls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SerialConnectCtrl
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
            Me.ConnectGroupBox = New System.Windows.Forms.GroupBox()
            Me.RefreshButt = New System.Windows.Forms.Button()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.SerialPortCbo = New System.Windows.Forms.ComboBox()
            Me.ConnectButt = New System.Windows.Forms.Button()
            Me.ConnectGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'ConnectGroupBox
            '
            Me.ConnectGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ConnectGroupBox.Controls.Add(Me.RefreshButt)
            Me.ConnectGroupBox.Controls.Add(Me.Label1)
            Me.ConnectGroupBox.Controls.Add(Me.SerialPortCbo)
            Me.ConnectGroupBox.Controls.Add(Me.ConnectButt)
            Me.ConnectGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.ConnectGroupBox.Name = "ConnectGroupBox"
            Me.ConnectGroupBox.Size = New System.Drawing.Size(507, 210)
            Me.ConnectGroupBox.TabIndex = 11
            Me.ConnectGroupBox.TabStop = False
            Me.ConnectGroupBox.Text = "Connection"
            '
            'RefreshButt
            '
            Me.RefreshButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RefreshButt.Location = New System.Drawing.Point(426, 24)
            Me.RefreshButt.Name = "RefreshButt"
            Me.RefreshButt.Size = New System.Drawing.Size(75, 23)
            Me.RefreshButt.TabIndex = 4
            Me.RefreshButt.Text = "Refresh"
            Me.RefreshButt.UseVisualStyleBackColor = True
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(19, 29)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(55, 13)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "Serial Port"
            '
            'SerialPortCbo
            '
            Me.SerialPortCbo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SerialPortCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.SerialPortCbo.FormattingEnabled = True
            Me.SerialPortCbo.Location = New System.Drawing.Point(80, 26)
            Me.SerialPortCbo.Name = "SerialPortCbo"
            Me.SerialPortCbo.Size = New System.Drawing.Size(259, 21)
            Me.SerialPortCbo.TabIndex = 1
            '
            'ConnectButt
            '
            Me.ConnectButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ConnectButt.Location = New System.Drawing.Point(345, 24)
            Me.ConnectButt.Name = "ConnectButt"
            Me.ConnectButt.Size = New System.Drawing.Size(75, 23)
            Me.ConnectButt.TabIndex = 2
            Me.ConnectButt.Text = "Connect"
            Me.ConnectButt.UseVisualStyleBackColor = True
            '
            'SerialConnectCtrl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.ConnectGroupBox)
            Me.Name = "SerialConnectCtrl"
            Me.Size = New System.Drawing.Size(513, 216)
            Me.ConnectGroupBox.ResumeLayout(False)
            Me.ConnectGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ConnectGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents RefreshButt As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents SerialPortCbo As System.Windows.Forms.ComboBox
        Friend WithEvents ConnectButt As System.Windows.Forms.Button

    End Class
End Namespace