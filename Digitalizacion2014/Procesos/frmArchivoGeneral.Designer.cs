namespace Digitalizacion2014.Procesos
{
    partial class frmArchivoGeneral
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArchivoGeneral));
            this.SC01 = new System.Windows.Forms.SplitContainer();
            this.tvArbolGeneral = new System.Windows.Forms.TreeView();
            this.ILImagenes = new System.Windows.Forms.ImageList(this.components);
            this.SP02 = new System.Windows.Forms.SplitContainer();
            this.tblImagenes = new Digitalizacion2014.Controles.ThumbnailList();
            this.cmsImagenes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seleccionarTodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.agregarDesdeScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sustituirPáginaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desdeScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desdeArchivoJPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_pages = new System.Windows.Forms.Panel();
            this.txtPage = new Digitalizacion2014.Controles.InnovaTXT();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRecords = new System.Windows.Forms.ComboBox();
            this.cboZoom = new System.Windows.Forms.ComboBox();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.PGECampos = new PropertyGridEx.PropertyGridEx();
            this.cmsContextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.agregarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deshabilitarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habilitarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparador1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMenuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.resumenDeExpedientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmdProcesos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generarExpedientesPDFsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarExpedientesPDFsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.SC01)).BeginInit();
            this.SC01.Panel1.SuspendLayout();
            this.SC01.Panel2.SuspendLayout();
            this.SC01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SP02)).BeginInit();
            this.SP02.Panel1.SuspendLayout();
            this.SP02.Panel2.SuspendLayout();
            this.SP02.SuspendLayout();
            this.cmsImagenes.SuspendLayout();
            this.Panel_pages.SuspendLayout();
            this.cmsContextual.SuspendLayout();
            this.cmdProcesos.SuspendLayout();
            this.SuspendLayout();
            // 
            // SC01
            // 
            this.SC01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC01.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SC01.Location = new System.Drawing.Point(0, 0);
            this.SC01.Name = "SC01";
            // 
            // SC01.Panel1
            // 
            this.SC01.Panel1.Controls.Add(this.tvArbolGeneral);
            // 
            // SC01.Panel2
            // 
            this.SC01.Panel2.Controls.Add(this.SP02);
            this.SC01.Size = new System.Drawing.Size(812, 513);
            this.SC01.SplitterDistance = 226;
            this.SC01.TabIndex = 0;
            // 
            // tvArbolGeneral
            // 
            this.tvArbolGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvArbolGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvArbolGeneral.HideSelection = false;
            this.tvArbolGeneral.ImageIndex = 0;
            this.tvArbolGeneral.ImageList = this.ILImagenes;
            this.tvArbolGeneral.LabelEdit = true;
            this.tvArbolGeneral.Location = new System.Drawing.Point(0, 0);
            this.tvArbolGeneral.Name = "tvArbolGeneral";
            this.tvArbolGeneral.SelectedImageIndex = 0;
            this.tvArbolGeneral.Size = new System.Drawing.Size(226, 513);
            this.tvArbolGeneral.TabIndex = 1;
            this.tvArbolGeneral.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvArbolGeneral_BeforeLabelEdit);
            this.tvArbolGeneral.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvArbolGeneral_AfterLabelEdit);
            this.tvArbolGeneral.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvArbolGeneral_AfterCollapse);
            this.tvArbolGeneral.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvArbolGeneral_AfterExpand);
            this.tvArbolGeneral.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvArbolGeneral_AfterSelect);
            this.tvArbolGeneral.DoubleClick += new System.EventHandler(this.tvArbolGeneral_DoubleClick);
            this.tvArbolGeneral.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvArbolGeneral_MouseDown);
            this.tvArbolGeneral.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvArbolGeneral_MouseUp);
            // 
            // ILImagenes
            // 
            this.ILImagenes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ILImagenes.ImageStream")));
            this.ILImagenes.TransparentColor = System.Drawing.Color.Transparent;
            this.ILImagenes.Images.SetKeyName(0, "FolderOpen");
            this.ILImagenes.Images.SetKeyName(1, "FolderClose");
            this.ILImagenes.Images.SetKeyName(2, "Expediente");
            this.ILImagenes.Images.SetKeyName(3, "Documento");
            // 
            // SP02
            // 
            this.SP02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SP02.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SP02.Location = new System.Drawing.Point(0, 0);
            this.SP02.Name = "SP02";
            // 
            // SP02.Panel1
            // 
            this.SP02.Panel1.Controls.Add(this.tblImagenes);
            this.SP02.Panel1.Controls.Add(this.Panel_pages);
            // 
            // SP02.Panel2
            // 
            this.SP02.Panel2.Controls.Add(this.PGECampos);
            this.SP02.Size = new System.Drawing.Size(582, 513);
            this.SP02.SplitterDistance = 363;
            this.SP02.TabIndex = 0;
            // 
            // tblImagenes
            // 
            this.tblImagenes.ContextMenuStrip = this.cmsImagenes;
            this.tblImagenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblImagenes.Location = new System.Drawing.Point(0, 0);
            this.tblImagenes.Name = "tblImagenes";
            this.tblImagenes.Size = new System.Drawing.Size(363, 481);
            this.tblImagenes.TabIndex = 0;
            this.tblImagenes.UseCompatibleStateImageBehavior = false;
            this.tblImagenes.ItemActivate += new System.EventHandler(this.tblImagenes_ItemActivate);
            this.tblImagenes.SelectedIndexChanged += new System.EventHandler(this.tblImagenes_SelectedIndexChanged);
            this.tblImagenes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblImagenes_KeyDown);
            this.tblImagenes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tblImagenes_KeyUp);
            this.tblImagenes.MouseLeave += new System.EventHandler(this.tblImagenes_MouseLeave);
            this.tblImagenes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tblImagenes_MouseMove);
            // 
            // cmsImagenes
            // 
            this.cmsImagenes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualizarToolStripMenuItem,
            this.seleccionarTodoToolStripMenuItem,
            this.copiarToolStripMenuItem1,
            this.toolStripSeparator1,
            this.agregarDesdeScannerToolStripMenuItem,
            this.sustituirPáginaToolStripMenuItem});
            this.cmsImagenes.Name = "cmsImagenes";
            this.cmsImagenes.Size = new System.Drawing.Size(205, 120);
            this.cmsImagenes.Opening += new System.ComponentModel.CancelEventHandler(this.cmsImagenes_Opening);
            // 
            // visualizarToolStripMenuItem
            // 
            this.visualizarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("visualizarToolStripMenuItem.Image")));
            this.visualizarToolStripMenuItem.Name = "visualizarToolStripMenuItem";
            this.visualizarToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.visualizarToolStripMenuItem.Text = "Visualizar";
            this.visualizarToolStripMenuItem.Click += new System.EventHandler(this.visualizarToolStripMenuItem_Click);
            // 
            // seleccionarTodoToolStripMenuItem
            // 
            this.seleccionarTodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("seleccionarTodoToolStripMenuItem.Image")));
            this.seleccionarTodoToolStripMenuItem.Name = "seleccionarTodoToolStripMenuItem";
            this.seleccionarTodoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.seleccionarTodoToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.seleccionarTodoToolStripMenuItem.Text = "Seleccionar todo";
            this.seleccionarTodoToolStripMenuItem.Click += new System.EventHandler(this.seleccionarTodoToolStripMenuItem_Click);
            // 
            // copiarToolStripMenuItem1
            // 
            this.copiarToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("copiarToolStripMenuItem1.Image")));
            this.copiarToolStripMenuItem1.Name = "copiarToolStripMenuItem1";
            this.copiarToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copiarToolStripMenuItem1.Size = new System.Drawing.Size(204, 22);
            this.copiarToolStripMenuItem1.Text = "Copiar";
            this.copiarToolStripMenuItem1.Click += new System.EventHandler(this.copiarToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // agregarDesdeScannerToolStripMenuItem
            // 
            this.agregarDesdeScannerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("agregarDesdeScannerToolStripMenuItem.Image")));
            this.agregarDesdeScannerToolStripMenuItem.Name = "agregarDesdeScannerToolStripMenuItem";
            this.agregarDesdeScannerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.agregarDesdeScannerToolStripMenuItem.Text = "Agregar Páginas";
            this.agregarDesdeScannerToolStripMenuItem.Click += new System.EventHandler(this.agregarDesdeScannerToolStripMenuItem_Click);
            // 
            // sustituirPáginaToolStripMenuItem
            // 
            this.sustituirPáginaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desdeScannerToolStripMenuItem,
            this.desdeArchivoJPGToolStripMenuItem});
            this.sustituirPáginaToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sustituirPáginaToolStripMenuItem.Image")));
            this.sustituirPáginaToolStripMenuItem.Name = "sustituirPáginaToolStripMenuItem";
            this.sustituirPáginaToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.sustituirPáginaToolStripMenuItem.Text = "Sustituir Página";
            // 
            // desdeScannerToolStripMenuItem
            // 
            this.desdeScannerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("desdeScannerToolStripMenuItem.Image")));
            this.desdeScannerToolStripMenuItem.Name = "desdeScannerToolStripMenuItem";
            this.desdeScannerToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.desdeScannerToolStripMenuItem.Text = "Desde Scanner";
            // 
            // desdeArchivoJPGToolStripMenuItem
            // 
            this.desdeArchivoJPGToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("desdeArchivoJPGToolStripMenuItem.Image")));
            this.desdeArchivoJPGToolStripMenuItem.Name = "desdeArchivoJPGToolStripMenuItem";
            this.desdeArchivoJPGToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.desdeArchivoJPGToolStripMenuItem.Text = "Desde archivo JPG";
            // 
            // Panel_pages
            // 
            this.Panel_pages.Controls.Add(this.txtPage);
            this.Panel_pages.Controls.Add(this.label1);
            this.Panel_pages.Controls.Add(this.cboRecords);
            this.Panel_pages.Controls.Add(this.cboZoom);
            this.Panel_pages.Controls.Add(this.btnFirst);
            this.Panel_pages.Controls.Add(this.btnPrevious);
            this.Panel_pages.Controls.Add(this.btnNext);
            this.Panel_pages.Controls.Add(this.btnLast);
            this.Panel_pages.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_pages.Location = new System.Drawing.Point(0, 481);
            this.Panel_pages.Name = "Panel_pages";
            this.Panel_pages.Size = new System.Drawing.Size(363, 32);
            this.Panel_pages.TabIndex = 9;
            // 
            // txtPage
            // 
            this.txtPage.ActivarAyuda = false;
            this.txtPage.ActivarEnter = false;
            this.txtPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPage.BackColorFocus = System.Drawing.Color.Yellow;
            this.txtPage.Catalogo = "";
            this.txtPage.colTexto = 0;
            this.txtPage.ControlDestinoDescripcion = null;
            this.txtPage.IsNumeric = true;
            this.txtPage.Location = new System.Drawing.Point(273, 6);
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(31, 20);
            this.txtPage.TabIndex = 11;
            this.txtPage.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            this.txtPage.Text = "1";
            this.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPage.TextChanged += new System.EventHandler(this.txtPage_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(95, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Imagenes";
            // 
            // cboRecords
            // 
            this.cboRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRecords.BackColor = System.Drawing.SystemColors.Window;
            this.cboRecords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRecords.FormattingEnabled = true;
            this.cboRecords.Items.AddRange(new object[] {
            "20",
            "30",
            "40",
            "50"});
            this.cboRecords.Location = new System.Drawing.Point(161, 6);
            this.cboRecords.Name = "cboRecords";
            this.cboRecords.Size = new System.Drawing.Size(53, 21);
            this.cboRecords.TabIndex = 9;
            this.cboRecords.SelectedIndexChanged += new System.EventHandler(this.cboRecords_SelectedIndexChanged);
            // 
            // cboZoom
            // 
            this.cboZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboZoom.BackColor = System.Drawing.SystemColors.Window;
            this.cboZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboZoom.FormattingEnabled = true;
            this.cboZoom.Items.AddRange(new object[] {
            "32x32",
            "64x64",
            "96x96",
            "128x128",
            "196x196",
            "254x254"});
            this.cboZoom.Location = new System.Drawing.Point(3, 5);
            this.cboZoom.Name = "cboZoom";
            this.cboZoom.Size = new System.Drawing.Size(86, 21);
            this.cboZoom.TabIndex = 8;
            this.cboZoom.SelectedIndexChanged += new System.EventHandler(this.cboZoom_SelectedIndexChanged);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Location = new System.Drawing.Point(220, 5);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(23, 23);
            this.btnFirst.TabIndex = 4;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrevious.Location = new System.Drawing.Point(243, 5);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(23, 23);
            this.btnPrevious.TabIndex = 3;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(310, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(23, 23);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(333, 5);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(23, 23);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // PGECampos
            // 
            this.PGECampos.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            // 
            // 
            // 
            this.PGECampos.DocCommentDescription.AccessibleName = "";
            this.PGECampos.DocCommentDescription.AutoEllipsis = true;
            this.PGECampos.DocCommentDescription.Cursor = System.Windows.Forms.Cursors.Default;
            this.PGECampos.DocCommentDescription.Location = new System.Drawing.Point(3, 18);
            this.PGECampos.DocCommentDescription.Name = "";
            this.PGECampos.DocCommentDescription.Size = new System.Drawing.Size(209, 37);
            this.PGECampos.DocCommentDescription.TabIndex = 1;
            this.PGECampos.DocCommentImage = null;
            // 
            // 
            // 
            this.PGECampos.DocCommentTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.PGECampos.DocCommentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.PGECampos.DocCommentTitle.Location = new System.Drawing.Point(3, 3);
            this.PGECampos.DocCommentTitle.Name = "";
            this.PGECampos.DocCommentTitle.Size = new System.Drawing.Size(209, 15);
            this.PGECampos.DocCommentTitle.TabIndex = 0;
            this.PGECampos.DocCommentTitle.UseMnemonic = false;
            this.PGECampos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PGECampos.DrawFlatToolbar = true;
            this.PGECampos.Location = new System.Drawing.Point(0, 0);
            this.PGECampos.Name = "PGECampos";
            this.PGECampos.ShowCustomPropertiesSet = true;
            this.PGECampos.Size = new System.Drawing.Size(215, 513);
            this.PGECampos.TabIndex = 0;
            // 
            // 
            // 
            this.PGECampos.ToolStrip.AccessibleName = "Barra de herramientas";
            this.PGECampos.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.PGECampos.ToolStrip.AllowMerge = false;
            this.PGECampos.ToolStrip.AutoSize = false;
            this.PGECampos.ToolStrip.CanOverflow = false;
            this.PGECampos.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.PGECampos.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.PGECampos.ToolStrip.Location = new System.Drawing.Point(0, 1);
            this.PGECampos.ToolStrip.Name = "";
            this.PGECampos.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
            this.PGECampos.ToolStrip.Size = new System.Drawing.Size(215, 25);
            this.PGECampos.ToolStrip.TabIndex = 1;
            this.PGECampos.ToolStrip.TabStop = true;
            this.PGECampos.ToolStrip.Text = "PropertyGridToolBar";
            this.PGECampos.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PGECampos_PropertyValueChanged);
            // 
            // cmsContextual
            // 
            this.cmsContextual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmsContextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarNodoToolStripMenuItem,
            this.deshabilitarNodoToolStripMenuItem,
            this.habilitarNodoToolStripMenuItem,
            this.eliminarNodoToolStripMenuItem,
            this.copiarToolStripMenuItem,
            this.generarPDFToolStripMenuItem,
            this.generarExpedientesPDFsToolStripMenuItem1,
            this.tsSeparador1,
            this.tsMenuReportes});
            this.cmsContextual.Name = "cmsContextual";
            this.cmsContextual.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsContextual.Size = new System.Drawing.Size(213, 208);
            // 
            // agregarNodoToolStripMenuItem
            // 
            this.agregarNodoToolStripMenuItem.Enabled = false;
            this.agregarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("agregarNodoToolStripMenuItem.Image")));
            this.agregarNodoToolStripMenuItem.Name = "agregarNodoToolStripMenuItem";
            this.agregarNodoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.agregarNodoToolStripMenuItem.Text = "Agregar Nodo";
            this.agregarNodoToolStripMenuItem.Click += new System.EventHandler(this.agregarNodoToolStripMenuItem_Click);
            // 
            // deshabilitarNodoToolStripMenuItem
            // 
            this.deshabilitarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deshabilitarNodoToolStripMenuItem.Image")));
            this.deshabilitarNodoToolStripMenuItem.Name = "deshabilitarNodoToolStripMenuItem";
            this.deshabilitarNodoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.deshabilitarNodoToolStripMenuItem.Text = "Deshabilitar Nodo";
            this.deshabilitarNodoToolStripMenuItem.Visible = false;
            // 
            // habilitarNodoToolStripMenuItem
            // 
            this.habilitarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("habilitarNodoToolStripMenuItem.Image")));
            this.habilitarNodoToolStripMenuItem.Name = "habilitarNodoToolStripMenuItem";
            this.habilitarNodoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.habilitarNodoToolStripMenuItem.Text = "Habilitar Nodo";
            this.habilitarNodoToolStripMenuItem.Visible = false;
            // 
            // eliminarNodoToolStripMenuItem
            // 
            this.eliminarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eliminarNodoToolStripMenuItem.Image")));
            this.eliminarNodoToolStripMenuItem.Name = "eliminarNodoToolStripMenuItem";
            this.eliminarNodoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.eliminarNodoToolStripMenuItem.Text = "Eliminar Nodo";
            this.eliminarNodoToolStripMenuItem.Visible = false;
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copiarToolStripMenuItem.Image")));
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.copiarToolStripMenuItem.Text = "Copiar Nodo";
            this.copiarToolStripMenuItem.Visible = false;
            // 
            // generarPDFToolStripMenuItem
            // 
            this.generarPDFToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("generarPDFToolStripMenuItem.Image")));
            this.generarPDFToolStripMenuItem.Name = "generarPDFToolStripMenuItem";
            this.generarPDFToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.generarPDFToolStripMenuItem.Text = "Generar PDF";
            this.generarPDFToolStripMenuItem.Visible = false;
            this.generarPDFToolStripMenuItem.Click += new System.EventHandler(this.generarPDFToolStripMenuItem_Click);
            // 
            // tsSeparador1
            // 
            this.tsSeparador1.Name = "tsSeparador1";
            this.tsSeparador1.Size = new System.Drawing.Size(209, 6);
            // 
            // tsMenuReportes
            // 
            this.tsMenuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resumenDeExpedientesToolStripMenuItem});
            this.tsMenuReportes.Name = "tsMenuReportes";
            this.tsMenuReportes.Size = new System.Drawing.Size(212, 22);
            this.tsMenuReportes.Text = "Reportes";
            // 
            // resumenDeExpedientesToolStripMenuItem
            // 
            this.resumenDeExpedientesToolStripMenuItem.Name = "resumenDeExpedientesToolStripMenuItem";
            this.resumenDeExpedientesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.resumenDeExpedientesToolStripMenuItem.Text = "Resumen de Expedientes";
            this.resumenDeExpedientesToolStripMenuItem.Click += new System.EventHandler(this.resumenDeExpedientesToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClosed += new System.EventHandler(this.notifyIcon1_BalloonTipClosed);
            // 
            // cmdProcesos
            // 
            this.cmdProcesos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarExpedientesPDFsToolStripMenuItem});
            this.cmdProcesos.Name = "cmdProcesos";
            this.cmdProcesos.Size = new System.Drawing.Size(213, 26);
            // 
            // generarExpedientesPDFsToolStripMenuItem
            // 
            this.generarExpedientesPDFsToolStripMenuItem.Name = "generarExpedientesPDFsToolStripMenuItem";
            this.generarExpedientesPDFsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.generarExpedientesPDFsToolStripMenuItem.Text = "Generar Expedientes PDF\'s";
            // 
            // generarExpedientesPDFsToolStripMenuItem1
            // 
            this.generarExpedientesPDFsToolStripMenuItem1.Image = global::Digitalizacion2014.Properties.Resources.IconPdf;
            this.generarExpedientesPDFsToolStripMenuItem1.Name = "generarExpedientesPDFsToolStripMenuItem1";
            this.generarExpedientesPDFsToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.generarExpedientesPDFsToolStripMenuItem1.Text = "Generar Expedientes PDF\'s";
            this.generarExpedientesPDFsToolStripMenuItem1.Visible = false;
            this.generarExpedientesPDFsToolStripMenuItem1.Click += new System.EventHandler(this.generarExpedientesPDFsToolStripMenuItem1_Click);
            // 
            // frmArchivoGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(812, 513);
            this.Controls.Add(this.SC01);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmArchivoGeneral";
            this.Text = "Archivo General";
            this.Activated += new System.EventHandler(this.frmArchivoGeneral_Activated);
            this.Deactivate += new System.EventHandler(this.frmArchivoGeneral_Deactivate);
            this.Load += new System.EventHandler(this.frmArchivoGeneral_Load);
            this.Shown += new System.EventHandler(this.frmArchivoGeneral_Shown);
            this.SC01.Panel1.ResumeLayout(false);
            this.SC01.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC01)).EndInit();
            this.SC01.ResumeLayout(false);
            this.SP02.Panel1.ResumeLayout(false);
            this.SP02.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SP02)).EndInit();
            this.SP02.ResumeLayout(false);
            this.cmsImagenes.ResumeLayout(false);
            this.Panel_pages.ResumeLayout(false);
            this.Panel_pages.PerformLayout();
            this.cmsContextual.ResumeLayout(false);
            this.cmdProcesos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SC01;
        private System.Windows.Forms.SplitContainer SP02;
        private System.Windows.Forms.TreeView tvArbolGeneral;
        private System.Windows.Forms.ImageList ILImagenes;
        private System.Windows.Forms.ContextMenuStrip cmsContextual;
        private System.Windows.Forms.ToolStripMenuItem agregarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deshabilitarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habilitarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private Controles.ThumbnailList tblImagenes;
        private System.Windows.Forms.ComboBox cboZoom;
        private System.Windows.Forms.ContextMenuStrip cmsImagenes;
        private System.Windows.Forms.ToolStripMenuItem visualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seleccionarTodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem1;
        private PropertyGridEx.PropertyGridEx PGECampos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem agregarDesdeScannerToolStripMenuItem;
        private System.Windows.Forms.Panel Panel_pages;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.ComboBox cboRecords;
        private System.Windows.Forms.Label label1;
        private Controles.InnovaTXT txtPage;
        private System.Windows.Forms.ToolStripMenuItem generarPDFToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripSeparator tsSeparador1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuReportes;
        private System.Windows.Forms.ToolStripMenuItem resumenDeExpedientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sustituirPáginaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desdeScannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desdeArchivoJPGToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmdProcesos;
        private System.Windows.Forms.ToolStripMenuItem generarExpedientesPDFsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarExpedientesPDFsToolStripMenuItem1;
    }
}
