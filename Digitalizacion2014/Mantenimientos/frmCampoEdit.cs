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
    public partial class frmCampoEdit : Form
    {
        //Objeto
        public Clases.clsCampo campo;

        //Variables para WebService
        private string procedimiento = "";
        private string parametros = "";
        private string validar = "";

        //Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();

        public frmCampoEdit()
        {
            InitializeComponent();
        }

        private void frmCampoEdit_Load(object sender, EventArgs e)
        {
            this.Size = new Size(420, 283);

            //Cargar Datos del ComboBox
            procedimiento = "sp_TiposCampos";
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
                this.cboTiposCampos.DataSource = Datos.ds.Tables[0];
            }

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

            this.Text = "Edición de Campo -- > " + (campo.id == "0" ? "Nuevo Campo" : campo.id.ToString());
            this.iTXTDescripcion.Text = campo.descripcion;
            this.iTXTExplicacion.Text = campo.explicacion;
            this.cboTiposCampos.SelectedValue = campo.tipoCampo.id;
            this.cboClasificacionCampos.SelectedValue = campo.clasificacion.id;
            this.numericUpDown1.Value = campo.longitud;

            this.iTXTDescripcion.Focus();
        }

        private void cboTiposCampos_Enter(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = Color.Yellow;
        }

        private void cboTiposCampos_Leave(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = Color.White;
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.BackColor = Color.Yellow;
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            numericUpDown1.BackColor = Color.White;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            campo.descripcion = iTXTDescripcion.Text;
            campo.explicacion = iTXTExplicacion.Text;
            campo.tipoCampo.id = cboTiposCampos.SelectedValue.ToString();
            campo.clasificacion.id = cboClasificacionCampos.SelectedValue.ToString();
            campo.longitud = Convert.ToInt16(numericUpDown1.Value);

            //Guardar Datos en el Server
            if (!this.campo.guardarDatos(Clases.vGlobales.id_User))
            {
                MessageBox.Show("Problemas al accesar la base de datos");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void cboTiposCampos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (((ComboBox)sender).Text == "Tabla" && this.campo.id != "0")
            if (this.campo.tipoCampo.descripcion == "Tabla" && this.campo.id != "0")
            {
                label4.Visible = false;
                numericUpDown1.Visible = false;
                btnEditTable.Visible = true;
            }
            else
            {
                //420,238
                label4.Visible = true;
                numericUpDown1.Visible = true;
                btnEditTable.Visible = false;
            }            
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            frmCamposTabla edicionCampo = new frmCamposTabla();
            edicionCampo.idCampo = this.campo.id;
            edicionCampo.Text = "Tabla del Campo --> " + this.campo.descripcion;
            edicionCampo.ShowDialog();
        }
    }
}
