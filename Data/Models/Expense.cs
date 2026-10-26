using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.ExpenseDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        public int ExpenseCategoryId { get; set; }

        [Required]
        public ExpenseCategory ExpenseCategory { get; set; }

        [MaxLength(ValidationConstants.ExpenseNotesMaxLength)]
        public string? Notes { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
