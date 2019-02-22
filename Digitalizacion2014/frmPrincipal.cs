using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Digitalizacion2014
{
    public partial class frmPrincipal : RibbonForm
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void cmdGrupos_Click(object sender, EventArgs e)
        {
            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Mantenimientos.frmGrupos))
                {
                    ventana.Activate();
                    return;
                }
            }
            Mantenimientos.frmGrupos miventana = new Mantenimientos.frmGrupos();
            miventana.MdiParent = this;
            miventana.Show();
        }

        private void cmdUsuarios_Click(object sender, EventArgs e)
        {
            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Mantenimientos.frmUsuarios))
                {
                    ventana.Activate();
                    return;
                }
            }
            Mantenimientos.frmUsuarios miventana = new Mantenimientos.frmUsuarios();
            miventana.MdiParent = this;
            miventana.Show();
        }

        private void cmdClasificacion_Click(object sender, EventArgs e)
        {
            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Mantenimientos.frmCamposClasificacion))
                {
                    ventana.Activate();
                    return;
                }
            }
            Mantenimientos.frmCamposClasificacion miventana = new Mantenimientos.frmCamposClasificacion();
            miventana.MdiParent = this;
            miventana.Show();
        }

        private void cmdConfigDatos_Click(object sender, EventArgs e)
        {
            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Mantenimientos.frmCampos))
                {
                    ventana.Activate();
                    return;
                }
            }
            Mantenimientos.frmCampos miventana = new Mantenimientos.frmCampos();
            miventana.MdiParent = this;
            miventana.Show();
        }

        public void EnableTab(string TabName)
        {
            foreach( RibbonTab tab in rbMenu.Tabs)
            {
                if(tab.Text == TabName)
                {
                    tab.Visible = true;
                    rbMenu.ActiveTab = tab;
                }
            }
        }

        public void DisableTab(string TabName)
        {
            foreach (RibbonTab tab in rbMenu.Tabs)
            {
                if (tab.Text == TabName)
                {
                    tab.Visible = false;
                    rbMenu.ActiveTab = rbMenu.Tabs[0];
                }
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            this.DisableTab("Acciones");
            this.SSLUsuario.Text = "Usuario : " + Clases.vGlobales.id_User;
            this.SSLDataBase.Text = "Base de Datos : " + Clases.vGlobales.conexion;
            this.Text = this.Text + " - " + Clases.vGlobales.conexion;
        }

        private void cmdReportDesig_Click(object sender, EventArgs e)
        {
            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Reportes.frmReportDesign))
                {
                    ventana.Activate();
                    return;
                }
            }
            Reportes.frmReportDesign miventana = new Reportes.frmReportDesign();
            miventana.MdiParent = this;
            miventana.Show();
        }

        private void cmdAgregar_Click(object sender, EventArgs e)
        {
            ((frmBases.frmGeneral01)this.ActiveMdiChild).newRecord();
        }

        private void cmdEditar_Click(object sender, EventArgs e)
        {
            ((frmBases.frmGeneral01)this.ActiveMdiChild).editRecord();
        }

        private void cmdEliminar_Click(object sender, EventArgs e)
        {
            ((frmBases.frmGeneral01)this.ActiveMdiChild).deleteRecord();
        }

        private void cmdExpedientes_Click(object sender, EventArgs e)
        {

            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Mantenimientos.frmFormularios) && ventana.Text == "Catalogo de Expedientes")
                {
                    ventana.Activate();
                    return;
                }
            }
            Mantenimientos.frmFormularios miventana = new Mantenimientos.frmFormularios();
            miventana.tipoFormulario = "1";
            miventana.Text = "Catalogo de Expedientes";
            miventana.MdiParent = this;
            miventana.BackColor = Color.LightYellow;
            miventana.Show();
        }

        private void cmdDocumentos_Click(object sender, EventArgs e)
        {
            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Mantenimientos.frmFormularios) && ventana.Text == "Catalogo de Documentos")
                {
                    ventana.Activate();
                    return;
                }
            }
            Mantenimientos.frmFormularios miventana = new Mantenimientos.frmFormularios();
            miventana.tipoFormulario = "2";
            miventana.Text = "Catalogo de Documentos";
            miventana.MdiParent = this;
            miventana.BackColor = Color.LightCyan;
            miventana.Show();
        }

        private void cmdArchivo_Click(object sender, EventArgs e)
        {
            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Configuracion.frmConfigArbol))
                {
                    ventana.Activate();
                    return;
                }
            }

            Configuracion.frmConfigArbol miventana = new Configuracion.frmConfigArbol();
            miventana.MdiParent = this;
            miventana.Show();
        }

        private void btnImagenes_Click(object sender, EventArgs e)
        {

        }

        private void btnCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void btnVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void btnHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void bntArchivoGeneral_Click(object sender, EventArgs e)
        {
            // Assembly asm = Assembly.GetEntryAssembly();
            // Type formtype = asm.GetType("Digitalizacion2014.Procesos.frmArchivoGeneral");

            foreach (Form ventana in this.MdiChildren)
            {
                //Verificar que no exista otra ventana
                if (ventana.GetType() == typeof(Procesos.frmArchivoGeneral))// typeof(Procesos.frmArchivoGeneral))
                {
                    ventana.Activate();
                    return;
                }
            }

            Procesos.frmArchivoGeneral miventana = new Procesos.frmArchivoGeneral();
            //Procesos.frmArchivoGeneral miventana = new Procesos.frmArchivoGeneral();
            miventana.MdiParent = this;
            miventana.PBGeneral = this.TSPBGeneral;
            miventana.Show();
        }
    }
}
