using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace Digitalizacion2014.Clases
{
    public class clsFormulario : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "SP_ConfigFormularios";
        string validar = "";
        string parametros = "";

        //Elementos del Obejto
        string _id = "";
        public string descripcion { get; set; }
        public int clasificacion { get; set; }
        public int tipoFormulario { get; set; }
        public bool camposAutomaticos { get; set; }
        public string consultaExterna { get; set; }

        //Campos configurados
        public List<clsFormularioCampo> campos = new List<clsFormularioCampo>();

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

        public clsFormulario()
        {
            cargarDatos("*");
        }

        public clsFormulario(string idFormulario)
        {
            cargarDatos(idFormulario);
        }

        public clsFormulario(DataRow registro)
        {
            cargarDatos(registro);
        }

        void cargarDatos(string idFormulario)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idFormulario == "*" || idFormulario == "0" || idFormulario == "")
            {
                //Iniciar todas las variables para el elemento nuevo
                this._id = "0";
                this.descripcion = "";
                this.clasificacion = 0;
                this.tipoFormulario = 1;
                this.camposAutomaticos = false;
                this.consultaExterna = "";
            }
            else
            {
                //Cargar los datos desde la B.D.
                this.validar = "2";
                this.parametros = "|V1=" + idFormulario + "|";
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
                this._id = r["idFormulario"].ToString();
                this.descripcion = r["Descripcion"].ToString();
                this.clasificacion = Convert.ToInt16(r["Clasificacion"]);
                this.tipoFormulario = Convert.ToInt16(r["TipoFormulario"]);
                this.camposAutomaticos = Convert.ToBoolean(r["CamposAutomaticos"]);
                this.consultaExterna = r["ConsultaExterna"].ToString();

                //Datos Comunes de Registros
                this.CargarDatosRegistro(r);
                this.campos.Clear();
                this.cargarCampos();

            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error al Cargar Datos \r\nProblemas con el registro adquirido" + ex.Message);
            }
        }

        void cargarCampos()
        {
            //Cargar los campos definidos en el formulario
            this.validar = "3";
            this.parametros = "|V1=" + this._id + "|";
            regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                foreach (DataRow r in regreso.ds.Tables[0].Rows)
                {
                    campos.Add(new clsFormularioCampo(r["idFormulario"].ToString(), r["idCampo"].ToString()));
                }
            }
        }

        public bool guardarDatos(string user = "Default")
        {
            //Guardar los Datos del Formulario
            this.validar = "4";
            this.parametros = "|V1=" + this._id.ToString() + "|V2=" + this.descripcion.ToString().Trim() + "|V4=" + this.tipoFormulario.ToString()
                            + "|V5=" + Convert.ToByte(this.camposAutomaticos).ToString() + "|V6=" + this.consultaExterna.ToString()
                            + "|C1=" + user.ToString() +
                              "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString() + "|";
            regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                //Grabación de Cabezera correcta, seguir con campos 
                foreach (clsFormularioCampo c in campos)
                {
                    c.idFormulario = this._id;
                    if (! c.guardarDatos())
                    {
                        throw new System.InvalidOperationException("Error al Guardar Datos \r\nProblemas con el registro a guadar ");
                    }
                }
                cargarDatos(regreso.ds.Tables[0].Rows[0]);
                return true;
            }
            else
            {
                throw new System.InvalidOperationException("Error al Guardar Datos \r\nProblemas con el registro a guadar " + this._id);
            }
        }
    }
}
