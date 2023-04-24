

namespace MyOToVer1._2.Models.ViewModels
{
    public class CarRentalCarOwn
    {
        public int rentalId { get; set; }
        public string CarName { get; set; }
        public DateTime RentalDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }

        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }

        public double Price { get; set; }
        public string NameImg { get; set; }
    }
}
