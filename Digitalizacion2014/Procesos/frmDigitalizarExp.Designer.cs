namespace Digitalizacion2014.Procesos
{
    partial class frmDigitalizarExp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDigitalizarExp));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Licencia",
            "2"}, -1);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblExpDescripcion = new System.Windows.Forms.Label();
            this.lblExpediente = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.TabWizard = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupExpediente = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnAllSettings = new System.Windows.Forms.Button();
            this.groupDuplex = new System.Windows.Forms.GroupBox();
            this.ckDuplex = new System.Windows.Forms.CheckBox();
            this.groupSize = new System.Windows.Forms.GroupBox();
            this.comboSize = new System.Windows.Forms.ComboBox();
            this.groupDepth = new System.Windows.Forms.GroupBox();
            this.comboDepth = new System.Windows.Forms.ComboBox();
            this.groupDPI = new System.Windows.Forms.GroupBox();
            this.comboDPI = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboScanner = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvDocumentos = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnBajar = new System.Windows.Forms.Button();
            this.btnSubir = new System.Windows.Forms.Button();
            this.cboZoom = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvTiposDocumentos = new System.Windows.Forms.TreeView();
            this.CMSOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDigitalizar = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PGExpediente = new PropertyGridEx.PropertyGridEx();
            this.iTXTidUnico = new Digitalizacion2014.Controles.InnovaTXT();
            this.tblImagenes = new Digitalizacion2014.Controles.ThumbnailList();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.TabWizard.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupExpediente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupDuplex.SuspendLayout();
            this.groupSize.SuspendLayout();
            this.groupDepth.SuspendLayout();
            this.groupDPI.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.CMSOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.lblExpDescripcion);
            this.panel1.Controls.Add(this.lblExpediente);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 56);
            this.panel1.TabIndex = 0;
            // 
            // lblExpDescripcion
            // 
            this.lblExpDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExpDescripcion.AutoEllipsis = true;
            this.lblExpDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpDescripcion.Location = new System.Drawing.Point(341, 18);
            this.lblExpDescripcion.Name = "lblExpDescripcion";
            this.lblExpDescripcion.Size = new System.Drawing.Size(284, 30);
            this.lblExpDescripcion.TabIndex = 2;
            this.lblExpDescripcion.Text = "Contrato Federal";
            this.lblExpDescripcion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblExpediente
            // 
            this.lblExpediente.AutoEllipsis = true;
            this.lblExpediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpediente.Location = new System.Drawing.Point(67, 18);
            this.lblExpediente.Name = "lblExpediente";
            this.lblExpediente.Size = new System.Drawing.Size(213, 30);
            this.lblExpediente.TabIndex = 1;
            this.lblExpediente.Text = "Digitalizar Expediente";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAnterior);
            this.panel2.Controls.Add(this.btnSiguiente);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnFinalizar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 434);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(637, 40);
            this.panel2.TabIndex = 1;
            // 
            // btnAnterior
            // 
            this.btnAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnterior.Enabled = false;
            this.btnAnterior.Image = ((System.Drawing.Image)(resources.GetObject("btnAnterior.Image")));
            this.btnAnterior.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnterior.Location = new System.Drawing.Point(373, 3);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(83, 33);
            this.btnAnterior.TabIndex = 3;
            this.btnAnterior.Text = "&Anterior";
            this.btnAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSiguiente.Image = ((System.Drawing.Image)(resources.GetObject("btnSiguiente.Image")));
            this.btnSiguiente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSiguiente.Location = new System.Drawing.Point(462, 3);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(83, 33);
            this.btnSiguiente.TabIndex = 0;
            this.btnSiguiente.Text = "&Siguiente";
            this.btnSiguiente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(551, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 33);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFinalizar.Image = ((System.Drawing.Image)(resources.GetObject("btnFinalizar.Image")));
            this.btnFinalizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFinalizar.Location = new System.Drawing.Point(462, 3);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(83, 33);
            this.btnFinalizar.TabIndex = 2;
            this.btnFinalizar.Text = "&Finalizar";
            this.btnFinalizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Visible = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // TabWizard
            // 
            this.TabWizard.Controls.Add(this.tabPage1);
            this.TabWizard.Controls.Add(this.tabPage2);
            this.TabWizard.Controls.Add(this.tabPage3);
            this.TabWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabWizard.ItemSize = new System.Drawing.Size(0, 1);
            this.TabWizard.Location = new System.Drawing.Point(0, 56);
            this.TabWizard.Multiline = true;
            this.TabWizard.Name = "TabWizard";
            this.TabWizard.SelectedIndex = 0;
            this.TabWizard.Size = new System.Drawing.Size(637, 378);
            this.TabWizard.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabWizard.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage1.Controls.Add(this.groupExpediente);
            this.tabPage1.Controls.Add(this.pictureBox2);
            this.tabPage1.Controls.Add(this.btnAllSettings);
            this.tabPage1.Controls.Add(this.groupDuplex);
            this.tabPage1.Controls.Add(this.groupSize);
            this.tabPage1.Controls.Add(this.groupDepth);
            this.tabPage1.Controls.Add(this.groupDPI);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(629, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1";
            // 
            // groupExpediente
            // 
            this.groupExpediente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupExpediente.Controls.Add(this.PGExpediente);
            this.groupExpediente.Controls.Add(this.iTXTidUnico);
            this.groupExpediente.Controls.Add(this.label2);
            this.groupExpediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupExpediente.Location = new System.Drawing.Point(201, 49);
            this.groupExpediente.Name = "groupExpediente";
            this.groupExpediente.Size = new System.Drawing.Size(419, 314);
            this.groupExpediente.TabIndex = 12;
            this.groupExpediente.TabStop = false;
            this.groupExpediente.Text = "Datos de Expediente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Identificador :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(8, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // btnAllSettings
            // 
            this.btnAllSettings.Enabled = false;
            this.btnAllSettings.Location = new System.Drawing.Point(13, 309);
            this.btnAllSettings.Name = "btnAllSettings";
            this.btnAllSettings.Size = new System.Drawing.Size(177, 38);
            this.btnAllSettings.TabIndex = 11;
            this.btnAllSettings.Text = "Abrir propiedades del Scanner";
            this.btnAllSettings.UseVisualStyleBackColor = true;
            this.btnAllSettings.Click += new System.EventHandler(this.btnAllSettings_Click);
            // 
            // groupDuplex
            // 
            this.groupDuplex.Controls.Add(this.ckDuplex);
            this.groupDuplex.Enabled = false;
            this.groupDuplex.Location = new System.Drawing.Point(13, 267);
            this.groupDuplex.Margin = new System.Windows.Forms.Padding(8);
            this.groupDuplex.Name = "groupDuplex";
            this.groupDuplex.Size = new System.Drawing.Size(177, 42);
            this.groupDuplex.TabIndex = 10;
            this.groupDuplex.TabStop = false;
            this.groupDuplex.Text = "Duplex";
            // 
            // ckDuplex
            // 
            this.ckDuplex.AutoSize = true;
            this.ckDuplex.Location = new System.Drawing.Point(18, 19);
            this.ckDuplex.Name = "ckDuplex";
            this.ckDuplex.Size = new System.Drawing.Size(64, 17);
            this.ckDuplex.TabIndex = 0;
            this.ckDuplex.Text = "Habilitar";
            this.ckDuplex.UseVisualStyleBackColor = true;
            this.ckDuplex.CheckedChanged += new System.EventHandler(this.ckDuplex_CheckedChanged);
            // 
            // groupSize
            // 
            this.groupSize.Controls.Add(this.comboSize);
            this.groupSize.Enabled = false;
            this.groupSize.Location = new System.Drawing.Point(13, 213);
            this.groupSize.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupSize.Name = "groupSize";
            this.groupSize.Size = new System.Drawing.Size(177, 54);
            this.groupSize.TabIndex = 9;
            this.groupSize.TabStop = false;
            this.groupSize.Text = "Tamaño";
            // 
            // comboSize
            // 
            this.comboSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSize.FormattingEnabled = true;
            this.comboSize.Location = new System.Drawing.Point(18, 19);
            this.comboSize.Name = "comboSize";
            this.comboSize.Size = new System.Drawing.Size(140, 21);
            this.comboSize.TabIndex = 0;
            this.comboSize.SelectedIndexChanged += new System.EventHandler(this.comboSize_SelectedIndexChanged);
            // 
            // groupDepth
            // 
            this.groupDepth.Controls.Add(this.comboDepth);
            this.groupDepth.Enabled = false;
            this.groupDepth.Location = new System.Drawing.Point(13, 159);
            this.groupDepth.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupDepth.Name = "groupDepth";
            this.groupDepth.Size = new System.Drawing.Size(177, 54);
            this.groupDepth.TabIndex = 8;
            this.groupDepth.TabStop = false;
            this.groupDepth.Text = "Formato";
            // 
            // comboDepth
            // 
            this.comboDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDepth.FormattingEnabled = true;
            this.comboDepth.Location = new System.Drawing.Point(18, 19);
            this.comboDepth.Name = "comboDepth";
            this.comboDepth.Size = new System.Drawing.Size(140, 21);
            this.comboDepth.TabIndex = 0;
            this.comboDepth.SelectedIndexChanged += new System.EventHandler(this.comboDepth_SelectedIndexChanged);
            // 
            // groupDPI
            // 
            this.groupDPI.Controls.Add(this.comboDPI);
            this.groupDPI.Enabled = false;
            this.groupDPI.Location = new System.Drawing.Point(13, 105);
            this.groupDPI.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupDPI.Name = "groupDPI";
            this.groupDPI.Size = new System.Drawing.Size(177, 54);
            this.groupDPI.TabIndex = 7;
            this.groupDPI.TabStop = false;
            this.groupDPI.Text = "DPI";
            // 
            // comboDPI
            // 
            this.comboDPI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDPI.FormattingEnabled = true;
            this.comboDPI.Location = new System.Drawing.Point(18, 19);
            this.comboDPI.Name = "comboDPI";
            this.comboDPI.Size = new System.Drawing.Size(140, 21);
            this.comboDPI.TabIndex = 0;
            this.comboDPI.SelectedIndexChanged += new System.EventHandler(this.comboDPI_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboScanner);
            this.groupBox1.Location = new System.Drawing.Point(13, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 55);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scanner";
            // 
            // cboScanner
            // 
            this.cboScanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboScanner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScanner.FormattingEnabled = true;
            this.cboScanner.Location = new System.Drawing.Point(18, 20);
            this.cboScanner.Name = "cboScanner";
            this.cboScanner.Size = new System.Drawing.Size(140, 21);
            this.cboScanner.TabIndex = 0;
            this.cboScanner.SelectedIndexChanged += new System.EventHandler(this.cboScanner_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuracion inicial";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage2.Controls.Add(this.lvDocumentos);
            this.tabPage2.Controls.Add(this.pictureBox3);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(629, 369);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2";
            // 
            // lvDocumentos
            // 
            this.lvDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDocumentos.CheckBoxes = true;
            this.lvDocumentos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvDocumentos.FullRowSelect = true;
            this.lvDocumentos.GridLines = true;
            listViewItem2.StateImageIndex = 0;
            this.lvDocumentos.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.lvDocumentos.LabelEdit = true;
            this.lvDocumentos.Location = new System.Drawing.Point(8, 59);
            this.lvDocumentos.Name = "lvDocumentos";
            this.lvDocumentos.Size = new System.Drawing.Size(613, 292);
            this.lvDocumentos.TabIndex = 6;
            this.lvDocumentos.UseCompatibleStateImageBehavior = false;
            this.lvDocumentos.View = System.Windows.Forms.View.Details;
            this.lvDocumentos.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvDocumentos_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tipo de Documento";
            this.columnHeader1.Width = 250;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Paginas";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 50;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(38, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Documentos a Digitalizar";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage3.Controls.Add(this.btnBajar);
            this.tabPage3.Controls.Add(this.btnSubir);
            this.tabPage3.Controls.Add(this.cboZoom);
            this.tabPage3.Controls.Add(this.splitContainer1);
            this.tabPage3.Controls.Add(this.btnDigitalizar);
            this.tabPage3.Controls.Add(this.pictureBox4);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(629, 369);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = " ";
            // 
            // btnBajar
            // 
            this.btnBajar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBajar.Enabled = false;
            this.btnBajar.Image = ((System.Drawing.Image)(resources.GetObject("btnBajar.Image")));
            this.btnBajar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBajar.Location = new System.Drawing.Point(378, 6);
            this.btnBajar.Name = "btnBajar";
            this.btnBajar.Size = new System.Drawing.Size(57, 33);
            this.btnBajar.TabIndex = 9;
            this.btnBajar.Text = "&Bajar";
            this.btnBajar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBajar.UseVisualStyleBackColor = true;
            this.btnBajar.Click += new System.EventHandler(this.btnBajar_Click);
            // 
            // btnSubir
            // 
            this.btnSubir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubir.Enabled = false;
            this.btnSubir.Image = ((System.Drawing.Image)(resources.GetObject("btnSubir.Image")));
            this.btnSubir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubir.Location = new System.Drawing.Point(313, 6);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(57, 33);
            this.btnSubir.TabIndex = 8;
            this.btnSubir.Text = "S&ubir";
            this.btnSubir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSubir.UseVisualStyleBackColor = true;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // cboZoom
            // 
            this.cboZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboZoom.BackColor = System.Drawing.SystemColors.Window;
            this.cboZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZoom.FormattingEnabled = true;
            this.cboZoom.Items.AddRange(new object[] {
            "32x32",
            "64x64",
            "96x96",
            "128x128",
            "196x196",
            "254x254"});
            this.cboZoom.Location = new System.Drawing.Point(441, 13);
            this.cboZoom.Name = "cboZoom";
            this.cboZoom.Size = new System.Drawing.Size(86, 21);
            this.cboZoom.TabIndex = 7;
            this.cboZoom.SelectedIndexChanged += new System.EventHandler(this.cboZoom_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 45);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvTiposDocumentos);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tblImagenes);
            this.splitContainer1.Size = new System.Drawing.Size(629, 324);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.TabIndex = 6;
            // 
            // tvTiposDocumentos
            // 
            this.tvTiposDocumentos.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tvTiposDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvTiposDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTiposDocumentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvTiposDocumentos.HideSelection = false;
            this.tvTiposDocumentos.Location = new System.Drawing.Point(0, 0);
            this.tvTiposDocumentos.Name = "tvTiposDocumentos";
            this.tvTiposDocumentos.ShowLines = false;
            this.tvTiposDocumentos.Size = new System.Drawing.Size(209, 324);
            this.tvTiposDocumentos.TabIndex = 2;
            this.tvTiposDocumentos.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTiposDocumentos_AfterSelect);
            // 
            // CMSOpciones
            // 
            this.CMSOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importarToolStripMenuItem,
            this.ScannerToolStripMenuItem});
            this.CMSOpciones.Name = "CMSOpciones";
            this.CMSOpciones.Size = new System.Drawing.Size(127, 48);
            // 
            // importarToolStripMenuItem
            // 
            this.importarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importarToolStripMenuItem.Image")));
            this.importarToolStripMenuItem.Name = "importarToolStripMenuItem";
            this.importarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.importarToolStripMenuItem.Text = "Importar";
            this.importarToolStripMenuItem.Click += new System.EventHandler(this.importarToolStripMenuItem_Click);
            // 
            // ScannerToolStripMenuItem
            // 
            this.ScannerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ScannerToolStripMenuItem.Image")));
            this.ScannerToolStripMenuItem.Name = "ScannerToolStripMenuItem";
            this.ScannerToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ScannerToolStripMenuItem.Text = "Digitalizar";
            this.ScannerToolStripMenuItem.Click += new System.EventHandler(this.ScannerToolStripMenuItem_Click);
            // 
            // btnDigitalizar
            // 
            this.btnDigitalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDigitalizar.Image = ((System.Drawing.Image)(resources.GetObject("btnDigitalizar.Image")));
            this.btnDigitalizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDigitalizar.Location = new System.Drawing.Point(533, 6);
            this.btnDigitalizar.Name = "btnDigitalizar";
            this.btnDigitalizar.Size = new System.Drawing.Size(88, 33);
            this.btnDigitalizar.TabIndex = 4;
            this.btnDigitalizar.Text = "&Digitalizar";
            this.btnDigitalizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDigitalizar.UseVisualStyleBackColor = true;
            this.btnDigitalizar.Click += new System.EventHandler(this.btnDigitalizar_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(8, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(38, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Proceso de Digitalización";
            // 
            // PGExpediente
            // 
            this.PGExpediente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PGExpediente.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            // 
            // 
            // 
            this.PGExpediente.DocCommentDescription.AutoEllipsis = true;
            this.PGExpediente.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.PGExpediente.DocCommentDescription.Location = new System.Drawing.Point(3, 18);
            this.PGExpediente.DocCommentDescription.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.PGExpediente.DocCommentDescription.Name = "";
            this.PGExpediente.DocCommentDescription.Size = new System.Drawing.Size(287, 37);
            this.PGExpediente.DocCommentDescription.TabIndex = 1;
            this.PGExpediente.DocCommentImage = null;
            // 
            // 
            // 
            this.PGExpediente.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.PGExpediente.DocCommentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.PGExpediente.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.PGExpediente.DocCommentTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.PGExpediente.DocCommentTitle.Name = "";
            this.PGExpediente.DocCommentTitle.Size = new System.Drawing.Size(287, 15);
            this.PGExpediente.DocCommentTitle.TabIndex = 0;
            this.PGExpediente.DocCommentTitle.UseMnemonic = false;
            this.PGExpediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PGExpediente.Location = new System.Drawing.Point(118, 68);
            this.PGExpediente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PGExpediente.Name = "PGExpediente";
            this.PGExpediente.Size = new System.Drawing.Size(293, 238);
            this.PGExpediente.TabIndex = 1;
            // 
            // 
            // 
            this.PGExpediente.ToolStrip.AccessibleName = "Barra de herramientas";
            this.PGExpediente.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.PGExpediente.ToolStrip.AllowMerge = false;
            this.PGExpediente.ToolStrip.AutoSize = false;
            this.PGExpediente.ToolStrip.CanOverflow = false;
            this.PGExpediente.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.PGExpediente.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.PGExpediente.ToolStrip.Location = new System.Drawing.Point(0, 1);
            this.PGExpediente.ToolStrip.Name = "";
            this.PGExpediente.ToolStrip.Padding = new System.Windows.Forms.Padding(4, 0, 3, 0);
            this.PGExpediente.ToolStrip.Size = new System.Drawing.Size(293, 25);
            this.PGExpediente.ToolStrip.TabIndex = 1;
            this.PGExpediente.ToolStrip.TabStop = true;
            this.PGExpediente.ToolStrip.Text = "PropertyGridToolBar";
            // 
            // iTXTidUnico
            // 
            this.iTXTidUnico.ActivarAyuda = false;
            this.iTXTidUnico.ActivarEnter = false;
            this.iTXTidUnico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iTXTidUnico.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTidUnico.Catalogo = "";
            this.iTXTidUnico.colTexto = 0;
            this.iTXTidUnico.ControlDestinoDescripcion = null;
            this.iTXTidUnico.IsNumeric = false;
            this.iTXTidUnico.Location = new System.Drawing.Point(118, 35);
            this.iTXTidUnico.Name = "iTXTidUnico";
            this.iTXTidUnico.Size = new System.Drawing.Size(293, 26);
            this.iTXTidUnico.TabIndex = 0;
            this.iTXTidUnico.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            this.iTXTidUnico.TextChanged += new System.EventHandler(this.iTXTidUnico_TextChanged);
            this.iTXTidUnico.Leave += new System.EventHandler(this.iTXTidUnico_Leave);
            // 
            // tblImagenes
            // 
            this.tblImagenes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tblImagenes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblImagenes.ContextMenuStrip = this.CMSOpciones;
            this.tblImagenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblImagenes.HideSelection = false;
            this.tblImagenes.Location = new System.Drawing.Point(0, 0);
            this.tblImagenes.Name = "tblImagenes";
            this.tblImagenes.Size = new System.Drawing.Size(416, 324);
            this.tblImagenes.TabIndex = 0;
            this.tblImagenes.UseCompatibleStateImageBehavior = false;
            this.tblImagenes.ItemActivate += new System.EventHandler(this.tblImagenes_ItemActivate);
            this.tblImagenes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblImagenes_KeyDown);
            this.tblImagenes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tblImagenes_KeyUp);
            this.tblImagenes.MouseLeave += new System.EventHandler(this.tblImagenes_MouseLeave);
            this.tblImagenes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tblImagenes_MouseMove);
            // 
            // frmDigitalizarExp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 474);
            this.Controls.Add(this.TabWizard);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDigitalizarExp";
            this.Text = "Digitalizar Expediente";
            this.Load += new System.EventHandler(this.frmDigitalizarExp_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.TabWizard.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupExpediente.ResumeLayout(false);
            this.groupExpediente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupDuplex.ResumeLayout(false);
            this.groupDuplex.PerformLayout();
            this.groupSize.ResumeLayout(false);
            this.groupDepth.ResumeLayout(false);
            this.groupDPI.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.CMSOpciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblExpediente;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.TabControl TabWizard;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboScanner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupDuplex;
        private System.Windows.Forms.CheckBox ckDuplex;
        private System.Windows.Forms.GroupBox groupSize;
        private System.Windows.Forms.ComboBox comboSize;
        private System.Windows.Forms.GroupBox groupDepth;
        private System.Windows.Forms.ComboBox comboDepth;
        private System.Windows.Forms.GroupBox groupDPI;
        private System.Windows.Forms.ComboBox comboDPI;
        private System.Windows.Forms.Button btnAllSettings;
        private System.Windows.Forms.Label lblExpDescripcion;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupExpediente;
        private Controles.InnovaTXT iTXTidUnico;
        private System.Windows.Forms.Label label2;
        private PropertyGridEx.PropertyGridEx PGExpediente;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvDocumentos;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDigitalizar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvTiposDocumentos;
        private Controles.ThumbnailList tblImagenes;
        private System.Windows.Forms.ComboBox cboZoom;
        private System.Windows.Forms.Button btnBajar;
        private System.Windows.Forms.Button btnSubir;
        private System.Windows.Forms.ContextMenuStrip CMSOpciones;
        private System.Windows.Forms.ToolStripMenuItem importarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ScannerToolStripMenuItem;
    }
}