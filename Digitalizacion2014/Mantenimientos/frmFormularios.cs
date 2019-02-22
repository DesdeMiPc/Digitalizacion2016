using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Mantenimientos
{
    public partial class frmFormularios : Digitalizacion2014.frmBases.frmCatalogos
    {
        public string tipoFormulario = "1";
        private Clases.clsFormulario formularioSeleccionado;
        private Clases.clsCampo campoSelecionado;

        public frmFormularios()
        {
            InitializeComponent();
            splitContainer1.Panel1.Controls.Add(lvDatos);
            lvDatos.Dock = DockStyle.Fill;
        }

        private void frmFormularios_Load(object sender, EventArgs e)
        {
            if (tipoFormulario == "1")
            {
                chkAutomaticos.Visible = true;
            }

            base.CargarDatos("sp_ConfigFormularios", "1", "|V4=" + tipoFormulario);
        }

        private void lvDatos_DoubleClick(object sender, EventArgs e)
        {
            this.editRecord();
        }

        public override void newRecord()
        {
            base.newRecord();
            frmFormularioEdit frm = new frmFormularioEdit();
            frm.formulario = new Clases.clsFormulario("0");
            
            frm.formulario.tipoFormulario = Convert.ToInt16(this.tipoFormulario);
            frm.Text = "Nuevo " + (this.tipoFormulario == "1" ? "Expediente..." : "Documento...");
            frm.chkCodigoBarras.Visible = (this.tipoFormulario == "1" ? true : false);
            
            frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Refrecar el Objeto
                base.CargarDatos();
                foreach (ListViewItem ele in lvDatos.Items)
                {
                    if (ele.Text == frm.formulario.id.ToString())
                    {
                        ele.Selected = true;
                    }
                }
                lvDatos.Select();
            }
        }

        public override void editRecord()
        {
            base.editRecord();
            if (lvDatos.SelectedItems.Count > 0)
            {
                //Si tenemos un elemento Seleccionado
                frmFormularioEdit frm = new frmFormularioEdit();
                frm.formulario = new Clases.clsFormulario(lvDatos.FocusedItem.Text.ToString());
                frm.Text = "Edición de " + (this.tipoFormulario == "1" ? "Expediente -->" : "Documento -->") + frm.formulario.id.ToString();
                frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Refrecar el Objeto
                    base.CargarDatos();
                    foreach (ListViewItem ele in lvDatos.Items)
                    {
                        if (ele.Text == frm.formulario.id.ToString())
                        {
                            ele.Selected = true;
                        }
                    }
                    lvDatos.Select();
                }
            }
        }

        private void lvDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cambio el elemento seleccionado y se deberan de presentar los campos en el panel derecho
            if (lvDatos.SelectedItems.Count > 0)
            {
                splitContainer1.Panel2.Enabled = true;
                //Tenemos un elemento seleccionado
                this.formularioSeleccionado = new Clases.clsFormulario(lvDatos.SelectedItems[0].Text.ToString());
                actualizarCampos();
            }
            else
            {
                lvCampos.Items.Clear();
                splitContainer1.Panel2.Enabled = false;
            }
        }

        void actualizarCampos()
        {
            lvCampos.Items.Clear();
            lblExplicacion.Text = "";

            chkAutomaticos.Checked = this.formularioSeleccionado.camposAutomaticos;
            iTXTConsultaExterna.Text = this.formularioSeleccionado.consultaExterna.Replace("\n", Environment.NewLine); 

            foreach (Clases.clsFormularioCampo campo in formularioSeleccionado.campos)
            {
                ListViewItem nr = new ListViewItem(campo.idCampo);
                nr.SubItems.Add(campo.campo.descripcion.ToString());
                nr.SubItems.Add(campo.campo.tipoCampo.descripcion.ToString());

                lvCampos.Items.Add(nr);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (lvDatos.SelectedItems.Count > 0)
            {
                this.formularioSeleccionado = new Clases.clsFormulario(lvDatos.SelectedItems[0].Text.ToString());

                //Se tiene seleccionado un registro 
                frmAgregarCampo frmAddCampo = new frmAgregarCampo();
                if (frmAddCampo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Clases.clsFormularioCampo newCampo = new Clases.clsFormularioCampo(frmAddCampo.cboCampos.SelectedValue.ToString());
                    newCampo.idFormulario = formularioSeleccionado.id;
                    formularioSeleccionado.campos.Add(newCampo);
                    formularioSeleccionado.guardarDatos();
                    actualizarCampos();
                }
            }
        }

        private void lvCampos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCampos.SelectedItems.Count > 0)
            {
                //Se selecciono un campo
                campoSelecionado = new Clases.clsCampo(lvCampos.SelectedItems[0].Text.ToString());
                lblExplicacion.Text = campoSelecionado.explicacion.ToString();
            }
        }

        private void chkAutomaticos_CheckStateChanged(object sender, EventArgs e)
        {

            lvCampos.Visible = !((System.Windows.Forms.CheckBox)sender).Checked;
            btnAgregar.Visible = !((System.Windows.Forms.CheckBox)sender).Checked;
            btnEliminar.Visible = !((System.Windows.Forms.CheckBox)sender).Checked;
            gpoExplicacion.Visible = !((System.Windows.Forms.CheckBox)sender).Checked;
            iTXTConsultaExterna.Visible = ((System.Windows.Forms.CheckBox)sender).Checked;
            btnGuardarConsulta.Visible = ((System.Windows.Forms.CheckBox)sender).Checked;

            if (((System.Windows.Forms.CheckBox)sender).Checked)
            {
                iTXTConsultaExterna.Focus();
            }
        }

        private void btnGuardarConsulta_Click(object sender, EventArgs e)
        {
            this.formularioSeleccionado.consultaExterna = this.iTXTConsultaExterna.Text;
            formularioSeleccionado.guardarDatos();
        }
    }
}
