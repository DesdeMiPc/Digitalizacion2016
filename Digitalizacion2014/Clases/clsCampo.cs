using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Digitalizacion2014.Clases
{
    public class clsCampo : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "sp_ConfigCampos";
        string validar = "";
        string parametros = "";

        //Elementos del Objeto
        string _id = "";
        public string descripcion { get; set; }
        public string explicacion { get; set; }
        public clsTipoCampo tipoCampo { get; set; }
        public clsClasificacionCampos clasificacion { get; set; }
        public int longitud { get; set; }
        public string campo_Valor { get; set; }
        public string campo_Mostrar { get; set; }
        public string consulta { get; set; }
        public bool obligatorio { get; set; }

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

        public clsCampo()
        {
            cargarDatos("*");
        }

        public clsCampo(string idCampo)
        {
            cargarDatos(idCampo);
        }

        public clsCampo(DataRow registro)
        {
            cargarDatos(registro);
        }

        void cargarDatos(string idCampo)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idCampo == "*" || idCampo == "0" || idCampo == "")
            {
                //Iniciar todas la variables
                this._id = "0";
                this.descripcion = "";
                this.explicacion = "";
                this.tipoCampo = new clsTipoCampo("*");
                this.clasificacion = new clsClasificacionCampos("*");
                this.longitud = 0;
                this.campo_Valor = "";
                this.campo_Mostrar = "";
                this.consulta = "";
                this.obligatorio = false;
            }
            else
            {
                //Cargar los Datos de la B.D.
                this.validar = "2";
                this.parametros = "|V1=" + idCampo.ToString().Trim() + "|";
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
                this._id = r["idCampo"].ToString();
                this.descripcion = r["Descripcion"].ToString();
                this.explicacion = r["Explicacion"].ToString();
                this.tipoCampo = new clsTipoCampo(r["TipoCampo"].ToString());
                this.clasificacion = new clsClasificacionCampos(r["Clasificacion"].ToString());
                this.longitud = Convert.ToInt16(r["Longitud"]);
                this.campo_Valor = r["Campo_Valor"].ToString();
                this.campo_Mostrar = r["Campo_Mostrar"].ToString();
                this.consulta = r["Consulta"].ToString();
                this.obligatorio = Convert.ToBoolean(r["Obligatorio"].ToString());

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
            //Preparar Datos de Envio a SQL Server
            this.validar = "4";
            this.parametros = "|V1=" + this._id + "|V2=" + this.descripcion + "|V3=" + this.explicacion +
                              "|V4=" + this.tipoCampo.id + "|V5=" + this.clasificacion.id +
                              "|V6=" + this.longitud + "|V7=" + this.campo_Valor +
                              "|V8=" + this.campo_Valor +
                              "|V9=" + this.consulta +
                              "|V10=" + this.obligatorio.ToString() +
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
