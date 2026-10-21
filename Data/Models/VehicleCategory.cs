using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class VehicleCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.vehicleCategoryNameMaxLength, MinimumLength = ValidationConstants.vehicleCategoryNameMinLength)]
        public string Name { get; set; }
    }
}
