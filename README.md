Encrypt Chrome Portable
-----------------------

This is a simple program that will use 7zip portable to encrypt a portable version of Chrome.

Install
=======

Steps to install
 - Download [7Zip portable][7z]
 - Extract 7Zip portable into a folder like `Desktop\Chrome`
 - Download the `StartChrome.exe` into this folder
 - Download [Chromium][Cr] or [Chrome][GC] portable
 - Extract it to `C:\Users\%USERNAME%\AppData\Local\ChomiumPortable`
 - Run `StartChrome.exe` and type `!password` with the `!` before your password

How to use
 - Type `!` before your password to encrypt the data for the first time
 - Type `!` ***after*** your current password to show the prompt to set a new password
 - Double click the "Please do not log off" label to abort the automatic reboot


Details
=======

When a password is entered:
 - `plainPass` is converted to a byte array
 - `passBytes` is hashed using the built in SHA512 algorithm
 - `passHash` is stored as a string using Base64 encoding
 - `plainPass` and `passBytes` are set to dummy strings
 - `passHash` is sent to 7zip portable as the password

7Zip command line flags:
 - Encrypt
  - `7z.exe a -bsp1 -sdel -mhe=on -r -p%passHash% "%userPath%\chrome.7z" "C:\Users\%username%\AppData\Local\%chromeVersion%Portable\*"`
 - Decrypt
  - `7z.exe x -bsp1 -p%passHash% -o"C:\Users\%username%\AppData\Local\%chromeVersion%Portable\" "%userPath%\chrome.7z"`

To change the version of Chrome that is used:
 - open the `StartChrome\Data.vb\` file and look for this line:
  - `Public chromeVersion As String = "Chromium"`
 - change `Chromium` to the version you want


[7z]: http://portableapps.com/apps/utilities/7-zip_portable
[Cr]: http://crportable.sourceforge.net/
[GC]: http://portableapps.com/apps/internet/google_chrome_portable
