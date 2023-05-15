using HospitalWebApp.Controllers.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    [Table("Doctors")]
    public class Doctor : Identified
    {
        [Key]
        [Display(Name = "Doctor")]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("PIC")]
        [MaxLength(20)]
        [Display(Name = "Personal Identity Code")]
        public string PIC { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [Column("first_name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        [Column("last_name")]
        public string lastName { get; set; }

        [Required]
        [Column("user_id")]
        public string userId { get; set; }

        [ForeignKey("userId")]
        public IdentityUser? User { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public List<Doctor_Speciality> Doctors_Specialities { get; set; } = new List<Doctor_Speciality>();

        public override string ToString()
        {
            return $"{firstName} {lastName}";
        }
    }
}
