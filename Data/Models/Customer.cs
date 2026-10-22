using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.customerFirstNameMaxLength, MinimumLength = ValidationConstants.customerFirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.customerLastNameMaxLength, MinimumLength = ValidationConstants.customerLastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Phone]
        [MaxLength(ValidationConstants.customerPhoneNumberMaxLength)]
        public string? PhoneNumber { get; set; } = null!;

        [EmailAddress]
        [MaxLength(ValidationConstants.customerEmailMaxLength)]
        public string? Email { get; set; }
        
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
