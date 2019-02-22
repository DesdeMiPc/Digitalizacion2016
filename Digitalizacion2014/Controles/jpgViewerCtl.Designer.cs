using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Digitalizacion2014.Controles
{
    partial class jpgViewerCtl
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(jpgViewerCtl));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.jpgviewer1 = new Digitalizacion2014.Controles.jpgViewer();
            this.tStrip = new System.Windows.Forms.ToolStrip();
            this.tsStretch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsZoomActual = new System.Windows.Forms.ToolStripButton();
            this.tsZoomPlus = new System.Windows.Forms.ToolStripButton();
            this.tsZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsZoom = new System.Windows.Forms.ToolStripLabel();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.jpgviewer1);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tStrip);
            // 
            // jpgviewer1
            // 
            resources.ApplyResources(this.jpgviewer1, "jpgviewer1");
            this.jpgviewer1.BackColor = System.Drawing.Color.White;
            this.jpgviewer1.Name = "jpgviewer1";
            this.jpgviewer1.Zoom = 0;
            // 
            // tStrip
            // 
            resources.ApplyResources(this.tStrip, "tStrip");
            this.tStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStretch,
            this.toolStripSeparator1,
            this.tsZoomActual,
            this.tsZoomPlus,
            this.tsZoomOut,
            this.toolStripSeparator2,
            this.tsZoom});
            this.tStrip.Name = "tStrip";
            // 
            // tsStretch
            // 
            this.tsStretch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsStretch, "tsStretch");
            this.tsStretch.Name = "tsStretch";
            this.tsStretch.CheckedChanged += new System.EventHandler(this.tsStretch_CheckedChanged);
            this.tsStretch.Click += new System.EventHandler(this.tsStretch_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsZoomActual
            // 
            this.tsZoomActual.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsZoomActual, "tsZoomActual");
            this.tsZoomActual.Name = "tsZoomActual";
            this.tsZoomActual.Click += new System.EventHandler(this.tsZoomActual_Click);
            // 
            // tsZoomPlus
            // 
            this.tsZoomPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsZoomPlus, "tsZoomPlus");
            this.tsZoomPlus.Name = "tsZoomPlus";
            this.tsZoomPlus.Click += new System.EventHandler(this.tsZoomPlus_Click);
            // 
            // tsZoomOut
            // 
            this.tsZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsZoomOut, "tsZoomOut");
            this.tsZoomOut.Name = "tsZoomOut";
            this.tsZoomOut.Click += new System.EventHandler(this.tsZoomOut_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsZoom
            // 
            this.tsZoom.Name = "tsZoom";
            resources.ApplyResources(this.tsZoom, "tsZoom");
            // 
            // jpgViewerCtl
            // 
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "jpgViewerCtl";
            resources.ApplyResources(this, "$this");
            this.SizeChanged += new System.EventHandler(this.jpgViewer_SizeChanged);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tStrip.ResumeLayout(false);
            this.tStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Image image;
        private ToolStrip tStrip;
        private jpgViewer jpgviewer1;
        private ToolStripContainer toolStripContainer1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsStretch;
        private ToolStripLabel tsZoom;
        private ToolStripButton tsZoomActual;
        private ToolStripButton tsZoomOut;
        private ToolStripButton tsZoomPlus;
    }
}
