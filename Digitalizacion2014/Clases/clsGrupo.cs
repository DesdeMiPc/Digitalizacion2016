using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Digitalizacion2014.Clases
{
    public class clsGrupo : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "SP_Grupos";
        string validar = "";
        string parametros = "";

        //Elementos del Objeto
        string _id = "";
        public string descripcion { get; set;}
        public bool activo { get; set;}

        public string id
        {
            get { return this._id; }
            set
            {
                if (value != "*" || value != "0" || value != "")
                {
                    this._id = value;
                    this.cargarDatos(_id);
                }
                else
                {
                    this._id = "0";
                }
            }
        }

        public clsGrupo()
        {
            cargarDatos("*");
        }

        public clsGrupo(string idGrupo)
        {
            cargarDatos(idGrupo);
        }

        public clsGrupo(DataRow registro)
        {
            cargarDatos(registro);
        }

        void cargarDatos(string idGrupo)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idGrupo == "*" || idGrupo == "0" || idGrupo == "")
            {
                //Iniciar todas la variables
                this._id = "0";
                this.descripcion = "";
                this.activo = true;
            }
            else
            {
                //Cargar los Datos desde la Base de Datos
                this.validar = "2";
                this.parametros = "|V1=" + idGrupo.ToString() + "|";
                regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
                if (regreso.bOk)
                {
                    //Regreso el Dato de Manera Correcta
                    cargarDatos(regreso.ds.Tables[0].Rows[0]);
                }
                else
                {
                    throw new System.InvalidOperationException("Error al Cargar Datos \r\nNo Existe un registro en la Base de Datos");
                }
            }
        }

        void cargarDatos(DataRow r)
        {
            try
            {
                this._id = r["id_Grupo"].ToString();
                this.descripcion= r["Descripcion"].ToString();
                this.activo = Convert.ToBoolean(r["bActivo"]);

                //Datos Comunes de Registros
                this.CargarDatosRegistro(r);
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error al Cargar Datos \r\n" + ex.Message);
            }
        }

        public bool guardarDatos(string user = "Default")
        {
            this.validar = "3";
            this.parametros = "|V1=" + this._id.ToString() + "|V2=" + this.descripcion.ToString() + 
                              "|V3=" + Convert.ToByte(this.activo) +
                              "|C1=" + user.ToString() +
                              "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString() + "|";

            //Envio de Datos y Lectura de Resultados
            regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                cargarDatos(regreso.ds.Tables[0].Rows[0]);
                return true;
            }
            else
            {
                throw new System.InvalidOperationException("Error al intentar guardar Datos \r\nRevisar acceso a Base de Datos");
            }
        }

        public bool desactivar(string user = "Default")
        {
            this.validar = "4";
            this.parametros = "V1=" + this._id.ToString() + "|" +
                              "|C1=" + user.ToString() +
                              "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString() + "|";

            regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                cargarDatos(regreso.ds.Tables[0].Rows[0]);
                return true;
            }
            else
            {
                throw new System.InvalidOperationException("Error al intentar desactivar el Registro \r\nRevisar acceso a Base de Datos");
            }
        }
    }
}
