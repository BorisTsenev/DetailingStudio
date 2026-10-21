using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(ValidationConstants.customerFirstNameMaxLength, MinimumLength = ValidationConstants.customerFirstNameMinLength)]
        public string FirstName { get; set; }

        [StringLength(ValidationConstants.customerLastNameMaxLength, MinimumLength = ValidationConstants.customerLastNameMinLength)]
        public string LastName { get; set; }

        [Phone]
        [MaxLength(ValidationConstants.customerPhoneNumberMaxLength)]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(ValidationConstants.customerEmailMaxLength)]
        public string? Email { get; set; }
    }
}
