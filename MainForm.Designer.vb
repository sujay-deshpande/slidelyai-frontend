<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BtnViewSubmissions = New System.Windows.Forms.Button()
        Me.BtnCreateSubmission = New System.Windows.Forms.Button()
        Me.BtnSearchSubmissions = New System.Windows.Forms.Button()
        Me.SuspendLayout()

        Me.BtnViewSubmissions.Location = New System.Drawing.Point(12, 12)
        Me.BtnViewSubmissions.Name = "BtnViewSubmissions"
        Me.BtnViewSubmissions.Size = New System.Drawing.Size(150, 23)
        Me.BtnViewSubmissions.TabIndex = 0
        Me.BtnViewSubmissions.Text = "View Submissions (CTRL+V)"
        Me.BtnViewSubmissions.UseVisualStyleBackColor = True

        Me.BtnCreateSubmission.Location = New System.Drawing.Point(12, 41)
        Me.BtnCreateSubmission.Name = "BtnCreateSubmission"
        Me.BtnCreateSubmission.Size = New System.Drawing.Size(150, 35)
        Me.BtnCreateSubmission.TabIndex = 1
        Me.BtnCreateSubmission.Text = "Create Submission (CTRL+N)"
        Me.BtnCreateSubmission.UseVisualStyleBackColor = True

        Me.BtnSearchSubmissions.Location = New System.Drawing.Point(12, 70)
        Me.BtnSearchSubmissions.Name = "BtnSearchSubmissions"
        Me.BtnSearchSubmissions.Size = New System.Drawing.Size(150, 23)
        Me.BtnSearchSubmissions.TabIndex = 2
        Me.BtnSearchSubmissions.Text = "Search (CTRL+S)"
        Me.BtnSearchSubmissions.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.BtnCreateSubmission)
        Me.Controls.Add(Me.BtnViewSubmissions)
        Me.Controls.Add(Me.BtnSearchSubmissions)
        Me.Name = "MainForm"
        Me.Text = "Sujay Deshpande Task 2 - Slidely Form App"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnViewSubmissions As System.Windows.Forms.Button
    Friend WithEvents BtnCreateSubmission As System.Windows.Forms.Button
    Friend WithEvents BtnSearchSubmissions As System.Windows.Forms.Button


End Class
