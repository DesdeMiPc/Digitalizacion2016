using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Mantenimientos
{
    public partial class frmCamposClasificacion : Digitalizacion2014.frmBases.frmCatalogos
    {
        public frmCamposClasificacion()
        {
            InitializeComponent();
        }

        private void frmCamposClasificacion_Load(object sender, EventArgs e)
        {
            base.CargarDatos("sp_ClasificacionCampos", "1", "");
        }

        private void lvDatos_DoubleClick(object sender, EventArgs e)
        {
            this.editRecord();
        }

        public override void newRecord()
        {
            base.newRecord();
            frmCamposClasificacionEdit frm = new frmCamposClasificacionEdit();
            frm.clasificacionCampos = new Clases.clsClasificacionCampos("0");
            frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Refrecar el Objeto
                base.CargarDatos();
                foreach (ListViewItem ele in lvDatos.Items)
                {
                    if (ele.Text == frm.clasificacionCampos.id.ToString())
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
                //Tenemos un elemento Seleccionado
                frmCamposClasificacionEdit frm = new frmCamposClasificacionEdit();
                frm.clasificacionCampos = new Clases.clsClasificacionCampos(lvDatos.FocusedItem.Text.ToString());
                frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Refrecar el Objeto
                    base.CargarDatos();
                    foreach (ListViewItem ele in lvDatos.Items)
                    {
                        if (ele.Text == frm.clasificacionCampos.id.ToString())
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
        }
    }
}
