using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DetailingStudio.Data.Configurations
{
    public class VehicleCategoryConfiguration : IEntityTypeConfiguration<VehicleCategory>
    {
        public void Configure(EntityTypeBuilder<VehicleCategory> builder)
        {
            builder
                .HasMany(vc => vc.Vehicles)
                .WithOne(v => v.VehicleCategory)
                .HasForeignKey(v => v.VehicleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(vc => vc.ServicePrices)
                .WithOne(sp => sp.VehicleCategory)
                .HasForeignKey(sp => sp.VehicleCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new VehicleCategory
                {
                    Id = 1,
                    Name = "Small",
                },
                new VehicleCategory
                {
                    Id = 2,
                    Name = "Medium",
                },
                new VehicleCategory
                {
                    Id = 3,
                    Name = "Large",
                },
                new VehicleCategory
                {
                    Id = 4,
                    Name = "SUV",
                },
                new VehicleCategory
                {
                    Id = 5,
                    Name = "Van",
                }
                );
        }
    }
}
