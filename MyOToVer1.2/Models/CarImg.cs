using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOToVer1._2.Models
{
    public class CarImg
    {
        [Key]
        public int id_img { get; set; }
        [Required]
        public string name_img { get; set; }
        [Required]
        public int car_id { get; set; }
        [ForeignKey("car_id")]
        public virtual Car Car { get; set; }
    }
}
