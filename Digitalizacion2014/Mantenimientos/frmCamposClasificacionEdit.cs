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
    public partial class frmCamposClasificacionEdit : Form
    {
        public Clases.clsClasificacionCampos clasificacionCampos;

        public frmCamposClasificacionEdit()
        {
            InitializeComponent();
        }

        private void frmCamposClasificacionEdit_Load(object sender, EventArgs e)
        {
            this.Text = "Edición de Clasificación --> " + (clasificacionCampos.id == "0" ? "Nuevo Clasificador" : clasificacionCampos.id.ToString());
            this.iTXTDescripcion.Text = clasificacionCampos.descripcion;

            iTXTDescripcion.Focus();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (iTXTDescripcion.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("La descripción esta Vacia");
                return;
            }

            clasificacionCampos.descripcion = iTXTDescripcion.Text;

            //Guardar Datos en el Server
            if (!this.clasificacionCampos.guardarDatos(Clases.vGlobales.id_User))
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
