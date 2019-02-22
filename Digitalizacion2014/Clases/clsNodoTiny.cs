using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public class clsNodoTiny 
    {
        public string idNodo { get; set; }
        public clsEnums.TipoNodo tipoNodo { get; set; }

        //Derechos Actuales sonre el Nodo
        public bool Read { get; set; }
        public bool Print { get; set; }
        public bool Export { get; set; }
        public bool Add { get; set; }
        public bool Modify { get; set; }
        public bool Delete { get; set; }

        public clsNodoTiny(string id, clsEnums.TipoNodo tipo)
        {
            this.idNodo = id;
            this.tipoNodo = tipo;
        }

        public clsNodoTiny(string id)
        {
            this.idNodo = id;
            this.tipoNodo = clsEnums.TipoNodo.Carpeta;
        }
    }
}
