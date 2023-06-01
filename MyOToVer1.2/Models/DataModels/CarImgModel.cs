using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarImgModel
    {
        private ApplicationDBContext db;
        public CarImgModel(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void AddImg(CarImg obj)
        {
            db.CarImgs.Add(obj);
            db.SaveChanges();
        }
        public List<CarImg> FindImageByCar(List<Car> car)
        {
            return (from car1 in car
                   join img1 in db.CarImgs on car1.car_id equals img1.car_id
                   select new CarImg
                   {
                       car_id = img1.car_id,
                       name_img = img1.name_img
                   }).ToList(); 
        }
        public List<CarImg> FindImageByCarOwnCus(List<CarOwnerCustomer> car)
        {
            return (from car1 in car
                    join img1 in db.CarImgs on car1.Car.car_id equals img1.car_id
                    select new CarImg
                    {
                        car_id = img1.car_id,
                        name_img = img1.name_img
                    }).ToList();
        }
        public List<CarImg> FindImageByCarCus(List<CarCustomerViewModel> car)
        {
            return (from car1 in car
                    join img1 in db.CarImgs on car1.Car.car_id equals img1.car_id
                    select new CarImg
                    {
                        car_id = img1.car_id,
                        name_img = img1.name_img
                    }).ToList();
        }
        public CarImg BannerImg(int Car_id)
        {
            return db.CarImgs.Where(p => p.car_id.Equals(Car_id)).First();
        }
    }
}