namespace MyOToVer1._2.Models.ViewModels
{
    public class CarRentalCarCus
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarAddress { get; set; }

        public DateTime Rental { get; set; }
        public DateTime Return { get; set; }

        public int DepositStatus { get; set; }

        public double Price { get; set; }

        public string Name_img { get; set; }
    }
}
