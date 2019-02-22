using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Digitalizacion2014.Clases
{
    class clsDocumento : clsBase
    {
        //Elementos de Acceso a la B.D.
        WSD2014.cRetorno regreso = new WSD2014.cRetorno();
        WSD2014.WSDatosSoap AccesoDatos = new WSD2014.WSDatosSoapClient();

        string procedimiento = "sp_Datos_Documentos";
        string validar = "";
        string parametros = "";
        
        string _id = "";
        string _imagenB64 = "";
        public string idUnicoExpediente { get; set; }
        public string idFormulario { get; set; }
        public string idLlave { get; set; }
        public int idPadre { get; set; }
        public int orden { get; set; }
        
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

        public clsDocumento()
        {
            cargarDatos("*");
        }

        public clsDocumento(string idDocumento)
        {
            cargarDatos(idDocumento);
        }

        public clsDocumento(DataRow registro)
        {
            cargarDatos(registro);
        }

        void cargarDatos(string idDocumento)
        {
            this.datosRegistro = new ClsDatosComunesRegistro();
            this.datosModificacion = new ClsDatosComunesRegistro();
            this.datosEliminacion = new ClsDatosComunesRegistro();

            if (idDocumento == "*" || idDocumento == "0" || idDocumento == "")
            {
                //Iniciar todas las variables por ser elemento nuevo
                this._id = "0";
                this.idUnicoExpediente = "0";
                this.idFormulario = "";
                this.idLlave = "";
                datosRegistro.fecha = DateTime.Now;
            }
            else
            {
                //Cargar los Datos desde la B.D.
                this.validar = "1";
                this.parametros = "|V1=" + idDocumento.ToString().Trim() + "|";
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
                this._id = r["idUnicoDocumento"].ToString();
                this.idUnicoExpediente = r["idUnicoExpediente"].ToString();
                this.idFormulario = r["idFormulario"].ToString();
                this.idLlave = r["idLlaveBusqueda"].ToString();
                this.orden = Convert.ToInt16(r["orden"]);
                this.idPadre = Convert.ToInt16(r["idPadre"]);

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
            this.parametros = "|V1=" + _id.ToString() + "|V2=" + this.idUnicoExpediente.ToString() +
                              "|V3=" + this.idFormulario.ToString() + "|V4=" + this.idLlave.ToString() + "|" +
                              "|V5=" + this.orden.ToString() + "|V6=" + this.idPadre.ToString() +
                              "|C1=" + user.ToString() +
                              "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString();

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

        public bool guardarImagen(string user = "Default")
        {
            this.validar = "3";
            this.parametros = "|V1=" + _id.ToString() + "|V2=" + this.idUnicoExpediente.ToString() +
                              "|V8=JPG|V9=0" + "|V5=" + this.orden.ToString() +
                              "|C1=" + user.ToString() + 
                              "|C3=" + Environment.UserName.ToString() + "/" + Environment.MachineName.ToString() +
                              "|V7=" + _imagenB64 + "|";

            regreso = AccesoDatos.ivkProcedimiento(this.procedimiento, this.validar, this.parametros, Clases.vGlobales.conexion, null);
            if (regreso.bOk)
            {
                //cargarDatos(regreso.ds.Tables[0].Rows[0]);
                return true;
            }
            else
            {
                throw new System.InvalidOperationException("Error al Guardar Datos \r\nProblemas con el registro a guardar " + this._id);
            }
        }

        public void fijarImagen(System.IO.Stream imgStream)
        {
            
            Byte[] bytes = ReadToEnd(imgStream);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            _imagenB64 = base64String;
        }

        public void fijarImagen(string imgBase64)
        {
            _imagenB64 = imgBase64;
        }

        public string obtenerImagenB64()
        {
            return _imagenB64;
        }

        byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}
