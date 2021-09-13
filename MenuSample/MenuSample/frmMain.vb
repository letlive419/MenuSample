Imports System.IO
Public Class frmMain
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        ofdOpen.DefaultExt = "*.txt"
        ofdOpen.Filter = "VB Forms (*.vb) | *.vb|Text files (*.txt)|*.txt|All Files (*.*|*.*"
        ofdOpen.FilterIndex = 1
        ofdOpen.InitialDirectory = Application.StartupPath
        sfdSave.DefaultExt = "*.txt"
        sfdSave.Filter = "VB Forms (*.vb) | *.vb|Text files (*.txt)|*.txt|All Files (*.*|*.*"
        sfdSave.FilterIndex = 1
        sfdSave.InitialDirectory = Application.StartupPath
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()

    End Sub

    Private Sub mnuOpen_Click(sender As Object, e As EventArgs) Handles mnuOpen.Click
        Dim srdTextFile As StreamReader
        ofdOpen.ShowDialog()
        If ofdOpen.FileName = "" Then
            sslStatus.Text = "File open action canceled"
            Exit Sub
        End If

        srdTextFile = File.openText(ofdOpen.FileName)
        sslStatus.Text = ofdOpen.FileName & "Opened"
        While Not srdTextFile.EndOfStream
            lstText.Items.Add(srdTextFile.ReadLine)
        End While
        srdTextFile.Close()
    End Sub

    Private Sub mnuSave_Click(sender As Object, e As EventArgs) Handles mnuSave.Click
        Dim swrTextFile As StreamWriter
        Dim i As Integer
        sfdSave.ShowDialog()
        If sfdSave.FileName = "" Then
            sslStatus.Text = "File save action canceled"
        End If
        swrTextFile = File.CreateText(sfdSave.FileName)
        sspProgress.Minimum = 0
        sspProgress.Maximum = 100
        sspProgress.Step = CInt(lstText.Items.Count / 100)


        For i = 0 To lstText.Items.Count - 1
            swrTextFile.WriteLine(lstText.Items(i).ToString)
            sspProgress.Increment(sspProgress.Step)
        Next
        swrTextFile.Close()
        sslStatus.Text = sfdSave.FileName & " Saved "

    End Sub

    Private Sub mnuCopy_Click(sender As Object, e As EventArgs) Handles mnuCopy.Click
        Clipboard.SetDataObject(txtInput.SelectedText)
        sslStatus.Text = "Copy action"
    End Sub

    Private Sub mnuPaste_Click(sender As Object, e As EventArgs) Handles mnuPaste.Click

        If Clipboard.GetDataObject.GetDataPresent(DataFormats.Text) Then
            txtInput.SelectedText = Clipboard.GetDataObject.GetData(DataFormats.Text)
            sslStatus.Text = "Paste action"
        Else
            sslStatus.Text = "Could not retrieve data from the clipboard"
        End If

    End Sub
    Private Sub mnuCut_Click(sender As Object, e As EventArgs) Handles mnuCut.Click
        Clipboard.SetDataObject(txtInput.SelectedText)
        txtInput.SelectedText = ""
        sslStatus.Text = "Cut Action"
    End Sub
    Private Sub mnuFont_Click(sender As Object, e As EventArgs) Handles mnuFont.Click
        Dim intResult As Integer
        intResult = fntFont.ShowDialog
        If intResult <> DialogResult.Cancel Then
            txtInput.Font = fntFont.Font
            sslStatus.Text = "Font change"
        Else
            sslStatus.Text = "Operation Canceled"
        End If

    End Sub

    Private Sub mnuPrint_Click(sender As Object, e As EventArgs) Handles mnuPrint.Click
        prtPrint.ShowDialog()
    End Sub

    Private Sub mnuRotate_Click(sender As Object, e As EventArgs) Handles mnuRotate.Click
        picDog.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
        picDog.Refresh()
        sslStatus.Text = "Image rotated"



    End Sub

    Private Sub mnuFlip_Click(sender As Object, e As EventArgs) Handles mnuFlip.Click
        picDog.Image.RotateFlip(RotateFlipType.RotateNoneFlipY)
        picDog.Refresh()
        sslStatus.Text = "Image flipped"
    End Sub

    Private Sub mnuAbout_Click(sender As Object, e As EventArgs) Handles mnuAbout.Click
        Dim About As New frmAbout
        AboutBox1.ShowDialog()
    End Sub
End Class
