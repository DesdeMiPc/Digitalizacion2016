using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Digitalizacion2014
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //Application.Run(new Procesos.frmLectorPDF());

            frmLogin login = new frmLogin();

            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new frmPrincipal());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
