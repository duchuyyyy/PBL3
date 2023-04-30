using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

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
            return (from car in db.Cars
             join owner in db.Owners on car.owner_id equals id
             join carrental in db.CarRentals on car.car_id equals carrental.car_id
             where carrental.deposit_status == 1
             select new CarRentalCarOwn
             {
                 rentalId = carrental.rental_id,
                 CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                 RentalDateTime = carrental.rental_datetime,
                 ReturnDateTime = carrental.return_datetime,
                 CustomerName = carrental.customer.Name,
                 CustomerContact = carrental.customer.Contact,
                 Price = carrental.total_price
             }).Distinct().ToList();
        }

        public List<CarRentalCarOwn> GetListConfirmed(int id)
        {
            return (from car in db.Cars
             join owner in db.Owners on car.owner_id equals id
             join carrental in db.CarRentals on car.car_id equals carrental.car_id
             where carrental.deposit_status == 2 && carrental.rental_status == 2
             select new CarRentalCarOwn
             {
                 rentalId = carrental.rental_id,
                 CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                 RentalDateTime = carrental.rental_datetime,
                 ReturnDateTime = carrental.return_datetime,
                 CustomerName = carrental.customer.Name,
                 CustomerContact = carrental.customer.Contact,
                 Price = carrental.total_price
             }).Distinct().ToList();
        }

        public List<CarRentalCarOwn> GetListWaiToHandOverCar(int id)
        {
            return (from car in db.Cars
             join owner in db.Owners on car.owner_id equals id
             join carrental in db.CarRentals on car.car_id equals carrental.car_id
             where  carrental.rental_status == 3
             select new CarRentalCarOwn
             {
                 carid = car.car_id,
                 rentalId = carrental.rental_id,
                 CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                 RentalDateTime = carrental.rental_datetime,
                 ReturnDateTime = carrental.return_datetime,
                 CustomerName = carrental.customer.Name,
                 CustomerContact = carrental.customer.Contact,
                 Price = carrental.total_price
             }).Distinct().ToList();
        }

        public List<CarRentalCarOwn> GetListOrderCompleted(int id)
        {
            return (from car in db.Cars
                    join owner in db.Owners on car.owner_id equals id
                    join carrental in db.CarRentals on car.car_id equals carrental.car_id
                    where carrental.rental_status == 4
                    select new CarRentalCarOwn
                    {
                        rentalId = carrental.rental_id,
                        CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                        RentalDateTime = carrental.rental_datetime,
                        ReturnDateTime = carrental.return_datetime,
                        CustomerName = carrental.customer.Name,
                        CustomerContact = carrental.customer.Contact,
                        Price = carrental.total_price
                    }).Distinct().ToList();
        }

        public List<CarRentalCarOwn> GetListOrderBeCanceled(int id)
        {
            return (from car in db.Cars
                    join owner in db.Owners on car.owner_id equals id
                    join carrental in db.CarRentals on car.car_id equals carrental.car_id
                    where carrental.rental_status == -1
                    select new CarRentalCarOwn
                    {
                        rentalId = carrental.rental_id,
                        CarName = car.car_name + " " + car.car_brand + " " + car.car_model_year + " " + car.car_capacity,
                        RentalDateTime = carrental.rental_datetime,
                        ReturnDateTime = carrental.return_datetime,
                        CustomerName = carrental.customer.Name,
                        CustomerContact = carrental.customer.Contact,
                        Price = carrental.total_price
                    }).Distinct().ToList();
        }
    }
}
