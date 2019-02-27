namespace Digitalizacion2014.Mantenimientos
{
    partial class frmCampoEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCampoEdit));
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTiposCampos = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cboClasificacionCampos = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.iTXTExplicacion = new Digitalizacion2014.Controles.InnovaTXT();
            this.iTXTDescripcion = new Digitalizacion2014.Controles.InnovaTXT();
            this.btnEditTable = new System.Windows.Forms.Button();
            this.chkObligatorio = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(332, 220);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 27);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(251, 220);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 27);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Descripción :";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Explicación :";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tipo :";
            // 
            // cboTiposCampos
            // 
            this.cboTiposCampos.DisplayMember = "Descripcion";
            this.cboTiposCampos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiposCampos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTiposCampos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTiposCampos.FormattingEnabled = true;
            this.cboTiposCampos.Location = new System.Drawing.Point(118, 123);
            this.cboTiposCampos.Name = "cboTiposCampos";
            this.cboTiposCampos.Size = new System.Drawing.Size(289, 26);
            this.cboTiposCampos.TabIndex = 2;
            this.cboTiposCampos.ValueMember = "id";
            this.cboTiposCampos.SelectedIndexChanged += new System.EventHandler(this.cboTiposCampos_SelectedIndexChanged);
            this.cboTiposCampos.Enter += new System.EventHandler(this.cboTiposCampos_Enter);
            this.cboTiposCampos.Leave += new System.EventHandler(this.cboTiposCampos_Leave);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 11;
            this.label4.Text = "Longitud :";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(118, 187);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 24);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Enter += new System.EventHandler(this.numericUpDown1_Enter);
            this.numericUpDown1.Leave += new System.EventHandler(this.numericUpDown1_Leave);
            // 
            // cboClasificacionCampos
            // 
            this.cboClasificacionCampos.DisplayMember = "Descripcion";
            this.cboClasificacionCampos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClasificacionCampos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboClasificacionCampos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboClasificacionCampos.FormattingEnabled = true;
            this.cboClasificacionCampos.Location = new System.Drawing.Point(118, 155);
            this.cboClasificacionCampos.Name = "cboClasificacionCampos";
            this.cboClasificacionCampos.Size = new System.Drawing.Size(289, 26);
            this.cboClasificacionCampos.TabIndex = 3;
            this.cboClasificacionCampos.ValueMember = "id";
            this.cboClasificacionCampos.Enter += new System.EventHandler(this.cboTiposCampos_Enter);
            this.cboClasificacionCampos.Leave += new System.EventHandler(this.cboTiposCampos_Leave);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "Clasificacion :";
            // 
            // iTXTExplicacion
            // 
            this.iTXTExplicacion.ActivarAyuda = false;
            this.iTXTExplicacion.ActivarEnter = false;
            this.iTXTExplicacion.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTExplicacion.Catalogo = "";
            this.iTXTExplicacion.colTexto = 0;
            this.iTXTExplicacion.ControlDestinoDescripcion = null;
            this.iTXTExplicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTXTExplicacion.IsNumeric = false;
            this.iTXTExplicacion.Location = new System.Drawing.Point(118, 36);
            this.iTXTExplicacion.Multiline = true;
            this.iTXTExplicacion.Name = "iTXTExplicacion";
            this.iTXTExplicacion.Size = new System.Drawing.Size(289, 81);
            this.iTXTExplicacion.TabIndex = 1;
            this.iTXTExplicacion.TeclaAyudaCatalogo = System.Windows.Forms.Keys.None;
            // 
            // iTXTDescripcion
            // 
            this.iTXTDescripcion.ActivarAyuda = false;
            this.iTXTDescripcion.ActivarEnter = false;
            this.iTXTDescripcion.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTDescripcion.Catalogo = "";
            this.iTXTDescripcion.colTexto = 0;
            this.iTXTDescripcion.ControlDestinoDescripcion = null;
            this.iTXTDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTXTDescripcion.IsNumeric = false;
            this.iTXTDescripcion.Location = new System.Drawing.Point(118, 6);
            this.iTXTDescripcion.Name = "iTXTDescripcion";
            this.iTXTDescripcion.Size = new System.Drawing.Size(289, 24);
            this.iTXTDescripcion.TabIndex = 0;
            this.iTXTDescripcion.TeclaAyudaCatalogo = System.Windows.Forms.Keys.None;
            // 
            // btnEditTable
            // 
            this.btnEditTable.Image = ((System.Drawing.Image)(resources.GetObject("btnEditTable.Image")));
            this.btnEditTable.Location = new System.Drawing.Point(93, 123);
            this.btnEditTable.Name = "btnEditTable";
            this.btnEditTable.Size = new System.Drawing.Size(23, 27);
            this.btnEditTable.TabIndex = 14;
            this.btnEditTable.UseVisualStyleBackColor = true;
            this.btnEditTable.Visible = false;
            this.btnEditTable.Click += new System.EventHandler(this.btnEditTable_Click);
            // 
            // chkObligatorio
            // 
            this.chkObligatorio.AutoSize = true;
            this.chkObligatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkObligatorio.Location = new System.Drawing.Point(300, 189);
            this.chkObligatorio.Name = "chkObligatorio";
            this.chkObligatorio.Size = new System.Drawing.Size(107, 22);
            this.chkObligatorio.TabIndex = 15;
            this.chkObligatorio.Text = "Obligatorio?";
            this.chkObligatorio.UseVisualStyleBackColor = true;
            // 
            // frmCampoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 255);
            this.Controls.Add(this.chkObligatorio);
            this.Controls.Add(this.btnEditTable);
            this.Controls.Add(this.cboClasificacionCampos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboTiposCampos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.iTXTExplicacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.iTXTDescripcion);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCampoEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edición de Campo ...";
            this.Load += new System.EventHandler(this.frmCampoEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private Controles.InnovaTXT iTXTDescripcion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controles.InnovaTXT iTXTExplicacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTiposCampos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox cboClasificacionCampos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditTable;
        private System.Windows.Forms.CheckBox chkObligatorio;
    }
}