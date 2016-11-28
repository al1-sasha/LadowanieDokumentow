<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LadowanieD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LadowanieD))
        Me.Disconnect = New System.Windows.Forms.Button()
        Me.Connect = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.sciezka = New System.Windows.Forms.TextBox()
        Me.Bt_laduj_pdf = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.SciezkaFDB = New System.Windows.Forms.TextBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Procent = New System.Windows.Forms.Label()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.ProcLadowania = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.PlikToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KoniecToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PomocToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OProgramieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TypPliku = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Bt_laduj_jpg = New System.Windows.Forms.Button()
        Me.B_ladZatry = New System.Windows.Forms.Button()
        Me.Bt_sprOrientacje = New System.Windows.Forms.Button()
        Me.Bt_kasujOpearty = New System.Windows.Forms.Button()
        Me.Bt_wyladuj = New System.Windows.Forms.Button()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.FolderBrowserDialog2 = New System.Windows.Forms.FolderBrowserDialog()
        Me.BackgroundWorker3 = New System.ComponentModel.BackgroundWorker()
        Me.Bt_polaczPdf = New System.Windows.Forms.Button()
        Me.Lb_LiczbaPlikow = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Disconnect
        '
        Me.Disconnect.Location = New System.Drawing.Point(144, 38)
        Me.Disconnect.Name = "Disconnect"
        Me.Disconnect.Size = New System.Drawing.Size(84, 42)
        Me.Disconnect.TabIndex = 3
        Me.Disconnect.Text = "Rozłącz z bazą"
        Me.Disconnect.UseVisualStyleBackColor = True
        '
        'Connect
        '
        Me.Connect.Location = New System.Drawing.Point(27, 38)
        Me.Connect.Name = "Connect"
        Me.Connect.Size = New System.Drawing.Size(87, 42)
        Me.Connect.TabIndex = 2
        Me.Connect.Text = "Połącz z bazą"
        Me.Connect.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(256, 84)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 13)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "Ścieżka do bazy FDB"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(256, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(110, 13)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "Scieżka do bazy FDR"
        '
        'sciezka
        '
        Me.sciezka.Location = New System.Drawing.Point(259, 50)
        Me.sciezka.Name = "sciezka"
        Me.sciezka.Size = New System.Drawing.Size(214, 20)
        Me.sciezka.TabIndex = 40
        Me.sciezka.Text = "d:\robotki\parczew\testy_bazy\OSRODEK.FDR"
        '
        'Bt_laduj_pdf
        '
        Me.Bt_laduj_pdf.Location = New System.Drawing.Point(144, 179)
        Me.Bt_laduj_pdf.Name = "Bt_laduj_pdf"
        Me.Bt_laduj_pdf.Size = New System.Drawing.Size(154, 64)
        Me.Bt_laduj_pdf.TabIndex = 45
        Me.Bt_laduj_pdf.Text = "Ładuj pliki do bazy (z kompresją) jako pliki"
        Me.Bt_laduj_pdf.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(528, 46)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ListBox1.ScrollAlwaysVisible = True
        Me.ListBox1.Size = New System.Drawing.Size(441, 394)
        Me.ListBox1.TabIndex = 46
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(27, 112)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(81, 47)
        Me.Button3.TabIndex = 47
        Me.Button3.Text = "Wczytaj pliki"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'SciezkaFDB
        '
        Me.SciezkaFDB.Location = New System.Drawing.Point(259, 100)
        Me.SciezkaFDB.Name = "SciezkaFDB"
        Me.SciezkaFDB.Size = New System.Drawing.Size(214, 20)
        Me.SciezkaFDB.TabIndex = 50
        Me.SciezkaFDB.Text = "d:\robotki\parczew\testy_bazy\OSRODEK.FDB"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.ForeColor = System.Drawing.Color.Green
        Me.ProgressBar1.Location = New System.Drawing.Point(528, 488)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(441, 23)
        Me.ProgressBar1.Step = 5
        Me.ProgressBar1.TabIndex = 60
        '
        'Procent
        '
        Me.Procent.AutoSize = True
        Me.Procent.Location = New System.Drawing.Point(739, 492)
        Me.Procent.Name = "Procent"
        Me.Procent.Size = New System.Drawing.Size(0, 13)
        Me.Procent.TabIndex = 61
        '
        'BackgroundWorker2
        '
        Me.BackgroundWorker2.WorkerReportsProgress = True
        Me.BackgroundWorker2.WorkerSupportsCancellation = True
        '
        'ProcLadowania
        '
        Me.ProcLadowania.AutoSize = True
        Me.ProcLadowania.Location = New System.Drawing.Point(739, 492)
        Me.ProcLadowania.Name = "ProcLadowania"
        Me.ProcLadowania.Size = New System.Drawing.Size(0, 13)
        Me.ProcLadowania.TabIndex = 63
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(231, 485)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(65, 34)
        Me.DataGridView1.TabIndex = 66
        Me.DataGridView1.Visible = False
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(313, 485)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(66, 34)
        Me.DataGridView2.TabIndex = 67
        Me.DataGridView2.Visible = False
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(259, 436)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(82, 30)
        Me.ListBox2.TabIndex = 68
        Me.ListBox2.Visible = False
        '
        'ListBox3
        '
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.Location = New System.Drawing.Point(366, 436)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(90, 30)
        Me.ListBox3.TabIndex = 69
        Me.ListBox3.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlikToolStripMenuItem, Me.PomocToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1030, 24)
        Me.MenuStrip1.TabIndex = 72
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'PlikToolStripMenuItem
        '
        Me.PlikToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KoniecToolStripMenuItem})
        Me.PlikToolStripMenuItem.Name = "PlikToolStripMenuItem"
        Me.PlikToolStripMenuItem.Size = New System.Drawing.Size(38, 20)
        Me.PlikToolStripMenuItem.Text = "Plik"
        '
        'KoniecToolStripMenuItem
        '
        Me.KoniecToolStripMenuItem.Name = "KoniecToolStripMenuItem"
        Me.KoniecToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.KoniecToolStripMenuItem.Text = "Koniec"
        '
        'PomocToolStripMenuItem
        '
        Me.PomocToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OProgramieToolStripMenuItem})
        Me.PomocToolStripMenuItem.Name = "PomocToolStripMenuItem"
        Me.PomocToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.PomocToolStripMenuItem.Text = "Pomoc"
        '
        'OProgramieToolStripMenuItem
        '
        Me.OProgramieToolStripMenuItem.Name = "OProgramieToolStripMenuItem"
        Me.OProgramieToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.OProgramieToolStripMenuItem.Text = "O programie"
        '
        'TypPliku
        '
        Me.TypPliku.Location = New System.Drawing.Point(27, 200)
        Me.TypPliku.Name = "TypPliku"
        Me.TypPliku.Size = New System.Drawing.Size(55, 20)
        Me.TypPliku.TabIndex = 74
        Me.TypPliku.Text = "pdf"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 184)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "Typ plików"
        '
        'Bt_laduj_jpg
        '
        Me.Bt_laduj_jpg.Location = New System.Drawing.Point(328, 179)
        Me.Bt_laduj_jpg.Name = "Bt_laduj_jpg"
        Me.Bt_laduj_jpg.Size = New System.Drawing.Size(145, 64)
        Me.Bt_laduj_jpg.TabIndex = 76
        Me.Bt_laduj_jpg.Text = "Ładuj pliki do bazy (bez kompresji) jako grafika"
        Me.Bt_laduj_jpg.UseVisualStyleBackColor = True
        '
        'B_ladZatry
        '
        Me.B_ladZatry.Location = New System.Drawing.Point(221, 281)
        Me.B_ladZatry.Name = "B_ladZatry"
        Me.B_ladZatry.Size = New System.Drawing.Size(189, 64)
        Me.B_ladZatry.TabIndex = 77
        Me.B_ladZatry.Text = "Ładuj pliki do bazy (bez kompresji) jako grafika wraz z atrybutami"
        Me.B_ladZatry.UseVisualStyleBackColor = True
        '
        'Bt_sprOrientacje
        '
        Me.Bt_sprOrientacje.Location = New System.Drawing.Point(528, 529)
        Me.Bt_sprOrientacje.Name = "Bt_sprOrientacje"
        Me.Bt_sprOrientacje.Size = New System.Drawing.Size(121, 63)
        Me.Bt_sprOrientacje.TabIndex = 78
        Me.Bt_sprOrientacje.Text = "Sprawdz orientacje plików jpg"
        Me.Bt_sprOrientacje.UseVisualStyleBackColor = True
        '
        'Bt_kasujOpearty
        '
        Me.Bt_kasujOpearty.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Bt_kasujOpearty.Location = New System.Drawing.Point(12, 453)
        Me.Bt_kasujOpearty.Name = "Bt_kasujOpearty"
        Me.Bt_kasujOpearty.Size = New System.Drawing.Size(175, 58)
        Me.Bt_kasujOpearty.TabIndex = 79
        Me.Bt_kasujOpearty.Text = "Kasuj operaty w bazie"
        Me.Bt_kasujOpearty.UseVisualStyleBackColor = False
        '
        'Bt_wyladuj
        '
        Me.Bt_wyladuj.Location = New System.Drawing.Point(836, 530)
        Me.Bt_wyladuj.Name = "Bt_wyladuj"
        Me.Bt_wyladuj.Size = New System.Drawing.Size(137, 62)
        Me.Bt_wyladuj.TabIndex = 80
        Me.Bt_wyladuj.Text = "Wyładuj pliki z bazy"
        Me.Bt_wyladuj.UseVisualStyleBackColor = True
        '
        'DataGridView3
        '
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Location = New System.Drawing.Point(394, 485)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.Size = New System.Drawing.Size(69, 34)
        Me.DataGridView3.TabIndex = 81
        Me.DataGridView3.Visible = False
        '
        'Bt_polaczPdf
        '
        Me.Bt_polaczPdf.Location = New System.Drawing.Point(678, 529)
        Me.Bt_polaczPdf.Name = "Bt_polaczPdf"
        Me.Bt_polaczPdf.Size = New System.Drawing.Size(123, 63)
        Me.Bt_polaczPdf.TabIndex = 82
        Me.Bt_polaczPdf.Text = "Polącz pliki pdf"
        Me.Bt_polaczPdf.UseVisualStyleBackColor = True
        '
        'Lb_LiczbaPlikow
        '
        Me.Lb_LiczbaPlikow.AutoSize = True
        Me.Lb_LiczbaPlikow.Location = New System.Drawing.Point(891, 453)
        Me.Lb_LiczbaPlikow.Name = "Lb_LiczbaPlikow"
        Me.Lb_LiczbaPlikow.Size = New System.Drawing.Size(0, 13)
        Me.Lb_LiczbaPlikow.TabIndex = 83
        '
        'LadowanieD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 615)
        Me.Controls.Add(Me.Lb_LiczbaPlikow)
        Me.Controls.Add(Me.Bt_polaczPdf)
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.Bt_wyladuj)
        Me.Controls.Add(Me.Bt_kasujOpearty)
        Me.Controls.Add(Me.Bt_sprOrientacje)
        Me.Controls.Add(Me.B_ladZatry)
        Me.Controls.Add(Me.Bt_laduj_jpg)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TypPliku)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ProcLadowania)
        Me.Controls.Add(Me.Procent)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.SciezkaFDB)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Bt_laduj_pdf)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.sciezka)
        Me.Controls.Add(Me.Disconnect)
        Me.Controls.Add(Me.Connect)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "LadowanieD"
        Me.Text = "Ładowanie Dokumentów "
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Disconnect As System.Windows.Forms.Button
    Friend WithEvents Connect As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents sciezka As System.Windows.Forms.TextBox
    Friend WithEvents Bt_laduj_pdf As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SciezkaFDB As System.Windows.Forms.TextBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Procent As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProcLadowania As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents PlikToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KoniecToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PomocToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OProgramieToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TypPliku As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Bt_laduj_jpg As System.Windows.Forms.Button
    Friend WithEvents B_ladZatry As System.Windows.Forms.Button
    Friend WithEvents Bt_sprOrientacje As System.Windows.Forms.Button
    Friend WithEvents Bt_kasujOpearty As System.Windows.Forms.Button
    Friend WithEvents Bt_wyladuj As System.Windows.Forms.Button
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents FolderBrowserDialog2 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Bt_polaczPdf As System.Windows.Forms.Button
    Friend WithEvents Lb_LiczbaPlikow As System.Windows.Forms.Label

End Class
