using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Digitalizacion2014.Mantenimientos;

namespace Digitalizacion2014.Configuracion
{
    public partial class frmConfigArbol : Digitalizacion2014.frmBases.frmGeneral01
    {
        //Variables para WebService
        private string procedimiento = "sp_Arbol_General";
        private string parametros = "";
        private string validar = "";

        private Clases.clsCadenas cadenas = new Clases.clsCadenas();

        //Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();

        //Nodo Actual
        TreeNode nodoActual;


        //Valores Logicos para Saber si se mandar guardar
        Boolean bRead = false;
        Boolean bPrint = false;
        Boolean bExport = false;
        Boolean bAdd = false;
        Boolean bModify = false;
        Boolean bDelete = false;

        public frmConfigArbol()
        {
            InitializeComponent();
        }

        private void tvArbolGeneral_MouseUp(object sender, MouseEventArgs e)
        {
            //Saber si existe un nodo en la posición Actual
            nodoActual = this.tvArbolGeneral.GetNodeAt(e.X, e.Y);

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (nodoActual == null)
                {
                    //Quitar opciones del menu contextual
                    this.deshabilitarNodoToolStripMenuItem.Enabled = false;
                    this.habilitarNodoToolStripMenuItem.Enabled = false;
                    this.eliminarNodoToolStripMenuItem.Enabled = false;
                    this.copiarToolStripMenuItem.Enabled = false;
                }
                else
                {
                    //Cargar Datos del Nodo para comportamiento de menu contextual
                    parametros = "|V2=" + nodoActual.Tag.ToString() + "|" +
                         "|C1=" + Clases.vGlobales.id_User +
                         "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString(); ;
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
                            this.deshabilitarNodoToolStripMenuItem.Enabled =false;
                            this.habilitarNodoToolStripMenuItem.Enabled = true;
                        }

                        if ((int)Datos.ds.Tables[0].Rows[0]["Tipo"] == 2)
                        {
                            this.agregarNodoToolStripMenuItem.Enabled = false;
                        }
                        else
                        {
                            this.agregarNodoToolStripMenuItem.Enabled = true;
                        }
                    }

