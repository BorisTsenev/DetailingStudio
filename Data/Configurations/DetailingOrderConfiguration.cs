using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DetailingStudio.Data.Configurations
{
    public class DetailingOrderConfiguration : IEntityTypeConfiguration<DetailingOrder>
    {
        public void Configure(EntityTypeBuilder<DetailingOrder> builder)
        {
            builder
                .HasMany(o => o.DetailingOrderServices)
                .WithOne(dos => dos.DetailingOrder)
                .HasForeignKey(dos => dos.DetailingOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
