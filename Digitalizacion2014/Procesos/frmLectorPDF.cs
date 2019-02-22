using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
//using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Forms;
using Digitalizacion2014.Gma;
using PDFLibNet;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using org.pdfclown.documents;
using org.pdfclown.files;
using org.pdfclown.tools;

namespace Digitalizacion2014.Procesos
{
    public partial class frmLectorPDF : Form
    {
        private PDFWrapper _pdfDoc;

        // Hacer Visible el Formulario de manera estatica
        public static frmLectorPDF Instance;

        //private FileStream fs;

        public frmLectorPDF()
        {
            InitializeComponent();
            
            this.pageViewControl1.PageSize = new Size(this.pageViewControl1.Width, (int)((double)(this.pageViewControl1.Width * 11) / 8.5));
            this.pageViewControl1.Visible = true;
            frmLectorPDF.Instance = this;
            this.StatusLabel.Text = "Listo";
        }

        private void Render()
        {
            this.pageViewControl1.PageSize = new Size(this._pdfDoc.PageWidth, this._pdfDoc.PageHeight);
            // this.txtPage.Text = string.Format("{0}/{1}", (object)this._pdfDoc.CurrentPage, (object)this._pdfDoc.PageCount);
            this.pageViewControl1.Refresh();
        }

        private void _pdfDoc_RenderFinished()
        {
            try
            {
            //this.Invoke((Delegate) new FinishedInvoker(this.Render));
            }
            catch (Exception ex)
            {
            }
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
            using (new StatusBusy("Cargando página"))
            {
                if (this._pdfDoc == null)
                return;
                this._pdfDoc.ZoomOut();
                this._pdfDoc.RenderFinished -= new RenderFinishedHandler(this._pdfDoc_RenderFinished);
                this._pdfDoc.RenderFinished += new RenderFinishedHandler(this._pdfDoc_RenderFinished);
                //this._pdfDoc.RenderPageThread(this.pageViewControl1.Handle, false);
                //this.Render();
            }
            }
            catch (Exception ex)
            {
            }
        }

        private void _pdfDoc_RenderNotifyFinished(int page, bool bSuccesss)
        {
          //this.Invoke((Delegate) new RenderNotifyInvoker(this.RenderNotifyFinished), (object) page, (object) bSuccesss);
        }

        private void _pdfDoc_PDFLoadBegin()
        {
            //this.UpdateParamsUI(false);
            //this.tvwOutline.BeforeExpand -= new TreeViewCancelEventHandler(this.tvwOutline_BeforeExpand);
            //this.tvwOutline.NodeMouseClick -= new TreeNodeMouseClickEventHandler(this.tvwOutline_NodeMouseClick);
            //this.Resize -= new EventHandler(this.frmPDFViewer_Resize);
            //this.FormClosing -= new FormClosingEventHandler(this.frmPDFViewer_FormClosing);
            //HookManager.MouseDown -= new MouseEventHandler(this.HookManager_MouseDown);
            //HookManager.MouseUp -= new MouseEventHandler(this.HookManager_MouseUp);
            //HookManager.MouseMove -= new MouseEventHandler(this.HookManager_MouseMove);
        }

        private void _pdfDoc_PDFLoadCompeted()
        {
            //this.tvwOutline.BeforeExpand += new TreeViewCancelEventHandler(this.tvwOutline_BeforeExpand);
            //this.tvwOutline.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.tvwOutline_NodeMouseClick);
            //this.Resize += new EventHandler(this.frmPDFViewer_Resize);
            //this.FormClosing += new FormClosingEventHandler(this.frmPDFViewer_FormClosing);
            //HookManager.MouseDown += new MouseEventHandler(this.HookManager_MouseDown);
            //HookManager.MouseUp += new MouseEventHandler(this.HookManager_MouseUp);
            //HookManager.MouseMove += new MouseEventHandler(this.HookManager_MouseMove);
            //this.UpdateParamsUI();
        }


        public delegate void RenderNotifyInvoker(int page, bool isCurrent);
        public delegate void FinishedInvoker();
  
        public enum CursorStatus
        {
            Select,
            Move,
            Zoom,
            Snapshot,
        }

