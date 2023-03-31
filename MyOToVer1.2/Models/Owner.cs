using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyOToVer1._2.Models
{
    public class Owner
    {
        [ForeignKey("Customer")]
        public int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public long owner_revenue { get; set; }
        public int owner_number_rented { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
