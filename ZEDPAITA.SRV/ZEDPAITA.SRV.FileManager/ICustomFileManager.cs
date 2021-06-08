using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.FileManager
{
    public interface ICustomFileManager
    {
        Task<bool> FileUpload(string rutaDestino, NetworkCredential credentials, byte[] file, string ruta);
        int ValidateDirectoryExists(NetworkCredential credentials, string rutaPrincial, string ruta);
    }
}
