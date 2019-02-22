using System.Drawing;
using System.Windows.Forms;
using Digitalizacion2014.Controles;

namespace Digitalizacion2014.Procesos
{
    partial class frmVisualizador
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (jpgViewer1 != null)
                {
                    jpgViewer1.Image.Dispose();
                    jpgViewer1.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisualizador));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.jpgViewer1 = new Digitalizacion2014.Controles.jpgViewerCtl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbPageCurrent = new System.Windows.Forms.ToolStripTextBox();
            this.lblPageTotal = new System.Windows.Forms.ToolStripLabel();
            this.tsPrev = new System.Windows.Forms.ToolStripButton();
            this.tsNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsdRotate = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsRotateLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRotateRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsFlip = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCustomRotation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCrop = new System.Windows.Forms.ToolStripButton();
            this.tsBrightness = new System.Windows.Forms.ToolStripButton();
            this.tsContrast = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.jpgViewer1);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // jpgViewer1
            // 
            resources.ApplyResources(this.jpgViewer1, "jpgViewer1");
            this.jpgViewer1.Image = null;
            this.jpgViewer1.Name = "jpgViewer1";
            this.jpgViewer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.jpgViewer1_KeyDown);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbPageCurrent,
            this.lblPageTotal,
            this.tsPrev,
            this.tsNext,
            this.toolStripSeparator1,
            this.tsdRotate,
            this.tsCrop,
            this.tsBrightness,
            this.tsContrast,
            this.tsDelete});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tbPageCurrent
            // 
            this.tbPageCurrent.Name = "tbPageCurrent";
            resources.ApplyResources(this.tbPageCurrent, "tbPageCurrent");
            this.tbPageCurrent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPageCurrent_KeyDown);
            this.tbPageCurrent.TextChanged += new System.EventHandler(this.tbPageCurrent_TextChanged);
            // 
            // lblPageTotal
            // 
            this.lblPageTotal.Name = "lblPageTotal";
            resources.ApplyResources(this.lblPageTotal, "lblPageTotal");
            // 
            // tsPrev
            // 
            this.tsPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsPrev, "tsPrev");
            this.tsPrev.Name = "tsPrev";
            this.tsPrev.Click += new System.EventHandler(this.tsPrev_Click);
            // 
            // tsNext
            // 
            this.tsNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsNext, "tsNext");
            this.tsNext.Name = "tsNext";
            this.tsNext.Click += new System.EventHandler(this.tsNext_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsdRotate
            // 
            this.tsdRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsdRotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRotateLeft,
            this.tsRotateRight,
            this.tsFlip,
            this.tsCustomRotation});
            resources.ApplyResources(this.tsdRotate, "tsdRotate");
            this.tsdRotate.Name = "tsdRotate";
            this.tsdRotate.ShowDropDownArrow = false;
            // 
            // tsRotateLeft
            // 
            resources.ApplyResources(this.tsRotateLeft, "tsRotateLeft");
            this.tsRotateLeft.Name = "tsRotateLeft";
            this.tsRotateLeft.Click += new System.EventHandler(this.tsRotateLeft_Click);
            // 
            // tsRotateRight
            // 
            resources.ApplyResources(this.tsRotateRight, "tsRotateRight");
            this.tsRotateRight.Name = "tsRotateRight";
            this.tsRotateRight.Click += new System.EventHandler(this.tsRotateRight_Click);
            // 
            // tsFlip
            // 
            resources.ApplyResources(this.tsFlip, "tsFlip");
            this.tsFlip.Name = "tsFlip";
            this.tsFlip.Click += new System.EventHandler(this.tsFlip_Click);
            // 
            // tsCustomRotation
            // 
            this.tsCustomRotation.Name = "tsCustomRotation";
            resources.ApplyResources(this.tsCustomRotation, "tsCustomRotation");
            this.tsCustomRotation.Click += new System.EventHandler(this.tsCustomRotation_Click);
            // 
            // tsCrop
            // 
            this.tsCrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsCrop, "tsCrop");
            this.tsCrop.Name = "tsCrop";
            this.tsCrop.Click += new System.EventHandler(this.tsCrop_Click);
            // 
            // tsBrightness
            // 
            this.tsBrightness.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsBrightness, "tsBrightness");
            this.tsBrightness.Name = "tsBrightness";
            this.tsBrightness.Click += new System.EventHandler(this.tsBrightness_Click);
            // 
            // tsContrast
            // 
            this.tsContrast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsContrast, "tsContrast");
            this.tsContrast.Name = "tsContrast";
            this.tsContrast.Click += new System.EventHandler(this.tsContrast_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsDelete, "tsDelete");
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // frmVisualizador
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "frmVisualizador";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmVisualizador_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ToolStripContainer toolStripContainer1;
        private ToolStrip toolStrip1;
        private ToolStripTextBox tbPageCurrent;
        private ToolStripLabel lblPageTotal;
        private ToolStripButton tsPrev;
        private ToolStripButton tsNext;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripDropDownButton tsdRotate;
        private ToolStripMenuItem tsRotateLeft;
        private ToolStripMenuItem tsRotateRight;
        private ToolStripMenuItem tsFlip;
        private ToolStripMenuItem tsCustomRotation;
        private ToolStripButton tsCrop;
        private ToolStripButton tsBrightness;
        private ToolStripButton tsContrast;
        private ToolStripButton tsDelete;
        private jpgViewerCtl jpgViewer1;
    }
}