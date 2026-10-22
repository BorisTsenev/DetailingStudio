using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DetailingStudio.Data.Configurations
{
    public class DetailingOrderServiceConfiguration : IEntityTypeConfiguration<DetailingOrderService>
    {
        public void Configure(EntityTypeBuilder<DetailingOrderService> builder)
        {
            builder
                .HasKey(dos => new { dos.DetailingOrderId, dos.ServiceId });

            builder
                .Property(dos => dos.Price)
                .HasPrecision(10, 2);
        }
    }
}
