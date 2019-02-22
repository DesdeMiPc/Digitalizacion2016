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
    public partial class frmAgregarCampo : Form
    {
        //Variables para WebService
        private string procedimiento = "";
        private string parametros = "";
        private string validar = "";

        //Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();


        public frmAgregarCampo()
        {
            InitializeComponent();
        }

        private void frmAgregarCampo_Load(object sender, EventArgs e)
        {
            //Cargar Datos del ComboBox
            validar = "1";
            parametros = "";
            procedimiento = "sp_ClasificacionCampos";

            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (Datos.bOk != true)
            {
                MessageBox.Show("Problemas al accesar la base de datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else
            {
                this.cboClasificacionCampos.DataSource = Datos.ds.Tables[0];
            }

        }

        private void cboClasificacionCampos_SelectedIndexChanged(object sender, EventArgs e)
        {
            procedimiento = "sp_ConfigCampos";
            parametros = "|V5=" + cboClasificacionCampos.SelectedValue.ToString() + "|";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (Datos.bOk != true)
            {
                MessageBox.Show("Problemas al accesar la base de datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else
            {
                this.cboCampos.DataSource = Datos.ds.Tables[0];
            }
        }
    }
}
