using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
    public partial class frmDigitalizarExp : Form
    {

        #region Variables Locales
        //Variables para Digitalizar
        //ImageCodecInfo _tiffCodecInfo;
        //ImageCodecInfo _jpgCodeInfo;

        TwainSession _twain;
        bool _stopScan;
        bool _loadingCaps;

        //Almacenamiento de imagenes en memoria
        //private readonly ScannedImageList imageList = new ScannedImageList();
        private List<ScannedImageList> imagenesCapturadas = new List<ScannedImageList> { };
        private int iImagenActual = 0;
        private int iActualTipoDocumento = 0;
        private bool bSoloTipoActual = false;

        //Ventana de Previsualizar de Scaner
        ScanPreview frmPreview;

        //Varibles de Datos y Digitalizacion2014
        public Clases.IclsNodo nodoBD;

        //Acceso a Carga inicial y refresh de datos
        string _procedimiento = "";
        string _opcion = "";
        string _parametros = "";

        //Variables Diversas
        private bool isControlKeyDown;
        private bool bsalida = false;
        //private readonly IScannedImageImporter scannedImageImporter;

        //Acceso a Datos
        WSD2014.cRetorno _regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap _AccesoDatos = new WSD2014.WSDatosSoapClient();
        #endregion

        #region Constructor
        //Inicio de Objeto Principal
        public frmDigitalizarExp()
        {
            InitializeComponent();
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

        //Carga inicial del Formlario
        private void frmDigitalizarExp_Load(object sender, EventArgs e)
        {
            cboZoom.SelectedItem = cboZoom.Items[3];

            lblExpDescripcion.Text = nodoBD.id.ToString() + " - " + nodoBD.descripcion;

            //Cargar la Propiedades del Expediente Final
            _procedimiento = "sp_ConfigFormularios";
            _opcion = "3";
            _parametros = "|V1=" + nodoBD.idFormulario.ToString() + "|";

            _regreso = _AccesoDatos.ivkProcedimiento(_procedimiento, _opcion, _parametros, Clases.vGlobales.conexion, null);
            if (_regreso.bOk)
            {
                PGExpediente.ShowCustomProperties = true;
                PGExpediente.Item.Clear();
                foreach (DataRow r in _regreso.ds.Tables[0].Rows)
                {
                    if (r["TipoCampo"].ToString().ToUpper() == "FECHA")
                    {
                        PGExpediente.Item.Add(r["Descripcion"].ToString(), new System.DateTime(DateTime.Today.Ticks), false, r["Clasificacion"].ToString(), r["Explicacion"].ToString(), true);                        
                    }
                    else if (r["TipoCampo"].ToString().ToUpper() == "TABLA")
                    {
                        PGExpediente.Item.Add(r["Descripcion"].ToString(), "", false, r["Clasificacion"].ToString(), r["Explicacion"].ToString(), true);

                        //Mandar cargar los Datos para la Tabla de Valores de la Tabla
                        WSD2014.cRetorno _valores = new WSD2014.cRetorno();
                        _valores = _AccesoDatos.ivkProcedimiento("sp_ConfigCamposTabla", "1", "|V2=" + r["idCampo"].ToString() + "|", Clases.vGlobales.conexion, null);
                        if (_valores.bOk)
                        {
                            //Cargo Datos para el Campo ComboBox
                            PGExpediente.Item[PGExpediente.Item.Count - 1].ValueMember = "Valor";
                            PGExpediente.Item[PGExpediente.Item.Count - 1].DisplayMember = "Valor";
                            PGExpediente.Item[PGExpediente.Item.Count - 1].Datasource = _valores.ds.Tables[0];
                        }
                    }
                    else
                    {
                        PGExpediente.Item.Add(r["Descripcion"].ToString(), "", false, r["Clasificacion"].ToString(), r["Explicacion"].ToString(), true);
                    }
                    PGExpediente.Item[PGExpediente.Item.Count - 1].Tag = r["idCampo"].ToString();
                }
                PGExpediente.Refresh();
            }


            if (nodoBD.tipo == Clases.clsEnums.TipoNodo.CarpetaExpediente)
            {
                //En caso de Expediente
                //Cargar el Listado de Documentos para Digitalizar
                _procedimiento = "sp_Arbol_General";
                _opcion = "1";
                _parametros = "|V1=" + nodoBD.id.ToString() + "|";

                _regreso = _AccesoDatos.ivkProcedimiento(_procedimiento, _opcion, _parametros, Clases.vGlobales.conexion, null);
                if (_regreso.bOk)
                {
                    //Borrar si hay datos actuales
                    lvDocumentos.Items.Clear();
                    //Agregar los nuevos Datos
                    foreach (DataRow r in _regreso.ds.Tables[0].Rows)
                    {
                        ListViewItem nr = new ListViewItem(r["Descripcion"].ToString());
                        clsConfiguracion config = new clsConfiguracion();
                        string cant = config.DD_Cantidad(r["idNodo"].ToString());
                        if (cant == null)
                        {
                            nr.SubItems.Add("3");
                        }
                        else
                        {
                            nr.SubItems.Add(cant);
                        }                        
                        nr.Tag = r["idNodo"].ToString();
                        nr.Checked = true;
                        lvDocumentos.Items.Add(nr);
                    }
                }
            }

            TabWizard.SelectedIndex = 0;
            btnAnterior.Enabled = false;
            iTXTidUnico.Focus();

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
                        imagenesCapturadas[iActualTipoDocumento].Images.Add(imagen);

                        if (!bSoloTipoActual)
                        {
                            if ((Convert.ToInt16(lvDocumentos.Items[iActualTipoDocumento].SubItems[1].Text) - (iImagenActual + 1)) == 0)
                            {
                                for (int i = iActualTipoDocumento + 1; i <= lvDocumentos.Items.Count - 1; i++)
                                {
                                    if (lvDocumentos.Items[i].Checked)
                                    {
                                        iActualTipoDocumento = i;
                                        break;
                                    }
                                }
                                iImagenActual = 0;
                            }
                            else
                            {
                                iImagenActual++;
                            }
                        }

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

                        if (!bSoloTipoActual)
                        {
                            tvTiposDocumentos.SelectedNode = tvTiposDocumentos.Nodes[0];
                        }
                        else
                        {
                            bSoloTipoActual = false;
                        }
                        
                        tvTiposDocumentos.Focus();
                    }
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

        private void btnAllSettings_Click(object sender, EventArgs e)
        {
            _twain.CurrentSource.Enable(SourceEnableMode.ShowUIOnly, true, this.Handle);
        }


        private void iTXTidUnico_TextChanged(object sender, EventArgs e)
        {
            this.Text = "Digitalizar Expediente - " + iTXTidUnico.Text;
        }

        #region Programación de Botones
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (TabWizard.SelectedIndex == 0 && iTXTidUnico.Text.Trim().Length == 0)
            {
                MessageBox.Show("Debe de Proporcionar un ID Unico,\n\rPara poder continuar","Faltan Datos",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

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
                //Guardar la Configuracón de Hojas de Documentos
                clsConfiguracion config = new clsConfiguracion();
                config.DD_Cantidad_Guardar(lvDocumentos);

                //Cargar los Objetos en arbol de Preview
                //Generar el nuevo arbol y inicializar el objeto de imagenes Capturadas
                foreach (ListViewItem elemento in lvDocumentos.Items)
                {
                    TreeNode nn = new TreeNode(elemento.Text);
                    nn.Tag = elemento.Tag;
                    tvTiposDocumentos.Nodes.Add(nn);
                    imagenesCapturadas.Add(new ScannedImageList());
                }
                tvTiposDocumentos.SelectedNode = tvTiposDocumentos.Nodes[0];
                tvTiposDocumentos.Select();
            }

            //Cambiar de TAB
            if (TabWizard.SelectedIndex == 2) 
            {
                return; 
            }

            btnAnterior.Enabled = true;
            TabWizard.SelectedIndex++;
            if (TabWizard.SelectedIndex == 2)
            {
                btnSiguiente.Visible = false;
                btnFinalizar.Visible = true;
            }
            else
            {
                btnSiguiente.Visible = true;
                btnFinalizar.Visible = false;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (TabWizard.SelectedIndex == 0) { return; }
            TabWizard.SelectedIndex--;
            if (TabWizard.SelectedIndex == 0) { btnAnterior.Enabled = false; }
            if (TabWizard.SelectedIndex != 2)
            {
                btnSiguiente.Visible = true;
                btnFinalizar.Visible = false;
            }
        }

        private void btnDigitalizar_Click(object sender, EventArgs e)
        {
            if (imagenesCapturadas.Count > 0)
            {
                if (MessageBox.Show("Esto borra las imagenes actuales en memoria,\n\rDesea continuar", "Precaución", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            //Borrar el arbol actual e imagenes actuales
            tvTiposDocumentos.Nodes.Clear();
            tblImagenes.Clear();
            imagenesCapturadas.Clear();
            iImagenActual = 0;
            iActualTipoDocumento = -1;

            foreach (ListViewItem elemento in lvDocumentos.Items)
            {
                if (elemento.Checked)
                {
                    iActualTipoDocumento = elemento.Index;
                    break;
                }
            }

            if (iActualTipoDocumento == -1)
            {
                MessageBox.Show("Debe de seleccionar al menos un tipo de documento para digitalizar");
                return;
            }

            //Generar el nuevo arbol y inicializar el objeto de imagenes Capturadas
            foreach (ListViewItem elemento in lvDocumentos.Items)
            {
                    TreeNode nn = new TreeNode(elemento.Text);
                    nn.Tag = elemento.Tag;
                    tvTiposDocumentos.Nodes.Add(nn);
                    imagenesCapturadas.Add(new ScannedImageList());
            }

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

        #region Finalizar y Guardar
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;

            //Mandar Grabar los elementos en la Base de Datos

            //Grabar los Datos de los Expediente
            Clases.clsExpediente newExpediente = new Clases.clsExpediente("*");
            newExpediente.idLlave = iTXTidUnico.Text.ToString().Trim();
            newExpediente.idPadre = nodoBD.id;
            newExpediente.idFormulario = nodoBD.idFormulario.ToString();

            try
            {
                newExpediente.guardarDatos(Clases.vGlobales.id_User);
            }
            catch (InvalidOperationException er)
            {
                MessageBox.Show("Problemas al guardar Datos\n\r" + er.Message);
                Cursor = Cursors.Default;
                return;
            }

            //Grabar los Campos del Expediente
            foreach (PropertyGridEx.CustomProperty p in PGExpediente.Item)
            {
                if (p.Value.ToString().Trim().Length != 0)
                {
                    Clases.clsCampoDato campo = new Clases.clsCampoDato(Clases.clsCampoDato.TipoCampo.Expediente, newExpediente.id.ToString(), p.Tag.ToString());
                    campo.valor = p.Value.ToString();
                    if (!campo.guardarDatos(Clases.vGlobales.id_User))
                    {
                        MessageBox.Show(campo.msgError, "Error al Guardar datos del Campo " + p.Tag);
                    }
                }
            }



            

            //Grabar las imagenes en la Base de Datos
            //Por los Tipos de Documentos
            //Recorrer El arbol con los Tipos de Documentos.
            foreach (TreeNode nDocumento in tvTiposDocumentos.Nodes)
            {
                // Control de Avance de grabación
                Controles.frmGauge gauge = new Controles.frmGauge();
                gauge.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                gauge.pb.Minimum = 0;
                gauge.pb.Maximum = imagenesCapturadas[nDocumento.Index].Images.Count - 1;
                gauge.pb.Value = 0;
                gauge.Show();

                foreach (IScannedImage img in imagenesCapturadas[nDocumento.Index].Images)
                {
                    clsDocumento newDocumento = new clsDocumento("*");
                    newDocumento.idPadre = Convert.ToInt16(nDocumento.Tag.ToString());
                    newDocumento.idUnicoExpediente = newExpediente.id;
                    newDocumento.orden = imagenesCapturadas[nDocumento.Index].Images.IndexOf(img) + 1;

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
            }

            Cursor = Cursors.Default;
            bsalida = true;
        }

        #endregion
        #endregion

        private void tvTiposDocumentos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (imagenesCapturadas.Count == 0) { return; }
            tblImagenes.UpdateImages(imagenesCapturadas[((TreeView)sender).SelectedNode.Index].Images);
            btnSubir.Enabled = true;
            btnBajar.Enabled = true;
            iActualTipoDocumento = ((TreeView)sender).SelectedNode.Index;
        }

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

            if (tvTiposDocumentos.Nodes.Count != 0)
            {
                tblImagenes.UpdateImages(imagenesCapturadas[tvTiposDocumentos.SelectedNode.Index].Images);
            }
        }

        private void lvDocumentos_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            foreach(Control item in lvDocumentos.Controls)
            {
                if (item is NumericUpDown)
                {
                    lvDocumentos.Controls.Remove(item);
                }
            }

            if(e.IsSelected)
            {
                Size udSize = new Size(lvDocumentos.Columns[1].Width, e.Item.SubItems[1].Bounds.Y);
                Point udLugar = new Point(e.Item.SubItems[1].Bounds.X, e.Item.SubItems[1].Bounds.Y);
                NumericUpDown udCantidad = new NumericUpDown();
                udCantidad.Name = "udCantidad";
                //udCantidad.BackColor = Color.Yellow;
                udCantidad.Location = udLugar;
                udCantidad.Size = udSize;
                udCantidad.BorderStyle = BorderStyle.Fixed3D;
                udCantidad.TextAlign = HorizontalAlignment.Right;                
                udCantidad.Value = Convert.ToDecimal(e.Item.SubItems[1].Text.ToString());
                udCantidad.Tag = e.Item.SubItems[1];
                lvDocumentos.Controls.Add(udCantidad);
                udCantidad.ValueChanged += new EventHandler(udCantidad_ValueChanged);
                udCantidad.Focus();

            }
        }

        private void udCantidad_ValueChanged(object sender, EventArgs e)
        {
            ((ListViewItem.ListViewSubItem)((NumericUpDown)sender).Tag).Text = ((NumericUpDown)sender).Value.ToString();
        }

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
            get { return imagenesCapturadas[tvTiposDocumentos.SelectedNode.Index].Images.ElementsAt(SelectedIndices); }
        }
        #endregion

        #region Scanner Config
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
        #endregion

        #region Mover Imagenes
        private void btnSubir_Click(object sender, EventArgs e)
        {
            IEnumerable<int> newSelect;
            if (!SelectedIndices.Any())
            {
                return;
            }
            newSelect = imagenesCapturadas[tvTiposDocumentos.SelectedNode.Index].MoveUp(SelectedIndices);
            tblImagenes.UpdateImages(imagenesCapturadas[tvTiposDocumentos.SelectedNode.Index].Images);
            SelectedIndices = newSelect;
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            IEnumerable<int> newSelect;
            if (!SelectedIndices.Any())
            {
                return;
            }
            newSelect = imagenesCapturadas[tvTiposDocumentos.SelectedNode.Index].MoveDown(SelectedIndices);
            tblImagenes.UpdateImages(imagenesCapturadas[tvTiposDocumentos.SelectedNode.Index].Images);
            SelectedIndices = newSelect;
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
                    viewer.ImageList = imagenesCapturadas[this.tvTiposDocumentos.SelectedNode.Index];
                    viewer.ImageIndex = SelectedIndices.First();
                    viewer.DeleteCallback = UpdateThumbnails;
                    viewer.UpdateCallback = UpdateThumbnails;
                    viewer.ShowDialog();
                }
            }
        }

        private void UpdateThumbnails()
        {
            tblImagenes.UpdateImages(imagenesCapturadas[this.tvTiposDocumentos.SelectedNode.Index].Images);
        }

        private void UpdateThumbnails(IEnumerable<int> selection)
        {
            UpdateThumbnails();
            SelectedIndices = selection;
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
            //if (tblImagenes.GetItemAt(e.X, e.Y) == null)
            //{
            //    Cursor = Cursors.Default;
            //    tblImagenes.ContextMenuStrip.Enabled = false;
            //}
            //else
            //{
            //    Cursor = Cursors.Hand;
            //    tblImagenes.ContextMenuStrip.Enabled = true;
            //}
        }

        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Import();
        }

        #region Importar
        private void Import()
        {
            var ofd = new OpenFileDialog
            {
                Multiselect = true,
                CheckFileExists = true,
                DefaultExt = "jpg"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var fileName in ofd.FileNames.OrderBy(x => x))
                {
                    // TODO: Run in thread, and show a dialog (just like exporting)
                    // Need to provide count somehow (progress callback). count = # files or # pages
                    try
                    {
                        Bitmap aImportar;
                        try
                        {
                            aImportar = new Bitmap(fileName);
                        }
                        catch (Exception e)
                        {
                            //Log.ErrorException("Error importing image: " + filePath, e);
                            // Handle and notify the user outside the method so that errors importing multiple files can be aggregated
                            throw;
                        }
                        using (aImportar)
                        {
                            for (int i = 0; i < aImportar.GetFrameCount(FrameDimension.Page); ++i)
                            {
                                aImportar.SelectActiveFrame(FrameDimension.Page, i);

                                Scan.Images.IScannedImage imagen = new Scan.Images.ScannedImage((Bitmap)aImportar.Clone(), ScanBitDepth.C24Bit, false);
                                imagenesCapturadas[iActualTipoDocumento].Images.Add(imagen);
                                Application.DoEvents();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            tblImagenes.UpdateImages(imagenesCapturadas[iActualTipoDocumento].Images);
        }
        #endregion

        private void ScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                        bSoloTipoActual = true;
                    }
                }
                else
                {
                    if (_twain.CurrentSource.Enable(SourceEnableMode.ShowUI, true, this.Handle) == ReturnCode.Success)
                    {
                        btnDigitalizar.Enabled = false;
                        bSoloTipoActual = true;
                    }
                }
            }
        }

        private void iTXTidUnico_Leave(object sender, EventArgs e)
        {
            //Verificar que no exista este ID unico en la seccion actual
            //MessageBox.Show("Paro");
        }

    }
}
