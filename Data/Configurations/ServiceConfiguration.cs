using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DetailingStudio.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder
                .Property(s => s.IsActive)
                .HasDefaultValue(true);

            builder
                .HasMany(s => s.ServicePrices)
                .WithOne(sp => sp.Service)
                .HasForeignKey(sp => sp.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(s => s.DetailingOrderServices)
                .WithOne(dos => dos.Service)
                .HasForeignKey(dos => dos.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
