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
            return db.CarRentals
                .Where(cr => cr.customer_id == id && cr.deposit_status == 1)
                .Join(db.Cars, cr => cr.car_id, c => c.car_id, (cr, c) => new { cr, c })
                .Join(db.CarImgs, x => x.cr.car_id, ci => ci.car_id, (x, ci) => new { x.cr, x.c, ci })
                .GroupBy(x => new
                {
                    x.cr.car_id,
                    x.c.car_name,
                    x.c.car_brand,
                    x.c.car_model_year,
                    x.c.car_capacity,
                    x.c.car_address,
                    x.cr.rental_datetime,
                    x.cr.return_datetime,
                    x.cr.deposit_status,
                    x.cr.total_price
                })
                .Select(g => new CarRentalCarCus
                {
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    CarAddress = g.Key.car_address,
                    Rental = g.Key.rental_datetime,
                    Return = g.Key.return_datetime,
                    DepositStatus = g.Key.deposit_status,
                    Price = g.Key.total_price,
                    Name_img = g.Select(x => x.ci.name_img).FirstOrDefault()
                })
                .ToList();
        }
        public List<CarRentalCarCus> GetListConfirmed(int id)
        {
            return db.CarRentals
                .Where(cr => cr.customer_id == id && cr.deposit_status == 2 && cr.rental_status != 4 && cr.rental_status != 3 && cr.rental_status != -1
                && cr.rental_status != 0)
                .Join(db.Cars, cr => cr.car_id, c => c.car_id, (cr, c) => new { cr, c })
                .Join(db.CarImgs, cr_c => cr_c.c.car_id, ci => ci.car_id, (cr_c, ci) => new { cr_c.cr, cr_c.c, ci })
                .GroupBy(x => new
                {
                    x.cr.rental_id,
                    x.cr.car_id,
                    x.c.car_name,
                    x.c.car_brand,
                    x.c.car_model_year,
                    x.c.car_capacity,
                    x.c.car_address,
                    x.cr.rental_datetime,
                    x.cr.return_datetime,
                    x.cr.deposit_status,
                    x.cr.total_price
                })
                .Select(g => new CarRentalCarCus
                {
                    RentalId = g.Key.rental_id,
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    CarAddress = g.Key.car_address,
                    Rental = g.Key.rental_datetime,
                    Return = g.Key.return_datetime,
                    DepositStatus = g.Key.deposit_status,
                    Price = g.Key.total_price,
                    Name_img = g.Select(x => x.ci.name_img).FirstOrDefault()
                })
                .ToList();
        }

        public List<CarRentalCarCus> GetListOrderIsCompleting(int id)
        {
            return db.CarRentals
                .Where(cr => cr.customer_id == id && cr.deposit_status == 2 && cr.rental_status == 3)
                .Join(db.Cars, cr => cr.car_id, c => c.car_id, (cr, c) => new { cr, c })
                .Join(db.CarImgs, cc => cc.c.car_id, ci => ci.car_id, (cc, ci) => new { cc.cr, cc.c, ci })
                .GroupBy(x => new
                {
                    x.c.car_id,
                    x.c.car_name,
                    x.c.car_brand,
                    x.c.car_model_year,
                    x.c.car_capacity,
                    x.c.car_address,
                    x.cr.rental_datetime,
                    x.cr.return_datetime,
                    x.cr.deposit_status,
                    x.cr.total_price
                })
                .Select(g => new CarRentalCarCus
                {
                    CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                    CarAddress = g.Key.car_address,
                    Rental = g.Key.rental_datetime,
                    Return = g.Key.return_datetime,
                    DepositStatus = g.Key.deposit_status,
                    Price = g.Key.total_price,
                    Name_img = g.Select(x => x.ci.name_img).FirstOrDefault()
                })
                .ToList();
        }


        public List<CarRentalCarCus> GetListOrderCompleted(int id)
        {
            return db.CarRentals
             .Where(cr => cr.customer_id == id && cr.deposit_status == 2 && cr.rental_status == 4)
             .Join(db.Cars, cr => cr.car_id, c => c.car_id, (cr, c) => new { cr, c })
             .Join(db.CarImgs, cc => cc.c.car_id, ci => ci.car_id, (cc, ci) => new { cc.cr, cc.c, ci })
             .GroupBy(x => new
             {
                 x.c.car_id,
                 x.c.car_name,
                 x.c.car_brand,
                 x.c.car_model_year,
                 x.c.car_capacity,
                 x.c.car_address,
                 x.cr.rental_datetime,
                 x.cr.return_datetime,
                 x.cr.deposit_status,
                 x.cr.total_price
             })
             .Select(g => new CarRentalCarCus
             {
                 CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                 CarAddress = g.Key.car_address,
                 Rental = g.Key.rental_datetime,
                 Return = g.Key.return_datetime,
                 DepositStatus = g.Key.deposit_status,
                 Price = g.Key.total_price,
                 Name_img = g.Select(x => x.ci.name_img).FirstOrDefault()
             })
             .ToList();
        }

        public List<CarRentalCarCus> GetListOrderBeCanceled(int id)
        {
            return db.CarRentals
            .Where(cr => cr.customer_id == id && cr.rental_status == -2 || cr.rental_status == -3)
            .Join(db.Cars, cr => cr.car_id, c => c.car_id, (cr, c) => new { cr, c })
            .Join(db.CarImgs, cc => cc.c.car_id, ci => ci.car_id, (cc, ci) => new { cc.cr, cc.c, ci })
            .GroupBy(x => new
            {
                x.c.car_id,
                x.c.car_name,
                x.c.car_brand,
                x.c.car_model_year,
                x.c.car_capacity,
                x.c.car_address,
                x.cr.rental_datetime,
                x.cr.return_datetime,
                x.cr.deposit_status,
                x.cr.total_price
            })
            .Select(g => new CarRentalCarCus
            {
                CarName = g.Key.car_name + " " + g.Key.car_brand + " " + g.Key.car_model_year + " " + g.Key.car_capacity,
                CarAddress = g.Key.car_address,
                Rental = g.Key.rental_datetime,
                Return = g.Key.return_datetime,
                DepositStatus = g.Key.deposit_status,
                Price = g.Key.total_price,
                Name_img = g.Select(x => x.ci.name_img).FirstOrDefault()
            })
            .ToList();
        }
    }
}
