using HospitalWebApp.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    [Table("Diagnostics")]
    public class Diagnostic : Identified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Required]
        [Column("id_patinet")]
        [Display(Name = "Patient")]
        public int patientId { get; set; }

        public Patient? Patient { get; set; }

        [Required]
        [Column("id_disease")]
        [Display(Name = "Disease")]
        public int diseaseId { get; set; }
        public Disease? Disease { get; set; }


    }
}
