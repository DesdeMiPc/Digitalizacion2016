using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using NTwain;
using NTwain.Data;
using Digitalizacion2014.Scan;
using Digitalizacion2014.Scan.Images;
using Digitalizacion2014.ImportExport.Images;

namespace Digitalizacion2014.Procesos
{
    public partial class frmArchivoGeneral : Digitalizacion2014.frmBases.frmGeneral01
    {

        #region Variables Locales

        public ToolStripProgressBar PBGeneral;

        //Variables para WebService
        private string procedimiento = "sp_Arbol_General";
        private string parametros = "";
        private string validar = "";

        //private String _idExpediente;
        //private String _idDocumento;

        private int totalPag = 0;

        private Clases.clsCadenas cadenas = new Clases.clsCadenas();

        //Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.cRetorno Datos2 = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();

        //Nodo Actual
        TreeNode nodoActual;
        Clases.IclsNodo nodoBD;

        //Manejo de Images
        private ScannedImageList imagenesDB = new ScannedImageList();

        //Variables Diversas
        private bool isControlKeyDown;

        //Para Exportar Imagenes
        private readonly ImageSaver imageSaver = new ImageSaver(new ImageFileNamer(), new MessageBoxErrorOutput());

        //Boton derecho del Mouse
        private bool rightButton = false;

        #endregion

        #region Constructor
        public frmArchivoGeneral()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos del Formulario
        private void frmArchivoGeneral_Load(object sender, EventArgs e)
        {
            cboZoom.SelectedItem = cboZoom.Items[3];

            cboRecords.SelectedIndexChanged -= cboRecords_SelectedIndexChanged;
            cboRecords.SelectedItem = cboRecords.Items[0];
            cboRecords.SelectedIndexChanged += cboRecords_SelectedIndexChanged;

            //Carga inicial a nivel antes de los Expedientes.
            cargarArbol(0);
            PBGeneral.Visible = false;
        }

