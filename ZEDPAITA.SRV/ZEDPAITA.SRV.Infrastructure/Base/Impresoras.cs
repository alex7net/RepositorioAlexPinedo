using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Infrastructure.Base
{
    public static class Impresoras
    {
       
        public static List<string> obtenerImpresorasLocal()
        {
            string consulta = "Select * from Win32_Printer";
            List<string> impresorasInstaladas = new List<string>();
            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath);
            objScope.Connect();

            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = consulta;
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();

            foreach (ManagementObject mo in MOC)
            {
                impresorasInstaladas.Add(mo["Name"].ToString());
            }

            return impresorasInstaladas;
        }
    }
}
