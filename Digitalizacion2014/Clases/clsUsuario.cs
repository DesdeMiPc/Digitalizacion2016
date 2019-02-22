using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public class clsUsuario : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "SP_Usuario";
        string validar = "";
        string parametros = "";

        //Elementos del Obejto
        string _id = "";
        public string loginName { get; set; }
        public string nombreCompleto { get; set; }
        public string puesto { get; set; }
        public string claveAcceso { get; set; }
        public string correo { get; set; }
        public int idGrupo { get; set; }
        public bool activo { get; set; }

        //Elemento para Manejar Encriptacion
        clsSeguridad.Crypto enc = new clsSeguridad.Crypto(clsSeguridad.Crypto.CryptoProvider.TripleDES);
        string key = "InnovaWeb2014$";
        string IV = "innova14";

        public string id { 
            get {return this._id;}
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

        public clsUsuario()
        {
            cargarDatos("*");
        }

        public clsUsuario(string idUsuario)
        {
            cargarDatos(idUsuario);
        }

        public clsUsuario(string idUsuario, bool bIdUnico)
        {
            if (idUsuario == "*" || idUsuario == "0" || idUsuario == "")
            {
                this.activo = false;
                return;
            }
            cargarDatos(idUsuario, bIdUnico);
        }

        public clsUsuario(DataRow registro)
        {
            cargarDatos(registro);
        }

        void cargarDatos(string idUsuario)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idUsuario == "*" || idUsuario == "0" || idUsuario == "")
            {
                //Iniciar todas las variables por ser elemento nuevo
                this._id = "0";
                this.loginName = "";
                this.nombreCompleto = "";
                this.puesto = "";
                this.claveAcceso = "";
                this.correo = "";
                this.idGrupo = 0;
                this.activo = true;
                datosRegistro.fecha = DateTime.Now;
            }
            else
            {
                //Cargar los Datos desde la B.D.
                this.validar = "2";
                this.parametros = "|V8=" + idUsuario.ToString().Trim() + "|";
                regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
                if (regreso.bOk)
                {
                    //Regreso el Dato de Manera Correcta
                    cargarDatos(regreso.ds.Tables[0].Rows[0]);
                }
                else
                {
                    throw new System.InvalidOperationException("Error al Cargar Datos \r\nNo Existe el registro en la Base de Datos" );
                }
            }
        }

        void cargarDatos(string idUsuario, bool idUnico)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            //Cargar los Datos desde la B.D.
            this.validar = "22";
            this.parametros = "|V1=" + idUsuario.ToString().Trim() + "|";
            regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                //Regreso el Dato de Manera Correcta
                cargarDatos(regreso.ds.Tables[0].Rows[0]);
            }
            else
            {
                this.activo = false;
            }
        }

        void cargarDatos(DataRow r)
        {
            try
            {
                this._id = r["idUnico"].ToString();
                this.loginName = r["id_User"].ToString();
                this.nombreCompleto = r["NombreCompleto"].ToString();
                this.puesto = r["Puesto"].ToString();
                this.correo = r["Correo"].ToString();
                this.idGrupo = Convert.ToInt16(r["id_Grupo"]);
                this.activo = Convert.ToBoolean(r["Activo"]);

                //Datos Comunes de Registros
                this.CargarDatosRegistro(r);

                enc.key = this.key;
                enc.IV = this.IV;
                if (r["Password"] == System.DBNull.Value)
                {
                    this.claveAcceso = "";
                }
                else
                {
                    this.claveAcceso = enc.CryptoDecryptor(r["Password"].ToString(), clsSeguridad.Crypto.CryptoAction.Desencrypt);
                }
                
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error al Cargar Datos \r\nProblemas con el registro adquirido" + ex.Message);
            }
        }

        public bool guardarDatos(string user = "Default")
        {
            enc.key = this.key;
            enc.IV = this.IV;
            
            //Preparar Datos de Envio a SQL Server
            this.validar = "4";
            this.parametros = "|V1=" + this.loginName.ToString() + "|V2=" + this.nombreCompleto +
                              "|V3=" + this.puesto + "|V4=" + enc.CryptoDecryptor(this.claveAcceso, clsSeguridad.Crypto.CryptoAction.Encrypt) +
                              "|V5=" + this.correo + "|V6=" + this.idGrupo.ToString() +
                              "|V7=" + Convert.ToByte(this.activo) + "|V8=" + this._id.ToString() + 
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

        public bool desactivar(string user = "Defaul")
        {
            this.validar = "5";
            this.parametros = "|V8=" + this._id.ToString() +  "|" +
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
