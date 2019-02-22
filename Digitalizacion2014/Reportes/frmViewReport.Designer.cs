namespace Digitalizacion2014.Reportes
{
    partial class frmViewReport
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
            this.Viewer = new DataDynamics.ActiveReports.Viewer.Viewer();
            this.SuspendLayout();
            // 
            // Viewer
            // 
            this.Viewer.BackColor = System.Drawing.SystemColors.Control;
            this.Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Viewer.Document = new DataDynamics.ActiveReports.Document.Document("Digitalizacion 2.0 - JAPAC");
            this.Viewer.Location = new System.Drawing.Point(0, 0);
            this.Viewer.Name = "Viewer";
            this.Viewer.ReportViewer.MultiplePageCols = 3;
            this.Viewer.ReportViewer.MultiplePageRows = 2;
            this.Viewer.ReportViewer.ViewType = DataDynamics.ActiveReports.Viewer.ViewType.Normal;
            this.Viewer.Size = new System.Drawing.Size(763, 292);
            this.Viewer.TabIndex = 0;
            this.Viewer.TableOfContents.Text = "Table Of Contents";
            this.Viewer.TableOfContents.Width = 200;
            this.Viewer.TabTitleLength = 35;
            this.Viewer.Toolbar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Viewer.ToolClick += new DataDynamics.ActiveReports.Toolbar.ToolClickEventHandler(this.Viewer_ToolClick);
            // 
            // frmViewReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 292);
            this.Controls.Add(this.Viewer);
            this.Name = "frmViewReport";
            this.Text = "frmViewReport";
            this.Load += new System.EventHandler(this.frmViewReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DataDynamics.ActiveReports.Viewer.Viewer Viewer;
    }
}