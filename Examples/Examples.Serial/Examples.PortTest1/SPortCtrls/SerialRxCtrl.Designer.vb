Namespace SPortCtrls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SerialRxCtrl
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
            Me.RxGroupBox = New System.Windows.Forms.GroupBox()
            Me.ClearRxButt = New System.Windows.Forms.Button()
            Me.RxTb = New System.Windows.Forms.TextBox()
            Me.RxGroupBox.SuspendLayout()
            Me.SuspendLayout()
            '
            'RxGroupBox
            '
            Me.RxGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RxGroupBox.Controls.Add(Me.ClearRxButt)
            Me.RxGroupBox.Controls.Add(Me.RxTb)
            Me.RxGroupBox.Location = New System.Drawing.Point(3, 3)
            Me.RxGroupBox.Name = "RxGroupBox"
            Me.RxGroupBox.Size = New System.Drawing.Size(931, 427)
            Me.RxGroupBox.TabIndex = 10
            Me.RxGroupBox.TabStop = False
            Me.RxGroupBox.Text = "Rx Data"
            '
            'ClearRxButt
            '
            Me.ClearRxButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ClearRxButt.Location = New System.Drawing.Point(850, 17)
            Me.ClearRxButt.Name = "ClearRxButt"
            Me.ClearRxButt.Size = New System.Drawing.Size(75, 23)
            Me.ClearRxButt.TabIndex = 11
            Me.ClearRxButt.Text = "Clear Rx"
            Me.ClearRxButt.UseVisualStyleBackColor = True
            '
            'RxTb
            '
            Me.RxTb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.RxTb.Location = New System.Drawing.Point(9, 46)
            Me.RxTb.Multiline = True
            Me.RxTb.Name = "RxTb"
            Me.RxTb.ReadOnly = True
            Me.RxTb.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.RxTb.Size = New System.Drawing.Size(916, 375)
            Me.RxTb.TabIndex = 0
            '
            'SerialRxCtrl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.RxGroupBox)
            Me.Name = "SerialRxCtrl"
            Me.Size = New System.Drawing.Size(937, 433)
            Me.RxGroupBox.ResumeLayout(False)
            Me.RxGroupBox.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents RxGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents ClearRxButt As System.Windows.Forms.Button
        Friend WithEvents RxTb As System.Windows.Forms.TextBox

    End Class
End Namespace