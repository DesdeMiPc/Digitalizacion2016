using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Digitalizacion2014.Clases
{
    public class clsNodoCarpeta : clsBase, Digitalizacion2014.Clases.IclsNodo
    {

        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "SP_Arbol_General";
        string validar = "";
        string parametros = "";

        string _idNodo = "";
        string _idPadre = "";
        public string descripcion { get; set; }
        public clsEnums.TipoNodo tipo { get; set; }
        public int idFormulario { get; set; }
        public bool activo { get; set; }

        public string id
        {
            get { return this._idNodo; }
            set
            {
                if (value != "*" || value != "0" || value != "")
                {
                    this._idNodo = value;
                    this.cargarDatos(_idNodo);
                }
                else
                {
                    this._idNodo = "0";
                }
            }
        }

        public clsNodoCarpeta()
        {
            this.cargarDatos("*");
        }

        public clsNodoCarpeta(string idNodo)
        {
            this.cargarDatos(idNodo);
        }

        public clsNodoCarpeta(DataRow registro)
        {
            this.cargarDatos(registro);
        }

        void cargarDatos(string idNodo)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idNodo == "*" || idNodo == "0" || idNodo == "")
            {
                //Iniciar todas la variables
                this._idNodo = "0";
                this._idPadre = "0";
                this.descripcion = "";
                this.tipo = 0;
                this.idFormulario = 0;
            }
            else
            {
                //Cargar los Datos desde la Base de Datos
                this.validar = "2";
                this.parametros = "|V2=" + idNodo.ToString() + "|";
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
                this._idNodo = r["idNodo"].ToString();
                this._idPadre = r["idPadre"].ToString();
                this.descripcion = r["Descripcion"].ToString();
                this.activo = Convert.ToBoolean(r["Activo"]);
                this.tipo = (Clases.clsEnums.TipoNodo)r["Tipo"];
                this.idFormulario = Convert.ToInt16(r["idFormulario"]);

                //Datos Comunes de Registros
                this.CargarDatosRegistro(r);
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error al Cargar Datos \r\n" + ex.Message);
            }
        }

    }
}
