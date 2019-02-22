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
    public partial class frmUsuarioEdit : Form
    {
        public Clases.clsUsuario usuario;

        //Variables para WebService
        private string procedimiento = "";
        private string parametros = "";
        private string validar = "";

        //Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();

        public frmUsuarioEdit()
        {
            InitializeComponent();
        }

        private void frmUsuarioEdit_Load(object sender, EventArgs e)
        {
            //Cargar Datos del ComboBox
            procedimiento = "sp_Grupos";
            validar = "1";
            parametros = "";

            Datos = Acceso.ivkProcedimiento(procedimiento, validar, parametros, Clases.vGlobales.conexion, null);
            if (Datos.bOk != true)
            {
                MessageBox.Show("Problemas al accesar la base de datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            else
            {
                cboGrupo.DataSource = Datos.ds.Tables[0];
            }

            this.Text = "Edición de usuario --> " + (usuario.id == "0" ? "Nuevo Usuario" : usuario.loginName.ToString());
            this.iTXTUsuario.Text = usuario.loginName;
            this.iTXTNombreCompleto.Text = usuario.nombreCompleto;
            this.iTXTPuesto.Text = usuario.puesto;
            this.iTXTCorreo.Text = usuario.correo;
            this.iTXTPwd1.Text = usuario.claveAcceso;
            this.iTXTPwd2.Text = usuario.claveAcceso;
            cboGrupo.SelectedValue = usuario.idGrupo;

            if (!this.usuario.activo)
            {
                if (MessageBox.Show("Usuario Descativado,\n\rDesea Activarlo?", "Activacion?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    //Activar el Registro actual
                    this.usuario.activo = true;
                    usuario.guardarDatos();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                }
                iTXTUsuario.Focus();   
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (iTXTPwd1.Text != iTXTPwd2.Text)
            {
                //los password no son iguales
                MessageBox.Show("Confirmación de clave, incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                iTXTPwd2.Text = "";
                iTXTPwd2.Focus();
                return;
            }

            if (cboGrupo.SelectedValue == null)
            {
                MessageBox.Show("Debe de Seleccionar un Grupo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                return;
            }

            usuario.loginName = iTXTUsuario.Text.ToString().Trim();
            usuario.nombreCompleto = iTXTNombreCompleto.Text.ToString().Trim();
            usuario.puesto = iTXTPuesto.Text.ToString().Trim();
            usuario.correo = iTXTCorreo.Text.ToString().Trim();
            usuario.claveAcceso = iTXTPwd1.Text;
            usuario.idGrupo = Convert.ToInt16(cboGrupo.SelectedValue.ToString());

            if (!this.usuario.guardarDatos(Clases.vGlobales.id_User))
            {
                MessageBox.Show("Problemas al accesar la base de datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void iTXTPwd2_Leave(object sender, EventArgs e)
        {
            if (iTXTPwd2.Text.Length == 0)
            {
                return;
            }

            if (iTXTPwd1.Text != iTXTPwd2.Text)
            {
                //los password no son iguales
                MessageBox.Show("Confirmación de clave, incorrecta");
                iTXTPwd2.Text = "";
                iTXTPwd2.Focus();
                return;
            }
        }

        private void cboGrupo_Enter(object sender, EventArgs e)
        {
            cboGrupo.BackColor = Color.Yellow;
        }

        private void cboGrupo_Leave(object sender, EventArgs e)
        {
            cboGrupo.BackColor = Color.White;
        }


    }
}
