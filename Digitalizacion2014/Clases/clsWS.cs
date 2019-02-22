using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public class clsWS
    {
        WSD2014.WSDatosSoapClient _Acceso = new WSD2014.WSDatosSoapClient();

        public WSD2014.cRetorno ivkProcedimiento(string procedimiento, string opcion, string DB, string parametros)
        {
            return _Acceso.ivkProcedimiento(procedimiento, opcion, parametros, DB, null);
        }
    }
}
