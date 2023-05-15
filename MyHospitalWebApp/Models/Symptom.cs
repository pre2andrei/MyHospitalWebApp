using HospitalWebApp.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{

    [Table("Symptoms")]
    public class Symptom : Identified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        public List<Symptom_Disease> Symptoms_Diseases { get; set; } = new List<Symptom_Disease>();
    }
}
