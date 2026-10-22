using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailingStudio.Data.Models
{
    public class ServicePrice
    {
        // This entity represents the price of a specific service for a specific vehicle category.
        // Different vehicle categories may have different prices for the same service.
        [Key]
        public int Id { get; set; }

        [Required]
        
        public int ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; } = null!;

        [Required]
        public int VehicleCategoryId { get; set; }

        [ForeignKey(nameof(VehicleCategoryId))]
        public VehicleCategory VehicleCategory { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

    }
}
