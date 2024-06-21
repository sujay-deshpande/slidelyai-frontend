Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class ViewSubmissionsForm
    Inherits Form

    Private currentIndex As Integer = 0
    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button
    Private lblName As Label
    Private lblEmail As Label
    Private lblPhone As Label
    Private WithEvents lblGitHubLink As LinkLabel
    Private lblStopwatchTime As Label

    Public Sub New()
        InitializeComponent()
        FetchAndShowSubmission(currentIndex)
    End Sub

    Private Sub InitializeComponent()
        Me.btnPrevious = New Button()
        Me.btnNext = New Button()
        Me.lblName = New Label()
        Me.lblEmail = New Label()
        Me.lblPhone = New Label()
        Me.lblGitHubLink = New LinkLabel()
        Me.lblStopwatchTime = New Label()

        Me.Text = "View Submissions"
        Me.ClientSize = New Size(400, 300)
        Me.Controls.Add(Me.btnPrevious)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblName)
        Me.Controls.Add(Me.lblEmail)
        Me.Controls.Add(Me.lblPhone)
        Me.Controls.Add(Me.lblGitHubLink)
        Me.Controls.Add(Me.lblStopwatchTime)

        Me.btnPrevious.Text = "Previous (CTRL+P)"
        Me.btnPrevious.Location = New Point(50, 250)
        Me.btnPrevious.Size = New Size(140, 30)

        Me.btnNext.Text = "Next (CTRL+N)"
        Me.btnNext.Location = New Point(250, 250)
        Me.btnNext.Size = New Size(140, 30)

        Dim labelWidth As Integer = 300
        Dim labelHeight As Integer = 30
        Dim startY As Integer = 50
        Dim gapY As Integer = 10

        Me.lblName.Location = New Point(50, startY)
        Me.lblName.Size = New Size(labelWidth, labelHeight)
        Me.lblName.BorderStyle = BorderStyle.FixedSingle

        Me.lblEmail.Location = New Point(50, startY + labelHeight + gapY)
        Me.lblEmail.Size = New Size(labelWidth, labelHeight)
        Me.lblEmail.BorderStyle = BorderStyle.FixedSingle

        Me.lblPhone.Location = New Point(50, startY + 2 * (labelHeight + gapY))
        Me.lblPhone.Size = New Size(labelWidth, labelHeight)
        Me.lblPhone.BorderStyle = BorderStyle.FixedSingle

        Me.lblGitHubLink.Location = New Point(50, startY + 3 * (labelHeight + gapY))
        Me.lblGitHubLink.Size = New Size(labelWidth, labelHeight)
        Me.lblGitHubLink.BorderStyle = BorderStyle.FixedSingle
        Me.lblGitHubLink.AutoSize = True

        Me.lblStopwatchTime.Location = New Point(50, startY + 4 * (labelHeight + gapY))
        Me.lblStopwatchTime.Size = New Size(labelWidth, labelHeight)
        Me.lblStopwatchTime.BorderStyle = BorderStyle.FixedSingle

        Dim padding As New Padding(5)

        Me.lblName.Padding = padding
        Me.lblEmail.Padding = padding
        Me.lblPhone.Padding = padding
        Me.lblGitHubLink.Padding = padding
        Me.lblStopwatchTime.Padding = padding
    End Sub

    Private Async Sub FetchAndShowSubmission(index As Integer)
        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync($"http://localhost:3000/read?index={index}")
                If response.IsSuccessStatusCode Then
                    Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                    Dim submission As JObject = JObject.Parse(responseBody)

                    lblName.Text = $"Name: {submission("name")}"
                    lblEmail.Text = $"Email: {submission("email")}"
                    lblPhone.Text = $"Phone Number: {submission("phone")}"
                    lblGitHubLink.Text = $"GitHub Link: {submission("github_link")}"
                    lblGitHubLink.Links.Clear()
                    lblGitHubLink.Links.Add(0, lblGitHubLink.Text.Length, submission("github_link").ToString())
                    lblStopwatchTime.Text = $"Stopwatch Time: {submission("stopwatch_time")}"

                    currentIndex = index
                Else
                    MessageBox.Show("No more submissions available.")
                End If
            Catch ex As Exception
                MessageBox.Show("Error fetching submission: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        NavigatePrevious()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        NavigateNext()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.N) Then
            NavigateNext()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.P) Then
            NavigatePrevious()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub NavigatePrevious()
        If currentIndex > 0 Then
            FetchAndShowSubmission(currentIndex - 1)
        Else
            MessageBox.Show("This is the first submission.")
        End If
    End Sub

    Private Sub NavigateNext()
        FetchAndShowSubmission(currentIndex + 1)
    End Sub

    Private Sub lblGitHubLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblGitHubLink.LinkClicked
        Process.Start(New ProcessStartInfo(e.Link.LinkData.ToString()) With {.UseShellExecute = True})
    End Sub
End Class
