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

        public void UpdateCar(Car obj)
        {
            db.Cars.Update(obj);
            db.SaveChanges();
        }

        public Car FindCarById(int carId)
        {
            return db.Cars.Find(carId);
        }

        public List<Car> GetAllCarsByOwnerId(int ownerid)
        {
           return db.Cars.Where(p => p.owner_id == ownerid && p.is_accept == true).ToList();
        }

        public List<Car> GetAllCarsById(int carId)
        {
            return db.Cars.Where(p => p.car_id == carId).ToList();
        }
        public bool IsValidCarNumber(string carNumber)
        {
            return db.Cars.Any(p => p.car_number.Equals(carNumber));
        }

        public List<Car> SearchCar(string location, DateTime rentalAt, DateTime returnAt, int id)
        {
            return db.Cars.Where(p => p.car_number_rented == 0 ? p.car_address.Contains(location) && p.is_accept == true && p.owner_id != id && p.Owner.owner_status == 2 : db.CarRentals
                         .Join(db.Cars, r => r.car_id, c => c.car_id, (r, c) => new { Rental = r, Car = c })
                         .Where(x => x.Car.car_address.Contains(location) && x.Car.owner_id != id && x.Car.car_status != false && x.Car.Owner.owner_status == 2)
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

        public List<Car> OrderByAscPrice(string location, int price, List<Car> car) 
        {
            return car.OrderBy(p => p.car_price).Where(p => p.car_address.Contains(location) && p.car_price < price).ToList();
        }

        public List<Car> OrderByDescPrice(string location, int price, List<Car> car)
        {
            return car.OrderByDescending(p => p.car_price).Where(p => p.car_address.Contains(location) && p.car_price < price).ToList();
        }

        public List<Car> FilterByBrand(string location, string brand, List<Car> car)
        {
            return car.Where(p => p.car_address.Contains(location) && p.car_brand.Equals(brand)).ToList();
        }

        public List<Car> FilterByPrice(string location, int price, List<Car> car)
        {
            return car.Where(p => p.car_address.Contains(location) && p.car_price <= price).ToList();
        }

        public List<Car> FilterByCapacity(string location, int capacity, List<Car> car)
        {
            return car.Where(p => p.car_capacity == capacity && p.car_address.Contains(location)).ToList();
        }

        public List<Car> GetListCarWaitAccept()
        {
            return db.Cars.Where(p => p.is_accept == false && p.accept_status == 0).ToList();
        }
        public List<Car> GetListCarWaitUpdate()
        {
            return db.Cars.Where(p => p.is_update == true ).ToList();
        }
    }
}
