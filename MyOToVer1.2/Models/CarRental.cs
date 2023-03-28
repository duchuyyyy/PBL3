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
        public bool rental_status { get; set; }
        public string deposit_method { get; set; }
        public bool deposit_status { get; set; }

        
        public int customer_id { get; set; }
        [ForeignKey("customer_id")]
        public virtual Customer customer { get; set; }

        
        public int car_id { get; set; }
        [ForeignKey("car_id")]
        public virtual Car Car { get; set; }
    }
}
