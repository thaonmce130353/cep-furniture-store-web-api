using Cep.Backend.ReceiveEndPoint.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cep.Backend.ReceiveEndPoint.Data
{
    class CategoryDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost; Initial Catalog=cep-furniture-store; User Id=sa; Password=123456");
        }
    }
}
