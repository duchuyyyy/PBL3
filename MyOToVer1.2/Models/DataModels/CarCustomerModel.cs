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
            return db.Cars
                .Where(c => c.car_status == true && c.is_accept == true)
                .Join(db.Owners, c => c.owner_id, o => o.Id, (c, o) => new { Car = c, Owner = o })
                .Join(db.Customers, co => co.Owner.Id, cu => cu.Id, (co, cu) => new CarCustomerViewModel
                {
                    Car = co.Car,
                    Customer = cu
                })
                .ToList();
        }

        public List<CarCustomerViewModel> GetListCarPauseToRent()
        {
            return db.Cars
                .Where(c => c.car_status == false && c.is_accept == true)
                .Join(db.Owners, c => c.owner_id, o => o.Id, (c, o) => new { Car = c, Owner = o })
                .Join(db.Customers, co => co.Owner.Id, cus => cus.Id, (co, cus) => new CarCustomerViewModel 
                { 
                    Car = co.Car, 
                    Customer = cus 
                })
                .Select(x=> new CarCustomerViewModel
                {
                   
                })
                .ToList();
        }

    }
}
