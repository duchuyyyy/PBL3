using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public List<OwnerIdentityPhoto> GetIdentityPhotos(int OwnerID) 
        {
            return db.OwnerIdentityPhotos.Where(op=>op.OwnerId.Equals(OwnerID)).ToList();    
        }
    }
}
