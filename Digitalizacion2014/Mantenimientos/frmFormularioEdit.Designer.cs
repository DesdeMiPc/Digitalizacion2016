namespace Digitalizacion2014.Mantenimientos
{
    partial class frmFormularioEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormularioEdit));
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkCodigoBarras = new System.Windows.Forms.CheckBox();
            this.iTXTDescripcion = new Digitalizacion2014.Controles.InnovaTXT();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(307, 42);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 27);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(226, 42);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 27);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Descripción :";
            // 
            // chkCodigoBarras
            // 
            this.chkCodigoBarras.AutoSize = true;
            this.chkCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCodigoBarras.Location = new System.Drawing.Point(4, 41);
            this.chkCodigoBarras.Name = "chkCodigoBarras";
            this.chkCodigoBarras.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkCodigoBarras.Size = new System.Drawing.Size(179, 22);
            this.chkCodigoBarras.TabIndex = 1;
            this.chkCodigoBarras.Text = "?Campos Automaticos";
            this.chkCodigoBarras.UseVisualStyleBackColor = true;
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
            this.iTXTDescripcion.Location = new System.Drawing.Point(136, 12);
            this.iTXTDescripcion.Name = "iTXTDescripcion";
            this.iTXTDescripcion.Size = new System.Drawing.Size(246, 24);
            this.iTXTDescripcion.TabIndex = 0;
            this.iTXTDescripcion.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            // 
            // frmFormularioEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 77);
            this.Controls.Add(this.chkCodigoBarras);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.iTXTDescripcion);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFormularioEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edicion de ...";
            this.Load += new System.EventHandler(this.frmFormularioEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private Controles.InnovaTXT iTXTDescripcion;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox chkCodigoBarras;
    }
}