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
        public string Name { get; set; } = null!;

        public ICollection<Vehicle> Vehicles { get; set; }
        = new HashSet<Vehicle>();

        public ICollection<ServicePrice> ServicePrices { get; set; }
            = new HashSet<ServicePrice>();
    }
}
