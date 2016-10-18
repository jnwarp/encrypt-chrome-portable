Module Data
    Public passHash As String
    Public progress As Integer = 0
    Public userPath As String
    Public username As String
    Public chromeVersion As String = "GoogleChrome"

    Sub EncryptChrome()
        'create backup of old zip file
        If My.Computer.FileSystem.FileExists(Data.userPath + "\chrome.7z") Then
            My.Computer.FileSystem.MoveFile(Data.userPath + "\chrome.7z", Data.userPath + "\chrome-backup.7z", True)
        End If

        Using process As Process = New Process
            Dim program = """" + Data.userPath + "\App\7-Zip\7z.exe"""
            process.StartInfo.FileName = program

            Dim arguments = " a -bsp1 -sdel -mhe=on -r -p" + Data.passHash + " """ + Data.userPath + "\chrome.7z"" ""C:\Users\" + Data.username + "\AppData\Local\" + Data.chromeVersion + "Portable\*"""""
            process.StartInfo.Arguments = arguments

            'Debug.Print(program + arguments)

            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.CreateNoWindow = True

            process.EnableRaisingEvents = True
            AddHandler process.OutputDataReceived, AddressOf OutputHandler

            process.Start()
            process.BeginOutputReadLine()
            process.WaitForExit()

            If process.ExitCode <> 0 Then
                Debug.Print("Encrypting Error!")
                Data.progress = -1
            Else
                Data.progress = 100

                'delete backup if encrytion worked
                If My.Computer.FileSystem.FileExists(Data.userPath + "\chrome-backup.7z") Then
                    My.Computer.FileSystem.DeleteFile(Data.userPath + "\chrome-backup.7z", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    Debug.Print("Delete backup file.")
                End If
            End If
        End Using
    End Sub

    Sub DecryptChrome()
        'delete any files leftover in directory
        If My.Computer.FileSystem.DirectoryExists("C:\Users\" + Data.username + "\AppData\Local\" + Data.chromeVersion + "Portable\") Then
            My.Computer.FileSystem.DeleteDirectory("C:\Users\" + Data.username + "\AppData\Local\" + Data.chromeVersion + "Portable\", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        End If

        Using process As Process = New Process
            Dim program = """" + Data.userPath + "\App\7-Zip\7z.exe"""
            process.StartInfo.FileName = program

            Dim arguments = " x -bsp1 -p" + Data.passHash + " -o""C:\Users\" + Data.username + "\AppData\Local\" + Data.chromeVersion + "Portable\"" """ + Data.userPath + "\chrome.7z"""
            process.StartInfo.Arguments = arguments

            'Debug.Print("cmd " + arguments)

            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.CreateNoWindow = True

            process.EnableRaisingEvents = True
            AddHandler process.OutputDataReceived, AddressOf OutputHandler

            process.Start()
            process.BeginOutputReadLine()
            process.WaitForExit()

            Debug.Print("Exit code: " + process.ExitCode.ToString)
            If process.ExitCode <> 0 Then
                Debug.Print("Decrypting Error!")
                Data.progress = -1
            Else
                Data.progress = 100
            End If
            Debug.Print("Progress: " + Data.progress.ToString)
        End Using
    End Sub
    Sub StartChrome()
        Using process As Process = New Process
            Dim program = """C:\Users\" + Data.username + "\AppData\Local\" + Data.chromeVersion + "Portable\" + Data.chromeVersion + "Portable.exe"""
            process.StartInfo.FileName = program

            Dim arguments = ""
            process.StartInfo.Arguments = arguments

            Debug.Print(program + arguments)

            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.CreateNoWindow = True

            process.Start()
            process.WaitForExit()
        End Using
    End Sub
    Sub OutputHandler(sender As Object, e As DataReceivedEventArgs)
        If Not String.IsNullOrEmpty(e.Data) Then
            'update the progress if output is collected
            Dim location = InStr(e.Data, "%")
            If location > 2 Then
                Data.progress = Int(e.Data.Substring(1, location - 2))
            End If

            'print command line output of program
            Debug.Print(e.Data)
        End If
    End Sub

    Sub RestartComputer()
        Using process As Process = New Process
            Dim program = """C:\Windows\System32\shutdown.exe"""
            process.StartInfo.FileName = program

            Dim arguments = " /r /t 3 /c ""Encryption complete, restarting!"""
            process.StartInfo.Arguments = arguments

            Debug.Print(program + arguments)

            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.CreateNoWindow = True

            process.Start()
            process.WaitForExit()
        End Using
    End Sub
End Module
