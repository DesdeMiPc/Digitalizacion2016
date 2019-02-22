using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Mantenimientos
{
    public partial class frmCampos : Digitalizacion2014.frmBases.frmCatalogos
    {

        public frmCampos()
        {
            InitializeComponent();
        }

        private void frmCampos_Load(object sender, EventArgs e)
        {
            base.CargarDatos("sp_ConfigCampos", "1", "");
        }

        private void lvDatos_DoubleClick(object sender, EventArgs e)
        {
            this.editRecord();
        }

        public override void newRecord()
        {
            base.newRecord();
            frmCampoEdit frm = new frmCampoEdit();
            frm.campo = new Clases.clsCampo("0");
            frm.Location = new Point(this.Location.X + 15, this.Location.Y + frm.Height - 15);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Refrecar el Objeto
                base.CargarDatos();
                foreach (ListViewItem ele in lvDatos.Items)
                {
                    if (ele.Text == frm.campo.id.ToString())
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
            frmCampoEdit frm = new frmCampoEdit();
            frm.campo = new Clases.clsCampo(lvDatos.FocusedItem.Text.ToString());
            frm.Location = new Point(this.Location.X + 15, this.Location.Y + frm.Height - 15);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Refrecar el Objeto
                base.CargarDatos();
                foreach (ListViewItem ele in lvDatos.Items)
                {
                    if (ele.Text == frm.campo.id.ToString())
                    {
                        ele.Selected = true;
                    }
                }
                lvDatos.Select();
            }
        }

        public override void deleteRecord()
        {
            base.deleteRecord();
        }
    }
}
