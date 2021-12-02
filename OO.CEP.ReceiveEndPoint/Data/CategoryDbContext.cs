using Microsoft.EntityFrameworkCore;
using OO.CEP.ReceiveEndPoint.Models;

namespace OO.CEP.ReceiveEndPoint.Data
{
    public class CategoryDbContext: DbContext
    {
        public DbSet<Category> categories { get; set; }

        CategoryDbContext() { }
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
