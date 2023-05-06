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
            return db.CarRentals
                .Where(rental => rental.deposit_status == 1 && EF.Functions.DateDiffDay(rental.booking_at, DateTime.Now) > 1)
                .Join(db.Cars, rental => rental.car_id, car => car.car_id, (rental, car) => new { rental, car })
                .Join(db.Owners, rc => rc.car.owner_id, owner => owner.Id, (rc, owner) => new { rc.rental, rc.car, owner })
                .Join(db.Customers, rco => rco.rental.customer_id, customer => customer.Id, (rco, customer) => new { rco.rental, rco.car, rco.owner, customer })
                .Join(db.TransferEvidencePhotos, rco => rco.rental.rental_id, evidencePhoto => evidencePhoto.rental_id, (rco, evidencePhoto) => new { rco.rental, rco.car, rco.owner, rco.customer, evidencePhoto })
                .Select(rcoe => new CarRentalBeReportedViewModel
                {
                    owner_id = rcoe.car.owner_id,
                    name_img = rcoe.evidencePhoto.name_img,
                    car_name = rcoe.car.car_brand + " " + rcoe.car.car_name + " " + rcoe.car.car_capacity + " Chỗ " + rcoe.car.car_model_year,
                    owner_name = rcoe.car.Owner.Customer.Name,
                    owner_contact = rcoe.car.Owner.Customer.Contact,
                    owner_status = rcoe.owner.owner_status,
                    car_address = rcoe.car.car_address,
                    car_number_rented = rcoe.car.car_number_rented,
                    customer_name = rcoe.customer.Name,
                    customer_contact = rcoe.customer.Contact,
                    rental_date_time = rcoe.rental.rental_datetime,
                    return_date_time = rcoe.rental.return_datetime,
                    booking_at = rcoe.rental.booking_at
                    
                })
                .ToList();

        }


    }
}
