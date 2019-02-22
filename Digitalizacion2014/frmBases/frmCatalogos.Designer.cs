namespace Digitalizacion2014.frmBases
{
    partial class frmCatalogos
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
            this.lvDatos = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvDatos
            // 
            this.lvDatos.AllowColumnReorder = true;
            this.lvDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDatos.FullRowSelect = true;
            this.lvDatos.GridLines = true;
            this.lvDatos.HideSelection = false;
            this.lvDatos.Location = new System.Drawing.Point(12, 12);
            this.lvDatos.MultiSelect = false;
            this.lvDatos.Name = "lvDatos";
            this.lvDatos.Size = new System.Drawing.Size(482, 287);
            this.lvDatos.TabIndex = 5;
            this.lvDatos.UseCompatibleStateImageBehavior = false;
            this.lvDatos.View = System.Windows.Forms.View.Details;
            this.lvDatos.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvDatos_ColumnClick);
            // 
            // frmCatalogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 311);
            this.Controls.Add(this.lvDatos);
            this.Name = "frmCatalogos";
            this.Text = "Catalogo de  ...";
            this.Activated += new System.EventHandler(this.frmCatalogos_Activated);
            this.Deactivate += new System.EventHandler(this.frmCatalogos_Deactivate);
            this.Load += new System.EventHandler(this.frmCatalogos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView lvDatos;
    }
}