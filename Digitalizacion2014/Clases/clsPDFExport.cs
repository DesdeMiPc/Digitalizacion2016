using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using Digitalizacion2014.Scan.Images;
using System.IO;
using System.Drawing;

namespace Digitalizacion2014.Clases
{
    public class clsPDFExport
    {
        private const string Autor = "Junta Municipal de Agua Potable y Alcantarillado de Culiacán";
        private const string Subject = "Sistema de Digitalización de Documentos V 2.0";
        private const string Creador = "Digitalización 2016, v 2.0";

        public bool Exportar(string archivo, List<IScannedImage> images)
        {

            Controles.frmGauge gauge = new Controles.frmGauge();
            gauge.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            gauge.pb.Minimum = 0;
            gauge.pb.Maximum = images.Count - 1;
            gauge.pb.Value = 0;
            gauge.Show();

            var documento = new PdfDocument { PageLayout = PdfPageLayout.SinglePage };

            documento.Info.Author = Autor;
            documento.Info.Subject = Subject;
            documento.Info.Creator = Creador;


            foreach (IScannedImage scannedImage in images)
            {
                

                using (Stream stream = scannedImage.GetImageStream())
                using (var img = new Bitmap(stream))
                {
                    float hAdjust = 72 / img.HorizontalResolution;
                    float vAdjust = 72 / img.VerticalResolution;
                    double realWidth = img.Width * hAdjust;
                    double realHeight = img.Height * vAdjust;
                    PdfPage newPage = documento.AddPage();
                    newPage.Width = (int)realWidth;
                    newPage.Height = (int)realHeight;
                    XGraphics gfx = XGraphics.FromPdfPage(newPage);
                    
                    MemoryStream memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    //gfx.DrawImage(XImage.FromStream(memoryStream), 0, 0, (int)realWidth, (int)realHeight);
                    gfx.DrawImage(XImage.FromStream(memoryStream), (double)0, (double)0, (double)realWidth, (double)realHeight);

                    //gfx.DrawImage(img, 0, 0, (int)realWidth, (int)realHeight);

                    gauge.pb.PerformStep();
                    gauge.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            documento.Save(archivo);
            gauge.Close();
            gauge.Dispose();
            return true;
        }

        public bool Exportar(string archivo, DataTable images)
        {
            Controles.frmGauge gauge = new Controles.frmGauge();
            gauge.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            gauge.pb.Minimum = 0;
            gauge.pb.Maximum = images.Rows.Count - 1;
            gauge.pb.Value = 0;
            gauge.Show();


            var documento = new PdfDocument { PageLayout = PdfPageLayout.SinglePage };

            documento.Info.Author = Autor;
            documento.Info.Subject = Subject;
            documento.Info.Creator = Creador;


            foreach (DataRow scannedImage in images.Rows)
            {
                using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(scannedImage["Imagen"].ToString())))
                using (Image image = Image.FromStream(stream))
                using (var img = new Bitmap(image))
                {
                    float hAdjust = 72 / img.HorizontalResolution;
                    float vAdjust = 72 / img.VerticalResolution;
                    double realWidth = img.Width * hAdjust;
                    double realHeight = img.Height * vAdjust;
                    PdfPage newPage = documento.AddPage();
                    newPage.Width = (int)realWidth;
                    newPage.Height = (int)realHeight;
                    XGraphics gfx = XGraphics.FromPdfPage(newPage);

                    //gfx.DrawImage(img, 0, 0, (int)realWidth, (int)realHeight);
                    MemoryStream memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    //gfx.DrawImage(XImage.FromStream(memoryStream), 0, 0, (int)realWidth, (int)realHeight);
                    gfx.DrawImage(XImage.FromStream(memoryStream), (double)0, (double)0, (double)realWidth, (double)realHeight);

                    gauge.pb.PerformStep();
                    gauge.BringToFront();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            documento.Save(archivo);
            gauge.Close();
            gauge.Dispose();
            return true;
        }

        public bool Exportar(string archivo, string idExpediente)
        {
            Controles.frmGauge gauge = new Controles.frmGauge();
            gauge.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            gauge.pb.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            gauge.Show();
            gauge.BringToFront();

            var documento = new PdfDocument { PageLayout = PdfPageLayout.SinglePage };
            documento.Info.Author = Autor;
            documento.Info.Subject = Subject;
            documento.Info.Creator = Creador;
            documento.Info.Title = "Poliza JAPAC - " + idExpediente;

            // Opciones de Seguridad
            documento.SecuritySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted128Bit;
            //documento.SecuritySettings.UserPassword = "japac2019$";
            documento.SecuritySettings.OwnerPassword = "JAPAC2019$";

            documento.SecuritySettings.PermitExtractContent = false;
            documento.SecuritySettings.PermitAccessibilityExtractContent = false;
            documento.SecuritySettings.PermitAnnotations = false;
            documento.SecuritySettings.PermitAssembleDocument = false;
            documento.SecuritySettings.PermitFormsFill = false;
            documento.SecuritySettings.PermitFullQualityPrint = false;
            documento.SecuritySettings.PermitModifyDocument = false;
            documento.SecuritySettings.PermitPrint = false;

            string parametros;
            string validar;
            WSD2014.cRetorno Datos;
            WSD2014.WSDatosSoapClient Acceso = new WSD2014.WSDatosSoapClient();
            int pagina = 1;

            try
            {
                while (true)
                {
                    parametros = "|V2=" + idExpediente + "|PA=" + pagina.ToString() + "|RP=25|";
                    validar = "411";
                    Datos = new WSD2014.cRetorno();
                    Datos = Acceso.ivkProcedimiento("sp_Datos_Documentos", validar, parametros, Clases.vGlobales.conexion, null);

                    if (Datos.ds.Tables[0].Rows.Count == 0) break;

                    foreach (DataRow scannedImage in Datos.ds.Tables[0].Rows)
                    {
                        double r = Convert.ToDouble(scannedImage["resolucion"].ToString());

                        using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(scannedImage["Imagen"].ToString())))
                        using (Image image = Image.FromStream(stream))
                        using (var img = new Bitmap(image, new Size((int)(image.Size.Width * r), (int)(image.Size.Height * r))))
                        {
                            float hAdjust = 72 / img.HorizontalResolution;
                            float vAdjust = 72 / img.VerticalResolution;
                            double realWidth = img.Width * hAdjust;
                            double realHeight = img.Height * vAdjust;

                            PdfPage newPage = documento.AddPage();
                            newPage.Width = (int)realWidth;
                            newPage.Height = (int)realHeight;
                            XGraphics gfx = XGraphics.FromPdfPage(newPage);

                            MemoryStream memoryStream = new MemoryStream();
                            img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                            //gfx.DrawImage(XImage.FromStream(memoryStream), 0, 0, (int)realWidth, (int)realHeight);
                            gfx.DrawImage(XImage.FromStream(memoryStream), (double)0, (double)0, (double)realWidth, (double)realHeight);

                            memoryStream.Dispose();

                            gauge.BringToFront();

                            System.Windows.Forms.Application.DoEvents();

                        }
                    }
                    pagina++;
                    Datos = null;
                    GC.Collect();
                }
            }
            catch (OutOfMemoryException e)
            {
                string m = e.Message;
            }
            finally {
                if (documento.Pages.Count > 0) documento.Save(archivo);
            }
            
            gauge.Close();
            gauge.Dispose();
            return true;
        }

    }
}
