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
            return db.Cars
                .Join(db.Owners, car => car.owner_id, owner => owner.Id, (car, owner) => new { car, owner })
                .Join(db.CarRentals, co => co.car.car_id, rental => rental.car_id, (co, rental) => new { co.car, co.owner, rental })
                .Where(corr => corr.rental.deposit_status == 1 && corr.owner.Id == id)
                .Select(corr => new CarRentalCarOwn
                {
                    rentalId = corr.rental.rental_id,
                    CarName = corr.car.car_name + " " + corr.car.car_brand + " " + corr.car.car_model_year + " " + corr.car.car_capacity,
                    RentalDateTime = corr.rental.rental_datetime,
                    ReturnDateTime = corr.rental.return_datetime,
                    CustomerName = corr.rental.customer.Name,
                    CustomerContact = corr.rental.customer.Contact,
                    Price = corr.rental.total_price
                })
                .Distinct()
                .ToList();
        }


        public List<CarRentalCarOwn> GetListConfirmed(int id)
        {
            return db.Cars
                .Join(db.Owners, car => car.owner_id, owner => owner.Id, (car, owner) => new { car, owner })
                .Join(db.CarRentals, co => co.car.car_id, rental => rental.car_id, (co, rental) => new { co.car, co.owner, rental })
                .Where(corr => corr.rental.deposit_status == 2 && corr.rental.rental_status == 2 && corr.owner.Id == id)
                .Select(corr => new CarRentalCarOwn
                {
                    rentalId = corr.rental.rental_id,
                    CarName = corr.car.car_name + " " + corr.car.car_brand + " " + corr.car.car_model_year + " " + corr.car.car_capacity,
                    RentalDateTime = corr.rental.rental_datetime,
                    ReturnDateTime = corr.rental.return_datetime,
                    CustomerName = corr.rental.customer.Name,
                    CustomerContact = corr.rental.customer.Contact,
                    Price = corr.rental.total_price
                })
                .Distinct()
                .ToList();
        }


        public List<CarRentalCarOwn> GetListWaiToHandOverCar(int id)
        {
            return db.Cars
                .Join(db.Owners, car => car.owner_id, owner => owner.Id, (car, owner) => new { car, owner })
                .Join(db.CarRentals, co => co.car.car_id, rental => rental.car_id, (co, rental) => new { co.car, co.owner, rental })
                .Where(corr => corr.rental.rental_status == 3 && corr.owner.Id == id)
                .Select(corr => new CarRentalCarOwn
                {
                    carid = corr.car.car_id,
                    rentalId = corr.rental.rental_id,
                    CarName = corr.car.car_name + " " + corr.car.car_brand + " " + corr.car.car_model_year + " " + corr.car.car_capacity,
                    RentalDateTime = corr.rental.rental_datetime,
                    ReturnDateTime = corr.rental.return_datetime,
                    CustomerName = corr.rental.customer.Name,
                    CustomerContact = corr.rental.customer.Contact,
                    Price = corr.rental.total_price
                })
                .Distinct()
                .ToList();
        }


        public List<CarRentalCarOwn> GetListOrderCompleted(int id)
        {
            return db.Cars
                .Join(db.Owners, car => car.owner_id, owner => owner.Id, (car, owner) => new { car, owner })
                .Join(db.CarRentals, co => co.car.car_id, rental => rental.car_id, (co, rental) => new { co.car, co.owner, rental })
                .Where(corr => corr.rental.rental_status == 4 && corr.owner.Id == id)
                .Select(corr => new CarRentalCarOwn
                {
                    rentalId = corr.rental.rental_id,
                    CarName = corr.car.car_name + " " + corr.car.car_brand + " " + corr.car.car_model_year + " " + corr.car.car_capacity,
                    RentalDateTime = corr.rental.rental_datetime,
                    ReturnDateTime = corr.rental.return_datetime,
                    CustomerName = corr.rental.customer.Name,
                    CustomerContact = corr.rental.customer.Contact,
                    Price = corr.rental.total_price
                })
                .Distinct()
                .ToList();
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