        private bool LoadFile(string filename, PDFWrapper pdfDoc)
        {
            //try
            //{
            //    if (this.fs != null)
            //    {
            //        this.fs.Close();
            //        this.fs = (FileStream)null;
            //    }
            //    bool flag = pdfDoc.LoadPDF(filename);
            //    //this.tsbUseMuPDF.Checked = pdfDoc.UseMuPDF;
            //    return flag;
            //}
            //catch (SecurityException ex)
            //{
            //    //
            //    // Formulario para Socitar el Password del PDF
            //    //

            //    //frmPassword frmPassword = new frmPassword();
            //    //if (frmPassword.ShowDialog() == DialogResult.OK)
            //    //{
            //    //    if (!frmPassword.UserPassword.Equals(string.Empty))
            //    //        pdfDoc.UserPassword = frmPassword.UserPassword;
            //    //    if (!frmPassword.OwnerPassword.Equals(string.Empty))
            //    //        pdfDoc.OwnerPassword = frmPassword.OwnerPassword;
            //    //    return this.LoadFile(filename, pdfDoc);
            //    //}
            //    int num = (int)MessageBox.Show("El archivo esta encriptado", this.Text);
            //    return false;
            //}
            return false;
        }

        private void frmLectorPDF_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OD = new OpenFileDialog();
                OD.Filter = "Portable Document Format (*.pdf)|*.pdf";
                if (OD.ShowDialog() != DialogResult.OK) return;

                using (File file = new File(OD.FileName))
                {
                    Document document = file.Document;
                    Pages pages = document.Pages;

                    // 2. Page rasterization.
                    int pageIndex = 1;
                    Page page = pages[pageIndex];
                    SizeF imageSize = page.Size;
                    Renderer renderer = new Renderer();
                    Image image = renderer.Render(page, imageSize);

                    // 3. Save the page image!
                    image.Save(OD.FileName.Replace(".pdf","_img.jpg").ToString(), ImageFormat.Jpeg);
                }

                
                


                //if (this._pdfDoc != null)
                //{
                //    this._pdfDoc.Dispose();
                //    this._pdfDoc = (PDFWrapper)null;
                //}

                //this._pdfDoc = new PDFWrapper();
                //this._pdfDoc.RenderNotifyFinished += new RenderNotifyFinishedHandler(this._pdfDoc_RenderNotifyFinished);
                //this._pdfDoc.PDFLoadCompeted += new PDFLoadCompletedHandler(this._pdfDoc_PDFLoadCompeted);
                //this._pdfDoc.PDFLoadBegin += new PDFLoadBeginHandler(this._pdfDoc_PDFLoadBegin);
                //this._pdfDoc.UseMuPDF = false; //this.tsbUseMuPDF.Checked;
                //int tickCount = Environment.TickCount;
                //using (new StatusBusy("Cargando archivo..."))
                //{
                //    if (!this.LoadFile(OD.FileName, this._pdfDoc))
                //        return;
                //    this.Text = string.Format("Datos PDF: {0} - {1}", (object)this._pdfDoc.Author, (object)this._pdfDoc.Title);
                //    //this.FillTree();
                //    this._pdfDoc.CurrentPage = 1;
                //    //this.UpdateText();
                //    this._pdfDoc.FitToWidth(this.pageViewControl1.Handle);
                //    this._pdfDoc.RenderPage(this.pageViewControl1.Handle);
                //    this.Render();
                //    PDFPage page = this._pdfDoc.Pages[1];

   

                //    this._pdfDoc.ExportJpg(OD.FileName.Replace(".pdf", ".jpg").ToString(), 1, 1, 200, 100);

                //    //this.listView2.TileSize = new Size(134, (int) (128.0 * page.Height / page.Width) + 10);
                //    //this.listView2.BeginUpdate();
                //    //this.listView2.Clear();
                //    //for (int index = 0; index < this._pdfDoc.PageCount; ++index)
                //    //  this.listView2.Items.Add((index + 1).ToString());
                //    //this.listView2.EndUpdate();
                //}
            }
            //catch (IOException ex)
            //{
            //    int num = (int)MessageBox.Show(ex.Message, "IOException");
            //}
            catch (SecurityException ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "SecurityException");
            }
            //catch (InvalidDataException ex)
            //{
            //    int num = (int)MessageBox.Show(ex.Message, "InvalidDataException");
            //}

        }
        
    }

    internal class StatusBusy : IDisposable
    {
        private string _oldStatus;
        private Cursor _oldCursor;
        private bool _disposedValue;

        public StatusBusy(string statusText)
        {
            this._oldStatus = frmLectorPDF.Instance.StatusLabel.Text;
            this._oldCursor = frmLectorPDF.Instance.Cursor;
            frmLectorPDF.Instance.StatusLabel.Text = statusText;
            frmLectorPDF.Instance.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
        }

        protected void Dispose(bool disposing)
        {
            if (!this._disposedValue && disposing)
            {
                frmLectorPDF.Instance.StatusLabel.Text = this._oldStatus;
                frmLectorPDF.Instance.Cursor = this._oldCursor;
            }
            this._disposedValue = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

    }

}
