Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.Net.Http

Public Class MainForm
    Private Async Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim heading As New Label()
        heading.Text = "Sujay Deshpande Slidely Task 2 - Slidely Form App"
        heading.Font = New Font("Arial", 9, FontStyle.Bold)
        heading.AutoSize = True
        heading.Location = New Point((Me.ClientSize.Width - heading.Width) \ 16, 5)
        Me.Controls.Add(heading)

        CenterButtons()

        Try
            Dim hasSubmissions As Boolean = Await CheckForSubmissions()
            If Not hasSubmissions Then
                MessageBox.Show("No submissions found.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading submissions: " & ex.Message)
        End Try
    End Sub

    Private Async Function CheckForSubmissions() As Task(Of Boolean)
        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/read?index=0")
                If response.IsSuccessStatusCode Then
                    Return True
                Else
                    If response.StatusCode = HttpStatusCode.NotFound Then
                        Return False
                    Else
                        MessageBox.Show("Error fetching submissions. Status code: " & response.StatusCode)
                        Return False
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("Error fetching submissions: " & ex.Message)
                Return False
            End Try
        End Using
    End Function

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles BtnViewSubmissions.Click
        Dim viewSubmissionsForm As New ViewSubmissionsForm()
        viewSubmissionsForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles BtnCreateSubmission.Click
        Dim submissionForm As New CreateSubmissionForm()
        submissionForm.Show()
    End Sub

    Private Sub btnSearchSubmissions_Click(sender As Object, e As EventArgs) Handles BtnSearchSubmissions.Click
        Dim searchForm As New SearchForm()
        searchForm.Show()
    End Sub

    Private Sub CenterButtons()
        Dim buttons As New List(Of Button) From {BtnViewSubmissions, BtnCreateSubmission, BtnSearchSubmissions}
        Dim yPosition As Integer = Me.ClientSize.Height \ 2 - (buttons.Count * (buttons(0).Height + 10)) \ 2

        For Each btn As Button In buttons
            btn.Location = New Point((Me.ClientSize.Width - btn.Width) \ 2, yPosition)
            yPosition += btn.Height + 10
            Me.Controls.Add(btn)
        Next
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.V) Then
            BtnViewSubmissions.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            BtnCreateSubmission.PerformClick()
            Return True
        ElseIf keyData = (Keys.Control Or Keys.S) Then
            BtnSearchSubmissions.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
