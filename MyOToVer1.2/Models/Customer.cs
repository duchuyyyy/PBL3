using System.ComponentModel.DataAnnotations;

namespace MyOToVer1._2.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        //public string? Address { get; set; }
        [Required]
        public string? Contact { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}
