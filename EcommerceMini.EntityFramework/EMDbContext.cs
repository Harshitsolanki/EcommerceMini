using EcommerceMini.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EcommerceMini.EntityFramework
{
    public class EMDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public EMDbContext(DbContextOptions<EMDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
