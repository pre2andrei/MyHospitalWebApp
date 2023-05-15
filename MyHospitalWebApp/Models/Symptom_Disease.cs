using HospitalWebApp.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    [Table("Symptoms_Diseases")]
    public class Symptom_Disease : Identified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_disease")]
        public int DiseaseId { get; set; }

        public Disease Disease { get; set; }

        [Required]
        [Column("id_symptom")]
        public int SymptomId { get; set; }

        public Symptom Symptom { get; set; }
    }
}
