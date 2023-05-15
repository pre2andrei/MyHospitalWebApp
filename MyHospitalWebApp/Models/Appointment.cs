using HospitalWebApp.Controllers.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebApp.Models
{
    public class Appointment : Identified
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_patient")]
        [Display(Name = "Patient")]
        public int patientId { get; set; }
        public Patient? Patient { get; set; }

        [Required]
        [Column("id_doctor")]
        [Display(Name = "Doctor")]
        public int doctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required]
        [Column("appointed_time")]
        [Display(Name = "Appointed Time")]
        public DateTime AppointedTime { get; set; }

        [Required]
        [Column("room")]
        [Display(Name = "Room")]
        [Range(1,40,ErrorMessage ="Room number must be between 1 and 40")]
        public int RoomNr { get; set; }
    }
}
