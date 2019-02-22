using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    interface i_clsNodo
    {
        /// <summary>
        /// Obtiene el número de nodo de la base de datos
        /// </summary>
        Int16 idNodo { get; }

        /// <summary>
        /// Descripcion del Nodo
        /// </summary>
        String Descripcion { get; }

        /// <summary>
        /// Tipo de Nodo
        /// </summary>
        Byte Tipo { get; }

        /// <summary>
        /// Carga de Propiedades
        /// </summary>
        /// 

    }
}
