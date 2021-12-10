using Cep.Backend.ReceiveEndPoint.Masstransit.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders{ get; set; }

        public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost; Initial Catalog=cep-furniture-store; User Id=sa; Password=123456");
        }
    }
}
