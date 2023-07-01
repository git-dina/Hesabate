using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
    class ReportsConfig
    {
        public static void setReportLanguage(List<ReportParameter> paramarr)
        {

            paramarr.Add(new ReportParameter("lang", AppSettings.lang));

        }

        public static void ExportToPDF(LocalReport report, String FullPath)
        {

            string deviceInfo = string.Format(
          CultureInfo.InvariantCulture,
          "<DeviceInfo>" +
              "<OutputFormat>PDF</OutputFormat>" +
          "</DeviceInfo>");
            

            byte[] Bytes = report.Render(format: "PDF", deviceInfo);

            try
            {
                using (FileStream stream = new FileStream(FullPath, FileMode.Create))
                {
                    try
                    {
                        stream.Write(Bytes, 0, Bytes.Length);
                        stream.Close();

                    }
                    catch
                    {

                    }
                    finally
                    {
                        stream.Close();
                    }
                }

                System.Diagnostics.Process.Start(FullPath);
            }
            catch { }

        }
      

       
    }
}
