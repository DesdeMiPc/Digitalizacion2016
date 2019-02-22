using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataDynamics.ActiveReports.Design;

namespace Digitalizacion2014.Reportes
{
    public partial class frmReportDesign : Form
    {

        string idReporte = "";
        string cNombre = "";
        string cDescripcion = "";
        string idCategoria = "";

        public frmReportDesign()
        {
            InitializeComponent();
            //Create new report instance and assign to Report Explorer
            //Note:  Assigning the ToolBox to the designer before calling NewReport
            // will automaticly add the default controls to the toolbox in a group called
            // "ActiveReports 6"
            arDesigner.Toolbox = toolbox1;
            arReportExplorer.ReportDesigner = arDesigner;

            // Add Menu and CommandBar to Form
            ToolStrip menuStrip = arDesigner.CreateToolStrips(DesignerToolStrips.Menu)[0];

            ToolStripDropDownItem fileMenu = (ToolStripDropDownItem)menuStrip.Items[0];
            fileMenu.Text = "&Archivo";

            ToolStripDropDownItem editMenu = (ToolStripDropDownItem)menuStrip.Items[1];
            editMenu.Text = "&Editar";

            foreach (ToolStripItem opc in editMenu.DropDownItems)
            {
                switch (opc.Text)
                {
                    case "&Undo": opc.Text = "&Deshacer"; break;
                    case "&Redo": opc.Text = "&Rehacer"; break;
                    case "Cu&t": opc.Text = "Co&rtar"; break;
                    case "&Copy": opc.Text = "&Copiar"; break;
                    case "&Paste": opc.Text = "&Pegar"; break;
                    case "&Delete": opc.Text = "B&orrar"; break;
                    case "Select &All": opc.Text = "Seleccionar &Todo"; break;
                }
            }

            foreach (ToolStripItem opc in fileMenu.DropDownItems)
            {
                switch (opc.Text)
                {
                    case "&New": opc.Text = "&Nuevo"; opc.Click += click_Nuevo; break;
                    case "&Open": opc.Text = "A&brir Archivo"; break;
                    case "&Save": opc.Text = "&Guardar Archivo"; break;
                    case "&Export": opc.Text = "&Exportar"; break;
                }
            }

            // Add a Export to RDF and Exit command to the File menu
            fileMenu.DropDownItems.Add(new ToolStripButton("Exportar a RDF...", null, DoExportRdf));
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(new ToolStripButton("Conecta&r WS", null, WS_Click));
            fileMenu.DropDownItems.Add(new ToolStripButton("Abrir &desde WS", null, WS_Open_Click));
            fileMenu.DropDownItems.Add(new ToolStripButton("Guardar en &WS", null, WS_Save_Click));
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(new ToolStripButton("&Salir", null, OnExit));

            AppendToolStrips(0, new ToolStrip[] { menuStrip });
            AppendToolStrips(1, arDesigner.CreateToolStrips(
                DesignerToolStrips.Report,
                DesignerToolStrips.Edit,
                DesignerToolStrips.Undo,
                DesignerToolStrips.Zoom));

            AppendToolStrips(2, arDesigner.CreateToolStrips(
                DesignerToolStrips.Format, DesignerToolStrips.Layout));

            //Fill Toolbox
            LoadTools(this.toolbox1);
            // Activate default group on the toolbox
            this.toolbox1.SelectedCategory = "ActiveReports 6";

        }

        private void click_Nuevo(object sender, EventArgs e)
        {
            idReporte = "";
            cNombre = "";
            cDescripcion = "";
            idCategoria = "";
        }

        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        private void WS_Open_Click(object sender, EventArgs e)
        {
            //Abrir desde el WebService el Reporte
            frmCargar frm = new frmCargar();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                idReporte = frm.idReporte;
                idCategoria = frm.idCategoria;
                cNombre = frm.cNombre;
                cDescripcion = frm.cDescripcion;

                byte[] rpxByte = Convert.FromBase64String(frm.rpxBase64);
                System.IO.MemoryStream rpxStream = new System.IO.MemoryStream(rpxByte);

                arDesigner.LoadReport(rpxStream);
                arDesigner.Refresh();
            }
        }

        private void WS_Save_Click(object sender, EventArgs e)
        {
            //Pasar Reporte a MemoryStream
            System.IO.MemoryStream rpxStream = new System.IO.MemoryStream();
            arDesigner.SaveReport(rpxStream);
            byte[] rpxByte = rpxStream.ToArray();
            string rpxBase64 = Convert.ToBase64String(rpxByte);

            //Guardar en el WebService el Reporte
            frmGuardar frm = new frmGuardar();
            frm.rpxBase64 = rpxBase64;
            frm.idReporte = idReporte;
            frm.cDescripcion = cDescripcion;
            frm.cNombre = cNombre;

            if (idReporte == "")
            {
                frm.chkNuevo.Checked = true;
            }
            else
            {
                frm.chkNuevo.Checked = false;
            }

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                idReporte = frm.idReporte;
                cNombre = frm.cNombre;
                cDescripcion = frm.cDescripcion;
            }

        }

        private void WS_Click(object sender, EventArgs e)
        {
            //Conectar al WebService


            //System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.AppSettings.Settings.Remove("WS");
            //config.AppSettings.Settings.Add("WS", "http://localhost/");
            //config.Save(ConfigurationSaveMode.Modified, true);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        private void DoExportRdf(object sender, EventArgs e)
        {
            SaveFileDialog saveRdfDialog = new SaveFileDialog();
            saveRdfDialog.Filter = "RDF files (*.rdf)|*.rdf|All files (*.*)|*.*";
            if (saveRdfDialog.ShowDialog() == DialogResult.OK)
            {
                arDesigner.Report.Run(false);
                arDesigner.Report.Document.Save(saveRdfDialog.FileName);
            }
        }

        private void AppendToolStrips(int row, IList<ToolStrip> toolStrips)
        {
            ToolStripPanel panel = toolStripContainer1.TopToolStripPanel;
            for (int i = toolStrips.Count; --i >= 0; )
            {
                panel.Join(toolStrips[i], row);
            }
        }

        private static void LoadTools(IToolboxService toolbox)
        {
            //Add Data Providers
            foreach (Type type in new Type[]
				{
					typeof (System.Data.DataSet),
					typeof (System.Data.DataView),
					typeof (System.Data.OleDb.OleDbConnection),
					typeof (System.Data.OleDb.OleDbDataAdapter),
					typeof (System.Data.Odbc.OdbcConnection),
					typeof (System.Data.Odbc.OdbcDataAdapter),
					typeof (System.Data.SqlClient.SqlConnection),
					typeof (System.Data.SqlClient.SqlDataAdapter)
				})
            {
                toolbox.AddToolboxItem(new ToolboxItem(type), "Data");
            }
        }
    }
}
