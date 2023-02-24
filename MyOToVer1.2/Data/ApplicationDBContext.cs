using Microsoft.EntityFrameworkCore;
using MyOToVer1._2.Models;
using System.Collections.Generic;

namespace MyOToVer1._2.Data
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
