using MyOToVer1._2.Data;
using MyOToVer1._2.Models.ViewModels;

namespace MyOToVer1._2.Models.DataModels
{
    public class InfoOwnerModel
    {
        private ApplicationDBContext db;

        public InfoOwnerModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public List<InfoOwnerViewModel> GetListOwnerBeLocked()
        {
            return (from owner in db.Owners
                    join customer in db.Customers on owner.Id equals customer.Id
                    where owner.owner_status == 1
                    select new InfoOwnerViewModel
                    {
                       owner_name = customer.Name,
                       owner_contact = customer.Contact,
                       owner_id = owner.Id
                    }).ToList();
        }
    }
}
