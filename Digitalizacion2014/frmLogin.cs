using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Digitalizacion2014
{
    public partial class frmLogin : Form
    {
        Clases.clsUsuario usuario;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;
            usuario = new Clases.clsUsuario(iTXTUsuario.Text, true);
            Cursor = Cursors.Default;

            if (!usuario.activo || usuario.claveAcceso != iTXTPwd.Text)
            {
                MessageBox.Show("Usuario y/o Clave incorrectas", "Error de Auntenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                iTXTUsuario.Focus();
                this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
                return;
            }

            Clases.vGlobales.id_Unico = usuario.id;
            Clases.vGlobales.id_User = usuario.loginName;
            Clases.vGlobales.sPwd = usuario.claveAcceso;
            Clases.vGlobales.validado = true;
        }

        private void frmLogin_Activated(object sender, EventArgs e)
        {
            iTXTUsuario.Focus();
        }

        private void iTXTUsuario_Leave(object sender, EventArgs e)
        {
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.Ignore)
            {
                e.Cancel = true;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            WSD2014.cRetorno db;
            WSD2014.WSDatosSoap acceso = new WSD2014.WSDatosSoapClient();

            db = acceso.ConexionesValidas();

            cboDatos.DataSource = db.ds.Tables[0];
            // Clases.vGlobales.conexion = db.ds.Tables[0].Rows[0]["Valor"].ToString();
        }

        private void cboDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clases.vGlobales.conexion = cboDatos.SelectedValue.ToString();
        }
    }
}
