<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Encrypt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.WarningLabel = New System.Windows.Forms.Label()
        Me.TimeLabel = New System.Windows.Forms.Label()
        Me.UpdateProgress = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(6, 133)
        Me.ProgressBar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(496, 24)
        Me.ProgressBar.TabIndex = 5
        '
        'WarningLabel
        '
        Me.WarningLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.875!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WarningLabel.ForeColor = System.Drawing.Color.Red
        Me.WarningLabel.Location = New System.Drawing.Point(6, 5)
        Me.WarningLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.WarningLabel.Name = "WarningLabel"
        Me.WarningLabel.Size = New System.Drawing.Size(496, 26)
        Me.WarningLabel.TabIndex = 6
        Me.WarningLabel.Text = "Please wait for this process to finish before logging out"
        Me.WarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimeLabel
        '
        Me.TimeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimeLabel.ForeColor = System.Drawing.Color.Black
        Me.TimeLabel.Location = New System.Drawing.Point(6, 38)
        Me.TimeLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(496, 88)
        Me.TimeLabel.TabIndex = 7
        Me.TimeLabel.Text = "0%"
        Me.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UpdateProgress
        '
        '
        'Encrypt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(508, 164)
        Me.Controls.Add(Me.TimeLabel)
        Me.Controls.Add(Me.WarningLabel)
        Me.Controls.Add(Me.ProgressBar)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Encrypt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Encryping..."
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressBar As ProgressBar
    Friend WithEvents WarningLabel As Label
    Friend WithEvents TimeLabel As Label
    Friend WithEvents UpdateProgress As Timer
End Class
