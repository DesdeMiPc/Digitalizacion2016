using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Digitalizacion2014.Clases
{
    public class clsCampoTabla : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "sp_ConfigCamposTabla";
        string validar = "";
        string parametros = "";

        //Elementos del Objeto
        string _id = "";
        public string descripcion { get; set; }
        public string idCampo { get; set; }

        public string id
        {
            get { return this._id; }
            set
            {
                if (value != "*" || value != "0" || value != "")
                {
                    this._id = value;
                    this.cargarDatos(idCampo, _id);
                }
                else
                {
                    this._id = "0";
                }
            }
        }

        public clsCampoTabla(string idCampo, string idCampoTabla)
        {
            cargarDatos(idCampo, idCampoTabla);
        }

        public clsCampoTabla(DataRow registro)
        {
            cargarDatos(registro);
        }

        void cargarDatos(string idCampo, string idCampoTabla)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idCampoTabla == "*" || idCampoTabla == "0" || idCampoTabla == "")
            {
                //Iniciar todas la variables
                this._id = "0";
                this.descripcion = "";
                this.idCampo = idCampo;
            }
            else
            {
                //Cargar los Datos desde la Base de Datos
                this.validar = "2";
                this.parametros = "|V1=" + idCampoTabla.ToString() + "|";
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
                this._id = r["idUnico"].ToString();
                this.idCampo = r["idCampo"].ToString();
                this.descripcion = r["Valor"].ToString();

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
            this.validar = "4";
            this.parametros = "|V1=" + this._id.ToString() + "|V2=" + this.idCampo.ToString() +
                              "|V3=" + this.descripcion.ToString() +
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
    }
}
