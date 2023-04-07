using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace MyOToVer1._2.Models
{
    public class CarRental
    {
        [Key]
        public int rental_id { get; set; }
        public DateTime rental_datetime { get; set; }
        public DateTime return_datetime { get; set; }
        public int rental_status { get; set; }
        
        public int deposit_status { get; set; }

        public int total_price { get; set; }

        public string img_confirm_transfer { get; set; }
        public int customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual Customer customer { get; set; }

        public int car_id { get; set; }
        [ForeignKey("car_id")]
        public virtual Car Car { get; set; }
    }
}
