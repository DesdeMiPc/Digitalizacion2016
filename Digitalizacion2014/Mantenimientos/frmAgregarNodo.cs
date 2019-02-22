using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Mantenimientos
{
    public partial class frmAgregarNodo : Form
    {
        public string idNodoPadre = "0";

        //Variables para WebService
        private string procedimiento = "sp_Arbol_General";
        private string parametros = "";
        private string validar = "";

        //Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();

        public frmAgregarNodo()
        {
            InitializeComponent();
        }

        private void frmAgregarNodo_Load(object sender, EventArgs e)
        {
            validar = "2";
            parametros = "|V2=" + idNodoPadre + "|";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (Datos.bOk)
            {
                if (Convert.ToInt16(Datos.ds.Tables[0].Rows[0]["Tipo"]) == 0)
                {
                    cboTipo.Items.Add("Carpeta");
                    cboTipo.Items.Add("Expediente");
                }
                else if (Convert.ToInt16(Datos.ds.Tables[0].Rows[0]["Tipo"]) == 1)
                {
                    cboTipo.Items.Add("Documento Final");
                }
                cboFormularios.Enabled = true;
                label2.Enabled = true;
            }
            else
            {
                cboTipo.Items.Add("Carpeta");
                cboFormularios.Enabled = false;
                label2.Enabled = false;
            }

            cboTipo.SelectedItem = cboTipo.Items[0];
        }

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "Carpeta")
            {
                cboFormularios.Enabled = false;
                iTXTDescripcion.Enabled = true;
            }
            else
            {
                cboFormularios.Enabled = true;
                iTXTDescripcion.Enabled = false;

                //Cargar el combo de los tipos de formularios
                //Expedientes o Documentos finales

                procedimiento = "sp_ConfigFormularios"; validar = "1"; parametros = "|V4=" + (((ComboBox)sender).Text == "Expediente" ? "1" : "2");
                Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
                if (Datos.bOk)
                {
                    cboFormularios.DataSource = Datos.ds.Tables[0];
                    cboFormularios.ValueMember = "Id";
                    cboFormularios.DisplayMember = "Descripcion";
                    cboFormularios.SelectedValue = Datos.ds.Tables[0].Rows[0]["Id"];
                }
            }
        }
    }
}
