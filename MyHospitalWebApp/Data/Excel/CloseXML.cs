using ClosedXML.Excel;
using HospitalWebApp.Models;

namespace MyHospitalWebApp.Data.Excel
{
    public static class CloseXML
    {
        public static void CreateAppointmentsExcel(List<Appointment> data,string fileName)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Appointments");
            worksheet.Cell("A1").Value = data.Select(a=>a.toAppointmentForExcel()).ToList();
            workbook.SaveAs(fileName);
        }
        public static void CreatePatientsExcel(List<Patient> data, string fileName)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Patients");
            worksheet.Cell("A1").Value = data.Select(p => p.toPatientForExcel()).ToList();
            workbook.SaveAs(fileName);
        }
        public static void CreateExcelWithSP(List<object> data, string fileName)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("data");
            worksheet.Cell("A1").Value = data;
            workbook.SaveAs(fileName);
        }
        public static MemoryStream DownloadList(string fileName, string uploadPath)
        {
            var memory = new MemoryStream();
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, fileName);
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }
    }
}
