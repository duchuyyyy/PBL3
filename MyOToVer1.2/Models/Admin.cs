
using System.ComponentModel.DataAnnotations;

namespace MyOToVer1._2.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Contact { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}