                    //Poner Opciones
                    this.eliminarNodoToolStripMenuItem.Enabled = true;
                    this.copiarToolStripMenuItem.Enabled = true;
                }
                //Se presiono el Boton de Menu Contextual
                this.cmsContextual.Show(this.tvArbolGeneral, new Point(e.X, e.Y));
            }
        }

        private void agregarNodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Agregar nuevo Nodo
            frmAgregarNodo frmNewNodo = new frmAgregarNodo();
            frmNewNodo.idNodoPadre = nodoActual == null ? "0" : nodoActual.Tag.ToString();
            if (frmNewNodo.ShowDialog() == DialogResult.OK)
            {
                //Agregar el nodo nuevo en la base de datos y en el TreeNode de Pantalla
                string tipoArbol ="";
                string formulario = "";
                string descripcion = "";

                formulario = (frmNewNodo.cboFormularios.SelectedValue == null ? "0" : frmNewNodo.cboFormularios.SelectedValue.ToString());

                if (frmNewNodo.cboTipo.Text == "Documento Final")
                {
                    tipoArbol = "2";
                    descripcion = frmNewNodo.cboFormularios.Text;
                }

                if (frmNewNodo.cboTipo.Text == "Expediente")
                {
                    tipoArbol = "1";
                    descripcion = frmNewNodo.cboFormularios.Text;
                } 

                if (frmNewNodo.cboTipo.Text == "Carpeta")
                {
                    tipoArbol = "0";
                    formulario = "0";
                    descripcion = frmNewNodo.iTXTDescripcion.Text.ToString();
                }

                parametros = "V1=" + frmNewNodo.idNodoPadre + "|V3=" + descripcion
                           + "|V4=" + tipoArbol + "|V5=" + formulario + "|"
                           + "|C1=" + Clases.vGlobales.id_User
                           + "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString();
                validar = "3";
                Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
                
                MessageBox.Show(Clases.clsCadenas.obtenerValor("2M",Datos.sResultado,"|"));

                if (Datos.bOk)
                {
                    //Actualizar el TreeView
                    if (nodoActual != null)
                    {
                        nodoActual.Nodes.Clear();
                    }
                    else
                    {
                        tvArbolGeneral.Nodes.Clear(); 
                    }

                    cargarArbol(Convert.ToInt16(frmNewNodo.idNodoPadre), nodoActual );
                }
            }
        }

        private void frmConfigArbol_Load(object sender, EventArgs e)
        {
            //Carga inicial del Arbol General
            cargarArbol(0);
        }


        void cargarArbol(int idInicial, TreeNode nodoInicial = null)
        {
            parametros = "|V1=" + idInicial.ToString() + "|" +
                         "|C1=" + Clases.vGlobales.id_User +
                         "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString(); ;
            validar = "1";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            
            if (Datos.bOk)
            {
                //Se obtuvieron Datos
                foreach (DataRow r in Datos.ds.Tables[0].Rows)
                {
                    TreeNode newNodo = new TreeNode();
                    newNodo.Text = r["Descripcion"].ToString();
                    newNodo.Tag = r["idNodo"].ToString();
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

                    //Se pondra en el evento de doubleClick del Nodo
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

        private void tvArbolGeneral_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                return;
            }

            //Se cambio el Nombre dentro del Arbol
            parametros = "V2=" + e.Node.Tag.ToString() + "|V3=" + e.Label + "|" +
                         "|C1=" + Clases.vGlobales.id_User +
                         "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString(); ;
            validar = "31";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk)
            {
                MessageBox.Show("No se puedo renombrar el Nodo","Protección del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
        }

        private void tvArbolGeneral_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            //Antes de Editar determinar que no sea Documento Final
            //Cargar Datos del Nodo para comportamiento de menu contextual
            parametros = "|V2=" + nodoActual.Tag.ToString() + "|" +
                         "|C1=" + Clases.vGlobales.id_User +
                         "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString();
            validar = "2";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);

            if (Datos.bOk)
            {
                if ((int)Datos.ds.Tables[0].Rows[0]["Tipo"] == 1 || (int)Datos.ds.Tables[0].Rows[0]["Tipo"] == 2)
                {
                    e.CancelEdit = true;
                    MessageBox.Show("No se puede editar el nombre de este nodo","Protección del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        private void tvArbolGeneral_DoubleClick(object sender, EventArgs e)
        {
            (((System.Windows.Forms.TreeView)(sender))).SelectedNode.Nodes.Clear();
            //(((System.Windows.Forms.TreeView)(sender))).SelectedNode.Tag.ToString()
            cargarArbol(Convert.ToInt16((((System.Windows.Forms.TreeView)(sender))).SelectedNode.Tag.ToString()), (((System.Windows.Forms.TreeView)(sender))).SelectedNode);
            (((System.Windows.Forms.TreeView)(sender))).SelectedNode.Expand();
        }

        private void tvArbolGeneral_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CargarDerechos();
        }

        private void CargarDerechos()
        {
            if (nodoActual == null)
            {
                return;
            }

            //Despues de Seleccionar el Nodo
            //Buscar los Derechos del los Grupos actuales
            parametros = "|V2=" + nodoActual.Tag.ToString() + "|";
            validar = "201";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk) return;

            //Se cargaron Datos
            lbGrupos.DataSource = Datos.ds.Tables[0];
        }

        private void frmConfigArbol_Activated(object sender, EventArgs e)
        {
            nodoActual = tvArbolGeneral.SelectedNode;
            CargarDerechos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarGrupo nuevoGrupo = new frmAgregarGrupo();
            nuevoGrupo.idNodoActual = nodoActual == null ? "0" : nodoActual.Tag.ToString();
            nuevoGrupo.StartPosition = FormStartPosition.CenterParent;

            if (nuevoGrupo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Obtner lo que selecciono el usuario
                var x = nuevoGrupo.cboGrupos.SelectedValue;

                //Grabar los Datos del Grupo en la B.D.
                parametros = "|V2=" + nodoActual.Tag.ToString() + "|V7=" + x.ToString() + "|L1=0|L2=0|L3=0|L4=0|L5=0|L6=0|";
                validar = "203";
                Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
                if (!Datos.bOk) 
                {
                    MessageBox.Show("Problemas al afectar los Derechos del Grupo\n\r" + parametros + "\n\r" + Datos.sResultado);
                    return;
                }

                //Recargar Derechos
                CargarDerechos();

            }
        }

        private void lbGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cargar los Derechos del Grupo Seleccionado
            parametros = "|V2=" + nodoActual.Tag.ToString() + "|V7=" + lbGrupos.SelectedValue.ToString() + "|";
            validar = "204";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk)
            {
                chkAgregar.Checked = false;
                chkEliminar.Checked = false;
                chkExportar.Checked = false;
                chkImprimir.Checked = false;
                chkLectura.Checked = false;
                chkModificar.Checked = false;
            }
            else
            {
                chkAgregar.Checked = Convert.ToBoolean(Datos.ds.Tables[0].Rows[0]["RightAdd"]);
                chkEliminar.Checked = Convert.ToBoolean(Datos.ds.Tables[0].Rows[0]["RightDelete"]);
                chkExportar.Checked = Convert.ToBoolean(Datos.ds.Tables[0].Rows[0]["RightExport"]);
                chkImprimir.Checked = Convert.ToBoolean(Datos.ds.Tables[0].Rows[0]["RightPrint"]);
                chkLectura.Checked = Convert.ToBoolean(Datos.ds.Tables[0].Rows[0]["RightRead"]);
                chkModificar.Checked = Convert.ToBoolean(Datos.ds.Tables[0].Rows[0]["RightModify"]);
            }

            bRead = chkLectura.Checked;
            bPrint = chkImprimir.Checked;
            bExport = chkExportar.Checked;
            bAdd = chkAgregar.Checked;
            bModify = chkModificar.Checked;
            bDelete = chkEliminar.Checked;

            gbDerechos.Visible = true;
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            //Encender por Logica Derechos de menor escala
            if (((System.Windows.Forms.CheckBox)sender).Checked)
            {
                switch (((System.Windows.Forms.CheckBox)sender).Name)
                {
                    case "chkEliminar":
                        chkAgregar.Checked = true;
                        chkModificar.Checked = true;
                        chkLectura.Checked = true;
                        break;
                    case "chkAgregar":
                        chkModificar.Checked = true;
                        chkLectura.Checked = true;
                        break;
                    case "chkModificar":
                        chkLectura.Checked = true;
                        break;
                    case "chkExportar":
                        chkImprimir.Checked = true;
                        chkLectura.Checked = true;
                        break;
                    case "chkImprimir":
                        chkLectura.Checked = true;
                        break;
                }
            }
            else
            {
                switch (((System.Windows.Forms.CheckBox)sender).Name)
                {
                    case "chkLectura":
                        chkAgregar.Checked = false;
                        chkModificar.Checked = false;
                        chkExportar.Checked = false;
                        chkImprimir.Checked = false;
                        chkEliminar.Checked = false;
                        break;
                    case "chkImprimir":
                        chkExportar.Checked = false;
                        break;
                    case "chkModificar":
                        chkAgregar.Checked = false;
                        chkEliminar.Checked = false;
                        break;
                    case "chkAgregar":
                        chkEliminar.Checked = false;
                        break;
                }
            }

            //Enceder o Apagar Boton de Guardar
            if (bRead == chkLectura.Checked &&
                bPrint == chkImprimir.Checked &&
                bExport == chkExportar.Checked &&
                bAdd == chkAgregar.Checked &&
                bModify == chkModificar.Checked &&
                bDelete == chkEliminar.Checked)
            {
                btnGuardar.Visible = false;
            }
            else
            {
                btnGuardar.Visible = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            //Guardar los Cambios
            //Grabar los Datos del Grupo en la B.D.
            parametros = "|V2=" + nodoActual.Tag.ToString() + "|V7=" + lbGrupos.SelectedValue.ToString() 
                       + "|L1=" + Convert.ToSByte(chkLectura.Checked).ToString()
                       + "|L2=" + Convert.ToSByte(chkImprimir.Checked).ToString()
                       + "|L3=" + Convert.ToSByte(chkExportar.Checked).ToString()
                       + "|L4=" + Convert.ToSByte(chkModificar.Checked).ToString()
                       + "|L5=" + Convert.ToSByte(chkAgregar.Checked).ToString()
                       + "|L6=" + Convert.ToSByte(chkEliminar.Checked).ToString() + "|" 
                       + "|C1=" + Clases.vGlobales.id_User 
                       + "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString(); ;
            validar = "203";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk)
            {
                MessageBox.Show("Problemas al afectar los Derechos del Grupo\n\r" + parametros + "\n\r" + Datos.sResultado);
                return;
            }

            //Cambiar los Estus
            bRead = chkLectura.Checked;
            bPrint = chkImprimir.Checked;
            bExport = chkExportar.Checked;
            bAdd = chkAgregar.Checked;
            bModify = chkModificar.Checked;
            bDelete = chkEliminar.Checked;

            btnGuardar.Visible = false;
        }
    }
}
