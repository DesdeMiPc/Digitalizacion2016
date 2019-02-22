using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Mantenimientos
{
    public partial class frmUsuarios : Digitalizacion2014.frmBases.frmCatalogos
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            base.CargarDatos("sp_Usuario", "1", "");
        }

        private void lvDatos_DoubleClick(object sender, EventArgs e)
        {
            this.editRecord();
        }

        public override void newRecord()
        {
            base.newRecord();
            frmUsuarioEdit frm = new frmUsuarioEdit();
            frm.usuario = new Clases.clsUsuario("0");
            frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Refrecar el Objeto
                base.CargarDatos();
                foreach(ListViewItem ele in lvDatos.Items)
                {
                    if (ele.Text == frm.usuario.id.ToString())
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
                frmUsuarioEdit frm = new frmUsuarioEdit();
                frm.usuario = new Clases.clsUsuario(lvDatos.FocusedItem.Text.ToString());
                frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Refrecar el Objeto
                    base.CargarDatos();
                    foreach (ListViewItem ele in lvDatos.Items)
                    {
                        if (ele.Text == frm.usuario.id.ToString())
                        {
                            ele.Selected = true;
                        }
                    }
                    lvDatos.Select();
                }
            }
        }

        public override void deleteRecord()
        {
            base.deleteRecord();
            if (MessageBox.Show("Desea desactivar el Registro Actual","Alerta",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //Cargar el Registro Actual
                Clases.clsUsuario registro = new Clases.clsUsuario(lvDatos.FocusedItem.Text.ToString());
                try
                {
                    if (registro.desactivar())
                    {
                        MessageBox.Show("Registro Desactivado");
                        //Refrecar el Objeto
                        base.CargarDatos();
                        foreach (ListViewItem ele in lvDatos.Items)
                        {
                            if (ele.Text == registro.id.ToString())
                            {
                                ele.Selected = true;
                            }
                        }
                        lvDatos.Select();
                    }
                }
                catch
                {
                    MessageBox.Show("Problemas al accesar los datos");
                }
            }
        }
    }
}
