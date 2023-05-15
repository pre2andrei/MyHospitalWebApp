using HospitalWebApp.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    [Table("Specialities")]
    public class Speciality : Identified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(55)]
       public string Name { get; set; }

        public List<Doctor_Speciality> Doctors_Specialities { get; set; } = new List<Doctor_Speciality>();
    }
}
