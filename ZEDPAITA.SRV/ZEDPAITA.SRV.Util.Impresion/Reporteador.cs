using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Util.Impresion
{
    public class Reporteador
    {
        public string ConvertirReportePdf(string archivoPdf, string rutaRdlc, DataSet conjuntoDatos)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string fileNameExtension;
            LocalReport reporteLocal = GenerarReporte(rutaRdlc, conjuntoDatos);

            try
            {
                byte[] bytes = reporteLocal.Render("PDF",
                                                    null,
                                                    out mimeType,
                                                    out encoding,
                                                    out fileNameExtension,
                                                    out streamids,
                                                    out warnings);
                using (FileStream fs = new FileStream(archivoPdf, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }

                Array.Resize(ref bytes, 0);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reporteLocal != null)
                {
                    reporteLocal.Dispose();
                }
            }
            return archivoPdf;
        }

        public LocalReport GenerarReporte(string rutaRdlc, DataSet conjuntoDatos)
        {
            LocalReport reporteLocal;
            try
            {
                reporteLocal = new LocalReport();
                reporteLocal.EnableExternalImages = true;
                reporteLocal.ReportPath = @rutaRdlc;
                reporteLocal.DataSources.Clear();
                if (conjuntoDatos != null)
                {
                    foreach (DataTable tabla in conjuntoDatos.Tables)
                    {
                        ReportDataSource reportDataSource = new ReportDataSource();
                        reportDataSource.Name = tabla.TableName;
                        reportDataSource.Value = tabla;
                        reporteLocal.DataSources.Add(reportDataSource);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return reporteLocal;
        }
    }
}
