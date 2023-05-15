namespace MyHospitalWebApp.Models.BindingModels
{
    public class PatientsFilter
    {
        public bool orderByFullName { get; set; } = false;
        public bool orderByDob { get; set; } = false;
        public bool desc { get; set; } = false;
        public string? fullNameFilter { get; set; }
        public string? picFilter { get; set; }
        public DateTime? dobFilter { get; set; }
    }
}
