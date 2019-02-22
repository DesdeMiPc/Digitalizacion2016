namespace Digitalizacion2014.Reportes
{
    partial class frmReportDesign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportDesign));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainerToolbox = new System.Windows.Forms.SplitContainer();
            this.toolbox1 = new DataDynamics.ActiveReports.Design.Toolbox.Toolbox();
            this.splitContainerDesigner = new System.Windows.Forms.SplitContainer();
            this.arDesigner = new DataDynamics.ActiveReports.Design.Designer();
            this.arPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
            this.arReportExplorer = new DataDynamics.ActiveReports.Design.ReportExplorer();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerToolbox)).BeginInit();
            this.splitContainerToolbox.Panel1.SuspendLayout();
            this.splitContainerToolbox.Panel2.SuspendLayout();
            this.splitContainerToolbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toolbox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDesigner)).BeginInit();
            this.splitContainerDesigner.Panel1.SuspendLayout();
            this.splitContainerDesigner.Panel2.SuspendLayout();
            this.splitContainerDesigner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).BeginInit();
            this.splitContainerProperties.Panel1.SuspendLayout();
            this.splitContainerProperties.Panel2.SuspendLayout();
            this.splitContainerProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerToolbox);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(804, 604);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(804, 629);
            this.toolStripContainer1.TabIndex = 10;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // splitContainerToolbox
            // 
            this.splitContainerToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerToolbox.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerToolbox.Location = new System.Drawing.Point(0, 0);
            this.splitContainerToolbox.Name = "splitContainerToolbox";
            // 
            // splitContainerToolbox.Panel1
            // 
            this.splitContainerToolbox.Panel1.Controls.Add(this.toolbox1);
            // 
            // splitContainerToolbox.Panel2
            // 
            this.splitContainerToolbox.Panel2.Controls.Add(this.splitContainerDesigner);
            this.splitContainerToolbox.Size = new System.Drawing.Size(804, 604);
            this.splitContainerToolbox.SplitterDistance = 130;
            this.splitContainerToolbox.TabIndex = 0;
            // 
            // toolbox1
            // 
            this.toolbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbox1.Location = new System.Drawing.Point(0, 0);
            this.toolbox1.Name = "toolbox1";
            this.toolbox1.Size = new System.Drawing.Size(130, 604);
            this.toolbox1.TabIndex = 1;
            // 
            // splitContainerDesigner
            // 
            this.splitContainerDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDesigner.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerDesigner.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDesigner.Name = "splitContainerDesigner";
            // 
            // splitContainerDesigner.Panel1
            // 
            this.splitContainerDesigner.Panel1.Controls.Add(this.arDesigner);
            // 
            // splitContainerDesigner.Panel2
            // 
            this.splitContainerDesigner.Panel2.Controls.Add(this.splitContainerProperties);
            this.splitContainerDesigner.Size = new System.Drawing.Size(670, 604);
            this.splitContainerDesigner.SplitterDistance = 472;
            this.splitContainerDesigner.TabIndex = 0;
            // 
            // arDesigner
            // 
            this.arDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arDesigner.IsDirty = false;
            this.arDesigner.Location = new System.Drawing.Point(0, 0);
            this.arDesigner.LockControls = false;
            this.arDesigner.Name = "arDesigner";
            this.arDesigner.PropertyGrid = this.arPropertyGrid;
            this.arDesigner.ReportTabsVisible = true;
            this.arDesigner.ShowDataSourceIcon = true;
            this.arDesigner.Size = new System.Drawing.Size(472, 604);
            this.arDesigner.TabIndex = 3;
            this.arDesigner.Toolbox = null;
            this.arDesigner.ToolBoxItem = null;
            // 
            // arPropertyGrid
            // 
            this.arPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.arPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.arPropertyGrid.Name = "arPropertyGrid";
            this.arPropertyGrid.Size = new System.Drawing.Size(194, 417);
            this.arPropertyGrid.TabIndex = 3;
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProperties.Name = "splitContainerProperties";
            this.splitContainerProperties.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.arReportExplorer);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.arPropertyGrid);
            this.splitContainerProperties.Size = new System.Drawing.Size(194, 604);
            this.splitContainerProperties.SplitterDistance = 183;
            this.splitContainerProperties.TabIndex = 0;
            // 
            // arReportExplorer
            // 
            this.arReportExplorer.AutoScroll = true;
            this.arReportExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.arReportExplorer.Location = new System.Drawing.Point(0, 0);
            this.arReportExplorer.Name = "arReportExplorer";
            this.arReportExplorer.ReportDesigner = null;
            this.arReportExplorer.Size = new System.Drawing.Size(194, 183);
            this.arReportExplorer.TabIndex = 1;
            // 
            // frmReportDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 629);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportDesign";
            this.Text = "Diseñador de Informes InnovaWeb";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainerToolbox.Panel1.ResumeLayout(false);
            this.splitContainerToolbox.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerToolbox)).EndInit();
            this.splitContainerToolbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toolbox1)).EndInit();
            this.splitContainerDesigner.Panel1.ResumeLayout(false);
            this.splitContainerDesigner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDesigner)).EndInit();
            this.splitContainerDesigner.ResumeLayout(false);
            this.splitContainerProperties.Panel1.ResumeLayout(false);
            this.splitContainerProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).EndInit();
            this.splitContainerProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainerToolbox;
        private DataDynamics.ActiveReports.Design.Toolbox.Toolbox toolbox1;
        private System.Windows.Forms.SplitContainer splitContainerDesigner;
        private DataDynamics.ActiveReports.Design.Designer arDesigner;
        private System.Windows.Forms.PropertyGrid arPropertyGrid;
        private System.Windows.Forms.SplitContainer splitContainerProperties;
        private DataDynamics.ActiveReports.Design.ReportExplorer arReportExplorer;
    }
}