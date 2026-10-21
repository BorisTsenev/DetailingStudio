using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.serviceNameMaxLength, MinimumLength = ValidationConstants.serviceNameMinLength)]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.serviceDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
