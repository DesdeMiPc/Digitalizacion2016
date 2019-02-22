using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Digitalizacion2014.Scan.Images;
using NTwain;
using NTwain.Data;
using System.Reflection;
using Digitalizacion2014.Clases;

namespace Digitalizacion2014.Procesos
{
    public partial class frmDigitalizarDocto : Form
    {
        #region Variable Publicas

        public Clases.clsExpediente Expediente;
        
        #endregion

        #region Variable Locales

        //Variables para Scanner
        TwainSession _twain;
        bool _stopScan;
        bool _loadingCaps;

        //Almacenamiento de imagenes en memoria
        private ScannedImageList imagenesCapturadas = new ScannedImageList();

        //Ventana de Previsualizar de Scaner
        ScanPreview frmPreview;

        //Varibles de Datos y Digitalizacion2014
        public Clases.clsNodoTiny DocumentoActual;
        public TreeNode nodoActual;

        //Variables Diversas
        private bool isControlKeyDown;
        private bool bsalida = false;

        //Acceso a Datos
        WSD2014.cRetorno _regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap _AccesoDatos = new WSD2014.WSDatosSoapClient();

        #endregion

        #region Constructor
        public frmDigitalizarDocto()
        {
            InitializeComponent();
        }
        #endregion

        #region Rutinas TWAIN
        //Cerrar TWAIN
        private void CleanupTwain()
        {
            if (_twain.State == 4)
            {
                _twain.CurrentSource.Close();
            }
            if (_twain.State == 3)
            {
                _twain.Close();
            }

            if (_twain.State > 2)
            {
                // normal close down didn't work, do hard kill
                _twain.ForceStepDown(2);
            }
        }

