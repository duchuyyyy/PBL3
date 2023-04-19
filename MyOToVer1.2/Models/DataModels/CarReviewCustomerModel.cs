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
            return (from car1 in cars
                    join review in db.CarReviews on car1.car_id equals review.car_id
                    join customer in db.Customers on review.CustomerID equals customer.Id
                    select new CarReviewCustomerViewModel
                    {
                        ReviewContent = review.ReviewContent,
                        ReviewDate = review.ReviewDate,
                        ReviewScore = review.ReviewScore,
                        CustomerName = customer.Name,
                        CarId = car1.car_id
                    }).ToList(); ;
        }      
    }
}
