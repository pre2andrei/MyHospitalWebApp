using HospitalWebApp.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    [Table("Doctors_Specialities")]
    public class Doctor_Speciality : Identified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_doctor")]
        [Display(Name = "Doctor")]
        public int doctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required]
        [Column("id_specialty")]
        [Display(Name ="Speciality")]
        public int specialityId { get; set; }
        public Speciality? Speciality { get; set; }
    }
}
