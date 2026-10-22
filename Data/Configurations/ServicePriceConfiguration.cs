using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DetailingStudio.Data.Configurations
{
    public class ServicePriceConfiguration : IEntityTypeConfiguration<ServicePrice>
    {
        public void Configure(EntityTypeBuilder<ServicePrice> builder)
        {
            builder
                .Property(sp => sp.Price)
                .HasPrecision(10, 2);

            builder
                .HasIndex(sp => new { sp.ServiceId, sp.VehicleCategoryId })
                .IsUnique();
        }
    }
}
