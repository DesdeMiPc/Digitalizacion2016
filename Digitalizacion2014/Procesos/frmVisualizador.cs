using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Digitalizacion2014.Scan.Images;

namespace Digitalizacion2014.Procesos
{
    public partial class frmVisualizador : Form
    {
    #region Variables de Trabajo

        public ScannedImageList ImageList { get; set; }
        public int ImageIndex { get; set; }
        public Action DeleteCallback { get; set; }
        public Action<IEnumerable<int>> UpdateCallback { get; set; }

        public bool bEdicion = true;

    #endregion

        public frmVisualizador()
        {
            InitializeComponent();            
        }

        #region Funciones Locales
        
        private void GoTo(int index)
        {
            if (index == ImageIndex || index < 0 || index >= ImageList.Images.Count)
            {
                return;
            }
            ImageIndex = index;
            UpdateImage();
            tbPageCurrent.Text = (ImageIndex + 1).ToString(CultureInfo.CurrentCulture);
        }

        private void UpdateImage()
        {
            jpgViewer1.Image.Dispose();
            jpgViewer1.Image = ImageList.Images[ImageIndex].GetImage();
        }

        #endregion

        #region Eventos

        private void tbPageCurrent_TextChanged(object sender, EventArgs e)
        {
            int indexOffBy1;
            if (int.TryParse(tbPageCurrent.Text, out indexOffBy1))
            {
                GoTo(indexOffBy1 - 1);
            }
        }

        private void tsNext_Click(object sender, EventArgs e)
        {
            GoTo(ImageIndex + 1);
        }

        private void tsPrev_Click(object sender, EventArgs e)
        {
            GoTo(ImageIndex - 1);
        }

        private void tsRotateLeft_Click(object sender, EventArgs e)
        {
            ImageList.RotateFlip(Enumerable.Range(ImageIndex, 1), RotateFlipType.Rotate270FlipNone);
            UpdateImage();
            UpdateCallback(Enumerable.Range(ImageIndex, 1));
        }

        private void tsRotateRight_Click(object sender, EventArgs e)
        {
            ImageList.RotateFlip(Enumerable.Range(ImageIndex, 1), RotateFlipType.Rotate90FlipNone);
            UpdateImage();
            UpdateCallback(Enumerable.Range(ImageIndex, 1));
        }

        private void tsFlip_Click(object sender, EventArgs e)
        {
            ImageList.RotateFlip(Enumerable.Range(ImageIndex, 1), RotateFlipType.Rotate180FlipNone);
            UpdateImage();
            UpdateCallback(Enumerable.Range(ImageIndex, 1));
        }

        private void tsCustomRotation_Click(object sender, EventArgs e)
        {
            //var form = FormFactory.Create<FRotate>();
            //form.Image = ImageList.Images[ImageIndex];
            //form.ShowDialog();
            //UpdateImage();
            //UpdateCallback(Enumerable.Range(ImageIndex, 1));
        }

        private void tsCrop_Click(object sender, EventArgs e)
        {
            //var form = FormFactory.Create<FCrop>();
            //form.Image = ImageList.Images[ImageIndex];
            //form.ShowDialog();
            //UpdateImage();
            //UpdateCallback(Enumerable.Range(ImageIndex, 1));
        }

        private void tsBrightness_Click(object sender, EventArgs e)
        {
            //var form = FormFactory.Create<FBrightness>();
            //form.Image = ImageList.Images[ImageIndex];
            //form.ShowDialog();
            //UpdateImage();
        }

        private void tsContrast_Click(object sender, EventArgs e)
        {
            //var form = FormFactory.Create<FContrast>();
            //form.Image = ImageList.Images[ImageIndex];
            //form.ShowDialog();
            //UpdateImage();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("¿Está seguro que desea borrar {0} item(s)?", 1), "Borrar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // Need to dispose the bitmap first to avoid file access issues
                jpgViewer1.Image.Dispose();
                // Actually delete the image
                ImageList.Delete(Enumerable.Range(ImageIndex, 1));
                // Update FDesktop in the background
                DeleteCallback();

                if (ImageList.Images.Any())
                {
                    //changeTracker.HasUnsavedChanges = true;
                    // Update the GUI for the newly displayed image
                    if (ImageIndex >= ImageList.Images.Count)
                    {
                        GoTo(ImageList.Images.Count - 1);
                    }
                    else
                    {
                        UpdateImage();
                    }
                    lblPageTotal.Text = string.Format("de {0}", ImageList.Images.Count);
                }
                else
                {
                    //changeTracker.HasUnsavedChanges = false;
                    // No images left to display, so no point keeping the form open
                    Close();
                }
            }
        }

        private void jpgViewer1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.PageDown:
                    GoTo(ImageIndex + 1);
                    break;
                case Keys.PageUp:
                    GoTo(ImageIndex - 1);
                    break;
            }
        }

        private void tbPageCurrent_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageDown:
                    GoTo(ImageIndex + 1);
                    break;
                case Keys.PageUp:
                    GoTo(ImageIndex - 1);
                    break;
            }
        }

        #endregion

        #region Eventos de Formulario
        private void frmVisualizador_Load(object sender, EventArgs e)
        {
            if (!bEdicion)
            {
                this.tsdRotate.Visible = false;
                this.tsCrop.Visible = false;
                this.tsBrightness.Visible = false;
                this.tsContrast.Visible = false;
                this.tsDelete.Visible = false;
                this.toolStripSeparator1.Visible = false;
            }

            jpgViewer1.Image = ImageList.Images[ImageIndex].GetImage();
            tbPageCurrent.Text = (ImageIndex + 1).ToString(CultureInfo.InvariantCulture);
            lblPageTotal.Text = string.Format("de {0}", ImageList.Images.Count);
        }
        #endregion
    }
}
