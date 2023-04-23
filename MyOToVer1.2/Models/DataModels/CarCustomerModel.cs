using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarCustomerModel
    {
        private ApplicationDBContext db;

        public CarCustomerModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public List<CarCustomerViewModel> GetListCarRenting()
        {
            return (from car in db.Cars
                    join owner in db.Owners on car.owner_id  equals owner.Id
                    join customer in db.Customers on owner.Id equals customer.Id
                    where car.car_status == true && car.is_accept == true
                    select new CarCustomerViewModel
                    {
                        Car = car,
                        Customer = customer
                    }).ToList();
        }

        public List<CarCustomerViewModel> GetListCarPauseToRent()
        {
            return (from car in db.Cars
                    join owner in db.Owners on car.owner_id equals owner.Id
                    join customer in db.Customers on owner.Id equals customer.Id
                    where car.car_status == false && car.is_accept == true
                    select new CarCustomerViewModel
                    {
                        Car = car,
                        Customer = customer
                    }).ToList();
        }
    }
}
