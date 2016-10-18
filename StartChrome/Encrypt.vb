Imports System.IO
Imports System.ComponentModel

Public Class Encrypt
    Dim restart = True
    Private Sub Encrypt_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
    End Sub

    Private Sub Encrypt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'get the current path and username
        Data.userPath = Directory.GetCurrentDirectory()
        Data.username = Environment.UserName
        Debug.Print(Data.userPath)
        Debug.Print(Data.username)

        'prompt for password to decrypt chrome
        Decrypt.ShowDialog()
        Data.StartChrome()

        'reset the progress back to 0 and begin encrypting chrome
        Data.progress = 0
        UpdateProgress.Start()
        System.Threading.ThreadPool.QueueUserWorkItem(AddressOf Data.EncryptChrome)
    End Sub

    Private Sub UpdateProgress_Tick(sender As Object, e As EventArgs) Handles UpdateProgress.Tick
        If Data.progress < 0 Then
            If TimeLabel.Text <> "ERROR" Then
                TimeLabel.Text = "ERROR"
                ProgressBar.Value = 100
            ElseIf ProgressBar.Value = 0 Then
                End
            Else
                ProgressBar.Value -= 1
            End If
        ElseIf Data.progress = 100 Then
            If restart Then
                'reboot computer in order to automatically log out
                Data.RestartComputer()
            End If

            End
        Else
            ProgressBar.Value = Data.progress
            TimeLabel.Text = Data.progress.ToString + "%"
        End If
    End Sub

    Private Sub WarningLabel_DoubleClick(sender As Object, e As EventArgs) Handles WarningLabel.DoubleClick
        restart = False
        WarningLabel.Text = "Automatic logout is now cancelled"
    End Sub
End Class
