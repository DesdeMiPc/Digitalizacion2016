using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    class clsNodoExpediente : clsBase, IclsNodo
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "sp_Datos_Expedientes";
        string validar = "";
        string parametros = "";

        //Datos Locales
        string _idExpediente = "";

        //Implementación de Interface
        public string descripcion { get; set; }
        public clsEnums.TipoNodo tipo { get; set; }
        public int idFormulario { get; set; }
        public bool activo { get; set; }

        public string id
        {
            get { return this._idExpediente; }
            set
            {
                if (value != "*" || value != "0" || value != "")
                {
                    this._idExpediente = value;
                    //this.cargarDatos(_idExpediente);
                }
                else
                {
                    this._idExpediente = "0";
                }
            }
        }
    }
}
