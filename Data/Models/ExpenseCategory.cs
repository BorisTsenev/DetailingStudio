using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ExpenseCategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();


    }
}
