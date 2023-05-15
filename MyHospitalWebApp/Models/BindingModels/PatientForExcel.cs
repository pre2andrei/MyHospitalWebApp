namespace MyHospitalWebApp.Models.BindingModels
{
    public class PatientForExcel
    {
        public int Id { get; set; }

        public string PIC { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string DOB { get; set; }

        public string userId { get; set; }
    }
}
