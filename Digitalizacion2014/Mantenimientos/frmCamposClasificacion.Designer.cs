namespace Digitalizacion2014.Mantenimientos
{
    partial class frmCamposClasificacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCamposClasificacion));
            this.SuspendLayout();
            // 
            // lvDatos
            // 
            this.lvDatos.Size = new System.Drawing.Size(279, 229);
            this.lvDatos.DoubleClick += new System.EventHandler(this.lvDatos_DoubleClick);
            // 
            // frmCamposClasificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(303, 253);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCamposClasificacion";
            this.Text = "Clasificación de Campos";
            this.Load += new System.EventHandler(this.frmCamposClasificacion_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
