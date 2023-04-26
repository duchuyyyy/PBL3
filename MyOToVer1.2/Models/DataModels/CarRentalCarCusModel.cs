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
            return (from cr in db.CarRentals
                    where cr.customer_id == id && cr.deposit_status == 1
                    join c in db.Cars on cr.car_id equals c.car_id
                    join ci in db.CarImgs on cr.car_id equals ci.car_id
                    group ci by new
                    {
                        cr.car_id,
                        c.car_name,
                        c.car_brand,
                        c.car_model_year,
                        c.car_capacity,
                        c.car_address,
                        cr.rental_datetime,
                        cr.return_datetime,
                        cr.deposit_status,
                        cr.total_price
                    } into g
                    select new CarRentalCarCus
                    {
                        CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                        CarAddress = g.Key.car_address,
                        Rental = g.Key.rental_datetime,
                        Return = g.Key.return_datetime,
                        DepositStatus = g.Key.deposit_status,
                        Price = g.Key.total_price,
                        Name_img = g.Select(ci => ci.name_img).FirstOrDefault()
                    }).ToList();
        }

        public List<CarRentalCarCus> GetListConfirmed(int id)
        {
            return (from cr in db.CarRentals
                    where cr.customer_id == id && cr.deposit_status == 2 && cr.rental_status != 4 && cr.rental_status != 3
                    join c in db.Cars on cr.car_id equals c.car_id
                    join ci in db.CarImgs on cr.car_id equals ci.car_id
                    group ci by new
                    {
                        cr.car_id,
                        c.car_name,
                        c.car_brand,
                        c.car_model_year,
                        c.car_capacity,
                        c.car_address,
                        cr.rental_datetime,
                        cr.return_datetime,
                        cr.deposit_status,
                        cr.total_price
                    } into g
                    select new CarRentalCarCus
                    {
                        CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                        CarAddress = g.Key.car_address,
                        Rental = g.Key.rental_datetime,
                        Return = g.Key.return_datetime,
                        DepositStatus = g.Key.deposit_status,
                        Price = g.Key.total_price,
                        Name_img = g.Select(ci => ci.name_img).FirstOrDefault()
                    }).ToList();
        }

        public List<CarRentalCarCus> GetListOrderIsCompleting(int id)
        {
            return (from cr in db.CarRentals
                    where cr.customer_id == id && cr.deposit_status == 2 && cr.rental_status == 3
                    join c in db.Cars on cr.car_id equals c.car_id
                    join ci in db.CarImgs on cr.car_id equals ci.car_id
                    group ci by new
                    {
                        cr.car_id,
                        c.car_name,
                        c.car_brand,
                        c.car_model_year,
                        c.car_capacity,
                        c.car_address,
                        cr.rental_datetime,
                        cr.return_datetime,
                        cr.deposit_status,
                        cr.total_price
                    } into g
                    select new CarRentalCarCus
                    {
                        CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                        CarAddress = g.Key.car_address,
                        Rental = g.Key.rental_datetime,
                        Return = g.Key.return_datetime,
                        DepositStatus = g.Key.deposit_status,
                        Price = g.Key.total_price,
                        Name_img = g.Select(ci => ci.name_img).FirstOrDefault()
                    }).ToList();
        }

        public List<CarRentalCarCus> GetListOrderCompleted(int id)
        {
            return (from cr in db.CarRentals
                    where cr.customer_id == id && cr.deposit_status == 2 && cr.rental_status == 4
                    join c in db.Cars on cr.car_id equals c.car_id
                    join ci in db.CarImgs on cr.car_id equals ci.car_id
                    group ci by new
                    {
                        cr.customer_id,
                        cr.car_id,
                        c.car_name,
                        c.car_brand,
                        c.car_model_year,
                        c.car_capacity,
                        c.car_address,
                        cr.rental_datetime,
                        cr.return_datetime,
                        cr.deposit_status,
                        cr.total_price
                    } into g
                    select new CarRentalCarCus
                    {
                        CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                        CarAddress = g.Key.car_address,
                        Rental = g.Key.rental_datetime,
                        Return = g.Key.return_datetime,
                        DepositStatus = g.Key.deposit_status,
                        Price = g.Key.total_price,
                        Name_img = g.Select(ci => ci.name_img).FirstOrDefault(),
                        CustomerId = g.Key.customer_id,
                        CarId = g.Key.car_id
                    }).ToList();
        }
    }
}
