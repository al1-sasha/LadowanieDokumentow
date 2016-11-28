Option Explicit On
Imports System.Runtime.InteropServices
Imports FirebirdSql.Data.FirebirdClient
Imports System
Imports System.IO
Imports ADODB
Imports OsrFB
Imports System.Text
Imports System.IO.Compression
Imports System.String
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports Ladowanie_Dokumentow.MergePdf
Imports System.Text.RegularExpressions



Public Class LadowanieD
    Dim OperatGrid As New DataSet
    Dim OperDoc As New DataSet
    Dim DokumentBaza As New DataSet
    Dim BlobGrid As New DataSet
    Dim Polaczenie As New FbConnection
    Dim PolaczenieFDB As New FbConnection
    Dim fb_string As FbConnectionStringBuilder = New FbConnectionStringBuilder
    Const lacznik = """"
    Dim SciezkaPliki As String
    Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
    Dim polaczPdf As New MergePdf

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
    End Sub



    <DllImport(".\ZIP.DLL", EntryPoint:="Compress")> _
    Private Shared Function CompressByteArray(ByVal dest As Byte(), ByRef destLen As Integer, ByVal src As Byte(), ByVal srcLen As Integer) As Integer
        ' Leave function empty - DLLImport attribute forwards calls to CompressByteArray to compress in zlib.dLL
    End Function
    <DllImport(".\ZIP.DLL", EntryPoint:="UnCompress")> _
    Private Shared Function UncompressByteArray(ByVal dest As Byte(), ByRef destLen As Integer, ByVal src As Byte(), ByVal srcLen As Integer) As Integer
        ' Leave function empty - DLLImport attribute forwards calls to UnCompressByteArray to Uncompress in zlib.dLL
    End Function


    Public Function CompressBytes(ByRef Dataa() As Byte, Optional ByRef TempBuffer() As Byte = Nothing) As Integer
        'Compresses Data into a temp buffer
        'Returns compressed Data in Data if TempBuff not specified
        'Returns Result = Size of compressed data if ok, -1 if not
        Dim OriginalSize As Long = UBound(Dataa) + 1
        'Allocate temporary Byte Array for storage
        Dim result As Integer
        Dim usenewstorage As Boolean
        If TempBuffer Is Nothing Then usenewstorage = False Else usenewstorage = True
        Dim BufferSize As Integer = UBound(Dataa) + 1 'Integer
        BufferSize = CInt(BufferSize + (BufferSize * 0.01) + 12)
        'BufferSize = CInt(BufferSize + (BufferSize * 10.01) + 12)
        ReDim TempBuffer(BufferSize)
        'Compress data byte array
        result = CompressByteArray(TempBuffer, BufferSize, Dataa, UBound(Dataa) + 1)
        'Store results
        If result = 0 Then
            If usenewstorage Then
                'Return results in TempBuffer
                ReDim Preserve TempBuffer(BufferSize - 1)
            Else
                'Return compressed Data in original Data Array
                '      Resize original data array to compressed size
                ReDim Dataa(BufferSize - 1)
                '       Copy Array to original data array
                Array.Copy(TempBuffer, Dataa, BufferSize)
                'Release TempBuffer STorage
                TempBuffer = Nothing
            End If
            Return BufferSize
        Else
            Return -1
        End If
        'ReDim Preserve Dataa(BufferSize + 4)
        'CopyMemory(Dataa, OriginalSize, 4)
    End Function
    Public Function DeCompressBytes(ByRef Dataa() As Byte, ByVal Origsize As Integer, Optional ByRef TempBuffer() As Byte = Nothing) As Integer
        'DeCompresses Data into a temp buffer..note that Origsize must be the size of the original data before compression
        'Returns compressed Data in Data if TempBuff not specified
        'Returns Result = Size of decompressed data if ok, -1 if not
        'Allocate memory for buffers
        Dim result As Integer
        Dim usenewstorage As Boolean
        Dim Buffersize As Integer = (Origsize + (Origsize * 0.01) + 12) 'Integer
        'Dim Buffersize As Integer = CInt(Origsize + (Origsize * 10.01) + 12) 'Integer
        If TempBuffer Is Nothing Then usenewstorage = False Else usenewstorage = True
        ReDim TempBuffer(Buffersize)

        'Decompress data
        result = UncompressByteArray(TempBuffer, Origsize, Dataa, UBound(Dataa) + 1)

        'Truncate buffer to compressed size
        If result = 0 Then
            If usenewstorage Then
                'Return decoompressed data in TempBuffer
                ReDim Preserve TempBuffer(Origsize - 1)
            Else
                'Return decompressed data in original source data file
                '       Truncate to compressed size
                ReDim Dataa(Origsize - 1)
                '       Copy Array to original data array
                Array.Copy(TempBuffer, Dataa, Origsize)
                'Release TempBuffer STorage
                TempBuffer = Nothing
            End If
            Return Origsize
        Else
            Return -1
        End If
    End Function


    Public Sub Polacz()
        ' Tworzenie połączenia z bazą FDR
        fb_string.ServerType = FbServerType.Default
        fb_string.UserID = "SYSDBA"
        fb_string.Password = "masterkey"
        fb_string.Dialect = 3
        fb_string.Database = sciezka.Text
        fb_string.Pooling = False
        Try
            Polaczenie.ConnectionString = fb_string.ToString
            Polaczenie.Open()

            If Polaczenie.State = ConnectionState.Open Then
                Connect.BackColor = Color.Green
                Connect.Enabled = False
                'MsgBox("Połączenie z[" & fb_string.Database & "] zostało ustanowione")
            End If

        Catch err As FbException
            MsgBox("Error: Błąd połączenia [" & fb_string.Database & "]")
            MsgBox(err.Message)
        End Try

    End Sub
    Public Sub PolaczFDB()
        ' Tworzenie połączenia do bazy FDB
        fb_string.ServerType = FbServerType.Default
        fb_string.UserID = "SYSDBA"
        fb_string.Password = "masterkey"
        fb_string.Dialect = 3
        fb_string.Database = SciezkaFDB.Text
        fb_string.Pooling = False
        Try
            PolaczenieFDB.ConnectionString = fb_string.ToString
            PolaczenieFDB.Open()

            If PolaczenieFDB.State = ConnectionState.Open Then
                Connect.BackColor = Color.Green
                Connect.Enabled = False
                'MsgBox("Połączenie z[" & fb_string.Database & "] zostało ustanowione")
            End If

        Catch err As FbException
            MsgBox("Error: Błąd połączenia [" & fb_string.Database & "]")
            MsgBox(err.Message)
        End Try

    End Sub

    Public Sub Rozlacz()
        'Rozłączanie z bazą FDR
        If (ConnectionState.Open) Then
            Polaczenie.Close()
            Connect.BackColor = Control.DefaultBackColor
            Connect.Enabled = True
            'MsgBox("Połączenie z [" & fb_string.Database & "] zostało zakończone")
        End If
    End Sub
    Public Sub RozlaczFDB()
        'Rozłączanie z bazą FDB
        If (ConnectionState.Open) Then
            PolaczenieFDB.Close()
            Connect.BackColor = Control.DefaultBackColor
            Connect.Enabled = True
            'MsgBox("Połączenie z [" & fb_string.Database & "] zostało zakończone")
        End If
    End Sub
    Private Sub Connect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Connect.Click
        Call Polacz()
        Call PolaczFDB()

    End Sub
    Private Sub Disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Disconnect.Click
        Call Rozlacz()
        Call RozlaczFDB()
    End Sub

    Public Sub WszystkiePliki() 'Wczytuje wszysktie pliki z rozszerzeniem sql do tabeli
        Dim path As String = System.IO.Path.Combine(SciezkaPliki)
        Dim dir As New System.IO.DirectoryInfo(path)
        For Each File As String In My.Computer.FileSystem.GetFiles(path, FileIO.SearchOption.SearchAllSubDirectories)
            If File.ToLower.Contains(TypPliku.Text) Then
                ListBox1.Items.Add(File)
            End If
        Next
    End Sub

    Public Sub WszystkieFoldery()
        For Each Dir As String In System.IO.Directory.GetDirectories(SciezkaPliki)
            Dim dirInfo As New System.IO.DirectoryInfo(Dir)
            ListBox1.Items.Add(dirInfo.Name)
        Next
    End Sub
  

    Private Sub Bt_laduj_pdf_Click(sender As Object, e As EventArgs) Handles Bt_laduj_pdf.Click
        Dim i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            OperatGrid.Clear()
            BlobGrid.Clear()
            ListBox3.Items.Clear()
            GC.Collect()
            Dim Dataa As Byte() = New Byte() {}
            Dataa = Nothing
            Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
            Try


                Dim plik = My.Computer.FileSystem.GetFileInfo(SciezkaDopliku)
                Dim text As String = plik.Directory.Name

                'Kasowanie numeru tomu z nazwy wsczytanego katalogu
                text = Regex.Replace(text, "_[^_]*$", "")

                'Walidacja nazwy folderu operatu ze wzorcem
                If Not Regex.IsMatch(text, "^\w\.\d{1,4}\.\d{1,4}\.\d{1,5}$", RegexOptions.IgnorePatternWhitespace) Then
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", SciezkaDopliku, True)
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", Environment.NewLine, True)
                    Continue For
                End If

                Dim parts As String() = text.Split(New Char() {"."})
                Dim part As String
                Dim NazwaPliku = plik.Name.ToString

                ListBox2.Items.Add(plik.Directory.Name)
                For Each part In parts

                    ListBox3.Items.Add(part) 'rozbicie nazwy katalogu speparatorem i zapis w liscie
                Next

                Dim C1 = ListBox3.Items(0)
                Dim C2 = ListBox3.Items(1)
                Dim C3 = ListBox3.Items(2)
                Dim C4 = ListBox3.Items(3)
    
                Dim fs As New System.IO.FileStream(SciezkaDopliku, IO.FileMode.Open, IO.FileAccess.Read)



                Dim selekcjaOP As String = ("select uid, typ from operaty where C1 = '" & C1.ToString & "' and C2 = '" & C2.ToString & "' and C3 = '" & C3.ToString & "' and C4 = '" & C4.ToString & "'")
                Dim custDAOP As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                custDAOP.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(selekcjaOP, PolaczenieFDB)
                custDAOP.Fill(OperatGrid, "Operat")

                If OperatGrid.Tables("Operat").Rows.Count = 0 Then
                    My.Computer.FileSystem.WriteAllText(".\BrakOperatu.txt", SciezkaDopliku, True)
                    My.Computer.FileSystem.WriteAllText(".\BrakOperatu.txt", Environment.NewLine, True)
                    Continue For
                End If

                DataGridView2.DataSource = OperatGrid.Tables("Operat")
                Dim operat = DataGridView2.Rows(0).Cells(0).Value.ToString
                Dim typ = DataGridView2.Rows(0).Cells(1).Value.ToString
                Dim rnew As BinaryReader = New BinaryReader(fs)
                Dataa = rnew.ReadBytes(fs.Length)
                'rnew.Close()
                'rnew.Dispose()
                'fs.Close()
                'fs.Dispose()
                'Dataa = Nothing
                'GC.Collect()

                Dim ucomp As Integer = UBound(Dataa) + 1
                Dim compsize As Integer = CompressBytes(Dataa)
                Dim Origsize As Integer = DeCompressBytes(Dataa, ucomp)
                CompressBytes(Dataa)

                Dim cmd As New FirebirdSql.Data.FirebirdClient.FbCommand("Insert into FBDOK (Uid, Tresc, Mini, Dtw, Osow, Dtu, Osou) values (gen_id(FBDOKg, 1), (@Picture), NULL, '2016-01-04 15:43:16', '11', '2016-01-04 15:43:16', NULL);", Polaczenie)
                cmd.Parameters.Add("@Picture", FbDbType.Binary, Dataa.Length).Value = Dataa
                cmd.ExecuteNonQuery()
                Dataa = Nothing

                Dim selekcja As String = ("SELECT gen_id(FBDOKG, 0) FROM RDB$DATABASE")
                Dim custDA As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                custDA.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(selekcja, Polaczenie)
                custDA.Fill(BlobGrid, "Wybrane")
                DataGridView1.DataSource = BlobGrid.Tables("Wybrane")
                Dim BlobID = DataGridView1.Rows(0).Cells(0).Value.ToString()



                Dim cmdOPERDOK As New FirebirdSql.Data.FirebirdClient.FbCommand("INSERT INTO OPERDOK (UID, TYP, ID_OPE, DOKUMENT, ADRES, TYP_DOK, GRUPA, XPPM, YPPM, KOMPRESJA, ORG_WIELK, KAT_AKT, ID_BLOB, DTW, OSOW, DTU, OSOU, NR_DOK, NAZ_DOK) VALUES (gen_id(OPERDOKG, 1), (@atyp), (@aoperat), (@aNazwaPliku), NULL, 3, NULL, NULL, NULL, 1, (@Origsize), NULL, (@aBlobGrid), '2016-02-03 15:32:08', 10, NULL, NULL, NULL, NULL);", PolaczenieFDB)

                cmdOPERDOK.Parameters.Add("@aoperat", FbDbType.Text, operat.Length).Value = operat
                cmdOPERDOK.Parameters.Add("@aNazwaPliku", FbDbType.Text, NazwaPliku.Length).Value = NazwaPliku
                cmdOPERDOK.Parameters.Add("@Origsize", FbDbType.Integer).Value = Origsize
                cmdOPERDOK.Parameters.Add("@aBlobGrid", FbDbType.Text, BlobID.Length).Value = BlobID
                cmdOPERDOK.Parameters.Add("@atyp", FbDbType.Text, typ.Length).Value = typ
                My.Computer.FileSystem.WriteAllText(".\Zaladowane.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\Zaladowane.txt", Environment.NewLine, True)
                cmdOPERDOK.ExecuteNonQuery()

                Dim j As Integer = ((i + 1) / ListBox1.Items.Count) * 100

                ProgressBar1.Value = j
                ProcLadowania.Text = "Załadowano " & ProgressBar1.Value & "%"
                ProcLadowania.Refresh()
                Me.Refresh()
                rnew.Close()
                rnew.Dispose()
                fs.Close()
                fs.Dispose()
                Dataa = Nothing
                GC.Collect()

            Catch ex As Exception
                'Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                File.WriteAllText(".\Bledy.txt", ex.ToString)
                My.Computer.FileSystem.WriteAllText(".\NieZaladowane.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\NieZaladowane.txt", Environment.NewLine, True)

            End Try

            GC.Collect()
          
        Next
        MsgBox("Załadowano")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ListBox1.HorizontalScrollbar = True
        Try
            FolderBrowserDialog1.ShowDialog()
            SciezkaPliki = FolderBrowserDialog1.SelectedPath
            ListBox1.Items.Clear()
            Call WszystkiePliki()
            Lb_LiczbaPlikow.Text = ListBox1.Items.Count.ToString
        Catch ex As Exception
            MsgBox("Wybierz folder z plikami do przetwarzania")
        End Try
    End Sub



   

    

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        MsgBox("Koniec")
        ProgressBar1.Value = ProgressBar1.Minimum
    End Sub
    

    Private Sub BackgroundWorker2_ProgressChanged(sender As Object, e As ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker2.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub


    Private Sub KoniecToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KoniecToolStripMenuItem.Click
        Me.Dispose()
    End Sub

    Private Sub OProgramieToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OProgramieToolStripMenuItem.Click
        Aboutbox.ShowDialog()
    End Sub

    Private Function CheckDate(ByVal dateToCheck As Date) As Boolean
        'http://www.vbforums.com/showthread.php?621068-Trial-Period-code-VB-NET
        'In reality, CheckDate would get the date (current date) itself and not have it passed in
        Dim retValue As Boolean = False 'Fail safe, default to false
        Dim usageDatesLeft As Int16 = 180 ' set it to 4 just for testing
        'Dim usageDatesLeft As Int16 = 30 ' set this to the number of days of application access 

        'Hash the date
        Dim hashedDate As String = HashDate(dateToCheck)
        'Check to see if the hash value exists in the UsageDates

        'Initialize the container if necessary
        If My.Settings.UsageDates Is Nothing Then
            My.Settings.UsageDates = New System.Collections.Specialized.StringCollection
        End If

        If My.Settings.UsageDates.Contains(hashedDate) Then
            'then we are ok...  it's already been checked
            retValue = True
            usageDatesLeft -= My.Settings.UsageDates.Count

            'sanity check... if the system date is backed up to a previous date in the list, but not the last date
            If usageDatesLeft <= 0 AndAlso My.Settings.UsageDates.IndexOf(hashedDate) <> My.Settings.UsageDates.Count - 1 Then
                retValue = False
            End If
        Else
            If My.Settings.UsageDates.Count < usageDatesLeft Then
                My.Settings.UsageDates.Add(hashedDate)
            End If
            usageDatesLeft -= My.Settings.UsageDates.Count


            'If not, and the remining count has "slots" open, add it
            If usageDatesLeft > 0 Then
                retValue = True
            Else
                'If not and tree are no more slots, tell user, exit app
                retValue = False
            End If

        End If
        'Display to the user how many days are remianing:
        'MessageBox.Show(String.Format("You have {0} day(s) remaining.", usageDatesLeft))

        Return retValue
    End Function

    Private Function HashDate(ByVal dateToHash As Date) As String
        'Get a hash object
        Dim hasher As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create()
        'Take date, make it a Long date and hash it
        Dim data As Byte() = hasher.ComputeHash(System.Text.Encoding.Default.GetBytes(dateToHash.ToLongDateString()))
        ' Create a new Stringbuilder to collect the bytes
        ' and create a string.
        Dim sBuilder As New System.Text.StringBuilder()

        ' Loop through each byte of the hashed data 
        ' and format each one as a hexadecimal string.
        Dim idx As Integer
        For idx = 0 To data.Length - 1
            sBuilder.Append(data(idx).ToString("x2"))
        Next idx

        Return sBuilder.ToString

    End Function



    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim aCount As Integer = 1
        Dim loopIt As Boolean = True
        'My.Settings.Reset() 'This is here for design time support... otherwise you won't get your app to run agin

        'Do While loopIt
        'MessageBox.Show(String.Format("Checking Date: {0}.", Date.Now.AddDays(aCount)))
        loopIt = CheckDate(Date.Now.AddDays(aCount))
        If Not loopIt Then
            MessageBox.Show("Upłynął czas dzaiłania aplikacji! Skontaktuj się z właścicielem!")
            Me.Close()
        Else
            'MessageBox.Show("You can keep using the app")
        End If
        aCount += 1
        'Loop



    End Sub

    Private Sub Bt_laduj_jpg_Click_2(sender As Object, e As EventArgs) Handles Bt_laduj_jpg.Click

        Dim i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            OperatGrid.Clear()
            BlobGrid.Clear()
            ListBox3.Items.Clear()
            Try
                Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                Dim plik = My.Computer.FileSystem.GetFileInfo(SciezkaDopliku)
                Dim text As String = plik.Directory.Name
                'Kasowanie numeru tomu z nazwy wsczytanego katalogu
                text = Regex.Replace(text, "_[^_]*$", "")
                'Walidacja nazwy folderu operatu ze wzorcem
                If Not Regex.IsMatch(text, "^\w\.\d{1,4}\.\d{1,4}\.\d{1,5}$", RegexOptions.IgnorePatternWhitespace) Then
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", SciezkaDopliku, True)
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", Environment.NewLine, True)
                    Continue For
                End If
                Dim parts As String() = text.Split(New Char() {"."})
                Dim part As String
                Dim NazwaPliku = plik.Name.ToString

                ListBox2.Items.Add(plik.Directory.Name)
                For Each part In parts

                    ListBox3.Items.Add(part) 'rozbicie nazwy katalogu speparatorem i zapis w liscie
                Next

                Dim C1 = ListBox3.Items(0)
                Dim C2 = ListBox3.Items(1)
                Dim C3 = ListBox3.Items(2)
                Dim C4 = ListBox3.Items(3)
                Dim fs As New System.IO.FileStream(SciezkaDopliku, IO.FileMode.Open, IO.FileAccess.Read)

                Dim img As Image = Image.FromFile(SciezkaDopliku)

                Dim mnoznik = 39.37
                Dim xdpi = img.HorizontalResolution
                Dim ydpi = img.VerticalResolution
                Dim wys = ydpi * 39.37
                Dim dlugosc = xdpi * 39.37
                Dim dlugoscX = img.Width
                Dim wysY = img.Height
                'Dim glebia = CInt(img.PixelFormat.ToString.Replace("Format", "").Replace("bppRgb", "")) / 8
                Dim glebia = 3
                'MsgBox(glebia)
                Dim WielPamiec As Integer = CInt(wysY * dlugoscX * glebia)
                img.Dispose()
                Dim Dataa As Byte() = New Byte() {}
                Dim rnew As BinaryReader = New BinaryReader(fs)
                Dataa = rnew.ReadBytes(fs.Length)

                Dim selekcjaOP As String = ("select uid, typ from operaty where C1 = '" & C1.ToString & "' and C2 = '" & C2.ToString & "' and C3 = '" & C3.ToString & "' and C4 = '" & C4.ToString & "'")
                Dim custDAOP As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                custDAOP.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(selekcjaOP, PolaczenieFDB)
                custDAOP.Fill(OperatGrid, "Operat")

                If OperatGrid.Tables("Operat").Rows.Count = 0 Then
                    My.Computer.FileSystem.WriteAllText(".\BrakOperatu.txt", SciezkaDopliku, True)
                    My.Computer.FileSystem.WriteAllText(".\BrakOperatu.txt", Environment.NewLine, True)
                    Continue For
                End If
                DataGridView2.DataSource = OperatGrid.Tables("Operat")
                Dim operat = DataGridView2.Rows(0).Cells(0).Value.ToString
                Dim typ = DataGridView2.Rows(0).Cells(1).Value.ToString


                Dim ucomp As Integer = UBound(Dataa) + 1
                Dim compsize As Integer = CompressBytes(Dataa)
                Dim Origsize As Integer = DeCompressBytes(Dataa, ucomp)
                Dim cmd As New FirebirdSql.Data.FirebirdClient.FbCommand("Insert into FBDOK (Uid, Tresc, Mini, Dtw, Osow, Dtu, Osou) values (gen_id(FBDOKg, 1), (@Picture), NULL, CURRENT_TIMESTAMP, '26', NULL, NULL);", Polaczenie)
                cmd.Parameters.Add("@Picture", FbDbType.Binary, Dataa.Length).Value = Dataa
                cmd.ExecuteNonQuery()

                Dim selekcja As String = ("SELECT gen_id(FBDOKG, 0) FROM RDB$DATABASE")
                Dim custDA As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                custDA.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(selekcja, Polaczenie)
                custDA.Fill(BlobGrid, "Wybrane")
                DataGridView1.DataSource = BlobGrid.Tables("Wybrane")
                Dim BlobID = DataGridView1.Rows(0).Cells(0).Value.ToString()
                
                Dim cmdOPERDOK As New FirebirdSql.Data.FirebirdClient.FbCommand("INSERT INTO OPERDOK (UID, TYP, ID_OPE, DOKUMENT, ADRES, TYP_DOK, GRUPA, XPPM, YPPM, KOMPRESJA, ORG_WIELK, KAT_AKT, ID_BLOB, DTW, OSOW, DTU, OSOU, NR_DOK, NAZ_DOK) VALUES (gen_id(OPERDOKG, 1), (@atyp), (@aoperat), (@aNazwaPliku), NULL, 4, NULL, " & wys & ", " & dlugosc & ", 0, (@Origsize), NULL, (@aBlobGrid), CURRENT_TIMESTAMP, 26, NULL, 26, NULL, NULL);", PolaczenieFDB)
                cmdOPERDOK.Parameters.Add("@aoperat", FbDbType.Text, operat.Length).Value = operat
                cmdOPERDOK.Parameters.Add("@aNazwaPliku", FbDbType.Text, NazwaPliku.Length).Value = NazwaPliku
                cmdOPERDOK.Parameters.Add("@Origsize", FbDbType.Integer).Value = WielPamiec
                cmdOPERDOK.Parameters.Add("@aBlobGrid", FbDbType.Text, BlobID.Length).Value = BlobID
                cmdOPERDOK.Parameters.Add("@atyp", FbDbType.Text, typ.Length).Value = typ
                My.Computer.FileSystem.WriteAllText(".\Zaladowane.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\Zaladowane.txt", Environment.NewLine, True)
                cmdOPERDOK.ExecuteNonQuery()
                rnew.Close()
                rnew.Dispose()
                fs.Close()
                fs.Dispose()
                Dataa = Nothing

                Dim j As Integer = ((i + 1) / ListBox1.Items.Count) * 100
                ProgressBar1.Value = j
                ProcLadowania.Text = "Załadowano " & ProgressBar1.Value & "%"
                ProcLadowania.Refresh()
                Me.Refresh()

            Catch ex As Exception
                Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                File.WriteAllText(".\Bledy.txt", ex.ToString)
                My.Computer.FileSystem.WriteAllText(".\NieZaladowane.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\NieZaladowane.txt", Environment.NewLine, True)

            End Try
 
        Next
        MsgBox("Załadowano")

    End Sub


    Private Sub B_ladZatry_Click(sender As Object, e As EventArgs) Handles B_ladZatry.Click
        Dim i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            OperatGrid.Clear()
            BlobGrid.Clear()
            ListBox3.Items.Clear()
            Try
                Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                Dim plik = My.Computer.FileSystem.GetFileInfo(SciezkaDopliku)
                Dim text As String = plik.Directory.Name
                'Kasowanie numeru tomu z nazwy wsczytanego katalog
                text = Regex.Replace(text, "_[^_]*$", "")
                'Walidacja nazwy folderu operatu ze wzorcem
                If Not Regex.IsMatch(text, "^\w\.\d{1,4}\.\d{1,4}\.\d{1,5}$", RegexOptions.IgnorePatternWhitespace) Then
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", SciezkaDopliku, True)
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", Environment.NewLine, True)
                    Continue For
                End If
                Dim parts As String() = text.Split(New Char() {"."})
                Dim part As String
                Dim NazwaPliku = plik.Name.ToString
                ListBox2.Items.Add(plik.Directory.Name)
                For Each part In parts

                    ListBox3.Items.Add(part) 'rozbicie nazwy katalogu speparatorem i zapis w liscie
                Next

                Dim C1 = ListBox3.Items(0)
                Dim C2 = ListBox3.Items(1)
                Dim C3 = ListBox3.Items(2)
                Dim C4 = ListBox3.Items(3)
                Dim fs As New System.IO.FileStream(SciezkaDopliku, IO.FileMode.Open, IO.FileAccess.Read)

                Dim img As Image = Image.FromFile(SciezkaDopliku)

                Dim mnoznik = 39.37
                Dim xdpi = img.HorizontalResolution
                Dim ydpi = img.VerticalResolution
                Dim wys = ydpi * 39.37
                Dim dlugosc = xdpi * 39.37
                Dim dlugoscX = img.Width
                Dim wysY = img.Height
                'Dim glebia = CInt(img.PixelFormat.ToString.Replace("Format", "").Replace("bppRgb", "")) / 8
                Dim glebia = 3
                'MsgBox(glebia)
                Dim WielPamiec As Integer = CInt(wysY * dlugoscX * glebia)
                img.Dispose()
                Dim Dataa As Byte() = New Byte() {}
                Dim rnew As BinaryReader = New BinaryReader(fs)
                Dataa = rnew.ReadBytes(fs.Length)


                Dim ucomp As Integer = UBound(Dataa) + 1
                Dim compsize As Integer = CompressBytes(Dataa)
                Dim Origsize As Integer = DeCompressBytes(Dataa, ucomp)
                Dim myDelims As String() = New String() {"_"}
                Dim tymnazwa = NazwaPliku.Split(myDelims, StringSplitOptions.None)
                Dim tymnazwa1 = tymnazwa(0)
                Dim tymnazwa2 = tymnazwa(1)

                Dim NazwaDokumentu As Integer
                NazwaDokumentu = Nothing
                If tymnazwa2.Contains("S.jpg") Then
                    NazwaDokumentu = 66
                ElseIf tymnazwa2.Contains("W.jpg") Then
                    NazwaDokumentu = 67
                ElseIf tymnazwa2.Contains("P.jpg") Then
                    NazwaDokumentu = 68
                ElseIf tymnazwa2.Contains("O.jpg") Then
                    NazwaDokumentu = 69
                ElseIf tymnazwa2.Contains("T.jpg") Then
                    NazwaDokumentu = 70
                ElseIf tymnazwa2.Contains("M.jpg") Then
                    NazwaDokumentu = 71
                ElseIf tymnazwa2.Contains("D.jpg") Then
                    NazwaDokumentu = 72
                ElseIf tymnazwa2.Contains("I.jpg") Then
                    NazwaDokumentu = 73
                End If

                Dim selekcjaOP As String = ("select uid, typ from operaty where C1 = '" & C1.ToString & "' and C2 = '" & C2.ToString & "' and C3 = '" & C3.ToString & "' and C4 = '" & C4.ToString & "'")
                Dim custDAOP As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                custDAOP.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(selekcjaOP, PolaczenieFDB)
                custDAOP.Fill(OperatGrid, "Operat")
                If OperatGrid.Tables("Operat").Rows.Count = 0 Then
                    My.Computer.FileSystem.WriteAllText(".\BrakOperatu.txt", SciezkaDopliku, True)
                    My.Computer.FileSystem.WriteAllText(".\BrakOperatu.txt", Environment.NewLine, True)
                    Continue For
                End If
                DataGridView2.DataSource = OperatGrid.Tables("Operat")
                Dim operat = DataGridView2.Rows(0).Cells(0).Value.ToString
                Dim typ = DataGridView2.Rows(0).Cells(1).Value.ToString

                Dim cmd As New FirebirdSql.Data.FirebirdClient.FbCommand("Insert into FBDOK (Uid, Tresc, Mini, Dtw, Osow, Dtu, Osou) values (gen_id(FBDOKg, 1), (@Picture), NULL, CURRENT_TIMESTAMP, '26', NULL, NULL);", Polaczenie)
                cmd.Parameters.Add("@Picture", FbDbType.Binary, Dataa.Length).Value = Dataa
                cmd.ExecuteNonQuery()

                Dim selekcja As String = ("SELECT gen_id(FBDOKG, 0) FROM RDB$DATABASE")
                Dim custDA As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                custDA.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(selekcja, Polaczenie)
                custDA.Fill(BlobGrid, "Wybrane")
                DataGridView1.DataSource = BlobGrid.Tables("Wybrane")
                Dim BlobID = DataGridView1.Rows(0).Cells(0).Value.ToString()

                Dim cmdOPERDOK As New FirebirdSql.Data.FirebirdClient.FbCommand("INSERT INTO OPERDOK (UID, TYP, ID_OPE, DOKUMENT, ADRES, TYP_DOK, GRUPA, XPPM, YPPM, KOMPRESJA, ORG_WIELK, KAT_AKT, ID_BLOB, DTW, OSOW, DTU, OSOU, NR_DOK, NAZ_DOK) VALUES (gen_id(OPERDOKG, 1), (@atyp), (@aoperat), (@aNazwaPliku), NULL, 4, NULL, " & wys & ", " & dlugosc & ", 0, (@Origsize), NULL, (@aBlobGrid), CURRENT_TIMESTAMP, 26, NULL, 26, NULL, @NAZ_DOK);", PolaczenieFDB)
                cmdOPERDOK.Parameters.Add("@aoperat", FbDbType.Text, operat.Length).Value = operat
                cmdOPERDOK.Parameters.Add("@aNazwaPliku", FbDbType.Text, NazwaPliku.Length).Value = NazwaPliku
                cmdOPERDOK.Parameters.Add("@Origsize", FbDbType.Integer).Value = WielPamiec
                cmdOPERDOK.Parameters.Add("@aBlobGrid", FbDbType.Text, BlobID.Length).Value = BlobID
                cmdOPERDOK.Parameters.Add("@atyp", FbDbType.Text, typ.Length).Value = typ
                cmdOPERDOK.Parameters.Add("@NAZ_DOK", FbDbType.Integer).Value = NazwaDokumentu
                My.Computer.FileSystem.WriteAllText(".\Zaladowane.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\Zaladowane.txt", Environment.NewLine, True)
                cmdOPERDOK.ExecuteNonQuery()
                fs.Close()
                rnew.Close()
                rnew.Dispose()
                fs.Close()
                fs.Dispose()
                Dataa = Nothing
                Dim j As Integer = ((i + 1) / ListBox1.Items.Count) * 100

                ProgressBar1.Value = j
                ProcLadowania.Text = "Załadowano " & ProgressBar1.Value & "%"
                ProcLadowania.Refresh()
                Me.Refresh()

            Catch ex As Exception
                Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                File.WriteAllText(".\Bledy.txt", ex.ToString)
                My.Computer.FileSystem.WriteAllText(".\NieZaladowane.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\NieZaladowane.txt", Environment.NewLine, True)

            End Try

        Next
        MsgBox("Załadowano")
    End Sub


    Private Sub Bt_sprOrientacje_Click(sender As Object, e As EventArgs) Handles Bt_sprOrientacje.Click
        'Sprawdza orientacje plików jpg
        Dim i As Integer
        My.Computer.FileSystem.WriteAllText(".\orientacja.csv", "", False)
        My.Computer.FileSystem.WriteAllText(".\orientacja.csv", "Scieżka" + ";" + " Długość" + ";" + "Szerokość" + ";" + "Orientacja", True)
        My.Computer.FileSystem.WriteAllText(".\orientacja.csv", Environment.NewLine, True)

        For i = 0 To ListBox1.Items.Count - 1
            Dim j As Integer = ((i + 1) / ListBox1.Items.Count) * 100
            ProgressBar1.Value = j
            ProcLadowania.Text = "Przetworzono " & ProgressBar1.Value & "%"
            ProcLadowania.Refresh()
            Me.Refresh()
            Try
                Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                Dim plik = My.Computer.FileSystem.GetFileInfo(SciezkaDopliku)
                Dim text As String = plik.Directory.Name
                Dim NazwaPliku = plik.Name.ToString
                Dim img As Image = Image.FromFile(SciezkaDopliku)
                Dim mnoznik = 39.37
                Dim xdpi = img.HorizontalResolution
                Dim ydpi = img.VerticalResolution
                Dim wys = ydpi * 39.37
                Dim dlugosc = xdpi * 39.37
                Dim dlugoscX = img.Width
                Dim wysY = img.Height
                Dim orientacja As String

                If dlugoscX < wysY Then orientacja = "Portrait" Else orientacja = "Landscape"
                img.Dispose()
                Dim zapisz = SciezkaDopliku + ";" + dlugoscX.ToString + ";" + wysY.ToString + ";" + orientacja
                My.Computer.FileSystem.WriteAllText(".\orientacja.csv", zapisz, True)
                My.Computer.FileSystem.WriteAllText(".\orientacja.csv", Environment.NewLine, True)
            Catch ex As Exception

                MsgBox("Błąd")
            End Try
        Next
        MessageBox.Show("Przetworzono")

    End Sub
    Private Sub kasujOperatyZbazy()
        'Kasuje operaty z bazy na podstawie nazw katalogów
        Dim i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            OperatGrid.Clear()
            BlobGrid.Clear()
            ListBox3.Items.Clear()
            Try
                Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                Dim plik = My.Computer.FileSystem.GetFileInfo(SciezkaDopliku)
                Dim text As String = plik.Directory.Name
                'Walidacja nazwy operatu
                If Not Regex.IsMatch(text, "^\w\.\d{1,4}\.\d{1,4}\.\d{1,5}$", RegexOptions.IgnorePatternWhitespace) Then
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", SciezkaDopliku, True)
                    My.Computer.FileSystem.WriteAllText(".\BlednaNazwaOperatu.txt", Environment.NewLine, True)
                    Continue For
                End If
                Dim parts As String() = text.Split(New Char() {"."})
                Dim part As String
                Dim NazwaPliku = plik.Name.ToString

                ListBox2.Items.Add(plik.Directory.Name)
                For Each part In parts

                    ListBox3.Items.Add(part) 'rozbicie nazwy katalogu speparatorem i zapis w liscie
                Next
                Dim C1 = ListBox3.Items(0)
                Dim C2 = ListBox3.Items(1)
                Dim C3 = ListBox3.Items(2)
                Dim C4 = ListBox3.Items(3)

                Dim selekcjaOP As String = ("select uid, typ from operaty where C1 = '" & C1.ToString & "' and C2 = '" & C2.ToString & "' and C3 = '" & C3.ToString & "' and C4 = '" & C4.ToString & "'")
                Dim custDAOP As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                custDAOP.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(selekcjaOP, PolaczenieFDB)
                custDAOP.Fill(OperatGrid, "Operat")
                DataGridView2.DataSource = OperatGrid.Tables("Operat")
                Dim operat = DataGridView2.Rows(0).Cells(0).Value.ToString
                Dim typ = DataGridView2.Rows(0).Cells(1).Value.ToString
                If String.IsNullOrEmpty(operat) Then Continue For


                Dim cmd As New FirebirdSql.Data.FirebirdClient.FbCommand(" Delete from FBDOK where Uid = (@Operat);", Polaczenie)
                cmd.Parameters.Add("@Operat", FbDbType.Integer, operat.Length).Value = operat
                cmd.ExecuteNonQuery()

                Dim cmdOPERDOK As New FirebirdSql.Data.FirebirdClient.FbCommand("Delete from OPERDOK where ID_OPE = (@aoperat);", PolaczenieFDB)
                cmdOPERDOK.Parameters.Add("@aoperat", FbDbType.Text, operat.Length).Value = operat
                My.Computer.FileSystem.WriteAllText(".\Usuniete.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\Usuniete.txt", Environment.NewLine, True)
                cmdOPERDOK.ExecuteNonQuery()

                Dim j As Integer = ((i + 1) / ListBox1.Items.Count) * 100

                ProgressBar1.Value = j
                ProcLadowania.Text = "Usunięto " & ProgressBar1.Value & "%"
                ProcLadowania.Refresh()
                Me.Refresh()

            Catch ex As Exception
                Dim SciezkaDopliku As String = ListBox1.Items(i).ToString
                File.WriteAllText(".\Bledy.txt", ex.ToString)
                My.Computer.FileSystem.WriteAllText(".\NieUsuniete.txt", SciezkaDopliku, True)
                My.Computer.FileSystem.WriteAllText(".\NieUsuniete.txt", Environment.NewLine, True)
            End Try
        Next
        MsgBox("Usunięto")
    End Sub

    Private Sub Bt_kasujOpearty_Click(sender As Object, e As EventArgs) Handles Bt_kasujOpearty.Click
        Try
            Dim response As MsgBoxResult
            response = MsgBox("Wszystkie pliki w bazie we wskazanych operatach zostaną usunięte. Czy na pewno chcesz kontynuować?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Potwierdz")
            If response = MsgBoxResult.Yes Then
                kasujOperatyZbazy()
            ElseIf response = MsgBoxResult.No Then
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Wystąpił problem")
        End Try
    End Sub


    Public Sub katalogDoLadowania()
        Try
            'Wskazuje katalog do zapisu
            FolderBrowserDialog2.ShowDialog()
            Dim sciezkaZapisu = FolderBrowserDialog2.SelectedPath
        Catch ex As Exception
            MsgBox("Wybierz folder z plikami do przetwarzania")
        End Try
    End Sub
    Private Sub wyladujZbazy()
        Try
            'Wskazuje katalog do zapisu
            FolderBrowserDialog2.ShowDialog()
            Dim sciezkaZapisu = FolderBrowserDialog2.SelectedPath
        Catch ex As Exception
            MsgBox("Wybierz folder z plikami do przetwarzania")
        End Try
        'Dodaje do okna skanów dokumentów
        Dim seleckcjaOPD As String = ("SELECT DOKUMENT, ORG_WIELK, ID_BLOB, ID_OPE, TYP FROM OPERDOK")
        Dim OpDocPol As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
        OpDocPol.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(seleckcjaOPD, PolaczenieFDB)
        OpDocPol.Fill(OperDoc, "OperatD")
        DataGridView1.DataSource = OperDoc.Tables("OperatD")

        For j = 0 To DataGridView1.Rows.Count - 1
            Dim k As Integer = ((j + 1) / DataGridView1.Rows.Count) * 100
            ProgressBar1.Value = k
            ProcLadowania.Text = "Przetworzono " & ProgressBar1.Value & "%"
            ProcLadowania.Refresh()
            Me.Refresh()
            Try

                Dim aktualnyDokument = j
                Dim dokumentID = DataGridView1.Rows(aktualnyDokument).Cells(2).Value
                Dim dokumentNazwa = DataGridView1.Rows(aktualnyDokument).Cells(0).Value
                Dim dokumentWielkosc = DataGridView1.Rows(aktualnyDokument).Cells(1).Value
                Dim dokumetTyp = DataGridView1.Rows(aktualnyDokument).Cells(3).Value
                Dim dokumentOperat = DataGridView1.Rows(aktualnyDokument).Cells(4).Value
                If String.IsNullOrEmpty(dokumentNazwa.ToString) Then dokumentNazwa = "Pusta_nazwa"

                'Pobiera dokumenty z bazy ze skanami i zapisuje je do datagridu
                Dim seleckcjaDok As String = ("SELECT UID, TRESC FROM FBDOK WHERE UID = '" & dokumentID & "'")
                Dim dokPol As New FirebirdSql.Data.FirebirdClient.FbDataAdapter()
                dokPol.SelectCommand = New FirebirdSql.Data.FirebirdClient.FbCommand(seleckcjaDok, Polaczenie)
                dokPol.Fill(DokumentBaza, "Dok")
                DataGridView3.DataSource = DokumentBaza.Tables("Dok")
                Dim dokumentPlik = DataGridView3.Rows(0).Cells(1).Value
                'Rozpakowuje wybrany plik z bazy przy użyciu biblioteki zlib, z parametrami: plik i orginalna wielkość
                DeCompressBytes(dokumentPlik, dokumentWielkosc)
                'Zapis pliku na dysku w katalogu z nazwa UID operatu i jego typem
                Dim sciezkaZapisu = FolderBrowserDialog2.SelectedPath
                If (Not System.IO.Directory.Exists(sciezkaZapisu & "\" & dokumentOperat.ToString & "_" & dokumetTyp.ToString)) Then
                    System.IO.Directory.CreateDirectory(sciezkaZapisu & "\" & dokumentOperat.ToString & "_" & dokumetTyp.ToString)
                End If
                File.WriteAllBytes(sciezkaZapisu & "\" & dokumentOperat.ToString & "_" & dokumetTyp.ToString & "\" & dokumentNazwa, dokumentPlik)
                DokumentBaza.Clear()
                DataGridView3.Rows.RemoveAt(0)
            Catch ex As Exception
                Continue For
            End Try
        Next

    End Sub



    Private Sub Bt_wyladuj_Click(sender As Object, e As EventArgs) Handles Bt_wyladuj.Click
        wyladujZbazy()
    End Sub



    Private Sub Bt_polaczPdf_Click(sender As Object, e As EventArgs) Handles Bt_polaczPdf.Click
        Try

            FolderBrowserDialog1.ShowDialog()

            ProcessDirectory(FolderBrowserDialog1.SelectedPath)
            MessageBox.Show("Połączono")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TypPliku_TextChanged(sender As Object, e As EventArgs) Handles TypPliku.TextChanged

    End Sub
End Class
