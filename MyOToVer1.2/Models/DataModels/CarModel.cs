using MyOToVer1._2.Controllers;
using MyOToVer1._2.Data;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarModel
    {
        private ApplicationDBContext db;

        public CarModel(ApplicationDBContext db)
        {
            this.db = db;
        }
        
        public void AddCar(Car obj)
        {
            db.Cars.Add(obj);
            db.SaveChanges();
        }

        public bool IsValidCarNumber(string carNumber)
        {
            return db.Cars.Any(p => p.car_number.Equals(carNumber));
        }

        public List<Car> SearchCar(string location, DateTime rentalAt, DateTime returnAt, int id)
        {
           return  db.Cars.Where(p => p.car_number_rented == 0 ? p.car_address.Contains(location) : db.CarRentals
                         .Join(db.Cars, r => r.car_id, c => c.car_id, (r, c) => new { Rental = r, Car = c })
                         .Where(x => x.Car.car_address.Contains(location) && x.Car.owner_id != id)
                         .GroupBy(x => x.Rental.car_id)
                         .Select(g => new
                         {
                             car_id = g.Key,
                             rental_datetime = g.Min(o => o.Rental.rental_datetime),
                             return_datetime = g.Max(o => o.Rental.return_datetime)
                         })
                         .Where(x => !(x.rental_datetime < returnAt && x.return_datetime > rentalAt))
                         .Any(x => x.car_id == p.car_id))
                         .ToList();
        }

        public List<Car> OrderByAscPrice(string location, int price, string brand)
        {
            return db.Cars.OrderBy(p => p.car_price).Where(p => p.car_address.Contains(location) && p.car_price < price && p.car_brand.Equals(brand)).ToList();
        }

        public List<Car> OrderByAscPrice(string location, int price)
        {
            return db.Cars.OrderBy(p => p.car_price).Where(p => p.car_address.Contains(location) && p.car_price < price).ToList();
        }

        public List<Car> OrderByDescPrice(string location, int price, string brand)
        {
            return db.Cars.OrderByDescending(p => p.car_price).Where(p => p.car_address.Contains(location) && p.car_price < price && p.car_brand.Equals(brand)).ToList();
        }

        public List<Car> OrderByDescPrice(string location, int price)
        {
            return db.Cars.OrderByDescending(p => p.car_price).Where(p => p.car_address.Contains(location) && p.car_price < price).ToList();
        }
    }
}
