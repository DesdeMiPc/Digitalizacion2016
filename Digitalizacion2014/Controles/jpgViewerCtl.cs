using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Controles
{
    public partial class jpgViewerCtl : UserControl
    {
        public jpgViewerCtl()
        {
            InitializeComponent();
            jpgviewer1.ZoomChanged += jpgviewer1OnZoomChanged;
            tsStretch_Click(null, null);
        }

        private void jpgviewer1OnZoomChanged(object sender, EventArgs eventArgs)
        {
            tsZoom.Text = (jpgviewer1.Zoom / 100.0).ToString("P0");
        }

        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                jpgviewer1.Image = value;
                tStrip.Enabled = value != null;
                AdjustZoom();
            }
        }

        private void jpgViewer_SizeChanged(object sender, EventArgs e)
        {
            AdjustZoom();
        }

        private void AdjustZoom()
        {
            if (tsStretch.Checked)
            {
                double containerWidth = Math.Max(jpgviewer1.Width - 20, 0);
                double containerHeight = Math.Max(jpgviewer1.Height - 20, 0);
                double zoomX = containerWidth / jpgviewer1.ImageWidth * 100;
                double zoomY = containerHeight / jpgviewer1.ImageHeight * 100;
                jpgviewer1.Zoom = (int)Math.Min(zoomX, zoomY);
            }
        }

        private void tsZoomPlus_Click(object sender, EventArgs e)
        {
            jpgviewer1.StepZoom(1);
        }

        private void tsZoomOut_Click(object sender, EventArgs e)
        {
            jpgviewer1.StepZoom(-1);
        }

        private void tsStretch_Click(object sender, EventArgs e)
        {
            tsStretch.Checked = !tsStretch.Checked;
        }

        private void tsStretch_CheckedChanged(object sender, EventArgs e)
        {
            AdjustZoom();
        }

        private void tsZoomActual_Click(object sender, EventArgs e)
        {
            jpgviewer1.Zoom = 100;
        }
    }
}
