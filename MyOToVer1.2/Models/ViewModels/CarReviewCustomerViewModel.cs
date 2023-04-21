namespace MyOToVer1._2.Models.ViewModels
{
    public class CarReviewCustomerViewModel
    {
        public string ReviewContent { get; set; }
        public DateTime ReviewDate { get; set; }
        public double ReviewScore { get; set; }
        public string CustomerName { get; set; }

        public int CarId { get; set; }
    }
}
