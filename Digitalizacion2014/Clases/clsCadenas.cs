using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public class clsCadenas
    {
        public static String obtenerValor(string campo, string cadena, string separador, Boolean mayusculas = false)
        {
            int nLong;
            int nPos_Campo;
            int nPos_Separador;
            string sRetorno = "";

            campo = campo.ToUpper();

            try
            {
                if (mayusculas == true)
                {
                    cadena = cadena.ToUpper();
                }

                nLong = campo.Length + 1;
                nPos_Campo = cadena.IndexOf(campo + "=");

                if (nPos_Campo > -1)
                {
                    nPos_Separador = cadena.IndexOf(separador, nPos_Campo + 1);
                    sRetorno = cadena.Substring(nPos_Campo + nLong, nPos_Separador - (nPos_Campo + nLong));
                }
            }
            catch
            {
                sRetorno = "";
            }

            return sRetorno;
        }
    }
}
