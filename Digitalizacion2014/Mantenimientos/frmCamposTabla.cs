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
    public partial class frmCamposTabla : Digitalizacion2014.frmBases.frmCatalogos
    {
        public string idCampo = "";

        public frmCamposTabla()
        {
            InitializeComponent();
        }

        private void frmCamposTabla_Load(object sender, EventArgs e)
        {
            base.CargarDatos("sp_ConfigCamposTabla", "1", "|V2=" + this.idCampo + "|");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmCamposTablaEdit frm = new frmCamposTablaEdit();
            frm.CampoTabla = new Clases.clsCampoTabla(idCampo,"0");
            frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Refrecar el Objeto
                base.CargarDatos();
                foreach (ListViewItem ele in lvDatos.Items)
                {
                    if (ele.Text == frm.CampoTabla.id.ToString())
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
                frmCamposTablaEdit frm = new frmCamposTablaEdit();
                frm.CampoTabla = new Clases.clsCampoTabla(idCampo, lvDatos.FocusedItem.Text.ToString());
                frm.Location = new Point(this.Location.X + 25, this.Location.Y + frm.Height - 15);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Refrecar el Objeto
                    base.CargarDatos();
                    foreach (ListViewItem ele in lvDatos.Items)
                    {
                        if (ele.Text == frm.CampoTabla.id.ToString())
                        {
                            ele.Selected = true;
                        }
                    }
                    lvDatos.Select();
                }
            }
        }

        private void lvDatos_DoubleClick(object sender, EventArgs e)
        {
            this.editRecord();
        }
    }
}
