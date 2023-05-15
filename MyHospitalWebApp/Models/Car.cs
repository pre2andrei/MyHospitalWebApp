using System.ComponentModel.DataAnnotations;

namespace MyHospitalWebApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