        private void SetupTwain()
        {
            var appId = TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetEntryAssembly());
            _twain = new TwainSession(appId);
            _twain.StateChanged += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("State changed to " + _twain.State + " on thread " + Thread.CurrentThread.ManagedThreadId);
            };
            _twain.TransferError += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Got xfer error on thread " + Thread.CurrentThread.ManagedThreadId);
            };

            _twain.DataTransferred += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Transferred data event on thread " + Thread.CurrentThread.ManagedThreadId);

                // example on getting ext image info
                var infos = e.GetExtImageInfo(ExtendedImageInfo.Camera).Where(it => it.ReturnCode == ReturnCode.Success);
                foreach (var it in infos)
                {
                    var values = it.ReadValues();
                    PlatformInfo.Current.Log.Info(string.Format("{0} = {1}", it.InfoID, values.FirstOrDefault()));
                    break;
                }

                // handle image data
                Image img = null;
                if (e.NativeData != IntPtr.Zero)
                {
                    var stream = e.GetNativeImageStream();
                    if (stream != null)
                    {
                        img = Image.FromStream(stream);
                    }
                }
                else if (!string.IsNullOrEmpty(e.FileDataPath))
                {
                    img = new Bitmap(e.FileDataPath);
                }
                if (img != null)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        Scan.Images.IScannedImage imagen = new Scan.Images.ScannedImage(new Bitmap(img), ScanBitDepth.C24Bit, false);
                        imagenesCapturadas.Images.Add(imagen);
                        frmPreview.tblPreview.AppendImage(imagen);
                    }));
                }
            };

            _twain.SourceDisabled += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Source disabled event on thread " + Thread.CurrentThread.ManagedThreadId);
                this.BeginInvoke(new Action(() =>
                {
                    //Se detuvo el proceso de Digitalizacion.
                    //Poner controles para digitalizar de nuevo.

                    btnDigitalizar.Enabled = true;

                    if (frmPreview != null)
                    {
                        frmPreview.Close();
                        frmPreview.Dispose();
                    }

                    tblImagenes.UpdateImages(imagenesCapturadas.Images);
                    LoadSourceCaps();

                }));
            };
            _twain.TransferReady += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Transferr ready event on thread " + Thread.CurrentThread.ManagedThreadId);
                e.CancelAll = _stopScan;
            };

            // either set sync context and don't worry about threads during events,
            // or don't and use control.invoke during the events yourself
            PlatformInfo.Current.Log.Info("Setup thread = " + Thread.CurrentThread.ManagedThreadId);
            _twain.SynchronizationContext = SynchronizationContext.Current;
            if (_twain.State < 3)
            {
                // use this for internal msg loop
                _twain.Open();
                // use this to hook into current app loop
                //_twain.Open(new WindowsFormsMessageLoopHook(this.Handle));
            }
        }
        #endregion

        #region Propiedades SCANNER
        private void LoadSourceCaps()
        {
            var src = _twain.CurrentSource;
            _loadingCaps = true;

            //var test = src.SupportedCaps;

            if (groupDepth.Enabled = src.Capabilities.ICapPixelType.IsSupported)
            {
                LoadDepth(src.Capabilities.ICapPixelType);
            }
            if (groupDPI.Enabled = src.Capabilities.ICapXResolution.IsSupported && src.Capabilities.ICapYResolution.IsSupported)
            {
                LoadDPI(src.Capabilities.ICapXResolution);
            }
            // TODO: find out if this is how duplex works or also needs the other option
            if (groupDuplex.Enabled = src.Capabilities.CapDuplexEnabled.IsSupported)
            {
                LoadDuplex(src.Capabilities.CapDuplexEnabled);
            }
            if (groupSize.Enabled = src.Capabilities.ICapSupportedSizes.IsSupported)
            {
                LoadPaperSize(src.Capabilities.ICapSupportedSizes);
            }
            btnAllSettings.Enabled = src.Capabilities.CapEnableDSUIOnly.IsSupported;
            _loadingCaps = false;
        }

        private void LoadPaperSize(ICapWrapper<SupportedSize> cap)
        {
            var list = cap.GetValues();
            comboSize.DataSource = list;
            var cur = cap.GetCurrent();
            if (list.Contains(cur))
            {
                comboSize.SelectedItem = cur;
            }
            var labelTest = cap.GetLabel();
            if (!string.IsNullOrEmpty(labelTest))
            {
                groupSize.Text = labelTest;
            }
        }

        private void LoadDuplex(ICapWrapper<BoolType> cap)
        {
            ckDuplex.Checked = cap.GetCurrent() == BoolType.True;
        }

        private void LoadDPI(ICapWrapper<TWFix32> cap)
        {
            // only allow dpi of certain values for those source that lists everything
            var list = cap.GetValues().Where(dpi => (dpi % 50) == 0).ToList();
            comboDPI.DataSource = list;
            var cur = cap.GetCurrent();
            if (list.Contains(cur))
            {
                comboDPI.SelectedItem = cur;
            }
        }

        private void LoadDepth(ICapWrapper<PixelType> cap)
        {
            var list = cap.GetValues();
            comboDepth.DataSource = list;
            var cur = cap.GetCurrent();
            if (list.Contains(cur))
            {
                comboDepth.SelectedItem = cur;
            }
            var labelTest = cap.GetLabel();
            if (!string.IsNullOrEmpty(labelTest))
            {
                groupDepth.Text = labelTest;
            }
        }

        private void ReloadSourceList()
        {
            if (_twain.State >= 3)
            {
                //Borrar los Valores actuales del ComboBox
                cboScanner.Items.Clear();
                foreach (var src in _twain)
                {
                    Clases.ComboBoxItem scrScanner = new Clases.ComboBoxItem();
                    scrScanner.Text = src.Name;
                    scrScanner.Value = src;
                    cboScanner.Items.Add(scrScanner);

                    //Verificar el ultimo scanner usado
                }

                //Buscar el ultimo Scanner Seleccionado
                //y cargar las ultimas opciones seleccionadas

                clsScanner Scanner;
                clsConfiguracion config = new clsConfiguracion();
                Scanner = config.LoadScanner();

                if (Scanner.Description == "" || Scanner.Description == null)
                {
                    cboScanner.SelectedIndex = 0;
                }
                else
                {
                    //Buscar el Scanner
                    foreach (ComboBoxItem e in cboScanner.Items)
                    {
                        if (e.Text == Scanner.Description)
                        {
                            cboScanner.SelectedItem = e;
                            //Buscar ahora el DPI
                            foreach (NTwain.Data.TWFix32 ee in comboDPI.Items)
                            {
                                if (ee.ToString() == Scanner.DPI)
                                {
                                    comboDPI.SelectedItem = ee;
                                    break;
                                }
                            }
                            //Buscar Depth
                            foreach (NTwain.Data.PixelType ee in comboDepth.Items)
                            {
                                if (ee.ToString() == Scanner.Depth)
                                {
                                    comboDepth.SelectedItem = ee;
                                    break;
                                }
                            }
                            //Buscar Size
                            foreach (NTwain.Data.SupportedSize ee in comboSize.Items)
                            {
                                if (ee.ToString() == Scanner.Size)
                                {
                                    comboSize.SelectedItem = ee;
                                    break;
                                }
                            }
                            ckDuplex.Checked = Scanner.Duplex;
                            break;
                        }
                    }
                }

            }
        }

        #endregion

        #region Seleccion de Elementos
        private IEnumerable<int> SelectedIndices
        {
            get
            {
                return tblImagenes.SelectedIndices.Cast<int>();
            }
            set
            {
                tblImagenes.SelectedIndices.Clear();
                foreach (int i in value)
                {
                    tblImagenes.SelectedIndices.Add(i);
                }
            }
        }

        private IEnumerable<IScannedImage> SelectedImages
        {
            get { return imagenesCapturadas.Images.ElementsAt(SelectedIndices); }
        }
        #endregion

        #region Visualizador
        private void PreviewImage()
        {
            if (SelectedIndices.Any())
            {
                using (var viewer = new frmVisualizador())
                {
                    viewer.bEdicion = true;
                    viewer.ImageList = imagenesCapturadas;
                    viewer.ImageIndex = SelectedIndices.First();
                    viewer.DeleteCallback = UpdateThumbnails;
                    viewer.UpdateCallback = UpdateThumbnails;
                    viewer.ShowDialog();
                }
            }
        }

        private void UpdateThumbnails()
        {
            tblImagenes.UpdateImages(imagenesCapturadas.Images);
        }

        private void UpdateThumbnails(IEnumerable<int> selection)
        {
            UpdateThumbnails();
            SelectedIndices = selection;
        }

        #endregion

        #region Eventos Formulario
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetupTwain();
            ReloadSourceList();
        }

        //Al momento de cerrar la forma cerrar las sesiones TWAIN
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!bsalida)
            {
                if (MessageBox.Show("Desea salir sin guardar\n\rPara Guadar presione el boton Finalizar", "Desea Guardar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = false;
                    return;
                }
            }

            if (_twain != null)
            {
                if (e.CloseReason == CloseReason.UserClosing && _twain.State > 4)
                {
                    e.Cancel = true;
                }
                else
                {
                    CleanupTwain();
                }
            }
            base.OnFormClosing(e);
        }

        #endregion

        

        private void cboZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox objSender = ((ComboBox)sender);
            int zoom = 32;
            if (objSender.SelectedIndex == 0)
            {
                zoom = 32;
            }
            else if (objSender.SelectedIndex == 1)
            {
                zoom = 64;
            }
            else if (objSender.SelectedIndex == 2)
            {
                zoom = 96;
            }
            else if (objSender.SelectedIndex == 3)
            {
                zoom = 128;
            }
            else if (objSender.SelectedIndex == 4)
            {
                zoom = 196;
            }
            else if (objSender.SelectedIndex == 5)
            {
                zoom = 254;
            }

            tblImagenes.changeSizeThumb(new Size(zoom, zoom));

            if (imagenesCapturadas.Images.Count != 0)
            {
                tblImagenes.UpdateImages(imagenesCapturadas.Images);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (TabWizard.SelectedIndex == 0)
            {
                //Guardar los Datos del Scanner actuales
                clsScanner Scanner = new clsScanner();
                clsConfiguracion config = new clsConfiguracion();

                Scanner.Description = cboScanner.Text;
                Scanner.DPI = comboDPI.Text;
                Scanner.Depth = comboDepth.Text;
                Scanner.Size = comboSize.Text;
                Scanner.Duplex = ckDuplex.Checked;

                config.SaveScanner(Scanner);
            }

            if (TabWizard.SelectedIndex == 1)
            {
                return;
            }

            
            TabWizard.SelectedIndex++;
            if (TabWizard.SelectedIndex == 1)
            {
                btnSiguiente.Visible = false;
                btnFinalizar.Visible = true;
                btnAnterior.Enabled = true;
            }
            else
            {
                btnSiguiente.Visible = true;
                btnFinalizar.Visible = false;
                btnAnterior.Enabled = false;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (TabWizard.SelectedIndex == 0) { return; }
            TabWizard.SelectedIndex--;
            if (TabWizard.SelectedIndex == 0) { btnAnterior.Enabled = false; }
            if (TabWizard.SelectedIndex != 1)
            {
                btnSiguiente.Visible = true;
                btnFinalizar.Visible = false;
            }
        }
        

        private void btnDigitalizar_Click(object sender, EventArgs e)
        {
            if (imagenesCapturadas.Images.Count > 0)
            {
                if (MessageBox.Show("Esto borra las imagenes actuales en memoria,\n\rDesea continuar", "Precaución", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            tblImagenes.Clear();
            imagenesCapturadas.Images.Clear();

            //Iniciar Proceso de Digitalización
            if (_twain.State == 4)
            {
                //_twain.CurrentSource.CapXferCount.Set(4);

                _stopScan = false;
                frmPreview = new ScanPreview();
                frmPreview.StartPosition = FormStartPosition.CenterScreen;
                frmPreview.tblPreview.changeSizeThumb(new Size(96, 96));
                frmPreview.Show();

                if (_twain.CurrentSource.Capabilities.CapUIControllable.IsSupported)//.SupportedCaps.Contains(CapabilityId.CapUIControllable))
                {
                    // hide scanner ui if possible
                    if (_twain.CurrentSource.Enable(SourceEnableMode.NoUI, false, this.Handle) == ReturnCode.Success)
                    {
                        btnDigitalizar.Enabled = false;
                    }
                }
                else
                {
                    if (_twain.CurrentSource.Enable(SourceEnableMode.ShowUI, true, this.Handle) == ReturnCode.Success)
                    {
                        btnDigitalizar.Enabled = false;
                    }
                }
            }
        }

        

        #region Mover Imagenes
        private void btnBajar_Click(object sender, EventArgs e)
        {
            IEnumerable<int> newSelect;
            if (!SelectedIndices.Any())
            {
                return;
            }
            newSelect = imagenesCapturadas.MoveDown(SelectedIndices);
            tblImagenes.UpdateImages(imagenesCapturadas.Images);
            SelectedIndices = newSelect;
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            IEnumerable<int> newSelect;
            if (!SelectedIndices.Any())
            {
                return;
            }
            newSelect = imagenesCapturadas.MoveUp(SelectedIndices);
            tblImagenes.UpdateImages(imagenesCapturadas.Images);
            SelectedIndices = newSelect;
        }
        #endregion

        private void tblImagenes_ItemActivate(object sender, EventArgs e)
        {
            PreviewImage();
        }

        private void tblImagenes_KeyDown(object sender, KeyEventArgs e)
        {
            isControlKeyDown = e.Control;
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    //Delete();
                    break;
                case Keys.Left:
                case Keys.Up:
                    if (e.Control)
                    {
                        //MoveUp();
                    }
                    break;
                case Keys.Right:
                case Keys.Down:
                    if (e.Control)
                    {
                        //MoveDown();
                    }
                    break;
                case Keys.O:
                    if (e.Control)
                    {
                        //Import();
                    }
                    break;
                case Keys.Enter:
                    if (e.Control)
                    {
                        //ScanDefault();
                    }
                    break;
                case Keys.S:
                    if (e.Control)
                    {
                        //SavePDF(imageList.Images);
                    }
                    break;
                case Keys.OemMinus:
                    if (e.Control)
                    {
                        //StepThumbnailSize(-1);
                    }
                    break;
                case Keys.Oemplus:
                    if (e.Control)
                    {
                        //StepThumbnailSize(1);
                    }
                    break;
            }
        }

        private void tblImagenes_KeyUp(object sender, KeyEventArgs e)
        {
            isControlKeyDown = e.Control;
        }

        private void tblImagenes_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void tblImagenes_MouseMove(object sender, MouseEventArgs e)
        {
            //
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Controles.frmGauge gauge = new Controles.frmGauge();
            gauge.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            gauge.pb.Minimum = 0;
            gauge.pb.Maximum = imagenesCapturadas.Images.Count - 1;
            gauge.pb.Value = 0;
            gauge.Show();

            //Grabar y Cerrar el Formulario
            Cursor = Cursors.AppStarting;

            foreach (IScannedImage img in imagenesCapturadas.Images)
            {
                clsDocumento newDocumento = new clsDocumento("*");
                newDocumento.idPadre = Convert.ToInt32(((Clases.clsNodoTiny)nodoActual.Tag).idNodo);
                newDocumento.idUnicoExpediente = Expediente.id;
                newDocumento.orden = 0;

                newDocumento.fijarImagen(img.GetImageBase64());

                try
                {
                    newDocumento.guardarDatos(Clases.vGlobales.id_User);
                }
                catch (InvalidOperationException er)
                {
                    MessageBox.Show("Problemas al guardar Datos\n\r" + er.Message);
                    Cursor = Cursors.Default;
                    return;
                }

                try
                {
                    newDocumento.guardarImagen(Clases.vGlobales.id_User);
                }
                catch (InvalidOperationException er)
                {
                    MessageBox.Show("Problemas al guardar la Imagen\n\r" + er.Message);
                    Cursor = Cursors.Default;
                    return;
                }

                gauge.pb.PerformStep();
                gauge.BringToFront();
                System.Windows.Forms.Application.DoEvents();
            }

            gauge.Close();
            gauge.Dispose();

            Cursor = Cursors.Default;
            bsalida = true;
        }

        private void frmDigitalizarDocto_Load(object sender, EventArgs e)
        {
            cboZoom.SelectedItem = cboZoom.Items[3];
            TabWizard.SelectedIndex = 0;
            btnAnterior.Enabled = false;
        }

        private void cboScanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            // do nothing if source is enabled
            if (_twain.State > 4) { return; }

            if (_twain.State == 4) { _twain.CurrentSource.Close(); }

            var src = ((Clases.ComboBoxItem)((ComboBox)sender).SelectedItem).Value as DataSource;
            if (src.Open() == ReturnCode.Success)
            {
                LoadSourceCaps();
            }
        }

        private void ckDuplex_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                _twain.CurrentSource.Capabilities.CapDuplexEnabled.SetValue(ckDuplex.Checked ? BoolType.True : BoolType.False);
            }
        }

        private void comboDPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (TWFix32)comboDPI.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapXResolution.SetValue(sel);
                _twain.CurrentSource.Capabilities.ICapYResolution.SetValue(sel);
            }
        }

        private void comboDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (PixelType)comboDepth.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapPixelType.SetValue(sel);
            }
        }

        private void comboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (SupportedSize)comboSize.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapSupportedSizes.SetValue(sel);
            }
        }

        private void btnAllSettings_Click(object sender, EventArgs e)
        {
            _twain.CurrentSource.Enable(SourceEnableMode.ShowUIOnly, true, this.Handle);
        }
    }
}
