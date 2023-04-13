using MyOToVer1._2.Controllers;
using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarRentalCarCusModel
    {
        private ApplicationDBContext db;

        public CarRentalCarCusModel(ApplicationDBContext db)
        {
            this.db = db;
        }
        public List<CarRentalCarCus> GetListNotConfirmed(int id)
        {
            return db.CarRentals.Where(p => p.customer_id == id && p.deposit_status == 1).Select(p => new CarRentalCarCus
            {
                CarName = p.Car.car_name + " " + p.Car.car_brand + " " + p.Car.car_model_year + " " + p.Car.car_capacity,
                CarAddress = p.Car.car_address,
                Rental = p.rental_datetime,
                Return = p.return_datetime,
                DepositStatus = p.deposit_status,
                Price = p.total_price
            }).ToList();
        }

        public List<CarRentalCarCus> GetListConfirmed(int id)
        {
            return db.CarRentals.Where(p => p.customer_id == HomeController.id && p.deposit_status == 2 && p.rental_status != 4 && p.rental_status != 3).Select(p => new CarRentalCarCus
            {
                CarName = p.Car.car_name + " " + p.Car.car_brand + " " + p.Car.car_model_year + " " + p.Car.car_capacity,
                CarAddress = p.Car.car_address,
                Rental = p.rental_datetime,
                Return = p.return_datetime,
                DepositStatus = p.deposit_status,
                Price = p.total_price
            }).ToList();
        }

        public List<CarRentalCarCus> GetListOrderIsCompleting(int id)
        {
            return db.CarRentals.Where(p => p.customer_id == id && p.deposit_status == 2 && p.rental_status == 3).Select(p => new CarRentalCarCus
            {
                CarName = p.Car.car_name + " " + p.Car.car_brand + " " + p.Car.car_model_year + " " + p.Car.car_capacity,
                CarAddress = p.Car.car_address,
                Rental = p.rental_datetime,
                Return = p.return_datetime,
                DepositStatus = p.deposit_status,
                Price = p.total_price
            }).ToList();
        }

        public List<CarRentalCarCus> GetListOrderCompleted(int id)
        {
            return db.CarRentals.Where(p => p.customer_id == id && p.deposit_status == 2 && p.rental_status == 4).Select(p => new CarRentalCarCus
            {
                CarId = p.Car.car_id,
                CustomerId = p.customer_id,
                CarName = p.Car.car_name + " " + p.Car.car_brand + " " + p.Car.car_model_year + " " + p.Car.car_capacity,
                CarAddress = p.Car.car_address,
                Rental = p.rental_datetime,
                Return = p.return_datetime,
                DepositStatus = p.deposit_status,
                Price = p.total_price
            }).ToList();
        }
    }
}
