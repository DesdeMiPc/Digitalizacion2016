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
    public partial class frmCamposTablaEdit : Form
    {
        public Clases.clsCampoTabla CampoTabla;

        public frmCamposTablaEdit()
        {
            InitializeComponent();
        }

        private void frmCamposTablaEdit_Load(object sender, EventArgs e)
        {
            this.Text = "Edidicón de Valor --> " + (CampoTabla.id == "0" ? "Nuevo Valor" : CampoTabla.id.ToString());
            this.iTXTValor.Text = CampoTabla.descripcion;
            this.iTXTValor.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.iTXTValor.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("La descripción esta Vacia");
                return;
            }

            CampoTabla.descripcion = this.iTXTValor.Text;

            //Guardar los Datos en el Server
            if (!this.CampoTabla.guardarDatos(Clases.vGlobales.id_User))
            {
                MessageBox.Show("Problemas al accesar la base de datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
