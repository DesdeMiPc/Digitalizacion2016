namespace Digitalizacion2014.Procesos
{
    partial class frmLectorPDF
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pageViewControl1 = new Digitalizacion2014.Controles.PageViewer();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 287);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(642, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(32, 17);
            this.StatusLabel.Text = "Listo";
            // 
            // pageViewControl1
            // 
            this.pageViewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageViewControl1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pageViewControl1.BorderColor = System.Drawing.Color.Black;
            this.pageViewControl1.DrawBorder = false;
            this.pageViewControl1.DrawShadow = true;
            this.pageViewControl1.Location = new System.Drawing.Point(241, 19);
            this.pageViewControl1.Margin = new System.Windows.Forms.Padding(10);
            this.pageViewControl1.Name = "pageViewControl1";
            this.pageViewControl1.PageColor = System.Drawing.SystemColors.AppWorkspace;
            this.pageViewControl1.PageSize = new System.Drawing.Size(0, 0);
            this.pageViewControl1.PaintMethod = Digitalizacion2014.Controles.PageViewer.DoubleBufferMethod.BuiltInOptimizedDoubleBuffer;
            this.pageViewControl1.ScrollPosition = new System.Drawing.Point(-10, -10);
            this.pageViewControl1.Size = new System.Drawing.Size(382, 258);
            this.pageViewControl1.TabIndex = 1;
            // 
            // frmLectorPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 309);
            this.Controls.Add(this.pageViewControl1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmLectorPDF";
            this.Text = "frmLectorPDF";
            this.DoubleClick += new System.EventHandler(this.frmLectorPDF_DoubleClick);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private Controles.PageViewer pageViewControl1;
    }
}