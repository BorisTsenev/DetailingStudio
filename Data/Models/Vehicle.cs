using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.VehicleMakeMaxLength)]
        public string Make { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.VehicleModelMaxLength)]
        public string Model { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.VehicleRegistrationNumberMaxLength, MinimumLength = ValidationConstants.VehicleRegistrationNumberMinLength)]
        public string RegistrationNumber { get; set; } = null!;

        // As of now the vin numbers are 17 chars long by standard, but some classical/vintige cars have 9, 11, 13 and so on long vin numbers
        [StringLength(ValidationConstants.VehicleVinNumberMaxLength, MinimumLength = ValidationConstants.VehicleVinNumberMinLength)]
        public string? VinNumber { get; set; }

        public int? Year { get; set; }

        [StringLength(ValidationConstants.VehicleColorMaxLength, MinimumLength = ValidationConstants.VehicleColorMinLength  )]
        public string? Color { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [Required]
        public int VehicleCategoryId { get; set; }

        [ForeignKey(nameof(VehicleCategoryId))]
        public VehicleCategory VehicleCategory { get; set; } = null!;

        public ICollection<DetailingOrder> DetailingOrders { get; set; }
        = new HashSet<DetailingOrder>();

    }
}
