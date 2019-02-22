using System;
namespace Digitalizacion2014.Clases
{
    public interface IclsNodo
    {
        bool activo { get; set; }
        string descripcion { get; set; }
        string id { get; set; }
        int idFormulario { get; set; }
        clsEnums.TipoNodo tipo { get; set; }
    }
}
