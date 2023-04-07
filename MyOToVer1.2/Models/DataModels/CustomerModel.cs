using Microsoft.EntityFrameworkCore;
using MyOToVer1._2.Data;

namespace MyOToVer1._2.Models.DataModels
{
    public class CustomerModel
    {
        private ApplicationDBContext db;

        public CustomerModel(ApplicationDBContext db)
        {
            this.db = db;
        }

        public void AddCustomer(Customer cus)
        {
            db.Customers.Add(cus);
            db.SaveChanges();
        }

        public Customer GetCustomerByContact(string contact)
        {
            return db.Customers.Where(p => p.Contact.Equals(contact)).FirstOrDefault();
        }

        public bool IsValidContact(string contact)
        {
            return db.Customers.Any(p => p.Contact.Equals(contact));
        }

        public bool IsValidPass(string contact, string pass)
        {
            return db.Customers.Any(p => p.Contact.Equals(contact) && p.Password.Equals(pass));
        }

    }
}
