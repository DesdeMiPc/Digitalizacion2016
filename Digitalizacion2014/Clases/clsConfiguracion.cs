using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public class clsConfiguracion
    {
        #region Configuracion de Scanner
        public clsScanner LoadScanner()
        {
            //Cargar desde el app.config todos los datos
            clsScanner Scanner = new clsScanner();

            try
            {
                Scanner.Description = ConfigurationManager.AppSettings["Scanner"];
                Scanner.DPI = ConfigurationManager.AppSettings["DPI"];
                Scanner.Depth = ConfigurationManager.AppSettings["Depth"];
                Scanner.Size = ConfigurationManager.AppSettings["Size"];
                Scanner.Duplex = Convert.ToBoolean(ConfigurationManager.AppSettings["Duplex"]);
            }
            catch
            {
                Scanner.Description = "";
            }
            return Scanner;
        }

        public void SaveScanner(clsScanner Scanner)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            try
            {
                
                config.AppSettings.Settings["Scanner"].Value = Scanner.Description;
                config.AppSettings.Settings["DPI"].Value = Scanner.DPI;
                config.AppSettings.Settings["Depth"].Value = Scanner.Depth;
                config.AppSettings.Settings["Size"].Value = Scanner.Size;
                config.AppSettings.Settings["Duplex"].Value = Scanner.Duplex.ToString();
            }
            catch
            {
                config.AppSettings.Settings.Add("Scanner", Scanner.Description);
                config.AppSettings.Settings.Add("DPI", Scanner.DPI);
                config.AppSettings.Settings.Add("Depth", Scanner.Depth);
                config.AppSettings.Settings.Add("Size", Scanner.Size);
                config.AppSettings.Settings.Add("Duplex", Scanner.Duplex.ToString());
            }

            config.Save(ConfigurationSaveMode.Full);
        }
        #endregion

        #region Digitalizar Documentos
        public string DD_Cantidad(string idNodo)
        {
            string Nodo = "DD_" + idNodo.Trim();
            try
            {
                return ConfigurationManager.AppSettings[Nodo].ToString();
            }
            catch
            {
                return null;
            }
        }

        public void DD_Cantidad_Guardar(System.Windows.Forms.ListView Doctos)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            foreach (System.Windows.Forms.ListViewItem e in Doctos.Items)
            {
                if (e.Checked)
                {
                    try
                    {
                        config.AppSettings.Settings["DD_"+e.Tag.ToString()].Value = e.SubItems[1].Text;
                    }
                    catch
                    {
                        config.AppSettings.Settings.Add("DD_"+e.Tag.ToString(), e.SubItems[1].Text);
                    }
                }
            }

            config.Save(ConfigurationSaveMode.Full);
        }
        #endregion
    }
}
