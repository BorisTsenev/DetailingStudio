using DetailingStudio.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DetailingStudio.Data
{
    public class DetailingStudioDbContext(DbContextOptions<DetailingStudioDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<DetailingOrder> DetailingOrders { get; set; }
        public DbSet<DetailingOrderService> DetailingOrderServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(DetailingStudioDbContext).Assembly);


        }
    }
    
}
