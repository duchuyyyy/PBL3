using MyOToVer1._2.Data;

namespace MyOToVer1._2.Models.DataModels
{
    public class OwnerIdentityPhotoModel
    {
        private ApplicationDBContext db;
        public OwnerIdentityPhotoModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public void AddImg(OwnerIdentityPhoto obj)
        {
            db.OwnerIdentityPhotos.Add(obj);
            db.SaveChanges();
        }

        public List<OwnerIdentityPhoto> GetPhotoByOwnerId(List<Car> cars)
        {
            return (from car1 in cars
                    join ownerPhotos in db.OwnerIdentityPhotos on car1.owner_id equals ownerPhotos.OwnerId
                    select new OwnerIdentityPhoto
                    {
                       NameImg = ownerPhotos.NameImg,
                       OwnerId = ownerPhotos.OwnerId                       
                    }).ToList();
        }
    }
}
