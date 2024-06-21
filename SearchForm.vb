Imports Newtonsoft.Json.Linq
Imports System.Net.Http

Public Class SearchForm
    Inherits Form

    Private WithEvents txtSearch As TextBox
    Private WithEvents btnSearch As Button
    Private WithEvents btnNext As Button
    Private lblResults As Label

    Private currentPage As Integer = 1
    Private currentSearchTerm As String

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.txtSearch = New TextBox()
        Me.btnSearch = New Button()
        Me.btnNext = New Button()
        Me.lblResults = New Label()

        Me.Text = "Search Submissions"
        Me.ClientSize = New Size(400, 250)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblResults)

        Me.txtSearch.Location = New Point(50, 30)
        Me.txtSearch.Size = New Size(200, 20)

        Me.btnSearch.Text = "Search"
        Me.btnSearch.Location = New Point(260, 30)
        Me.btnSearch.Size = New Size(100, 30)

        Me.btnNext.Text = "Next"
        Me.btnNext.Location = New Point(150, 200)
        Me.btnNext.Size = New Size(100, 30)
        Me.btnNext.Enabled = False

        Me.lblResults.Location = New Point(50, 70)
        Me.lblResults.Size = New Size(300, 120)
        Me.lblResults.BackColor = Color.LightGray
        Me.lblResults.TextAlign = ContentAlignment.MiddleCenter
        Me.lblResults.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        currentPage = 1
        currentSearchTerm = txtSearch.Text.Trim()
        If Not String.IsNullOrEmpty(currentSearchTerm) Then
            Await SearchSubmissionByName(currentSearchTerm, currentPage)
        Else
            MessageBox.Show("Please enter a name to search.")
        End If
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If Not String.IsNullOrEmpty(currentSearchTerm) Then
            currentPage += 1
            Await SearchSubmissionByName(currentSearchTerm, currentPage)
        End If
    End Sub

    Private Async Function SearchSubmissionByName(name As String, page As Integer) As Task
        Using client As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await client.GetAsync($"http://localhost:3000/search?name={name}&page={page}&limit=5")
                If response.IsSuccessStatusCode Then
                    Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                    Dim jsonResponse As JObject = JObject.Parse(responseBody)
                    Dim submissions As JArray = CType(jsonResponse("results"), JArray)
                    Dim pagination As JObject = CType(jsonResponse("pagination"), JObject)

                    If submissions.Count > 0 Then
                        Dim result As String = ""
                        For Each submission In submissions
                            result &= $"Name: {submission("name")}{Environment.NewLine}" &
                                      $"Email: {submission("email")}{Environment.NewLine}" &
                                      $"Phone: {submission("phone")}{Environment.NewLine}" &
                                      $"GitHub: {submission("github_link")}{Environment.NewLine}" &
                                      $"Time: {submission("stopwatch_time")}{Environment.NewLine}{Environment.NewLine}"
                        Next
                        lblResults.Text = result

                        Dim currentPage As Integer = pagination("currentPage")
                        Dim totalPages As Integer = pagination("totalPages")
                        btnNext.Enabled = currentPage < totalPages
                    Else
                        lblResults.Text = "No submissions found."
                        btnNext.Enabled = False
                    End If
                Else
                    MessageBox.Show("Error fetching submissions. Status code: " & response.StatusCode)
                End If
            Catch ex As Exception
                MessageBox.Show("Error fetching submissions: " & ex.Message)
            End Try
        End Using
    End Function
End Class
