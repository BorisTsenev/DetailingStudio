using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.CustomerFirstNameMaxLength, MinimumLength = ValidationConstants.CustomerFirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.CustomerLastNameMaxLength, MinimumLength = ValidationConstants.CustomerLastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(ValidationConstants.CustomerPhoneNumberMaxLength, MinimumLength = ValidationConstants.CustomerPhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        [MaxLength(ValidationConstants.CustomerEmailMaxLength)]
        public string? Email { get; set; }
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public ICollection<Vehicle> Vehicles { get; set; }
       = new HashSet<Vehicle>();
    }
}
