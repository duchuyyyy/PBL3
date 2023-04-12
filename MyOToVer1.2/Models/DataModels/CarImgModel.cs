using MyOToVer1._2.Data;

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
        public List<CarImg> Search(int Car_id)
        {
            return db.CarImgs.Where(p => p.car_id.Equals(Car_id)).ToList();
        }

        public CarImg BannerImg(int Car_id)
        {
            return db.CarImgs.Where(p => p.car_id.Equals(Car_id)).First();
        }
    }
}