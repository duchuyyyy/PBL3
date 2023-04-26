namespace MyOToVer1._2.Models.ViewModels
{
    public class CarRentalBeReportedViewModel
    {
        public string car_name { get; set; }
        public string owner_name { get; set; }
        public string owner_contact { get; set; }
        public string car_address { get; set; }
        public string customer_name { get; set; }
        public string customer_contact { get; set; }

        public DateTime rental_date_time { get; set; }

        public DateTime return_date_time { get; set; }

        public int car_number_rented { get; set; }

        public string name_img { get; set; }
    }
}
