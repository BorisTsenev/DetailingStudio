using DetailingStudio.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DetailingStudio.Data.Configurations
{
    public class ExpenseCategoryConfiguration
        : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
            

            builder.HasMany(ec => ec.Expenses)
                .WithOne(e => e.ExpenseCategory)
                .HasForeignKey(e => e.ExpenseCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new ExpenseCategory
                {
                    Id = 1,
                    Name = "Consumables"
                },
                new ExpenseCategory
                {
                    Id = 2,
                    Name = "Rent"
                },
                new ExpenseCategory
                {
                    Id = 3,
                    Name = "Utilities"
                },
                new ExpenseCategory
                {
                    Id = 4,
                    Name = "Equipment"
                },
                new ExpenseCategory
                {
                    Id = 5,
                    Name = "Marketing"
                },
                new ExpenseCategory
                {
                    Id = 6,
                    Name = "Other"
                }
            );
        }
    }
}
