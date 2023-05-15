using HospitalWebApp.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    [Table("Diseases")]
    public class Disease : Identified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("danger_level")]
        [Display(Name = "Danger Level")]
        [Range(1, 10, ErrorMessage = "Danger Level must be between 1 and 10")]
        public int DangerLevel { get; set; }

        public List<Symptom_Disease> Symptoms_Diseases { get; set; } = new List<Symptom_Disease>();

        public List<Diagnostic> Diagnostics { get; set; } = new List<Diagnostic>();

    }
}
