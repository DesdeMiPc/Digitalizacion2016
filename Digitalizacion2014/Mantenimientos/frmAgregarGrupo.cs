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
    public partial class frmAgregarGrupo : Form
    {
        //Variables para WebService
        private string procedimiento = "sp_Arbol_General";
        private string parametros = "";
        private string validar = "";

        //Agregar desde el Origen
        public string idNodoActual = "";

        //Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();

        public frmAgregarGrupo()
        {
            InitializeComponent();
        }

        private void frmAgregarGrupo_Load(object sender, EventArgs e)
        {
            //Cargar los Grupos Disponibles
            validar = "202";
            parametros = "|V2=" + idNodoActual.ToString().Trim() + "|";
            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk)
            {
                MessageBox.Show("No Existen Grupos disponibles para este Nodo");
                this.Close();
            }

            cboGrupos.DataSource = Datos.ds.Tables[0];
            cboGrupos.SelectedIndex = 0;
            cboGrupos.Focus();
        }
    }
}
