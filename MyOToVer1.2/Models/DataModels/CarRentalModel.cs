using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarRentalModel
    {
        private ApplicationDBContext db;

        public CarRentalModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public void AddCarRental(CarRental carRental)
        {
            try
            {
                db.CarRentals.Add(carRental);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<CarRental> GetCarsByCarId(int carid)
        {
            return db.CarRentals.Where(p => p.car_id == carid && p.deposit_status != 1).ToList();
        }

        public bool isDuplicateCarRental(DateTime rentaldatetime, DateTime returndatetime, int customerid, int carid, double price)
        {
            return db.CarRentals.Any(p => p.rental_datetime ==  rentaldatetime && p.return_datetime == returndatetime && p.customer_id == customerid
            && p.car_id == carid && p.total_price == price);
        }

        public CarRental FindCarRentalById(int id)
        {
            return db.CarRentals.Find(id);
        }

        public void UpdateCarRental(CarRental obj)
        {
            db.CarRentals.Update(obj);
            db.SaveChanges();
        }


    }
}
