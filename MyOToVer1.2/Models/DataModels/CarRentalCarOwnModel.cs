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
            return (from car in db.Cars
             join owner in db.Owners on car.owner_id equals id
             join carrental in db.CarRentals on car.car_id equals carrental.car_id
             join carimg in db.CarImgs on car.car_id equals carimg.car_id
             where car.car_number_rented != 0 && carrental.deposit_status == 1
             group carimg by new 
             {
                 car.car_id,
                 car.car_name,
                 car.car_brand,
                 car.car_price,
                 car.car_model_year,
                 car.car_capacity,
                 carrental.rental_id,
                 carrental.rental_datetime,
                 carrental.return_datetime,
                 carrental.total_price,
                 carrental.customer.Name,
                 carrental.customer.Contact,
                 carimg.name_img,
             } into g
             select new CarRentalCarOwn
             {
                 rentalId = g.Key.rental_id,
                 CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                 RentalDateTime = g.Key.rental_datetime,
                 ReturnDateTime = g.Key.return_datetime,
                 CustomerName = g.Key.Name,
                 CustomerContact = g.Key.Contact,
                 Price = g.Key.total_price,
                 NameImg = g.Select(carimg=>carimg.name_img).FirstOrDefault()
             }).Distinct().ToList();
        }

        public List<CarRentalCarOwn> GetListConfirmed(int id)
        {
            return (from car in db.Cars
                    join owner in db.Owners on car.owner_id equals id
                    join carrental in db.CarRentals on car.car_id equals carrental.car_id
                    join carimg in db.CarImgs on car.car_id equals carimg.car_id
                    where car.car_number_rented != 0 && carrental.deposit_status == 2 && carrental.rental_status == 2
                    group carimg by new
                    {
                        car.car_id,
                        car.car_name,
                        car.car_brand,
                        car.car_price,
                        car.car_model_year,
                        car.car_capacity,
                        carrental.rental_id,
                        carrental.rental_datetime,
                        carrental.return_datetime,
                        carrental.total_price,
                        carrental.customer.Name,
                        carrental.customer.Contact,
                        carimg.name_img,
                    } into g
                    select new CarRentalCarOwn
                    {
                        rentalId = g.Key.rental_id,
                        CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                        RentalDateTime = g.Key.rental_datetime,
                        ReturnDateTime = g.Key.return_datetime,
                        CustomerName = g.Key.Name,
                        CustomerContact = g.Key.Contact,
                        Price = g.Key.total_price,
                        NameImg = g.Select(carimg => carimg.name_img).FirstOrDefault()
                    }).Distinct().ToList();
        }

        public List<CarRentalCarOwn> GetListWaiToHandOverCar(int id)
        {
            return (from car in db.Cars
                    join owner in db.Owners on car.owner_id equals id
                    join carrental in db.CarRentals on car.car_id equals carrental.car_id
                    join carimg in db.CarImgs on car.car_id equals carimg.car_id
                    where car.car_number_rented != 0 && carrental.rental_status == 3
                    group carimg by new
                    {
                        car.car_id,
                        car.car_name,
                        car.car_brand,
                        car.car_price,
                        car.car_model_year,
                        car.car_capacity,
                        carrental.rental_id,
                        carrental.rental_datetime,
                        carrental.return_datetime,
                        carrental.total_price,
                        carrental.customer.Name,
                        carrental.customer.Contact,
                        carimg.name_img,
                    } into g
                    select new CarRentalCarOwn
                    {
                        rentalId = g.Key.rental_id,
                        CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                        RentalDateTime = g.Key.rental_datetime,
                        ReturnDateTime = g.Key.return_datetime,
                        CustomerName = g.Key.Name,
                        CustomerContact = g.Key.Contact,
                        Price = g.Key.total_price,
                        NameImg = g.Select(carimg => carimg.name_img).FirstOrDefault()
                    }).Distinct().ToList();
        }

        public List<CarRentalCarOwn> GetListOrderCompleted(int id)
        {
            return (from car in db.Cars
                    join owner in db.Owners on car.owner_id equals id
                    join carrental in db.CarRentals on car.car_id equals carrental.car_id
                    join carimg in db.CarImgs on car.car_id equals carimg.car_id
                    where car.car_number_rented != 0 && carrental.rental_status == 4
                    group carimg by new
                    {
                        car.car_id,
                        car.car_name,
                        car.car_brand,
                        car.car_price,
                        car.car_model_year,
                        car.car_capacity,
                        carrental.rental_id,
                        carrental.rental_datetime,
                        carrental.return_datetime,
                        carrental.total_price,
                        carrental.customer.Name,
                        carrental.customer.Contact,
                        carimg.name_img,
                    } into g
                    select new CarRentalCarOwn
                    {
                        rentalId = g.Key.rental_id,
                        CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                        RentalDateTime = g.Key.rental_datetime,
                        ReturnDateTime = g.Key.return_datetime,
                        CustomerName = g.Key.Name,
                        CustomerContact = g.Key.Contact,
                        Price = g.Key.total_price,
                        NameImg = g.Select(carimg => carimg.name_img).FirstOrDefault()
                    }).Distinct().ToList();
        }
    }
}
