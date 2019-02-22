using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Reportes
{
    public partial class frmGuardar : Form
    {
        //Acceso y Retorno de Datos
        WSD2014.cRetorno datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap acceso = new WSD2014.WSDatosSoapClient();

        //Variables de Acceso
        string procedimiento = "SP_Reportes";
        string validar = "";
        string parametros = "";

        public string rpxBase64;
        public string idReporte;
        public string cNombre;
        public string cDescripcion;

        public frmGuardar()
        {
            InitializeComponent();
        }

        private void frmGuardar_Load(object sender, EventArgs e)
        {
            //Cargar el Catalogo de Categorias
            validar = "11";

            datos = acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (!datos.bOk)
            {
                //Problemas al Accesar los Datos
                MessageBox.Show("No Existen Categorias, Favor de Agregar alguna");
                return;
            }

            cboCategorias.DataSource = datos.ds.Tables[0];
            this.txtReporte.Text = cNombre;
            this.txtDescripcion.Text = cDescripcion;
            if (idReporte == "" || chkNuevo.Checked == true)
            {
                this.Text = "Guardando Reporte ...";
            }
            else
            {
                this.Text = "Guardando Reporte - " + idReporte.ToString();
            }


        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            frmNewCategoria frm = new frmNewCategoria();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                //Se procede a guadar una nueva Categoria
                //sin repetir en caso de existir otra igual
                validar = "12";
                parametros = "|V11=" + frm.txtNombre.Text.Trim() + "|V12=" + frm.txtDescripcion.Text.Trim() + "|";
                datos = acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
                if (!datos.bOk)
                {
                    MessageBox.Show("Problemas al accesar los Datos");
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    return;
                }

                //Cargar de nuevo el combo Box
                validar = "11";
                datos = acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
                if (!datos.bOk)
                {
                    //Problemas al Accesar los Datos
                    MessageBox.Show("Problemas al accesar los Datos");
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    return;
                }

                cboCategorias.DataSource = datos.ds.Tables[0];
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            validar = "2";
            parametros = "|V1=" + (chkNuevo.Checked == true ? "" : idReporte) + "|V2=" + txtReporte.Text.Trim() +
                         "|V3=" + txtDescripcion.Text.Trim() + "|V4=" + cboCategorias.SelectedValue.ToString() +
                         "|V5=" + rpxBase64 + "|";
            datos = acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (!datos.bOk)
            {
                //Problemas al Accesar los Datos
                MessageBox.Show("Problemas al accesar los Datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
                return;
            }

            idReporte = datos.ds.Tables[0].Rows[0]["idReporte"].ToString();
            cNombre = datos.ds.Tables[0].Rows[0]["cNombre"].ToString();
            cDescripcion = datos.ds.Tables[0].Rows[0]["cDescripcion"].ToString();
        }
    }
}
