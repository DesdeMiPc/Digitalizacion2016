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
    public partial class frmFormularioEdit : Form
    {
        public Clases.clsFormulario formulario;

        public frmFormularioEdit()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            formulario.descripcion = this.iTXTDescripcion.Text.Trim();
            formulario.camposAutomaticos = this.chkCodigoBarras.Checked;

            if (!this.formulario.guardarDatos(Clases.vGlobales.id_User))
            {
                MessageBox.Show("Problemas al accesar la base de datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void frmFormularioEdit_Load(object sender, EventArgs e)
        {
            iTXTDescripcion.Text = this.formulario.descripcion;
            chkCodigoBarras.Checked = this.formulario.camposAutomaticos;
        }
    }
}
