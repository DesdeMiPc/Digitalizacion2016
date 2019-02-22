namespace Digitalizacion2014.Configuracion
{
    partial class frmConfigArbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigArbol));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvArbolGeneral = new System.Windows.Forms.TreeView();
            this.ILImagenes = new System.Windows.Forms.ImageList(this.components);
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.gbDerechos = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.chkExportar = new System.Windows.Forms.CheckBox();
            this.chkImprimir = new System.Windows.Forms.CheckBox();
            this.chkEliminar = new System.Windows.Forms.CheckBox();
            this.chkAgregar = new System.Windows.Forms.CheckBox();
            this.chkModificar = new System.Windows.Forms.CheckBox();
            this.chkLectura = new System.Windows.Forms.CheckBox();
            this.lbGrupos = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmsContextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.agregarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deshabilitarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.habilitarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarNodoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copiarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbDerechos.SuspendLayout();
            this.cmsContextual.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvArbolGeneral);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnAgregar);
            this.splitContainer1.Panel2.Controls.Add(this.btnEliminar);
            this.splitContainer1.Panel2.Controls.Add(this.gbDerechos);
            this.splitContainer1.Panel2.Controls.Add(this.lbGrupos);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(725, 524);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 0;
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
            this.tvArbolGeneral.Size = new System.Drawing.Size(333, 524);
            this.tvArbolGeneral.TabIndex = 0;
            this.tvArbolGeneral.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvArbolGeneral_BeforeLabelEdit);
            this.tvArbolGeneral.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvArbolGeneral_AfterLabelEdit);
            this.tvArbolGeneral.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvArbolGeneral_AfterCollapse);
            this.tvArbolGeneral.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvArbolGeneral_AfterExpand);
            this.tvArbolGeneral.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvArbolGeneral_AfterSelect);
            this.tvArbolGeneral.DoubleClick += new System.EventHandler(this.tvArbolGeneral_DoubleClick);
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
            // btnAgregar
            // 
            this.btnAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(220, 329);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 27);
            this.btnAgregar.TabIndex = 5;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(301, 329);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 27);
            this.btnEliminar.TabIndex = 4;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // gbDerechos
            // 
            this.gbDerechos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDerechos.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.gbDerechos.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gbDerechos.BackgroundImage")));
            this.gbDerechos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.gbDerechos.Controls.Add(this.btnGuardar);
            this.gbDerechos.Controls.Add(this.chkExportar);
            this.gbDerechos.Controls.Add(this.chkImprimir);
            this.gbDerechos.Controls.Add(this.chkEliminar);
            this.gbDerechos.Controls.Add(this.chkAgregar);
            this.gbDerechos.Controls.Add(this.chkModificar);
            this.gbDerechos.Controls.Add(this.chkLectura);
            this.gbDerechos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDerechos.Location = new System.Drawing.Point(16, 362);
            this.gbDerechos.Name = "gbDerechos";
            this.gbDerechos.Size = new System.Drawing.Size(360, 150);
            this.gbDerechos.TabIndex = 3;
            this.gbDerechos.TabStop = false;
            this.gbDerechos.Text = "Derechos efectivos";
            this.gbDerechos.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(278, 117);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(76, 27);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // chkExportar
            // 
            this.chkExportar.AutoSize = true;
            this.chkExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExportar.Location = new System.Drawing.Point(135, 52);
            this.chkExportar.Name = "chkExportar";
            this.chkExportar.Size = new System.Drawing.Size(83, 22);
            this.chkExportar.TabIndex = 5;
            this.chkExportar.Text = "Exportar";
            this.chkExportar.UseVisualStyleBackColor = true;
            this.chkExportar.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkImprimir
            // 
            this.chkImprimir.AutoSize = true;
            this.chkImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkImprimir.Location = new System.Drawing.Point(135, 24);
            this.chkImprimir.Name = "chkImprimir";
            this.chkImprimir.Size = new System.Drawing.Size(80, 22);
            this.chkImprimir.TabIndex = 4;
            this.chkImprimir.Text = "Imprimir";
            this.chkImprimir.UseVisualStyleBackColor = true;
            this.chkImprimir.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkEliminar
            // 
            this.chkEliminar.AutoSize = true;
            this.chkEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEliminar.Location = new System.Drawing.Point(7, 108);
            this.chkEliminar.Name = "chkEliminar";
            this.chkEliminar.Size = new System.Drawing.Size(80, 22);
            this.chkEliminar.TabIndex = 3;
            this.chkEliminar.Text = "Eliminar";
            this.chkEliminar.UseVisualStyleBackColor = true;
            this.chkEliminar.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkAgregar
            // 
            this.chkAgregar.AutoSize = true;
            this.chkAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAgregar.Location = new System.Drawing.Point(7, 80);
            this.chkAgregar.Name = "chkAgregar";
            this.chkAgregar.Size = new System.Drawing.Size(78, 22);
            this.chkAgregar.TabIndex = 2;
            this.chkAgregar.Text = "Agregar";
            this.chkAgregar.UseVisualStyleBackColor = true;
            this.chkAgregar.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkModificar
            // 
            this.chkModificar.AutoSize = true;
            this.chkModificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkModificar.Location = new System.Drawing.Point(7, 52);
            this.chkModificar.Name = "chkModificar";
            this.chkModificar.Size = new System.Drawing.Size(88, 22);
            this.chkModificar.TabIndex = 1;
            this.chkModificar.Text = "Modificar";
            this.chkModificar.UseVisualStyleBackColor = true;
            this.chkModificar.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkLectura
            // 
            this.chkLectura.AutoSize = true;
            this.chkLectura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLectura.Location = new System.Drawing.Point(7, 24);
            this.chkLectura.Name = "chkLectura";
            this.chkLectura.Size = new System.Drawing.Size(76, 22);
            this.chkLectura.TabIndex = 0;
            this.chkLectura.Text = "Lectura";
            this.chkLectura.UseVisualStyleBackColor = true;
            this.chkLectura.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // lbGrupos
            // 
            this.lbGrupos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGrupos.DisplayMember = "Descripcion";
            this.lbGrupos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrupos.FormattingEnabled = true;
            this.lbGrupos.ItemHeight = 18;
            this.lbGrupos.Location = new System.Drawing.Point(16, 33);
            this.lbGrupos.Name = "lbGrupos";
            this.lbGrupos.Size = new System.Drawing.Size(360, 274);
            this.lbGrupos.TabIndex = 1;
            this.lbGrupos.ValueMember = "id_Grupo";
            this.lbGrupos.SelectedIndexChanged += new System.EventHandler(this.lbGrupos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Derechos de Grupos";
            // 
            // cmsContextual
            // 
            this.cmsContextual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmsContextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarNodoToolStripMenuItem,
            this.deshabilitarNodoToolStripMenuItem,
            this.habilitarNodoToolStripMenuItem,
            this.eliminarNodoToolStripMenuItem,
            this.copiarToolStripMenuItem});
            this.cmsContextual.Name = "cmsContextual";
            this.cmsContextual.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsContextual.Size = new System.Drawing.Size(170, 114);
            // 
            // agregarNodoToolStripMenuItem
            // 
            this.agregarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("agregarNodoToolStripMenuItem.Image")));
            this.agregarNodoToolStripMenuItem.Name = "agregarNodoToolStripMenuItem";
            this.agregarNodoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.agregarNodoToolStripMenuItem.Text = "Agregar Nodo";
            this.agregarNodoToolStripMenuItem.Click += new System.EventHandler(this.agregarNodoToolStripMenuItem_Click);
            // 
            // deshabilitarNodoToolStripMenuItem
            // 
            this.deshabilitarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deshabilitarNodoToolStripMenuItem.Image")));
            this.deshabilitarNodoToolStripMenuItem.Name = "deshabilitarNodoToolStripMenuItem";
            this.deshabilitarNodoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.deshabilitarNodoToolStripMenuItem.Text = "Deshabilitar Nodo";
            // 
            // habilitarNodoToolStripMenuItem
            // 
            this.habilitarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("habilitarNodoToolStripMenuItem.Image")));
            this.habilitarNodoToolStripMenuItem.Name = "habilitarNodoToolStripMenuItem";
            this.habilitarNodoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.habilitarNodoToolStripMenuItem.Text = "Habilitar Nodo";
            // 
            // eliminarNodoToolStripMenuItem
            // 
            this.eliminarNodoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eliminarNodoToolStripMenuItem.Image")));
            this.eliminarNodoToolStripMenuItem.Name = "eliminarNodoToolStripMenuItem";
            this.eliminarNodoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.eliminarNodoToolStripMenuItem.Text = "Eliminar Nodo";
            // 
            // copiarToolStripMenuItem
            // 
            this.copiarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copiarToolStripMenuItem.Image")));
            this.copiarToolStripMenuItem.Name = "copiarToolStripMenuItem";
            this.copiarToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.copiarToolStripMenuItem.Text = "Copiar Nodo";
            // 
            // frmConfigArbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(725, 524);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfigArbol";
            this.Text = "Configuracion Arbol General";
            this.Activated += new System.EventHandler(this.frmConfigArbol_Activated);
            this.Load += new System.EventHandler(this.frmConfigArbol_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbDerechos.ResumeLayout(false);
            this.gbDerechos.PerformLayout();
            this.cmsContextual.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvArbolGeneral;
        private System.Windows.Forms.GroupBox gbDerechos;
        private System.Windows.Forms.ListBox lbGrupos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkEliminar;
        private System.Windows.Forms.CheckBox chkAgregar;
        private System.Windows.Forms.CheckBox chkModificar;
        private System.Windows.Forms.CheckBox chkLectura;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.ContextMenuStrip cmsContextual;
        private System.Windows.Forms.ToolStripMenuItem agregarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deshabilitarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem habilitarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarNodoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copiarToolStripMenuItem;
        private System.Windows.Forms.ImageList ILImagenes;
        private System.Windows.Forms.CheckBox chkExportar;
        private System.Windows.Forms.CheckBox chkImprimir;
        private System.Windows.Forms.Button btnGuardar;
    }
}
