using System.ComponentModel.DataAnnotations;

namespace MyHospitalWebApp.Models.BindingModels
{
    public class CreateIdentityDoctorModel
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
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
