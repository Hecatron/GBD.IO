Namespace SPortCtrls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SerialTxCtrl
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
            Me.TxGroupBox = New System.Windows.Forms.GroupBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.SendButt = New System.Windows.Forms.Button()
            Me.ComboBox2 = New System.Windows.Forms.ComboBox()
            Me.SendTb = New System.Windows.Forms.TextBox()
            Me.SendX10Butt = New System.Windows.Forms.Button()
            Me.TxGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'TxGroupBox
            '
            Me.TxGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TxGroupBox.Controls.Add(Me.SendX10Butt)
            Me.TxGroupBox.Controls.Add(Me.Label3)
            Me.TxGroupBox.Controls.Add(Me.Label2)
            Me.TxGroupBox.Controls.Add(Me.SendButt)
            Me.TxGroupBox.Controls.Add(Me.ComboBox2)
            Me.TxGroupBox.Controls.Add(Me.SendTb)
            Me.TxGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.TxGroupBox.Name = "TxGroupBox"
            Me.TxGroupBox.Size = New System.Drawing.Size(721, 363)
            Me.TxGroupBox.TabIndex = 9
            Me.TxGroupBox.TabStop = False
            Me.TxGroupBox.Text = "Tx Data"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(6, 22)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(59, 13)
            Me.Label3.TabIndex = 10
            Me.Label3.Text = "Send Type"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(6, 49)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(58, 13)
            Me.Label2.TabIndex = 9
            Me.Label2.Text = "Send Data"
            '
            'SendButt
            '
            Me.SendButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SendButt.Location = New System.Drawing.Point(640, 44)
            Me.SendButt.Name = "SendButt"
            Me.SendButt.Size = New System.Drawing.Size(75, 23)
            Me.SendButt.TabIndex = 6
            Me.SendButt.Text = "Send"
            Me.SendButt.UseVisualStyleBackColor = True
            '
            'ComboBox2
            '
            Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.ComboBox2.FormattingEnabled = True
            Me.ComboBox2.Items.AddRange(New Object() {"ASCII", "Hex"})
            Me.ComboBox2.Location = New System.Drawing.Point(68, 19)
            Me.ComboBox2.Name = "ComboBox2"
            Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
            Me.ComboBox2.TabIndex = 7
            '
            'SendTb
            '
            Me.SendTb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SendTb.Location = New System.Drawing.Point(68, 46)
            Me.SendTb.Multiline = True
            Me.SendTb.Name = "SendTb"
            Me.SendTb.Size = New System.Drawing.Size(566, 311)
            Me.SendTb.TabIndex = 5
            '
            'SendX10Butt
            '
            Me.SendX10Butt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.SendX10Butt.Location = New System.Drawing.Point(640, 73)
            Me.SendX10Butt.Name = "SendX10Butt"
            Me.SendX10Butt.Size = New System.Drawing.Size(75, 23)
            Me.SendX10Butt.TabIndex = 11
            Me.SendX10Butt.Text = "Send X10"
            Me.SendX10Butt.UseVisualStyleBackColor = True
            '
            'SerialTxCtrl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.TxGroupBox)
            Me.Name = "SerialTxCtrl"
            Me.Size = New System.Drawing.Size(727, 369)
            Me.TxGroupBox.ResumeLayout(False)
            Me.TxGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TxGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents SendButt As System.Windows.Forms.Button
        Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
        Friend WithEvents SendTb As System.Windows.Forms.TextBox
        Friend WithEvents SendX10Butt As System.Windows.Forms.Button

    End Class
End Namespace