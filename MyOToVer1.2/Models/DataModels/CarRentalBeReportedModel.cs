using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarRentalBeReportedModel
    {
        private ApplicationDBContext db;

        public CarRentalBeReportedModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public List<CarRentalBeReportedViewModel> GetListCarRentalBeReported()
        {
            return (from rental in db.CarRentals
                    join car in db.Cars on rental.car_id equals car.car_id
                    join customer in db.Customers on rental.customer_id equals customer.Id
                    join evidencePhoto in db.TransferEvidencePhotos on rental.rental_id equals evidencePhoto.rental_id
                    where rental.deposit_status == 1 /*&& DbFunctions.DiffDays(rental.rental_datetime, DateTime.Now) > 1*/
                    select new CarRentalBeReportedViewModel
                    {
                        name_img = evidencePhoto.name_img,
                        car_name = car.car_brand + " " + car.car_name + " " + car.car_capacity + " Chỗ " + car.car_model_year,
                        owner_name = car.Owner.Customer.Name,
                        owner_contact = car.Owner.Customer.Contact,
                        car_address = car.car_address,
                        car_number_rented = car.car_number_rented,
                        customer_name = customer.Name,
                        customer_contact = customer.Contact,
                        rental_date_time = rental.rental_datetime,
                        return_date_time = rental.return_datetime
                    }).ToList();
        }

    }
}
