using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Core.ServiceInterfaces
{
    public interface IGenerateLegalDocumentService
    {
        string ConsultaTiempoEjecucion();
        void ProcesarGenerateLegalDocument();
        void ProgramarFechaSiguienteEjecucion();
    }
}
