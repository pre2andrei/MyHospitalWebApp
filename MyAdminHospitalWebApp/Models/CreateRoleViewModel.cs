using System.ComponentModel.DataAnnotations;

namespace MyAdminHospitalWebApp.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}