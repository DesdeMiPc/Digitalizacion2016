using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public  class clsFormularioCampo : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "sp_ConfigFormularioCampos";
        string validar = "";
        string parametros = "";

        string _idFormulario;
        string _idCampo;

        public bool obligatorio = false;

        //Objeto del Campo 
        public clsCampo campo;

        public string idFormulario
        {
            get{ return this._idFormulario;}
            set{ this._idFormulario = value;}
        }

        public string idCampo
        {
            get { return this._idCampo; }
            set { this._idCampo = value; }
        }

        public clsFormularioCampo()
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            this._idFormulario = "";
            this._idCampo = "";
        }

        public clsFormularioCampo(string idFormulario, string idCampo)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            this._idFormulario = idFormulario;
            this._idCampo = idCampo;
            cargarDatos(idFormulario, idCampo);
        }

        public clsFormularioCampo(string idCampo)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            this._idCampo = idCampo;
            this.campo = new clsCampo(_idCampo);
        }

        void cargarDatos(string idFormulario, string idCampo)
        {
            validar = "2";
            parametros = "|V1=" + idFormulario + "|V2=" + idCampo + "|";
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

        void cargarDatos(DataRow r)
        {
            try
            {
                this.obligatorio = Convert.ToBoolean(r["Obligatorio"]);
                this.campo = new clsCampo(_idCampo);

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
            this.validar = "4";
            this.parametros = "|V1=" + this._idFormulario + "|V2=" + this._idCampo + "|V3=" + Convert.ToByte(this.obligatorio).ToString() +
                              "|C1=" + user.ToString() +
                              "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString() + "|";
            regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                return true;
            }
            else
            {
                throw new System.InvalidOperationException("Error al Guardar Datos \r\nProblemas con el registro a guadar " + this._idFormulario + "|" + this.idCampo);
            }
        }
    }
}
