using HospitalWebApp.Controllers.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    [Table("Patients")]
    public class Patient : Identified
    {
        [Key]
        [Display(Name = "Patient")]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("PIC")]
        [MaxLength(20)]
        [Display(Name = "Personal Identity Code")]
        public string PIC { get; set; }

        [Required]
        [Column("first_name")]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Column("last_name")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Column("DOB")]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [Column("user_id")]
        public string userId { get; set; }

        public IdentityUser? User { get; set; }

        public List<Diagnostic> Diagnostics { get; set; } = new List<Diagnostic>();

        public List<Appointment> Appointments { get; set; } =new List<Appointment>();
    }
}
