using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Digitalizacion2014.Clases
{
    public class clsExpediente : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "sp_Datos_Expedientes";
        string validar = "";
        string parametros = "";

        string _id = "";
        public string idPadre { get; set; }
        public string idFormulario { get; set; }
        public string idLlave { get; set; }

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

        public clsExpediente()
        {
            cargarDatos("*");
        }

        public clsExpediente(string idExpediente)
        {
            cargarDatos(idExpediente);
        }

        public clsExpediente(DataRow registro)
        {
            cargarDatos(registro);
        }

        void cargarDatos(string idExpediente)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idExpediente == "*" || idExpediente == "0" || idExpediente == "")
            {
                //Iniciar todas las variables por ser elemento nuevo
                this._id = "0";
                this.idPadre = "0";
                this.idFormulario = "";
                this.idLlave = "";
                datosRegistro.fecha = DateTime.Now;
            }
            else
            {
                //Cargar los Datos desde la B.D.
                this.validar = "1";
                this.parametros = "|V1=" + idExpediente.ToString().Trim() + "|";
                regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
                if (regreso.bOk)
                {
                    //Regreso el Dato de Manera Correcta
                    cargarDatos(regreso.ds.Tables[0].Rows[0]);
                }
                else
                {
                    throw new System.InvalidOperationException("Error al Cargar Datos \r\nNo Existe el registro en la Base de Datos");
                }
            }
        }

        void cargarDatos(DataRow r)
        {
            try
            {
                this._id = r["idUnicoExpediente"].ToString();
                this.idPadre = r["idPadre"].ToString();
                this.idFormulario = r["idFormulario"].ToString();
                this.idLlave = r["idLlaveBusqueda"].ToString();

                //Datos Comunes de Registros
                this.CargarDatosRegistro(r);

            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error al Cargar Datos \r\nProblemas con el registro adquirido" + ex.Message);
            }
        }

        public bool guardarDatos(string user = "Default")
        {
            this.validar = "2";
            this.parametros = "|V1=" + _id.ToString() + "|V2=" + this.idPadre.ToString() +
                              "|V3=" + this.idFormulario.ToString() + "|V4=" + this.idLlave.ToString() + "|" +
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
                throw new System.InvalidOperationException("Error al Guardar Datos \r\nProblemas con el registro a guardar " + this._id);
            }
        }
    }
}
