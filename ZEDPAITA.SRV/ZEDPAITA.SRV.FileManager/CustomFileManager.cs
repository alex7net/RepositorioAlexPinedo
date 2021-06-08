using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.FileManager
{
    public class CustomFileManager : ICustomFileManager
    {
        public async Task<bool> FileUpload(string rutaDestino, NetworkCredential credentials, byte[] file, string ruta)
        {
            bool result = false;
            try
            {
                using (new ConnectToSharedFolder(ruta, credentials))
                {
                    await Task.Run(() =>
                    {
                        File.WriteAllBytes(rutaDestino, file);
                    });
                }

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public int ValidateDirectoryExists(NetworkCredential credentials, string rutaPrincial, string ruta)
        {
            int result = 0;
            try
            {
                using (new ConnectToSharedFolder(rutaPrincial, credentials))
                {
                    if (System.IO.Directory.Exists(ruta))
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 2;
                    }
                }
            }
            catch (Win32Exception ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

    }
}
