using MyOToVer1._2.Data;

namespace MyOToVer1._2.Models.DataModels
{
    public class AdminModel
    {
        private ApplicationDBContext db;

        public AdminModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public bool isAdmin(string contact, string password)
        {
            return db.Admins.Any(p => p.Contact== contact && p.Password == password);
        }
    }
}
