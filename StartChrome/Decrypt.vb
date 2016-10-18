Imports System.ComponentModel
Imports System.Security.Cryptography
Public Class Decrypt
    Dim encryptMode = False
    Dim changePass = False

    Private Sub Decrypt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PasswordBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PasswordBox.KeyDown
        'only try password if enter key pressed
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PasswordLabel.Text = "Decrypting..."
            PasswordBox.Hide()
            ProgressBar.Show()

            'get plain password and destroy entered text
            Dim plainPass = PasswordBox.Text
            PasswordBox.Text = ""

            'exit program if nothing entered
            If plainPass = "" Then
                End
            ElseIf Mid(plainPass, 1, 1) = "!" Then
                encryptMode = True
                PasswordLabel.Text = "Encrypting..."
                plainPass = Mid(plainPass, 2)
            ElseIf encryptMode Then
                PasswordLabel.Text = "Encrypting..."
            ElseIf Mid(plainPass, Len(plainPass), 1) = "!" Then
                Debug.Print("ChangePass")
                changePass = True
                plainPass = Mid(plainPass, 1, Len(plainPass) - 1)
            End If

            'compute the hash of the password
            Dim passBytes() As Byte =
                System.Text.Encoding.Unicode.GetBytes(plainPass)
            Data.passHash =
                Convert.ToBase64String(New SHA512CryptoServiceProvider().ComputeHash(passBytes))

            'clear temporary variables
            plainPass = "nothingtoseehere"
            passBytes = System.Text.Encoding.Unicode.GetBytes(plainPass)

            'start encrypting/decrypting
            If encryptMode Then
                UpdateProgress.Start()
                System.Threading.ThreadPool.QueueUserWorkItem(AddressOf Data.EncryptChrome)
            Else
                UpdateProgress.Start()
                System.Threading.ThreadPool.QueueUserWorkItem(AddressOf Data.DecryptChrome)
            End If
        End If
    End Sub

    Private Sub UpdateProgress_Tick(sender As Object, e As EventArgs) Handles UpdateProgress.Tick
        If Data.progress < 0 Then
            UpdateProgress.Stop()
            ProgressBar.Hide()
            PasswordBox.Show()
            PasswordLabel.Text = "Enter &password: "
        ElseIf Data.progress = 100 Then
            If encryptMode Then
                End
            ElseIf changePass Then
                UpdateProgress.Stop()
                changePass = False
                encryptMode = True
                ProgressBar.Hide()
                PasswordBox.Show()
                PasswordLabel.Text = "Enter new &password: "
            Else
                UpdateProgress.Stop()
                Me.Close()
            End If
        Else
            ProgressBar.Value = Data.progress
        End If
    End Sub

    Private Sub Decrypt_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = UpdateProgress.Enabled
    End Sub
End Class
