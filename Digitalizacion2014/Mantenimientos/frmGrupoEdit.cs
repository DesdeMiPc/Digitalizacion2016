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
    public partial class frmGrupoEdit : Form
    {
        public Clases.clsGrupo grupo;

        public frmGrupoEdit()
        {
            InitializeComponent();
        }

        private void frmGrupoEdit_Load(object sender, EventArgs e)
        {
            this.Text = "Edidicón de grupo --> " + (grupo.id == "0" ? "Nuevo Grupo" : grupo.id.ToString());
            this.iTXTDescripcion.Text = grupo.descripcion;

            if (!this.grupo.activo)
            {
                if (MessageBox.Show("Grupo no Activo,\n\rDesea Activarlo=?", "Activación?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    //Activar el Grupo Actual
                    this.grupo.activo = true;
                    this.grupo.guardarDatos();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                }
            }
            iTXTDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (iTXTDescripcion.Text.ToString().Trim().Length == 0)
            {
                MessageBox.Show("La descripción esta Vacia");
                return;
            }

            grupo.descripcion = iTXTDescripcion.Text;

            //Guardar los Datos en el Server
            if (!this.grupo.guardarDatos(Clases.vGlobales.id_User))
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
