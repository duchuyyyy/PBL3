using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyOToVer1._2.Models
{
    public class Car_img
    {
        [Key]
        public int img_id { get; set; }
        public string img_name { get; set; }
        public string img_url { get; set; }

        public int car_id { get; set; }
        [ForeignKey("car_id")]
        public  virtual Car Car { get; set; }
    }
}
