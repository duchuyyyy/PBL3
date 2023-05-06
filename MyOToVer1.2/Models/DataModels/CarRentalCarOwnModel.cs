using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;
using System.Security.Authentication;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarRentalCarOwnModel
    {
        private ApplicationDBContext db;

        public CarRentalCarOwnModel(ApplicationDBContext db)
        {
            this.db = db;
        }
        public List<CarRentalCarOwn> GetListNotConfirm(int id)
        {
            return db.Cars
                .Where(car => car.owner_id == id && car.car_number_rented != 0)
                .Join(db.CarRentals.Where(rental => rental.rental_status == 1), car => car.car_id, rental => rental.car_id, (car, rental) => new { Car = car, Rental = rental })
                .Join(db.CarImgs, cr => cr.Car.car_id, img => img.car_id, (cr, img) => new { CarRental = cr, CarImg = img })
                .GroupBy(g => new
                {
                    g.CarRental.Car.car_id,
                    g.CarRental.Car.car_name,
                    g.CarRental.Car.car_brand,
                    g.CarRental.Car.car_price,
                    g.CarRental.Car.car_model_year,
                    g.CarRental.Car.car_capacity,
                    g.CarRental.Rental.rental_id,
                    g.CarRental.Rental.rental_datetime,
                    g.CarRental.Rental.return_datetime,
                    g.CarRental.Rental.total_price,
                    g.CarRental.Rental.customer.Name,
                    g.CarRental.Rental.customer.Contact,
                    g.CarImg.name_img,
                })
                .Select(g => new CarRentalCarOwn
                {
                    rentalId = g.Key.rental_id,
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    RentalDateTime = g.Key.rental_datetime,
                    ReturnDateTime = g.Key.return_datetime,
                    CustomerName = g.Key.Name,
                    CustomerContact = g.Key.Contact,
                    Price = g.Key.total_price,
                    NameImg = g.Select(carimg => carimg.CarImg.name_img).FirstOrDefault()
                })
                .Distinct()
                .ToList();
        }

        public List<CarRentalCarOwn> GetListConfirmed(int id)
        {
            return db.Cars
                .Where(car => car.owner_id == id && car.car_number_rented != 0)
                .Join(db.CarRentals.Where(rental => rental.deposit_status == 2 && rental.rental_status == 2), car => car.car_id, rental => rental.car_id, (car, rental) => new { Car = car, Rental = rental })
                .Join(db.CarImgs, cr => cr.Car.car_id, img => img.car_id, (cr, img) => new { CarRental = cr, CarImg = img })
                .GroupBy(g => new
                {
                    g.CarRental.Car.car_id,
                    g.CarRental.Car.car_name,
                    g.CarRental.Car.car_brand,
                    g.CarRental.Car.car_price,
                    g.CarRental.Car.car_model_year,
                    g.CarRental.Car.car_capacity,
                    g.CarRental.Rental.rental_id,
                    g.CarRental.Rental.rental_datetime,
                    g.CarRental.Rental.return_datetime,
                    g.CarRental.Rental.total_price,
                    g.CarRental.Rental.customer.Name,
                    g.CarRental.Rental.customer.Contact,
                    g.CarImg.name_img,
                })
                .Select(g => new CarRentalCarOwn
                {
                    rentalId = g.Key.rental_id,
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    RentalDateTime = g.Key.rental_datetime,
                    ReturnDateTime = g.Key.return_datetime,
                    CustomerName = g.Key.Name,
                    CustomerContact = g.Key.Contact,
                    Price = g.Key.total_price,
                    NameImg = g.Select(carimg => carimg.CarImg.name_img).FirstOrDefault()
                })
                .Distinct()
                .ToList();
        }

        public List<CarRentalCarOwn> GetListWaiToHandOverCar(int id)
        {

            return db.Cars
                .Where(car => car.owner_id == id && car.car_number_rented != 0)
                .Join(db.CarRentals.Where(rental =>  rental.rental_status == 3), car => car.car_id, rental => rental.car_id, (car, rental) => new { Car = car, Rental = rental })
                .Join(db.CarImgs, cr => cr.Car.car_id, img => img.car_id, (cr, img) => new { CarRental = cr, CarImg = img })
                .GroupBy(g => new
                {
                    g.CarRental.Car.car_id,
                    g.CarRental.Car.car_name,
                    g.CarRental.Car.car_brand,
                    g.CarRental.Car.car_price,
                    g.CarRental.Car.car_model_year,
                    g.CarRental.Car.car_capacity,
                    g.CarRental.Rental.rental_id,
                    g.CarRental.Rental.rental_datetime,
                    g.CarRental.Rental.return_datetime,
                    g.CarRental.Rental.total_price,
                    g.CarRental.Rental.customer.Name,
                    g.CarRental.Rental.customer.Contact,
                    g.CarImg.name_img,
                })
                .Select(g => new CarRentalCarOwn
                {
                    rentalId = g.Key.rental_id,
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    RentalDateTime = g.Key.rental_datetime,
                    ReturnDateTime = g.Key.return_datetime,
                    CustomerName = g.Key.Name,
                    CustomerContact = g.Key.Contact,
                    Price = g.Key.total_price,
                    NameImg = g.Select(carimg => carimg.CarImg.name_img).FirstOrDefault()
                })
                .Distinct()
                .ToList();
        }

        public List<CarRentalCarOwn> GetListOrderCompleted(int id)
        {

            return db.Cars
                .Where(car => car.owner_id == id && car.car_number_rented != 0)
                .Join(db.CarRentals.Where(rental => rental.rental_status == 4), car => car.car_id, rental => rental.car_id, (car, rental) => new { Car = car, Rental = rental })
                .Join(db.CarImgs, cr => cr.Car.car_id, img => img.car_id, (cr, img) => new { CarRental = cr, CarImg = img })
                .GroupBy(g => new
                {
                    g.CarRental.Car.car_id,
                    g.CarRental.Car.car_name,
                    g.CarRental.Car.car_brand,
                    g.CarRental.Car.car_price,
                    g.CarRental.Car.car_model_year,
                    g.CarRental.Car.car_capacity,
                    g.CarRental.Rental.rental_id,
                    g.CarRental.Rental.rental_datetime,
                    g.CarRental.Rental.return_datetime,
                    g.CarRental.Rental.total_price,
                    g.CarRental.Rental.customer.Name,
                    g.CarRental.Rental.customer.Contact,
                    g.CarImg.name_img,
                })
                .Select(g => new CarRentalCarOwn
                {
                    rentalId = g.Key.rental_id,
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    RentalDateTime = g.Key.rental_datetime,
                    ReturnDateTime = g.Key.return_datetime,
                    CustomerName = g.Key.Name,
                    CustomerContact = g.Key.Contact,
                    Price = g.Key.total_price,
                    NameImg = g.Select(carimg => carimg.CarImg.name_img).FirstOrDefault()
                })
                .Distinct()
                .ToList();
        }
        public List<CarRentalCarOwn> GetListOrderBeCanceled(int id)
        {

            return db.Cars
                .Where(car => car.owner_id == id )
                .Join(db.CarRentals.Where(rental => rental.rental_status == -1), car => car.car_id, rental => rental.car_id, (car, rental) => new { Car = car, Rental = rental })
                .Join(db.CarImgs, cr => cr.Car.car_id, img => img.car_id, (cr, img) => new { CarRental = cr, CarImg = img })
                .GroupBy(g => new
                {
                    g.CarRental.Car.car_id,
                    g.CarRental.Car.car_name,
                    g.CarRental.Car.car_brand,
                    g.CarRental.Car.car_price,
                    g.CarRental.Car.car_model_year,
                    g.CarRental.Car.car_capacity,
                    g.CarRental.Rental.rental_id,
                    g.CarRental.Rental.rental_datetime,
                    g.CarRental.Rental.return_datetime,
                    g.CarRental.Rental.total_price,
                    g.CarRental.Rental.customer.Name,
                    g.CarRental.Rental.customer.Contact,
                    g.CarImg.name_img,
                })
                .Select(g => new CarRentalCarOwn
                {
                    rentalId = g.Key.rental_id,
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    RentalDateTime = g.Key.rental_datetime,
                    ReturnDateTime = g.Key.return_datetime,
                    CustomerName = g.Key.Name,
                    CustomerContact = g.Key.Contact,
                    Price = g.Key.total_price,
                    NameImg = g.Select(carimg => carimg.CarImg.name_img).FirstOrDefault()
                })
                .Distinct()
                .ToList();
        }
    }
}
