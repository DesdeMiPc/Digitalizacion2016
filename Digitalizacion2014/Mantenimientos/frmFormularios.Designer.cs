namespace Digitalizacion2014.Mantenimientos
{
    partial class frmFormularios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormularios));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnGuardarConsulta = new System.Windows.Forms.Button();
            this.iTXTConsultaExterna = new Digitalizacion2014.Controles.InnovaTXT();
            this.chkAutomaticos = new System.Windows.Forms.CheckBox();
            this.gpoExplicacion = new System.Windows.Forms.GroupBox();
            this.lblExplicacion = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lvCampos = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gpoExplicacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDatos
            // 
            this.lvDatos.Size = new System.Drawing.Size(520, 363);
            this.lvDatos.SelectedIndexChanged += new System.EventHandler(this.lvDatos_SelectedIndexChanged);
            this.lvDatos.DoubleClick += new System.EventHandler(this.lvDatos_DoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnGuardarConsulta);
            this.splitContainer1.Panel2.Controls.Add(this.iTXTConsultaExterna);
            this.splitContainer1.Panel2.Controls.Add(this.chkAutomaticos);
            this.splitContainer1.Panel2.Controls.Add(this.gpoExplicacion);
            this.splitContainer1.Panel2.Controls.Add(this.btnEliminar);
            this.splitContainer1.Panel2.Controls.Add(this.btnAgregar);
            this.splitContainer1.Panel2.Controls.Add(this.lvCampos);
            this.splitContainer1.Size = new System.Drawing.Size(544, 387);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 6;
            // 
            // btnGuardarConsulta
            // 
            this.btnGuardarConsulta.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGuardarConsulta.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardarConsulta.Image")));
            this.btnGuardarConsulta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarConsulta.Location = new System.Drawing.Point(3, 4);
            this.btnGuardarConsulta.Name = "btnGuardarConsulta";
            this.btnGuardarConsulta.Size = new System.Drawing.Size(142, 27);
            this.btnGuardarConsulta.TabIndex = 12;
            this.btnGuardarConsulta.Text = "&Guardar Consulta";
            this.btnGuardarConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarConsulta.UseVisualStyleBackColor = true;
            this.btnGuardarConsulta.Visible = false;
            this.btnGuardarConsulta.Click += new System.EventHandler(this.btnGuardarConsulta_Click);
            // 
            // iTXTConsultaExterna
            // 
            this.iTXTConsultaExterna.AcceptsReturn = true;
            this.iTXTConsultaExterna.AcceptsTab = true;
            this.iTXTConsultaExterna.ActivarAyuda = false;
            this.iTXTConsultaExterna.ActivarEnter = false;
            this.iTXTConsultaExterna.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iTXTConsultaExterna.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTConsultaExterna.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iTXTConsultaExterna.Catalogo = "";
            this.iTXTConsultaExterna.colTexto = 0;
            this.iTXTConsultaExterna.ControlDestinoDescripcion = null;
            this.iTXTConsultaExterna.IsNumeric = false;
            this.iTXTConsultaExterna.Location = new System.Drawing.Point(3, 37);
            this.iTXTConsultaExterna.Multiline = true;
            this.iTXTConsultaExterna.Name = "iTXTConsultaExterna";
            this.iTXTConsultaExterna.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.iTXTConsultaExterna.Size = new System.Drawing.Size(296, 259);
            this.iTXTConsultaExterna.TabIndex = 11;
            this.iTXTConsultaExterna.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            this.iTXTConsultaExterna.Visible = false;
            // 
            // chkAutomaticos
            // 
            this.chkAutomaticos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutomaticos.AutoSize = true;
            this.chkAutomaticos.Location = new System.Drawing.Point(168, 10);
            this.chkAutomaticos.Name = "chkAutomaticos";
            this.chkAutomaticos.Size = new System.Drawing.Size(131, 17);
            this.chkAutomaticos.TabIndex = 10;
            this.chkAutomaticos.Text = "Campos Automaticos?";
            this.chkAutomaticos.UseVisualStyleBackColor = true;
            this.chkAutomaticos.Visible = false;
            this.chkAutomaticos.CheckStateChanged += new System.EventHandler(this.chkAutomaticos_CheckStateChanged);
            // 
            // gpoExplicacion
            // 
            this.gpoExplicacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpoExplicacion.Controls.Add(this.lblExplicacion);
            this.gpoExplicacion.Location = new System.Drawing.Point(4, 303);
            this.gpoExplicacion.Name = "gpoExplicacion";
            this.gpoExplicacion.Size = new System.Drawing.Size(295, 81);
            this.gpoExplicacion.TabIndex = 9;
            this.gpoExplicacion.TabStop = false;
            this.gpoExplicacion.Text = "Explicación";
            // 
            // lblExplicacion
            // 
            this.lblExplicacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExplicacion.Location = new System.Drawing.Point(3, 16);
            this.lblExplicacion.Name = "lblExplicacion";
            this.lblExplicacion.Size = new System.Drawing.Size(289, 62);
            this.lblExplicacion.TabIndex = 0;
            // 
            // btnEliminar
            // 
            this.btnEliminar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEliminar.Location = new System.Drawing.Point(77, 4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(68, 27);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(3, 4);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(68, 27);
            this.btnAgregar.TabIndex = 7;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lvCampos
            // 
            this.lvCampos.AllowColumnReorder = true;
            this.lvCampos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvCampos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvCampos.FullRowSelect = true;
            this.lvCampos.GridLines = true;
            this.lvCampos.HideSelection = false;
            this.lvCampos.Location = new System.Drawing.Point(3, 37);
            this.lvCampos.MultiSelect = false;
            this.lvCampos.Name = "lvCampos";
            this.lvCampos.Size = new System.Drawing.Size(296, 259);
            this.lvCampos.TabIndex = 6;
            this.lvCampos.UseCompatibleStateImageBehavior = false;
            this.lvCampos.View = System.Windows.Forms.View.Details;
            this.lvCampos.SelectedIndexChanged += new System.EventHandler(this.lvCampos_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Campo";
            this.columnHeader1.Width = 45;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Descripción";
            this.columnHeader2.Width = 155;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tipo";
            this.columnHeader3.Width = 69;
            // 
            // frmFormularios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(544, 387);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFormularios";
            this.Load += new System.EventHandler(this.frmFormularios_Load);
            this.Controls.SetChildIndex(this.lvDatos, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gpoExplicacion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.ListView lvCampos;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox gpoExplicacion;
        private System.Windows.Forms.Label lblExplicacion;
        private System.Windows.Forms.CheckBox chkAutomaticos;
        private Controles.InnovaTXT iTXTConsultaExterna;
        private System.Windows.Forms.Button btnGuardarConsulta;
    }
}
