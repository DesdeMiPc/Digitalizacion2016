namespace Digitalizacion2014.Mantenimientos
{
    partial class frmUsuarioEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsuarioEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.iTXTPwd2 = new Digitalizacion2014.Controles.InnovaTXT();
            this.iTXTPwd1 = new Digitalizacion2014.Controles.InnovaTXT();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboGrupo = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.iTXTCorreo = new Digitalizacion2014.Controles.InnovaTXT();
            this.iTXTPuesto = new Digitalizacion2014.Controles.InnovaTXT();
            this.iTXTNombreCompleto = new Digitalizacion2014.Controles.InnovaTXT();
            this.iTXTUsuario = new Digitalizacion2014.Controles.InnovaTXT();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de Usuario :";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre Completo :";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Puesto :";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Correo Electrónico :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.iTXTPwd2);
            this.groupBox1.Controls.Add(this.iTXTPwd1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 86);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contraseña";
            // 
            // iTXTPwd2
            // 
            this.iTXTPwd2.ActivarAyuda = false;
            this.iTXTPwd2.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTPwd2.Catalogo = "";
            this.iTXTPwd2.colTexto = 0;
            this.iTXTPwd2.ControlDestinoDescripcion = null;
            this.iTXTPwd2.IsNumeric = false;
            this.iTXTPwd2.Location = new System.Drawing.Point(7, 54);
            this.iTXTPwd2.Name = "iTXTPwd2";
            this.iTXTPwd2.PasswordChar = '*';
            this.iTXTPwd2.Size = new System.Drawing.Size(130, 24);
            this.iTXTPwd2.TabIndex = 5;
            this.iTXTPwd2.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            this.iTXTPwd2.Leave += new System.EventHandler(this.iTXTPwd2_Leave);
            // 
            // iTXTPwd1
            // 
            this.iTXTPwd1.ActivarAyuda = false;
            this.iTXTPwd1.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTPwd1.Catalogo = "";
            this.iTXTPwd1.colTexto = 0;
            this.iTXTPwd1.ControlDestinoDescripcion = null;
            this.iTXTPwd1.IsNumeric = false;
            this.iTXTPwd1.Location = new System.Drawing.Point(7, 24);
            this.iTXTPwd1.Name = "iTXTPwd1";
            this.iTXTPwd1.PasswordChar = '*';
            this.iTXTPwd1.Size = new System.Drawing.Size(130, 24);
            this.iTXTPwd1.TabIndex = 4;
            this.iTXTPwd1.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboGrupo);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(172, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 63);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grupo";
            // 
            // cboGrupo
            // 
            this.cboGrupo.DisplayMember = "Descripcion";
            this.cboGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboGrupo.FormattingEnabled = true;
            this.cboGrupo.Location = new System.Drawing.Point(7, 24);
            this.cboGrupo.Name = "cboGrupo";
            this.cboGrupo.Size = new System.Drawing.Size(268, 26);
            this.cboGrupo.TabIndex = 0;
            this.cboGrupo.ValueMember = "Grupo";
            this.cboGrupo.Enter += new System.EventHandler(this.cboGrupo_Enter);
            this.cboGrupo.Leave += new System.EventHandler(this.cboGrupo_Leave);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(378, 214);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 27);
            this.btnGuardar.TabIndex = 12;
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
            this.btnCancelar.Location = new System.Drawing.Point(297, 214);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 27);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // iTXTCorreo
            // 
            this.iTXTCorreo.ActivarAyuda = false;
            this.iTXTCorreo.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTCorreo.Catalogo = "";
            this.iTXTCorreo.colTexto = 0;
            this.iTXTCorreo.ControlDestinoDescripcion = null;
            this.iTXTCorreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTXTCorreo.IsNumeric = false;
            this.iTXTCorreo.Location = new System.Drawing.Point(172, 102);
            this.iTXTCorreo.Name = "iTXTCorreo";
            this.iTXTCorreo.Size = new System.Drawing.Size(281, 24);
            this.iTXTCorreo.TabIndex = 3;
            this.iTXTCorreo.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            // 
            // iTXTPuesto
            // 
            this.iTXTPuesto.ActivarAyuda = false;
            this.iTXTPuesto.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTPuesto.Catalogo = "";
            this.iTXTPuesto.colTexto = 0;
            this.iTXTPuesto.ControlDestinoDescripcion = null;
            this.iTXTPuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTXTPuesto.IsNumeric = false;
            this.iTXTPuesto.Location = new System.Drawing.Point(172, 72);
            this.iTXTPuesto.Name = "iTXTPuesto";
            this.iTXTPuesto.Size = new System.Drawing.Size(281, 24);
            this.iTXTPuesto.TabIndex = 2;
            this.iTXTPuesto.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            // 
            // iTXTNombreCompleto
            // 
            this.iTXTNombreCompleto.ActivarAyuda = false;
            this.iTXTNombreCompleto.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTNombreCompleto.Catalogo = "";
            this.iTXTNombreCompleto.colTexto = 0;
            this.iTXTNombreCompleto.ControlDestinoDescripcion = null;
            this.iTXTNombreCompleto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTXTNombreCompleto.IsNumeric = false;
            this.iTXTNombreCompleto.Location = new System.Drawing.Point(172, 42);
            this.iTXTNombreCompleto.Name = "iTXTNombreCompleto";
            this.iTXTNombreCompleto.Size = new System.Drawing.Size(281, 24);
            this.iTXTNombreCompleto.TabIndex = 1;
            this.iTXTNombreCompleto.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            // 
            // iTXTUsuario
            // 
            this.iTXTUsuario.ActivarAyuda = false;
            this.iTXTUsuario.BackColorFocus = System.Drawing.Color.Yellow;
            this.iTXTUsuario.Catalogo = "";
            this.iTXTUsuario.colTexto = 0;
            this.iTXTUsuario.ControlDestinoDescripcion = null;
            this.iTXTUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTXTUsuario.IsNumeric = false;
            this.iTXTUsuario.Location = new System.Drawing.Point(172, 13);
            this.iTXTUsuario.Name = "iTXTUsuario";
            this.iTXTUsuario.Size = new System.Drawing.Size(281, 24);
            this.iTXTUsuario.TabIndex = 0;
            this.iTXTUsuario.TeclaAyudaCatalogo = System.Windows.Forms.Keys.F2;
            // 
            // frmUsuarioEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 246);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.iTXTCorreo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.iTXTPuesto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.iTXTNombreCompleto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.iTXTUsuario);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUsuarioEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edición de Usuario ...";
            this.Load += new System.EventHandler(this.frmUsuarioEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Controles.InnovaTXT iTXTUsuario;
        private Controles.InnovaTXT iTXTNombreCompleto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Controles.InnovaTXT iTXTPuesto;
        private Controles.InnovaTXT iTXTCorreo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controles.InnovaTXT iTXTPwd2;
        private Controles.InnovaTXT iTXTPwd1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboGrupo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}