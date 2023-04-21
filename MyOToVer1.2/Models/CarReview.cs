using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MyOToVer1._2.Models
{
    public class CarReview
    {
        [Key]
        public int Reviewid { get; set; }
        public string ReviewContent { get; set; }
        public DateTime ReviewDate { get; set; }
        public double ReviewScore { get; set; }

        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer customer { get; set; }
        public int car_id { get; set; }
        [ForeignKey("car_id")]
        public virtual Car car { get; set; }
    }
}
