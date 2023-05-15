using HospitalWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyHospitalWebApp.Models.BindingModels
{
    public class CreateIdentityPatientModel
    {
        [Required]
        [MaxLength(20)]
        [Display(Name = "Personal Identity Code")]
        public string PIC { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; } = new DateTime(1997,12,24,0,0,0,0);

        [Required]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