        protected override void OnHandleCreated(EventArgs ee)
        {
            base.OnHandleCreated(ee);
            //Eventos de Exportar PDF's
            ((frmPrincipal)this.MdiParent).rbPDF.Click += new System.EventHandler(this.ExportPDF);
            ((frmPrincipal)this.MdiParent).rbPDFAll.Click += new System.EventHandler(this.ExportPDF);
            ((frmPrincipal)this.MdiParent).rbPDFSelect.Click += new System.EventHandler(this.ExportPDFSelect);

            //Eventos de Exportar JPG's
            ((frmPrincipal)this.MdiParent).rbJPG.Click += new System.EventHandler(this.ExportJPG);
            ((frmPrincipal)this.MdiParent).rbJPGTodos.Click += new System.EventHandler(this.ExportJPG);
            ((frmPrincipal)this.MdiParent).rbJPGSelect.Click += new System.EventHandler(this.ExportJPGSelect);


            //Activar Pintar texto en TreeView sin Focus
            this.tvArbolGeneral.HideSelection = false;
            this.tvArbolGeneral.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.tvArbolGeneral.DrawNode += (o, e) =>
            {
                if (!e.Node.TreeView.Focused && e.Node == e.Node.TreeView.SelectedNode)
                {
                    Font treeFont = e.Node.NodeFont ?? e.Node.TreeView.Font;
                    e.Graphics.FillRectangle(Brushes.Gray, e.Bounds);
                    ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, SystemColors.HighlightText, SystemColors.Highlight);
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, treeFont, e.Bounds, SystemColors.HighlightText, TextFormatFlags.GlyphOverhangPadding);
                }
                else
                    e.DrawDefault = true;
            };
            this.tvArbolGeneral.MouseDown += (o, e) =>
            {
                try
                {
                    TreeNode node = this.tvArbolGeneral.GetNodeAt(e.X, e.Y);
                    if (node != null && node.Bounds.Contains(e.X, e.Y))
                        this.tvArbolGeneral.SelectedNode = node;
                }
                catch
                {
                }
            };

        }

        private void frmArchivoGeneral_Activated(object sender, EventArgs e)
        {
            if (tblImagenes.Items.Count > 0)
            {
                ((frmPrincipal)this.MdiParent).PanelExportar.Enabled = true;
            }
        }

        private void frmArchivoGeneral_Deactivate(object sender, EventArgs e)
        {
            ((frmPrincipal)this.MdiParent).PanelExportar.Enabled = false;
        }

        private void frmArchivoGeneral_Shown(object sender, EventArgs e)
        {
            updateToolBar();
        }

        #endregion

        #region Manejo del Arbol General

        void cargarExpedientes(int idInicial, TreeNode nodoInicial = null)
        {
            
            PBGeneral.Visible = true;
            Cursor = System.Windows.Forms.Cursors.AppStarting;

            parametros = "|V1=" + idInicial.ToString() + "|";
            validar = "100";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);

            if (Datos.bOk)
            {
                nodoInicial.Nodes.Clear();

                TreeNode[] nuevosNodos = new TreeNode[Datos.ds.Tables[0].Rows.Count];
                int c = 0;

                foreach (DataRow r in Datos.ds.Tables[0].Rows)
                {
                    this.BeginInvoke(new Action<DataRow>((rr) =>
                    {
                        TreeNode newNodo = new TreeNode();
                        newNodo.Text = rr["idLlaveBusqueda"].ToString();
                        newNodo.Tag = new Clases.clsNodoTiny(rr["idUnicoExpediente"].ToString(), Clases.clsEnums.TipoNodo.Expediente);

                        newNodo.ImageIndex = 2;
                        newNodo.StateImageIndex = 2;
                        newNodo.SelectedImageIndex = 2;

                        //Cargar Tipos de Documentos en Base a Nodo Padre del Expediente
                        cargarArbol(idInicial, newNodo, true);
                        //nodoInicial.Nodes.Add(newNodo);
                        nuevosNodos[c] = (TreeNode)newNodo.Clone();
                        c++;
                    }), r);
                    
                    Application.DoEvents();
                }

                nodoInicial.Nodes.AddRange(nuevosNodos);

                //tvArbolGeneral.EndUpdate();
            }

            PBGeneral.Visible = false;
            Cursor = System.Windows.Forms.Cursors.Default;

        }

        void cargarArbol(int idInicial, TreeNode nodoInicial = null, bool cargaDoc = false)
        {
            parametros = "|V1=" + idInicial.ToString() + "|";
            validar = "7";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);

            if (Datos.bOk)
            {
                //Se obtuvieron Datos
                foreach (DataRow r in Datos.ds.Tables[0].Rows)
                {
                    if ((Clases.clsEnums.TipoNodo)r["Tipo"] != Clases.clsEnums.TipoNodo.Documento || cargaDoc)
                    {
                        TreeNode newNodo = new TreeNode();
                        newNodo.Text = r["Descripcion"].ToString();
                        newNodo.Tag = new Clases.clsNodoTiny(r["idNodo"].ToString(), (Clases.clsEnums.TipoNodo)r["Tipo"]);

                        if ((int)r["Tipo"] == 0)
                        {
                            newNodo.ImageIndex = 1;
                            newNodo.StateImageIndex = 1;
                            newNodo.SelectedImageIndex = 1;
                        }
                        if ((int)r["Tipo"] == 1)
                        {
                            newNodo.ImageIndex = 2;
                            newNodo.StateImageIndex = 2;
                            newNodo.SelectedImageIndex = 2;
                        }

                        if ((int)r["Tipo"] == 2)
                        {
                            newNodo.ImageIndex = 3;
                            newNodo.StateImageIndex = 3;
                            newNodo.SelectedImageIndex = 3;
                        }

                        //Se Cambia al DoubleClick del Nodo
                        //cargarArbol(Convert.ToInt16(r["idNodo"]), newNodo);

                        if (nodoInicial == null)
                        {
                            tvArbolGeneral.Nodes.Add(newNodo);
                        }
                        else
                        {
                            nodoInicial.Nodes.Add(newNodo);                            
                        }
                    }
                }
            }

        }

        void cargarImagenes(String idExpediente, String idDocumento)
        {

            Cursor = System.Windows.Forms.Cursors.AppStarting;
            PBGeneral.Visible = true;

            parametros = "|V2=" + idExpediente + "|V6=" + idDocumento + "|PA=" + txtPage.Text.ToString() + "|RP=" + cboRecords.SelectedItem.ToString() + "|";
            validar = "41";
            Datos = Acceso.ivkProcedimiento("sp_Datos_Documentos", validar, parametros, Clases.vGlobales.conexion, null);

            if (Datos.bOk)
            {
                if (Datos.ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No existen imagenes");
                    if (Convert.ToInt16(txtPage.Text) > 1)
                    {
                        this.txtPage.TextChanged -= this.txtPage_TextChanged;
                        this.txtPage.Text = (Convert.ToInt16(txtPage.Text) - 1).ToString();
                        this.txtPage.TextChanged += this.txtPage_TextChanged;
                    }
                    Cursor = System.Windows.Forms.Cursors.Default;
                    PBGeneral.Visible = false;
                    return;
                }

                this.totalPag = Convert.ToInt16(Datos.ds.Tables[1].Rows[0][0].ToString());

                //Borrar imagenes actuales
                tblImagenes.Clear();
                imagenesDB.Images.Clear();

                //Se Cargan las Imagenes
                foreach (DataRow r in Datos.ds.Tables[0].Rows)
                {
                    this.BeginInvoke(new Action<DataRow>((rr) =>
                    {
                        IScannedImage imagen = new Scan.Images.ScannedImage(rr["Imagen"].ToString(), ScanBitDepth.C24Bit, false);
                        imagen.Tag = rr["idUnicoDocumento"].ToString();
                        imagenesDB.Images.Add(imagen);
                        tblImagenes.AppendImage(imagen);
                    }), r);

                    Application.DoEvents();
                }
            }
            
            if (Datos.ds.Tables[0].Rows.Count > 0)
            {
                ((frmPrincipal)this.MdiParent).PanelExportar.Enabled = true;
            }
            else
            {
                ((frmPrincipal)this.MdiParent).PanelExportar.Enabled = false;
            }

            updateToolBar();

            Cursor = System.Windows.Forms.Cursors.Default;
            PBGeneral.Visible = false;

            this.ShowNotify("Proceso finalizado", "Carga de imagenes completada", ToolTipIcon.Info, 2000);

        }

        //delegate void cargaimg(DataRow r);
        //void cargaimagen(DataRow r)
        //{
        //    IScannedImage imagen = new Scan.Images.ScannedImage(r["Imagen"].ToString(), ScanBitDepth.C24Bit, false);
        //    imagen.Tag = r["idUnicaImagen"].ToString();
        //    imagenesDB.Images.Add(imagen);
        //    tblImagenes.AppendImage(imagen);
        //}

        private void tvArbolGeneral_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (((TreeView)sender).SelectedNode.ImageIndex == 0)
            {
                ((TreeView)sender).SelectedNode.ImageIndex = 1;
                ((TreeView)sender).SelectedNode.StateImageIndex = 1;
                ((TreeView)sender).SelectedNode.SelectedImageIndex = 1;
            }
        }
        
        private void tvArbolGeneral_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (((TreeView)sender).SelectedNode.ImageIndex == 1)
            {
                ((TreeView)sender).SelectedNode.ImageIndex = 0;
                ((TreeView)sender).SelectedNode.StateImageIndex = 0;
                ((TreeView)sender).SelectedNode.SelectedImageIndex = 0;
            }
        }

        private void tvArbolGeneral_MouseUp(object sender, MouseEventArgs e)
        {

            this.generarPDFToolStripMenuItem.Visible = false;
            this.tsMenuReportes.Visible = false;
            this.tsSeparador1.Visible = false;

            //Validar que sea Boton Derecho del Mouse
            if (e.Button != System.Windows.Forms.MouseButtons.Right) { return; }
            
            //Saber si existe un nodo en la posición Actual
            nodoActual = this.tvArbolGeneral.GetNodeAt(e.X, e.Y);

            if (nodoActual == null)
            {
                //No existe elemento seleccionado salir de este menu
                return;
            }
            else
            {
                if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.CarpetaExpediente)
                {
                    nodoBD = new Clases.clsNodoCarpeta(((Clases.clsNodoTiny)nodoActual.Tag).idNodo);

                    //Cargar Datos del Nodo para comportamiento de menu contextual
                    parametros = "|V2=" + ((Clases.clsNodoTiny)nodoActual.Tag).idNodo + "|";
                    validar = "2";
                    Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);

                    if (Datos.bOk)
                    {
                        //Consulta correcta
                        if ((bool)Datos.ds.Tables[0].Rows[0]["Activo"])
                        {
                            this.deshabilitarNodoToolStripMenuItem.Enabled = true;
                            this.habilitarNodoToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            this.deshabilitarNodoToolStripMenuItem.Enabled = false;
                            this.habilitarNodoToolStripMenuItem.Enabled = true;
                        }

                        if ((int)Datos.ds.Tables[0].Rows[0]["Tipo"] == 2 || (int)Datos.ds.Tables[0].Rows[0]["Tipo"] == 0)
                        {
                            this.agregarNodoToolStripMenuItem.Text = "Agregar Nodo";
                            this.agregarNodoToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            this.agregarNodoToolStripMenuItem.Text = "Agregar " + nodoBD.descripcion.ToString();
                            this.agregarNodoToolStripMenuItem.Enabled = true;
                        }

                        this.tsMenuReportes.Visible = true;
                        this.tsSeparador1.Visible = true;
                    }

                    //Poner Opciones
                    this.eliminarNodoToolStripMenuItem.Enabled = true;
                    this.copiarToolStripMenuItem.Enabled = true;
                    this.generarExpedientesPDFsToolStripMenuItem1.Visible = true;

                    //Se presiono el Boton de Menu Contextual
                    this.cmsContextual.Show(this.tvArbolGeneral, new Point(e.X, e.Y));
                }

                // En caso que sea un Nodo Expediente
                if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Expediente)
                {
                    this.generarPDFToolStripMenuItem.Visible = true;
                    this.agregarNodoToolStripMenuItem.Enabled = false;
                    this.generarExpedientesPDFsToolStripMenuItem1.Visible = false;
                    this.cmsContextual.Show(this.tvArbolGeneral, new Point(e.X, e.Y));                    
                }
            }
        }

        private void tvArbolGeneral_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Activar el Nodo Actual
            nodoActual = this.tvArbolGeneral.SelectedNode;

            //Si se presiono boton derecho del mouse se sale de esta seccion
            if (this.rightButton) { return; }

            //Quitar la Parte de Propiedades
            SP02.Panel2Collapsed = true;

            //Buscar Hijos en caso de ser Tipo CarpetaExpediente
            if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.CarpetaExpediente)
            {
                if (nodoActual.Nodes.Count > 0) 
                {
                    if (MessageBox.Show("Desea volver a cargar\n\resta Sección?", "Volver a Cargar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        (((System.Windows.Forms.TreeView)(sender))).SelectedNode.Expand();
                        return;
                    }
                }

                cargarExpedientes(Convert.ToInt16(((Clases.clsNodoTiny)nodoActual.Tag).idNodo), nodoActual);
                //Borrar las imagens si hubiera            
                tblImagenes.Clear();
                imagenesDB.Images.Clear();
            }

            //Cargar las imagenes desde la base de Datos
            if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Documento)
            {
                txtPage.Text = "1";
                //Cargar Imagenes
                cargarImagenes(((Clases.clsNodoTiny)nodoActual.Parent.Tag).idNodo, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo);
                return;
            }


            if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Carpeta)
            {
                //Cargar el Nodo como tipo Carpeta
                nodoBD = new Clases.clsNodoCarpeta(((Clases.clsNodoTiny)nodoActual.Tag).idNodo);
                //Borrar las imagens si hubiera
                tblImagenes.Clear();
                imagenesDB.Images.Clear();
            }

            //En caso de ser nodo Expediente cargar las Propiedades para Visualizar o Editar
            if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Expediente)
            {
                //Cargar las Propiedades si existen
                cargarCampos(Clases.clsEnums.TipoNodo.Expediente, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo);


                //Abrir el Panel de Propiedades
                if (PGECampos.Item.Count != 0)
                {
                    SP02.Panel2Collapsed = false;
                }
                else
                {
                    SP02.Panel2Collapsed = true;
                }

                //Borrar las imagens si hubiera
                tblImagenes.Clear();
                imagenesDB.Images.Clear();
            }
        }

        private void tvArbolGeneral_DoubleClick(object sender, EventArgs e)
        {
            //Activar el Nodo Actual
            nodoActual = this.tvArbolGeneral.SelectedNode;

            if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Carpeta)// || ((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.CarpetaExpediente)
            {
                if (((System.Windows.Forms.TreeView)(sender)).SelectedNode.Nodes.Count > 0)
                {
                    if (MessageBox.Show("Desea volver a cargar\n\resta Sección?", "Volver a Cargar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        (((System.Windows.Forms.TreeView)(sender))).SelectedNode.Expand();
                        return;
                    }
                }

                ((System.Windows.Forms.TreeView)(sender)).SelectedNode.Nodes.Clear();
                cargarArbol(Convert.ToInt16(((Digitalizacion2014.Clases.clsNodoTiny)(((System.Windows.Forms.TreeView)(sender)).SelectedNode.Tag)).idNodo), (((System.Windows.Forms.TreeView)(sender))).SelectedNode);
                (((System.Windows.Forms.TreeView)(sender))).SelectedNode.Expand();
            }
        }

        private void tvArbolGeneral_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null || e.Label == e.Node.Text)
            {
                e.CancelEdit = true;
                return;
            }

            ////Se cambio el Nombre o Llave al Expediente
            parametros = "V1=" + ((Clases.clsNodoTiny)nodoActual.Tag).idNodo.ToString() + "|V4=" + e.Label + "|" +
                         "|C1=" + Clases.vGlobales.id_User +
                         "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString(); ;
            validar = "8";
            Datos = Acceso.ivkProcedimiento("sp_Datos_Expedientes", validar, parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk)
            {
                MessageBox.Show("No se puedo renombrar el Nodo", "Protección del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void tvArbolGeneral_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo != Clases.clsEnums.TipoNodo.Expediente)
            {
                e.CancelEdit = true;
                return;
            }

        }

        #endregion

        #region Manejo de Propiedades

        void cargarCampos(Clases.clsEnums.TipoNodo TipoNodo, string idDocumentoPadre)
        {
            string proc = ""; string opc = ""; string para = "";
            WSD2014.cRetorno campos = new WSD2014.cRetorno();

            if (TipoNodo == Clases.clsEnums.TipoNodo.Expediente)
            {
                proc = "sp_Datos_Expedientes_Campos";
                para = "|V1=" + idDocumentoPadre + "|V4=" + ((Clases.clsNodoTiny)nodoActual.Tag).idNodo.ToString() + "|";
                opc = "3";
                PGECampos.Tag = "";
            }
            else
            {
                proc = "sp_Datos_Documentos_Campos";
                para = "|V1=" + idDocumentoPadre + "|";
                opc = "3";
                PGECampos.Tag = idDocumentoPadre;
            }

            PGECampos.Item.Clear();

            //Carga de Datos
            campos = Acceso.ivkProcedimiento(proc, opc, para, Clases.vGlobales.conexion, null);
            if (campos.bOk)
            {
                //Cargar los Datos en Pantalla
                PGECampos.ShowCustomProperties = true;
                PGECampos.Item.Clear();
                foreach (DataRow r in campos.ds.Tables[0].Rows)
                {
                    if (r["TipoCampo"].ToString().ToUpper() == "FECHA")
                    {
                        DateTime fecha = r["valor"].ToString().Length > 0 ? Convert.ToDateTime(r["valor"].ToString().Substring(0,10)) : DateTime.Now;
                        PGECampos.Item.Add(r["Descripcion"].ToString(), fecha, false, r["Clasificacion"].ToString(), r["Explicacion"].ToString(), true);
                    }
                    else if (r["TipoCampo"].ToString().ToUpper() == "TABLA")
                    {
                        PGECampos.Item.Add(r["Descripcion"].ToString(), r["valor"].ToString(), false, r["Clasificacion"].ToString(), r["Explicacion"].ToString(), true);

                        //Mandar cargar los Datos para la Tabla de Valores de la Tabla
                        WSD2014.cRetorno _valores = new WSD2014.cRetorno();
                        _valores = Acceso.ivkProcedimiento("sp_ConfigCamposTabla", "1", "|V2=" + r["idCampo"].ToString() + "|", Clases.vGlobales.conexion, null);
                        if (_valores.bOk)
                        {
                            //Cargo Datos para el Campo ComboBox
                            PGECampos.Item[PGECampos.Item.Count - 1].ValueMember = "Valor";
                            PGECampos.Item[PGECampos.Item.Count - 1].DisplayMember = "Valor";
                            PGECampos.Item[PGECampos.Item.Count - 1].Datasource = _valores.ds.Tables[0];
                        }
                    }
                    else
                    {
                        PGECampos.Item.Add(r["Descripcion"].ToString(), r["valor"].ToString(), false, r["Clasificacion"].ToString(), r["Explicacion"].ToString(), true);
                    }

                    PGECampos.Item[PGECampos.Item.Count - 1].Tag = r["idCampo"].ToString();
                    
                    try
                    {

                        if (r["bAutomatico"].ToString() == "0")
                        {
                            PGECampos.Item[PGECampos.Item.Count - 1].IsReadOnly = true;
                        }
                    }
                    catch
                    {
                    }
                }
                PGECampos.Refresh();
            }

        }

        private void PGECampos_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyGridEx.PropertyGridEx obj = (PropertyGridEx.PropertyGridEx)s;
            PropertyGridEx.CustomProperty campo = null;
            foreach (PropertyGridEx.CustomProperty c in obj.Item)
            {
                if (c.Name == e.ChangedItem.Label)
                {
                    campo = c;
                    break;
                }
            }

            if (campo != null)
            {
                //Se asigno un campo
                if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Expediente)
                {
                    //Es un expediente
                    Clases.clsCampoDato myCampo = new Clases.clsCampoDato(Clases.clsCampoDato.TipoCampo.Expediente, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo, campo.Tag.ToString());
                    myCampo.valor = campo.Value.ToString();
                    myCampo.guardarDatos();
                }
                else if (((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Documento)
                {
                    //Es un Documentos Final
                    Clases.clsCampoDato myCampo = new Clases.clsCampoDato(Clases.clsCampoDato.TipoCampo.Documento, PGECampos.Tag.ToString(), campo.Tag.ToString());
                    myCampo.valor = campo.Value.ToString();
                    myCampo.guardarDatos();

                }
            }
        }
        #endregion


        #region Menu Contextual Arbol General

        private void agregarNodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Agregar Elemento puede ser expediente o documento
            if (nodoActual == null) { return; } //No esta seleccionado ningun nodo
            frmDigitalizarExp frmNuevoExpediente = new frmDigitalizarExp();
            frmNuevoExpediente.nodoBD = this.nodoBD;
            frmNuevoExpediente.StartPosition = FormStartPosition.CenterParent;
            if (frmNuevoExpediente.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShowNotify("Expediente agregado", "Se agrego un expediente de manera correcta", ToolTipIcon.Info, 2000);
                //MessageBox.Show("Proceso realizado correctamente");
            }
            frmNuevoExpediente.Dispose();
            System.GC.WaitForPendingFinalizers();
            System.GC.Collect();
        }

        private void generarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var sd = new SaveFileDialog
            {
                OverwritePrompt = true,
                AddExtension = true,
                FileName = nodoActual.Text + ".pdf",
                Filter = "Documento PDF (*.pdf)|*.pdf"
            };

            if (sd.ShowDialog() == DialogResult.OK)
            {
                Cursor = System.Windows.Forms.Cursors.WaitCursor;

                //Cargar DataSet de Imagenes
                //parametros = "|V2=" + ((Clases.clsNodoTiny)nodoActual.Tag).idNodo + "|";
                //validar = "42";
                //Datos = Acceso.ivkProcedimiento("sp_Datos_Documentos", validar, parametros, Clases.vGlobales.conexion, null);

                Clases.clsPDFExport pdfExport = new Clases.clsPDFExport();
                pdfExport.Exportar(sd.FileName, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo);


                Cursor = Cursors.Default;

                ShowNotify("Generación de PDF", "Termino el proceso de generación de PDF", ToolTipIcon.Info, 2000);
            }

        }

        private void generarExpedientesPDFsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var carpeta = new FolderBrowserDialog
            {
                Description = "Carpeta de Salida",
                ShowNewFolderButton = true,
            };

            if (carpeta.ShowDialog() == DialogResult.OK)
            {
                Cursor = System.Windows.Forms.Cursors.WaitCursor;

                parametros = "|V2=" + ((Clases.clsNodoTiny)nodoActual.Tag).idNodo + "|";
                validar = "10";
                Datos = Acceso.ivkProcedimiento("sp_Datos_Expedientes", validar, parametros, Clases.vGlobales.conexion, null);

                if (Datos.bOk)
                {
                    // Iniciar el Proceso de Generación de PDF's
                    PBGeneral.Style = ProgressBarStyle.Continuous;
                    PBGeneral.Minimum = 1;
                    PBGeneral.Maximum = Datos.ds.Tables[0].Rows.Count;
                    PBGeneral.Value = 1;
                    PBGeneral.Step = 1;

                    PBGeneral.Visible = true;

                    foreach (DataRow r in Datos.ds.Tables[0].Rows)
                    {
                        //parametros = "|V2=" + r[0].ToString() +"|";
                        //validar = "42";
                        //Datos2 = Acceso.ivkProcedimiento("sp_Datos_Documentos", validar, parametros, Clases.vGlobales.conexion, null);
                        
                        if (!File.Exists(carpeta.SelectedPath + "\\" + r[1].ToString() + ".pdf"))
                        {
                            Clases.clsPDFExport pdfExport = new Clases.clsPDFExport();
                            pdfExport.Exportar(carpeta.SelectedPath + "\\" + r[1].ToString() + ".pdf", r[0].ToString());
                        }
                        
                        PBGeneral.PerformStep();
                        Application.DoEvents();
                    }

                    PBGeneral.Visible = false;
                    PBGeneral.Style = ProgressBarStyle.Marquee;
                }

                ShowNotify("Generación de PDF", "Termino el proceso de generación de PDF's", ToolTipIcon.Info, 2000);
                Cursor = Cursors.Default;
            }
        }
        #endregion
        
        #region Visualizador
        private void PreviewImage()
        {
            if (imagenesDB.Images.Count > 0)
            {
                using (var viewer = new frmVisualizador())
                {
                    viewer.bEdicion = false;
                    viewer.ImageList = imagenesDB;
                    viewer.ImageIndex = 0;  //SelectedIndices.First();
                    viewer.DeleteCallback = UpdateThumbnails;
                    viewer.UpdateCallback = UpdateThumbnails;
                    viewer.ShowDialog();
                }
            }
        }
        #endregion

        #region Thumbails Principal

        private void updateToolBar()
        {
            //Opciones de Menu
            //PDF's
            ((frmPrincipal)this.MdiParent).rbPDFAll.Text = string.Format("Todo ({0})", imagenesDB.Images.Count);
            ((frmPrincipal)this.MdiParent).rbPDFSelect.Text = string.Format("Los ({0}) seleccionados", SelectedIndices.Count());
            ((frmPrincipal)this.MdiParent).rbPDFSelect.Enabled = SelectedIndices.Any();

            //JPEG's
            ((frmPrincipal)this.MdiParent).rbJPGTodos.Text = string.Format("Todo ({0})", imagenesDB.Images.Count);
            ((frmPrincipal)this.MdiParent).rbJPGSelect.Text = string.Format("Los ({0}) seleccionados", SelectedIndices.Count());
            ((frmPrincipal)this.MdiParent).rbJPGSelect.Enabled = SelectedIndices.Any();
        }

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
            get { return imagenesDB.Images.ElementsAt(SelectedIndices); }
        }

        private void UpdateThumbnails()
        {
            tblImagenes.UpdateImages(imagenesDB.Images);
            updateToolBar();
        }

        private void UpdateThumbnails(IEnumerable<int> selection)
        {
            UpdateThumbnails();
            SelectedIndices = selection;
        }

        private void SelectAll()
        {
            SelectedIndices = Enumerable.Range(0, imagenesDB.Images.Count);
        }

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
                    if (e.Control)
                    {
                        Delete();
                    }
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

        private void tblImagenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateToolBar();
            //Se cambio de imagen a ver activar sus propiedas y etc

            //Verificar que solo una este seleccionada
            if (SelectedIndices.Count() == 1)
            {
                // Cargar las Propiedades del Documento Seleccionado 
                // MessageBox.Show("");
                // string id=((new System.Linq.SystemCore_EnumerableDebugView<Digitalizacion2014.Scan.Images.IScannedImage>(SelectedImages)).Items[0]).Tag.ToString();

                string idDocumento = SelectedImages.ToList()[0].Tag.ToString();
                cargarCampos(Clases.clsEnums.TipoNodo.Documento, idDocumento);

                //Abrir el Panel de Propiedades
                if (PGECampos.Item.Count != 0)
                {
                    SP02.Panel2Collapsed = false;
                }
                else
                {
                    SP02.Panel2Collapsed = true;
                }

            }
        }

        private void tblImagenes_MouseMove(object sender, MouseEventArgs e)
        {
            if (nodoActual == null)
            {
                return;
            }

            //Cursor = tblImagenes.GetItemAt(e.X, e.Y) == null ? Cursors.Default : Cursors.Hand;
            if (imagenesDB.Images.Count > 0 || ((Clases.clsNodoTiny)nodoActual.Tag).tipoNodo == Clases.clsEnums.TipoNodo.Documento)
            {
                tblImagenes.ContextMenuStrip.Enabled = true;
            }
            else
            {
                tblImagenes.ContextMenuStrip.Enabled = false;
            }

            if (tblImagenes.GetItemAt(e.X, e.Y) == null)
            {
                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.Hand;
            }
        }

        private void tblImagenes_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        #endregion

        #region Exportación a PDF y Jpeg

        public void ExportJPG(object sender, EventArgs e)
        {
            SaveImages(imagenesDB.Images);
        }

        public void ExportJPGSelect(object sender, EventArgs e)
        {
            SaveImages(SelectedImages.ToList());
        }

        private void SaveImages(List<IScannedImage> images)
        {
            if (images.Any())
            {
                var sd = new SaveFileDialog
                {
                    OverwritePrompt = true,
                    AddExtension = true,
                    Filter = "Imagen BMP (*.bmp)|*.bmp|" +
                             "Metaarchivo mejorado de Windows (*.emf)|*.emf|" +
                             "Archivo de Imagen Intercambiable (*.exif)|*.exif|" +
                             "Imagen GIF (*.gif)|*.gif|" +
                             "Imagen JPEG (*.jpg, *.jpeg)|*.jpg;*.jpeg|" +
                             "Imagen PNG (*.png)|*.png"
                };
                sd.FilterIndex = 5;

                if (sd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageSaver.SaveImages(sd.FileName, images, path =>
                        {
                            if (images.Count() == 1)
                            {
                                // One image, so the file name is the same and the save dialog already prompted the user to overwrite
                                return true;
                            }
                            switch (
                                MessageBox.Show(
                                    string.Format("El archivo {0} ya existe. ¿Desea sobreescribirlo?", path),
                                    "Sobreescribir archivo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                            {
                                case DialogResult.Yes:
                                    return true;
                                case DialogResult.No:
                                    return false;
                                default:
                                    throw new InvalidOperationException("Cancelado por el Usuario");
                            }
                        });
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }
        }

        public void ExportPDF(object sender, EventArgs e)
        {
            SavePDF(imagenesDB.Images);
        }

        public void ExportPDFSelect(object sender, EventArgs e)
        {
            SavePDF(SelectedImages.ToList());
        }

        private void SavePDF(List<IScannedImage> images)
        {
            if (images.Any())
            {
                var sd = new SaveFileDialog
                {
                    OverwritePrompt = true,
                    AddExtension = true,
                    Filter = "Documento PDF (*.pdf)|*.pdf"
                };

                if (sd.ShowDialog() == DialogResult.OK)
                {
                    Clases.clsPDFExport pdfExport = new Clases.clsPDFExport();
                    
                    Cursor = Cursors.WaitCursor;

                    pdfExport.Exportar(sd.FileName, images);
                    ShowNotify("Generación de PDF", "Termino el proceso de generación de PDF", ToolTipIcon.Info, 2000);

                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        private void visualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewImage();
        }

        private void seleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void copiarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CopyImages();
        }

        private void CopyImages()
        {
            if (SelectedIndices.Any())
            {
                var images = SelectedImages.Select(x => x.GetImage()).ToList();
                IDataObject ido = new DataObject();
                ido.SetData(DataFormats.Bitmap, true, images.First());
                var rtfEncodedImages = "{" + string.Join(@"\par", images.Select(GetRtfEncodedImage)) + "}";
                ido.SetData(DataFormats.Rtf, true, rtfEncodedImages);
                Clipboard.SetDataObject(ido);
            }
        }

        private void Delete()
        {
            if (SelectedImages.ToList().Count < 1)
            {
                return;
            }

            if (MessageBox.Show(string.Format("Realmente desea borrar {0} imagenes?", SelectedImages.ToList().Count), "Borrar...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Cursor = Cursors.AppStarting;

                Controles.frmGauge gauge = new Controles.frmGauge();
                gauge.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                gauge.pb.Minimum = 0;
                gauge.pb.Maximum = SelectedImages.ToList().Count - 1;
                gauge.pb.Value = 0;
                gauge.Show();


                // Se procede a elimiar las imagenes Seleccionadas de la B.D.
                foreach (IScannedImage scannedImage in SelectedImages.ToList())
                {
                    
                    // Borrar o Desactivar de la B.D.
                    parametros = "|V1=" + scannedImage.Tag.ToString() +
                              "|C1=" + Clases.vGlobales.id_User +
                              "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString();

                    Datos = Acceso.ivkProcedimiento("sp_Datos_Documentos", "5", parametros, Clases.vGlobales.conexion, null);

                    imagenesDB.Images.Remove(scannedImage);

                    gauge.pb.PerformStep();
                    gauge.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                }

                tblImagenes.UpdateImages(imagenesDB.Images);
                gauge.Close();
                gauge.Dispose();
                Cursor = Cursors.Default;
            }

        }

        private static string GetRtfEncodedImage(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                string hexString = BitConverter.ToString(stream.ToArray(), 0).Replace("-", string.Empty);

                return @"{\pict\pngblip\picw" +
                       image.Width + @"\pich" + image.Height +
                       @"\picwgoa" + image.Width + @"\pichgoa" + image.Height +
                       @"\hex " + hexString + "}";
            }
        }
        
        private void agregarDesdeScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDigitalizarDocto addImages = new frmDigitalizarDocto();
            addImages.Expediente = new Clases.clsExpediente(((Clases.clsNodoTiny)nodoActual.Parent.Tag).idNodo.ToString());
            addImages.DocumentoActual = (Clases.clsNodoTiny)nodoActual.Tag;
            addImages.Text = "Expediente " + nodoActual.Parent.Text.ToString();
            addImages.lblExpDescripcion.Text = nodoActual.Text.ToString();
            addImages.nodoActual = nodoActual;
            
            if (addImages.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ShowNotify("Agregar páginas", "Se agregaron páginas de manera correcta", ToolTipIcon.Info, 2000);
                cargarImagenes(((Clases.clsNodoTiny)nodoActual.Parent.Tag).idNodo, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo); 
            }

        }

        #region Botones inferiores

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

            if (imagenesDB.Images.Count != 0)
            {
                tblImagenes.ClearItems();
                tblImagenes.UpdateImages(imagenesDB.Images);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            txtPage.Text = (Convert.ToInt16(txtPage.Text) + 1).ToString();
            // cargarImagenes(((Clases.clsNodoTiny)nodoActual.Parent.Tag).idNodo, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(txtPage.Text) > 1)
            {
                txtPage.Text = (Convert.ToInt16(txtPage.Text) - 1).ToString();
                // cargarImagenes(((Clases.clsNodoTiny)nodoActual.Parent.Tag).idNodo, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo);
            }
        }

        private void cboRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPage.Text = "1";
            cargarImagenes(((Clases.clsNodoTiny)nodoActual.Parent.Tag).idNodo, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo);
        }
        
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (totalPag != 0)
            {
                txtPage.Text = "1";
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (totalPag != 0)
            {
                txtPage.Text = totalPag.ToString();
            }
        }

        private void txtPage_TextChanged(object sender, EventArgs e)
        {
            cargarImagenes(((Clases.clsNodoTiny)nodoActual.Parent.Tag).idNodo, ((Clases.clsNodoTiny)nodoActual.Tag).idNodo);
        }

        #endregion

        private void ShowNotify(string Title, string Text, ToolTipIcon TipIcono, int TimeMS)
        {
            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.BalloonTipTitle = Title;
            notifyIcon1.BalloonTipText = Text;
            notifyIcon1.BalloonTipIcon = TipIcono;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(TimeMS);
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        private void resumenDeExpedientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes.frmViewReport miVisor = new Reportes.frmViewReport();

            miVisor.idReporte = "1";
            miVisor.R_parametros = "|V1=" + miVisor.idReporte.ToString();
            miVisor.R_validar = "1";

            miVisor.D_procedimiento = "SP_Resumenes";
            miVisor.D_validar = "1";
            miVisor.D_parametros = "|V1=" + ((Clases.clsNodoTiny)nodoActual.Tag).idNodo.ToString() + "|";

            miVisor.MdiParent = this.MdiParent;
            miVisor.Show();
        }

        private void tvArbolGeneral_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.rightButton = true;
            }
            else
            {
                this.rightButton = false;
            }
        }

        private void cmsImagenes_Opening(object sender, CancelEventArgs e)
        {
            if (tblImagenes.SelectedItems.Count > 0)
            {
                sustituirPáginaToolStripMenuItem.Enabled = true;
            }
            else
            {
                sustituirPáginaToolStripMenuItem.Enabled = false;
            }
        }        
        
    }
}
