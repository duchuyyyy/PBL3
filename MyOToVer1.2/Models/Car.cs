using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyOToVer1._2.Models
{
    public class Car
    {
        [Key]
        public int car_id { get; set; }

        [Required(ErrorMessage = "Điền đẩy đủ thông tin")]
        public string car_number { get; set; }
       
        public string car_brand { get; set; }
        public string car_name { get; set; }
       
        public int car_capacity { get; set; }
        
        public int car_model_year { get; set; }
        
        public string car_tranmission { get; set; }
       
        public string car_fuel { get; set; }
        public int car_consume_fuel { get; set; }
        public string car_description { get; set; }
        public int car_price { get; set; }
        [NotMapped]
        public string car_ward_address { get; set; }
        [NotMapped]
        public string car_street_address { get; set; }
        public string car_address { get; set; }
        public string car_rule { get; set; }
        
        public bool car_status { get; set; }

        public bool is_accept { get; set; }

        public bool is_update { get; set; }
        
        public int car_number_rented { get; set; }
        public int owner_id { get; set; }
        [ForeignKey("owner_id")]
        public virtual Owner Owner { get; set; }

        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<CarImg> CarImgs { get; set; }

        public virtual ICollection<CarReview> CarReviews { get; set; }

        public int AdminId { get; set; }
        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
    }
}
