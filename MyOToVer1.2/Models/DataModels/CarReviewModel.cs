using MyOToVer1._2.Data;


namespace MyOToVer1._2.Models.DataModels
{
    public class CarReviewModel
    {
        private ApplicationDBContext db;

        public CarReviewModel(ApplicationDBContext db)
        {
            this.db = db;
        }
        public void AddCarReview(CarReview carReview)
        {
            db.CarReviews.Add(carReview);
            db.SaveChanges();
        }
        public List<CarReview> FindCarReviewByCarId(int carId)
        {
            return db.CarReviews.Where(p => p.car_id == carId).ToList();
        }

        public double CalcAVG(int carId)
        {
            return db.CarReviews.Where(p => p.car_id == carId).Average(p => p.ReviewScore);
        }
    }
}
