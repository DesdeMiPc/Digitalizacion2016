namespace Digitalizacion2014.Procesos
{
    partial class ScanPreview
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
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
            this.tblPreview = new Digitalizacion2014.Controles.ThumbnailList();
            this.SuspendLayout();
            // 
            // tblPreview
            // 
            this.tblPreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tblPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPreview.Location = new System.Drawing.Point(9, 9);
            this.tblPreview.Name = "tblPreview";
            this.tblPreview.Size = new System.Drawing.Size(540, 400);
            this.tblPreview.TabIndex = 0;
            this.tblPreview.UseCompatibleStateImageBehavior = false;
            // 
            // ScanPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 418);
            this.ControlBox = false;
            this.Controls.Add(this.tblPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanPreview";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Digitalizando ....";
            this.ResumeLayout(false);

        }

        #endregion

        public Controles.ThumbnailList tblPreview;

    }
}
