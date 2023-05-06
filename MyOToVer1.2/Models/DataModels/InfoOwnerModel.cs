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
            return db.Owners
                .Where(owner => owner.owner_status == 1)
                .Join(db.Customers, owner => owner.Id, customer => customer.Id, (owner, customer) => new InfoOwnerViewModel
                {
                    owner_name = customer.Name,
                    owner_contact = customer.Contact,
                    owner_id = owner.Id
                })
                .ToList();
        }

    }
}
