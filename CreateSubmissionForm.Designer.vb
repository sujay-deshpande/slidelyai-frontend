Partial Class CreateSubmissionForm
    ' Declare controls
    Private WithEvents btnToggleStopwatch As Button
    Private WithEvents lblStopwatchTime As Label
    Private WithEvents txtName As TextBox
    Private WithEvents txtEmail As TextBox
    Private WithEvents txtPhoneNum As TextBox
    Private WithEvents txtGithubLink As TextBox
    Private WithEvents btnSubmit As Button

    Private Sub InitializeComponent()
        Me.btnToggleStopwatch = New Button()
        Me.lblStopwatchTime = New Label()
        Me.txtName = New TextBox()
        Me.txtEmail = New TextBox()
        Me.txtPhoneNum = New TextBox()
        Me.txtGithubLink = New TextBox()
        Me.btnSubmit = New Button()

        ' Initialize the form controls
        ' (You will need to set properties such as location, size, text, etc. here)
        ' Example:
        ' Me.btnToggleStopwatch.Location = New Point(10, 10)
        ' Me.btnToggleStopwatch.Size = New Size(100, 30)
        ' Me.btnToggleStopwatch.Text = "Toggle Stopwatch"

        ' Add controls to the form
        Me.Controls.Add(Me.btnToggleStopwatch)
        Me.Controls.Add(Me.lblStopwatchTime)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPhoneNum)
        Me.Controls.Add(Me.txtGithubLink)
        Me.Controls.Add(Me.btnSubmit)

        ' Add event handlers
        AddHandler Me.btnToggleStopwatch.Click, AddressOf Me.btnToggleStopwatch_Click
        AddHandler Me.btnSubmit.Click, AddressOf Me.btnSubmit_Click
        AddHandler Me.Load, AddressOf Me.CreateSubmissionForm_Load
        AddHandler Me.KeyDown, AddressOf Me.CreateSubmissionForm_KeyDown
    End Sub
End Class
