Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Inherits Form

    Private stopwatch As New Stopwatch()

    Private WithEvents btnSubmit As Button
    Private WithEvents btnToggleStopwatch As Button
    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhoneNum As TextBox
    Private txtGithubLink As TextBox
    Private lblStopwatchTime As Label

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.btnSubmit = New Button()
        Me.btnToggleStopwatch = New Button()
        Me.txtName = New TextBox()
        Me.txtEmail = New TextBox()
        Me.txtPhoneNum = New TextBox()
        Me.txtGithubLink = New TextBox()
        Me.lblStopwatchTime = New Label()

        Me.Text = "Create Submission"
        Me.ClientSize = New Size(400, 300)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.btnToggleStopwatch)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtPhoneNum)
        Me.Controls.Add(Me.txtGithubLink)
        Me.Controls.Add(Me.lblStopwatchTime)

        Me.btnSubmit.Text = "SUBMIT (CTRL + S)"
        Me.btnSubmit.Location = New Point(150, 250)
        Me.btnSubmit.Size = New Size(100, 30)
        Me.btnSubmit.BackColor = Color.LightBlue

        Me.btnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
        Me.btnToggleStopwatch.Location = New Point(50, 200)
        Me.btnToggleStopwatch.Size = New Size(300, 30)

        Dim labels = {"Name", "Email", "Phone Num", "Github Link"}
        Dim textboxes = {Me.txtName, Me.txtEmail, Me.txtPhoneNum, Me.txtGithubLink}
        For i As Integer = 0 To labels.Length - 1
            Dim label = New Label()
            label.Text = labels(i)
            label.Location = New Point(50, 30 + 40 * i)
            label.Size = New Size(100, 20)
            Me.Controls.Add(label)
            textboxes(i).Location = New Point(150, 30 + 40 * i)
            textboxes(i).Size = New Size(200, 20)
        Next

        Me.lblStopwatchTime.Text = "00:00:00"
        Me.lblStopwatchTime.Location = New Point(150, 170)
        Me.lblStopwatchTime.Size = New Size(100, 20)
        Me.lblStopwatchTime.BackColor = Color.LightGray
        Me.lblStopwatchTime.TextAlign = ContentAlignment.MiddleCenter

        Me.KeyPreview = True
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
        Else
            stopwatch.Start()
        End If
        UpdateStopwatchLabel()
    End Sub



    Private Sub UpdateStopwatchLabel()
        lblStopwatchTime.Text = String.Format("{0:00}:{1:00}:{2:00}", stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds)
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim submission As New Dictionary(Of String, String) From {
            {"name", txtName.Text},
            {"email", txtEmail.Text},
            {"phone", txtPhoneNum.Text},
            {"github_link", txtGithubLink.Text},
            {"stopwatch_time", lblStopwatchTime.Text}
        }

        Using client As New HttpClient()
            Try
                Dim content As New StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = Await client.PostAsync("http://localhost:3000/submit", content)

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission successful!")
                Else
                    Dim errorMessage As String = $"Submission failed with status code {response.StatusCode}."
                    MessageBox.Show(errorMessage)
                End If
            Catch ex As Exception
                MessageBox.Show($"An error occurred: {ex.Message}")
            End Try
        End Using
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.T Then
            btnToggleStopwatch.PerformClick()
        End If
    End Sub
End Class
