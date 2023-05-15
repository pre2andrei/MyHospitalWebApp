namespace MyHospitalWebApp.Models.BindingModels
{
    public class AppointmentForExcel
    {
        public int Id { get; set; }
        public int patientId { get; set; }
        public string Patient { get; set; }
        public int doctorId { get; set; }
        public string Doctor { get; set; }
        public string AppointedTime { get; set; }
        public int RoomNr { get; set; }
    }
}
