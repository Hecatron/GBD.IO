Namespace App
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class MainDialog
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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
            Me.LaunchExeButt = New System.Windows.Forms.Button()
            Me.ExeBrowseButt = New System.Windows.Forms.Button()
            Me.ArgumentsTb = New System.Windows.Forms.TextBox()
            Me.WorkingDirTb = New System.Windows.Forms.TextBox()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.ExePathTb = New System.Windows.Forms.TextBox()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.StdInSendButt = New System.Windows.Forms.Button()
            Me.ClearStdErrorButt = New System.Windows.Forms.Button()
            Me.ClearStdOutButt = New System.Windows.Forms.Button()
            Me.StdOutTb = New System.Windows.Forms.TextBox()
            Me.StdErrorTb = New System.Windows.Forms.TextBox()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.StdInputTb = New System.Windows.Forms.TextBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.TabControl1.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            Me.TabPage2.SuspendLayout()
            Me.SuspendLayout()
            '
            'TabControl1
            '
            Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TabControl1.Controls.Add(Me.TabPage1)
            Me.TabControl1.Controls.Add(Me.TabPage2)
            Me.TabControl1.Location = New System.Drawing.Point(12, 12)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(565, 378)
            Me.TabControl1.TabIndex = 0
            '
            'TabPage1
            '
            Me.TabPage1.Controls.Add(Me.LaunchExeButt)
            Me.TabPage1.Controls.Add(Me.ExeBrowseButt)
            Me.TabPage1.Controls.Add(Me.ArgumentsTb)
            Me.TabPage1.Controls.Add(Me.WorkingDirTb)
            Me.TabPage1.Controls.Add(Me.Label6)
            Me.TabPage1.Controls.Add(Me.Label5)
            Me.TabPage1.Controls.Add(Me.Label1)
            Me.TabPage1.Controls.Add(Me.ExePathTb)
            Me.TabPage1.Location = New System.Drawing.Point(4, 22)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage1.Size = New System.Drawing.Size(557, 352)
            Me.TabPage1.TabIndex = 0
            Me.TabPage1.Text = "Process Options"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'LaunchExeButt
            '
            Me.LaunchExeButt.Location = New System.Drawing.Point(104, 117)
            Me.LaunchExeButt.Name = "LaunchExeButt"
            Me.LaunchExeButt.Size = New System.Drawing.Size(75, 23)
            Me.LaunchExeButt.TabIndex = 7
            Me.LaunchExeButt.Text = "Launch"
            Me.LaunchExeButt.UseVisualStyleBackColor = True
            '
            'ExeBrowseButt
            '
            Me.ExeBrowseButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExeBrowseButt.Location = New System.Drawing.Point(525, 16)
            Me.ExeBrowseButt.Name = "ExeBrowseButt"
            Me.ExeBrowseButt.Size = New System.Drawing.Size(26, 23)
            Me.ExeBrowseButt.TabIndex = 6
            Me.ExeBrowseButt.Text = "..."
            Me.ExeBrowseButt.UseVisualStyleBackColor = True
            '
            'ArgumentsTb
            '
            Me.ArgumentsTb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ArgumentsTb.Location = New System.Drawing.Point(104, 91)
            Me.ArgumentsTb.Name = "ArgumentsTb"
            Me.ArgumentsTb.Size = New System.Drawing.Size(447, 20)
            Me.ArgumentsTb.TabIndex = 5
            '
            'WorkingDirTb
            '
            Me.WorkingDirTb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.WorkingDirTb.Location = New System.Drawing.Point(104, 52)
            Me.WorkingDirTb.Name = "WorkingDirTb"
            Me.WorkingDirTb.Size = New System.Drawing.Size(447, 20)
            Me.WorkingDirTb.TabIndex = 4
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Location = New System.Drawing.Point(41, 94)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(57, 13)
            Me.Label6.TabIndex = 3
            Me.Label6.Text = "Arguments"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(6, 55)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(92, 13)
            Me.Label5.TabIndex = 2
            Me.Label5.Text = "Working Directory"
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Location = New System.Drawing.Point(48, 21)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(50, 13)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "Exe Path"
            '
            'ExePathTb
            '
            Me.ExePathTb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ExePathTb.Location = New System.Drawing.Point(104, 18)
            Me.ExePathTb.Name = "ExePathTb"
            Me.ExePathTb.Size = New System.Drawing.Size(418, 20)
            Me.ExePathTb.TabIndex = 0
            '
            'TabPage2
            '
            Me.TabPage2.Controls.Add(Me.StdInSendButt)
            Me.TabPage2.Controls.Add(Me.ClearStdErrorButt)
            Me.TabPage2.Controls.Add(Me.ClearStdOutButt)
            Me.TabPage2.Controls.Add(Me.StdOutTb)
            Me.TabPage2.Controls.Add(Me.StdErrorTb)
            Me.TabPage2.Controls.Add(Me.Label4)
            Me.TabPage2.Controls.Add(Me.Label3)
            Me.TabPage2.Controls.Add(Me.StdInputTb)
            Me.TabPage2.Controls.Add(Me.Label2)
            Me.TabPage2.Location = New System.Drawing.Point(4, 22)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
            Me.TabPage2.Size = New System.Drawing.Size(557, 352)
            Me.TabPage2.TabIndex = 1
            Me.TabPage2.Text = "IO"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'StdInSendButt
            '
            Me.StdInSendButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.StdInSendButt.Location = New System.Drawing.Point(476, 309)
            Me.StdInSendButt.Name = "StdInSendButt"
            Me.StdInSendButt.Size = New System.Drawing.Size(75, 23)
            Me.StdInSendButt.TabIndex = 8
            Me.StdInSendButt.Text = "Send"
            Me.StdInSendButt.UseVisualStyleBackColor = True
            '
            'ClearStdErrorButt
            '
            Me.ClearStdErrorButt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.ClearStdErrorButt.Location = New System.Drawing.Point(11, 162)
            Me.ClearStdErrorButt.Name = "ClearStdErrorButt"
            Me.ClearStdErrorButt.Size = New System.Drawing.Size(67, 23)
            Me.ClearStdErrorButt.TabIndex = 7
            Me.ClearStdErrorButt.Text = "Clear"
            Me.ClearStdErrorButt.UseVisualStyleBackColor = True
            '
            'ClearStdOutButt
            '
            Me.ClearStdOutButt.Location = New System.Drawing.Point(11, 25)
            Me.ClearStdOutButt.Name = "ClearStdOutButt"
            Me.ClearStdOutButt.Size = New System.Drawing.Size(67, 23)
            Me.ClearStdOutButt.TabIndex = 6
            Me.ClearStdOutButt.Text = "Clear"
            Me.ClearStdOutButt.UseVisualStyleBackColor = True
            '
            'StdOutTb
            '
            Me.StdOutTb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.StdOutTb.Location = New System.Drawing.Point(89, 6)
            Me.StdOutTb.Multiline = True
            Me.StdOutTb.Name = "StdOutTb"
            Me.StdOutTb.ReadOnly = True
            Me.StdOutTb.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.StdOutTb.Size = New System.Drawing.Size(462, 131)
            Me.StdOutTb.TabIndex = 5
            '
            'StdErrorTb
            '
            Me.StdErrorTb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.StdErrorTb.Location = New System.Drawing.Point(89, 143)
            Me.StdErrorTb.Multiline = True
            Me.StdErrorTb.Name = "StdErrorTb"
            Me.StdErrorTb.ReadOnly = True
            Me.StdErrorTb.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.StdErrorTb.Size = New System.Drawing.Size(462, 157)
            Me.StdErrorTb.TabIndex = 4
            '
            'Label4
            '
            Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label4.AutoSize = True
            Me.Label4.Location = New System.Drawing.Point(8, 146)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(75, 13)
            Me.Label4.TabIndex = 3
            Me.Label4.Text = "Standard Error"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(8, 9)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(70, 13)
            Me.Label3.TabIndex = 2
            Me.Label3.Text = "Standard Out"
            '
            'StdInputTb
            '
            Me.StdInputTb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.StdInputTb.Location = New System.Drawing.Point(89, 306)
            Me.StdInputTb.Multiline = True
            Me.StdInputTb.Name = "StdInputTb"
            Me.StdInputTb.Size = New System.Drawing.Size(381, 40)
            Me.StdInputTb.TabIndex = 1
            '
            'Label2
            '
            Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(6, 309)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(77, 13)
            Me.Label2.TabIndex = 0
            Me.Label2.Text = "Standard Input"
            '
            'MainDialog
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(589, 402)
            Me.Controls.Add(Me.TabControl1)
            Me.Name = "MainDialog"
            Me.Text = "Process Test1"
            Me.TabControl1.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            Me.TabPage1.PerformLayout()
            Me.TabPage2.ResumeLayout(False)
            Me.TabPage2.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents ExePathTb As System.Windows.Forms.TextBox
        Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
        Friend WithEvents StdInSendButt As System.Windows.Forms.Button
        Friend WithEvents ClearStdErrorButt As System.Windows.Forms.Button
        Friend WithEvents ClearStdOutButt As System.Windows.Forms.Button
        Friend WithEvents StdOutTb As System.Windows.Forms.TextBox
        Friend WithEvents StdErrorTb As System.Windows.Forms.TextBox
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents StdInputTb As System.Windows.Forms.TextBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents ExeBrowseButt As System.Windows.Forms.Button
        Friend WithEvents ArgumentsTb As System.Windows.Forms.TextBox
        Friend WithEvents WorkingDirTb As System.Windows.Forms.TextBox
        Friend WithEvents LaunchExeButt As System.Windows.Forms.Button

    End Class
End Namespace