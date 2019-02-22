using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Digitalizacion2014.Clases
{
    public class clsBase
    {
        public ClsDatosComunesRegistro datosRegistro { get; set; }
        public ClsDatosComunesRegistro datosModificacion { get; set; }
        public ClsDatosComunesRegistro datosEliminacion { get; set; }

        public void CargarDatosRegistro(DataRow r)
        {
            this.datosRegistro.fecha = r["dFecha_Registro"] == System.DBNull.Value ? DateTime.Now : Convert.ToDateTime(r["dFecha_Registro"]);
            this.datosRegistro.maquina = r["cMaquina_Registro"] == System.DBNull.Value ? "" : r["cMaquina_Registro"].ToString();
            this.datosRegistro.usuario = r["cUsuario_Registro"] == System.DBNull.Value ? "" : r["cUsuario_Registro"].ToString();

            this.datosModificacion.fecha = r["cMaquina_UltimaModificacion"] == System.DBNull.Value ? DateTime.Now : Convert.ToDateTime(r["dFecha_UltimaModificacion"]);
            this.datosModificacion.maquina = r["cMaquina_UltimaModificacion"] == System.DBNull.Value ? "" : r["cMaquina_UltimaModificacion"].ToString();
            this.datosModificacion.usuario = r["cUsuario_UltimaModificacion"] == System.DBNull.Value ? "" : r["cUsuario_UltimaModificacion"].ToString();

            this.datosEliminacion.fecha = r["cMaquina_Eliminacion"] == System.DBNull.Value ? DateTime.Now : Convert.ToDateTime(r["dFecha_Eliminacion"]);
            this.datosEliminacion.maquina = r["cMaquina_Eliminacion"] == System.DBNull.Value ? "" : r["cMaquina_Eliminacion"].ToString();
            this.datosEliminacion.usuario = r["cUsuario_Eliminacion"] == System.DBNull.Value ? "" : r["cUsuario_Eliminacion"].ToString();
        }
    }
}
