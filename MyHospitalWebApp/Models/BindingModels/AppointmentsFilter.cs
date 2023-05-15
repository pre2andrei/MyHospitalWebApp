using System.ComponentModel.DataAnnotations;

namespace MyHospitalWebApp.Models.BindingModels
{
    public class AppointmentsFilter
    {
        [Display(Name="Order By Doctor Name")]
        public bool orderByDoctorName { get; set; } = false;
        [Display(Name = "Order By Patient Name")]
        public bool orderByPatientName { get; set; } = false;
        [Display(Name = "Order By Appointed Time")]
        public bool orderByAppointedTime { get; set; } = false;
        [Display(Name = "Order By Room Number Name")]
        public bool orderRoomNr { get; set; } = false;
        [Display(Name = "Order By Doctor Name")]
        public bool desc { get; set; } = false;


        [Display(Name = "Filter Doctor Name")]
        public string? doctorNameFilter { get; set; }

        [Display(Name = "Filter Patient Name")]
        public string? patientNameFilter { get; set; }

        [Display(Name = "Search Appointed Time")]
        public DateTime? dateFilter { get; set; }

        [Display(Name = "Search Room Number")]
        public int? roomNrFilter { get; set; }
    }
}
