using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyOToVer1._2.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Số điện thoại phải có 10 số")]
        public string? Contact { get; set; }

        [Required(ErrorMessage = "Mật khẩu kông được bỏ trống")]
        public string? Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<CarReview> CarReviews { get; set; }

        public int AdminId { get; set; }
        [ForeignKey("AdminId")]

        public virtual Admin Admin { get; set; }
    }
}
