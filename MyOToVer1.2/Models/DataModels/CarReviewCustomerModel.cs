using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Models.DataModels
{
    public class CarReviewCustomerModel
    {
        private ApplicationDBContext db;

        public CarReviewCustomerModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public List<CarReviewCustomerViewModel> GetReviewByCar(List<Car> cars)
        {
            return db.CarReviews
                .Join(cars, review => review.car_id, car => car.car_id, (review, car) => new { Review = review, Car = car })
                .Join(db.Customers, rc => rc.Review.CustomerID, customer => customer.Id, (rc, customer) => new { Review = rc.Review, Car = rc.Car, Customer = customer })
                .Select(rc => new CarReviewCustomerViewModel
                {
                    ReviewContent = rc.Review.ReviewContent,
                    ReviewDate = rc.Review.ReviewDate,
                    ReviewScore = rc.Review.ReviewScore,
                    CustomerName = rc.Customer.Name,
                    CarId = rc.Car.car_id
                })
                .ToList();
        }
        public List<CarReviewCustomerViewModel> GetReviewScore(List<Car> cars)
        {
            return db.Cars
                .Join(db.CarReviews, car => car.car_id, review => review.car_id, (car, review) => new { car, review })
                .Where(x => cars.Any(c => c.car_id == x.car.car_id))
                .OrderBy(x => x.car.car_id)
                .Select(x => new CarReviewCustomerViewModel
                {
                    CarId = x.car.car_id,
                    ReviewScore = x.review.ReviewScore
                })
                .ToList();
        }

    }
}
