using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Digitalizacion2014.Clases
{
    public class clsSeguridad
    {
        public class Crypto
        {
            public enum CryptoProvider { DES, TripleDES, RC2, Rijndael };
            public enum CryptoAction { Encrypt, Desencrypt };

            string _key;
            string _IV;

            private CryptoProvider algorithm;

            public Crypto(CryptoProvider alg)
            {
                this.algorithm = alg;
            }

            public string key
            {
                get { return _key; }
                set { _key = value; }
            }

            public string IV
            {
                get { return _IV; }
                set { _IV = value; }
            }

            //Devuelve el arreglo de bytes correspondiente a la llave.
            private byte[] MakeKeyByteArray()
            {
                switch (this.algorithm)
                {
                    case CryptoProvider.DES:
                    case CryptoProvider.RC2:
                        if (_key.Length < 8) { _key = _key.PadRight(8); }
                        else if (_key.Length > 8) { _key = _key.Substring(0, 8); }
                        break;
                    case CryptoProvider.TripleDES:
                    case CryptoProvider.Rijndael:
                        if (_key.Length < 16) { _key = _key.PadRight(16); }
                        else if (_key.Length > 16) { _key = _key.Substring(16); }
                        break;
                }
                return Encoding.UTF8.GetBytes(_key);
            }

            //Devuelve el arreglo de bytes correspondiente al VI.
            private byte[] MakeIVByteArray()
            {
                switch (this.algorithm)
                {
                    case CryptoProvider.DES:
                    case CryptoProvider.RC2:
                    case CryptoProvider.TripleDES:
                        if (_IV.Length < 8) { _IV = _IV.PadRight(8); }
                        else if (_IV.Length > 8) { _IV = _IV.Substring(8); }
                        break;
                    case CryptoProvider.Rijndael:
                        if (_IV.Length < 16) { _IV = _IV.PadRight(16); }
                        else if (_IV.Length > 16) { _IV = _IV.Substring(16); }
                        break;
                }
                return Encoding.UTF8.GetBytes(_IV);
            }

            //Devuelve la cadena cifrada.
            public string CryptoDecryptor(string cadenaOriginal, CryptoAction accion)
            {
                //creamos el flujo tomando la memoria como respaldo.
                MemoryStream memStream;
                try
                {
                    //verificamos que la llave y el VI han sido proporcionados.
                    if (_key != null && _IV != null)
                    {
                        //obtenemos el arreglo de bytes correspondiente a la llave
                        //y al vector de inicialización.
                        byte[] key = MakeKeyByteArray();
                        byte[] IV = MakeIVByteArray();
                        //convertimos el mensaje original en sus correspondiente
                        //arreglo de bytes.
                        byte[] textoplano = (accion == CryptoAction.Encrypt ? Encoding.UTF8.GetBytes(cadenaOriginal) : Convert.FromBase64String(cadenaOriginal));

                        //creamos el flujo 
                        memStream = new MemoryStream(cadenaOriginal.Length * (accion == CryptoAction.Encrypt ? 2 : 1));
                        //obtenemos nuestro objeto cifrador, usando la clase 
                        //CryptoServiceProvider codificada anteriormente.
                        CryptoServiceProvider cryptoProvider = new CryptoServiceProvider((CryptoServiceProvider.CryptoProvider)this.algorithm, (CryptoServiceProvider.CryptoAction)accion);
                        ICryptoTransform transform = cryptoProvider.GetServiceProvider(key, IV);
                        //creamos el flujo de cifrado, usando el objeto cifrador creado y almancenando
                        //el resultado en el flujo MemoryStream.
                        CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                        //ciframos el mensaje.
                        cryptoStream.Write(textoplano, 0, textoplano.Length);
                        //cerramos el flujo de cifrado.
                        cryptoStream.Close();
                    }
                    else
                    {
                        throw new Exception("Error al inicializar la clave y el vector");
                    }
                }
                catch { throw; }
                return accion == CryptoAction.Encrypt ? Convert.ToBase64String(memStream.ToArray()) : Encoding.UTF8.GetString(memStream.ToArray());
            }
        }

        internal class CryptoServiceProvider
        {
            //Lista con los proveedores de cifrado que proporciona la clase.
            internal enum CryptoProvider { DES, TripleDES, RC2, Rijndael };
            //Lista con las posibles acciones a realizar dentro de la clase
            internal enum CryptoAction { Encrypt, Desencrypt };

            private CryptoProvider algorithm;
            private CryptoAction cAction;

            public CryptoServiceProvider(CryptoProvider Alg, CryptoAction Action)
            {
                //asignamos las opciones seleccionadas.
                this.algorithm = Alg;
                this.cAction = Action;
            }

            internal ICryptoTransform GetServiceProvider(byte[] Key, byte[] IV)
            {
                ICryptoTransform transform = null;
                switch (this.algorithm)
                {
                    //Algoritmo DES.
                    case CryptoProvider.DES:
                        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                        switch (cAction)
                        {
                            case CryptoAction.Encrypt:
                                transform = des.CreateEncryptor(Key, IV);
                                break;
                            case CryptoAction.Desencrypt:
                                transform = des.CreateDecryptor(Key, IV);
                                break;
                        }
                        return transform;
                    //algoritmo TripleDES.
                    case CryptoProvider.TripleDES:
                        TripleDESCryptoServiceProvider des3 = new TripleDESCryptoServiceProvider();
                        switch (cAction)
                        {
                            case CryptoAction.Encrypt:
                                transform = des3.CreateEncryptor(Key, IV);
                                break;
                            case CryptoAction.Desencrypt:
                                transform = des3.CreateDecryptor(Key, IV);
                                break;
                        }
                        return transform;
                    //algoritmo RC2.
                    case CryptoProvider.RC2:
                        RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                        switch (cAction)
                        {
                            case CryptoAction.Encrypt:
                                transform = rc2.CreateEncryptor(Key, IV);
                                break;
                            case CryptoAction.Desencrypt:
                                transform = rc2.CreateDecryptor(Key, IV);
                                break;
                        }
                        return transform;
                    //algoritmo Rijndael.
                    case CryptoProvider.Rijndael:
                        RijndaelManaged rijndael = new RijndaelManaged();
                        switch (cAction)
                        {
                            case CryptoAction.Encrypt:
                                transform = rijndael.CreateEncryptor(Key, IV);
                                break;
                            case CryptoAction.Desencrypt:
                                transform = rijndael.CreateDecryptor(Key, IV);
                                break;
                        }
                        return transform;
                    default:
                        throw new CryptographicException("Error al inicializar al proveedor de cifrado");
                }
            }
        }
    }
}
