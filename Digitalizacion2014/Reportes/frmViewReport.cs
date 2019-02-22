using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Reportes
{
    public partial class frmViewReport : Form
    {
        public string idReporte;

        //Variable de Acceso a Datos
        WSD2014.cRetorno Datos = new WSD2014.cRetorno();
        WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();

        public string R_procedimiento = "SP_Reportes";
        public string R_validar = "";
        public string R_parametros = "";

        public string D_procedimiento = "SP_???";
        public string D_validar = "";
        public string D_parametros = "";

        public frmViewReport()
        {
            InitializeComponent();
        }

        private void frmViewReport_Load(object sender, EventArgs e)
        {
            Viewer.Toolbar.Images.Images.Add("myIcon", new Bitmap(Digitalizacion2014.Properties.Resources.IconPdf));
            int myIcon = Viewer.Toolbar.Images.Images.IndexOfKey("myIcon");

            DataDynamics.ActiveReports.Toolbar.Button PDFButton = new DataDynamics.ActiveReports.Toolbar.Button();
            PDFButton.Caption = "PDF";

            PDFButton.ButtonStyle = DataDynamics.ActiveReports.Toolbar.ButtonStyle.TextAndIcon;
            PDFButton.Id = 42;
            PDFButton.ImageIndex = myIcon;
            this.Viewer.Toolbar.Tools.Add(PDFButton);

            Viewer.Toolbar.Images.Images.Add("myIcon", new Bitmap(Digitalizacion2014.Properties.Resources.IconXLS));
            myIcon = Viewer.Toolbar.Images.Images.IndexOfKey("myIcon") + 1;

            DataDynamics.ActiveReports.Toolbar.Button EXCELButton = new DataDynamics.ActiveReports.Toolbar.Button();
            EXCELButton.Caption = "Excel";

            EXCELButton.ButtonStyle = DataDynamics.ActiveReports.Toolbar.ButtonStyle.TextAndIcon;
            EXCELButton.Id = 43;
            EXCELButton.ImageIndex = myIcon;
            this.Viewer.Toolbar.Tools.Add(EXCELButton);

            //Cambiar apariencia de Botones

            Viewer.Toolbar.Tools[0].ToolTip = "Tabla de Contenido";
            Viewer.Toolbar.Tools[0].Visible = false;
            Viewer.Toolbar.Tools[2].Caption = "Imprimir";
            Viewer.Toolbar.Tools[2].ToolTip = "Imprimir";
            Viewer.Toolbar.Tools[4].ToolTip = "Copiar";
            Viewer.Toolbar.Tools[6].ToolTip = "Buscar";
            Viewer.Toolbar.Tools[8].ToolTip = "Página";
            Viewer.Toolbar.Tools[9].ToolTip = "Multiples páginas";
            Viewer.Toolbar.Tools[12].ToolTip = "Alejar";
            Viewer.Toolbar.Tools[13].ToolTip = "Acercar";
            Viewer.Toolbar.Tools[14].ToolTip = "Valor de acercamiento";
            Viewer.Toolbar.Tools[16].ToolTip = "Página anterior";
            Viewer.Toolbar.Tools[17].ToolTip = "Página siguiente";
            Viewer.Toolbar.Tools[18].ToolTip = "Páginas";
            Viewer.Toolbar.Tools[20].ToolTip = "Regresar";
            Viewer.Toolbar.Tools[20].Caption = "Regresar";
            Viewer.Toolbar.Tools[21].ToolTip = "Avanzar";
            Viewer.Toolbar.Tools[21].Caption = "Avanzar";
            Viewer.Toolbar.Tools[23].ToolTip = "Anotaciones";
            Viewer.Toolbar.Tools[24].ToolTip = "Exportar a PDF";
            Viewer.Toolbar.Tools[25].ToolTip = "Exportar a EXCEL";

            if (idReporte == null)
            {
                return;
            }

            //Cargar el Reporte
            Datos = Acceso.ivkProcedimiento(R_procedimiento, R_validar, R_parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk)
            {
                MessageBox.Show("Problemas al cargar el reporte");
                return;
            }

            this.Text = Datos.ds.Tables[0].Rows[0]["CNombre"].ToString();

            byte[] rpxByte = Convert.FromBase64String(Datos.ds.Tables[0].Rows[0]["archivoRPX"].ToString());
            System.IO.MemoryStream rpxStream = new System.IO.MemoryStream(rpxByte);

            DataDynamics.ActiveReports.ActiveReport reporte = new DataDynamics.ActiveReports.ActiveReport();
            //reporte.AddAssembly(System.Reflection.Assembly.Load("System.Drawing.dll"));
            reporte.LoadLayout(rpxStream);

            //Cargar Datos
            Datos = Acceso.ivkProcedimiento(D_procedimiento, D_validar, D_parametros, Clases.vGlobales.conexion, null);
            if (!Datos.bOk)
            {
                MessageBox.Show("Problemas al cargar los datos");
                return;
            }

            reporte.DataSource = Datos.ds.Tables[0];

            Viewer.Document = reporte.Document;
            reporte.Run();
        }

        private void Viewer_ToolClick(object sender, DataDynamics.ActiveReports.Toolbar.ToolClickEventArgs e)
        {
            if (e.Tool.Id == 42)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Archivos PDF|*.pdf";
                dlg.Title = "Guardar reporte como ...";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DataDynamics.ActiveReports.Export.Pdf.PdfExport pdf = new DataDynamics.ActiveReports.Export.Pdf.PdfExport();
                    pdf.Export(this.Viewer.Document, dlg.FileName.ToString());
                }
            }
            if (e.Tool.Id == 43)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Archivos Excel|*.xls";
                dlg.Title = "Guardar reporte como ...";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DataDynamics.ActiveReports.Export.Xls.XlsExport xls = new DataDynamics.ActiveReports.Export.Xls.XlsExport();
                    xls.Export(this.Viewer.Document, dlg.FileName.ToString());
                }
            }
        }
    }
}
