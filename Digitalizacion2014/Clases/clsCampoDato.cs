using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Digitalizacion2014.Clases
{
    class clsCampoDato : clsBase
    {
        public enum TipoCampo
        {
            Expediente,
            Documento
        }

        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "";
        string validar = "";
        string parametros = "";

        TipoCampo _tipoCampo;
        Clases.clsCampo _campo;
        string _idPadre;
        string _msgError;

        public string valor { get; set; }
        public string msgError { get {return _msgError;} }

        //Generar un objeto para poder tener valor
        public clsCampoDato(TipoCampo tipoCampo, string padre, string campo)
        {
            //Determinar el campo si existe
            _campo = new clsCampo(campo);
            _idPadre = padre;

            _tipoCampo = tipoCampo;
        }

        public bool guardarDatos(string user = "Default")
        {
            if (this._tipoCampo == TipoCampo.Expediente)
            {
                procedimiento = "sp_Datos_Expedientes_Campos";
            }
            else if (this._tipoCampo == TipoCampo.Documento)
            {
                procedimiento = "sp_Datos_Documentos_Campos";
            }
            validar = "2";
            parametros = "|V1=" + _idPadre.ToString() +
                         "|V2=" + _campo.id.ToString() +
                         "|V3=" + this.valor.ToString() + "|" +
                         "|C1=" + user.ToString() +
                         "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString();
            
            //Invocar el WebService
            regreso = AccesoDatos.ivkProcedimiento(procedimiento, validar, parametros, vGlobales.conexion, null);
            if (regreso.bOk)
            {
                this._msgError = regreso.sResultado;
                return true;
            }

            return false;

        }
    }
}
