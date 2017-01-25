Module Data
    Public passHash As String
    Public progress As Integer = 0
    Public userPath As String
    Public username As String
    Public chromeVersion As String = "GoogleChrome"
    Public version As String = "1.1.0"
    Public tempDir As String

    Sub EncryptChrome()
        'reset progress to 0
        Data.progress = 0

        'create backup of old zip file
        If My.Computer.FileSystem.FileExists(Data.userPath + "\chrome.7z") Then
            System.IO.File.Copy(
                Data.userPath + "\chrome.7z",
                Data.userPath + "\chrome-backup.7z",
                True)
        End If

        'delete the old temporary file
        If My.Computer.FileSystem.FileExists(Data.tempDir + "\chrome-encrypt.7z") Then
            System.IO.File.Delete(Data.tempDir + "\chrome-encrypt.7z")
        End If

        Using process As Process = New Process
            Dim program = """" + Data.userPath + "\App\7-Zip\7z.exe"""
            process.StartInfo.FileName = program

            Dim arguments = " a -bsp1 -sdel -mhe=on -r -p" + Data.passHash + " " + Data.tempDir + "\chrome-encrypt.7z " + Data.tempDir + "\StartChrome\*"
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
                'copy chrome back to normal location
                System.IO.File.Delete(Data.userPath + "\chrome.7z")
                My.Computer.FileSystem.CopyFile(
                    Data.tempDir + "\chrome-encrypt.7z",
                    Data.userPath + "\chrome.7z",
                    Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                    Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                Data.progress = 100
            End If
        End Using
    End Sub

    Sub DecryptChrome()
        'reset progress to 0
        Data.progress = 0

        'delete any files leftover in directory
        If My.Computer.FileSystem.DirectoryExists(Data.tempDir + "\" + Data.chromeVersion + "Portable\") Then
            My.Computer.FileSystem.DeleteDirectory(Data.tempDir + "\" + Data.chromeVersion + "Portable\", FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        End If

        'copy zip file to temporary directory
        My.Computer.FileSystem.CopyFile(
            Data.userPath + "\chrome.7z",
            Data.tempDir + "\chrome.7z",
            Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
            Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)

        Using process As Process = New Process
            Dim program = "" + Data.userPath + "\App\7-Zip\7z.exe"
            process.StartInfo.FileName = program

            Dim arguments = " x -bsp1 -p" + Data.passHash + " -o" + Data.tempDir + "\StartChrome\ " + Data.tempDir + "\chrome.7z"
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
            Dim program = "" + Data.tempDir + "\StartChrome\" + Data.chromeVersion + "Portable.exe"
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
