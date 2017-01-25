Imports System.IO


Public Class MergePdf
    Public Shared Sub ProcessDirectory(ByVal targetDirectory As String)
        Dim fileEntries As String() = Directory.GetFiles(targetDirectory, "*.pdf")
        ' Process the list of files found in the directory.
        'Dim fileName As String
        'For Each fileName In fileEntries
        '    ProcessFile(fileName)
        System.Array.Sort(Of String)(fileEntries)
        If fileEntries.Length > 0 Then
            BuildMultiPagePDF(fileEntries, targetDirectory & "\polaczony.pdf")

        End If
        'Next fileName
        Dim subdirectoryEntries As String() = Directory.GetDirectories(targetDirectory)
        ' Recurse into subdirectories of this directory.
        Dim subdirectory As String
        For Each subdirectory In subdirectoryEntries
            ProcessDirectory(subdirectory)
        Next subdirectory
    End Sub

    Public Shared Sub BuildMultiPagePDF(ByVal fileArray As String(), ByVal outPutPDF As String)
        Try

            Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
            Dim pageCount As Integer = 0
            Dim currentPage As Integer = 0
            Dim pdfDoc As iTextSharp.text.Document = Nothing
            Dim writer As iTextSharp.text.pdf.PdfCopy = Nothing
            Dim page As iTextSharp.text.pdf.PdfImportedPage = Nothing
            Dim currentPDF As Integer = 0
            Dim keywords As String = Nothing

            If fileArray.Length > 0 Then

                reader = New iTextSharp.text.pdf.PdfReader(fileArray(currentPDF))
                pdfDoc = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(1))
                writer = New iTextSharp.text.pdf.PdfCopy(pdfDoc,
                                                      New System.IO.FileStream(outPutPDF,
                                                      System.IO.FileMode.OpenOrCreate,
                                                      System.IO.FileAccess.Write))

                pageCount = reader.NumberOfPages

                While currentPDF < fileArray.Length
                    pdfDoc.Open()

                    While currentPage < pageCount
                        currentPage += 1
                        pdfDoc.SetPageSize(reader.GetPageSizeWithRotation(currentPage))
                        pdfDoc.NewPage()
                        pdfDoc.AddKeywords(keywords)
                        page = writer.GetImportedPage(reader, currentPage)
                        writer.AddPage(page)
                    End While

                    currentPDF += 1
                    If currentPDF < fileArray.Length Then
                        reader = New iTextSharp.text.pdf.PdfReader(fileArray(currentPDF))
                        pageCount = reader.NumberOfPages
                        currentPage = 0
                    End If
                End While

                pdfDoc.Close()
                pdfDoc.Dispose()
            Else
                MessageBox.Show("The input file array is empty.  Processing terminated.",
                                "INVALID FILE LIST",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
