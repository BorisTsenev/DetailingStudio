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
        [StringLength(ValidationConstants.vehicleMakeMaxLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(ValidationConstants.vehicleModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [StringLength(ValidationConstants.vehicleRegistrationNumberMaxLength, MinimumLength = ValidationConstants.vehicleRegistrationNumberMinLength)]
        public string RegistrationNumber { get; set; }

        // As of now the vin numbers are 17 chars long by standard, but some classical/vintige cars have 9, 11, 13 and so on long vin numbers
        [StringLength(ValidationConstants.vehicleVinNumberMaxLength, MinimumLength = ValidationConstants.vehicleVinNumberMinLength)]
        public string? VinNumber { get; set; }

        public int? Year { get; set; }

        [StringLength(ValidationConstants.vehicleColorMaxLength, MinimumLength = ValidationConstants.vehicleColorMinLength)]
        public string? Color { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [Required]
        public int VehicleCategoryId { get; set; }

        [ForeignKey(nameof(VehicleCategoryId))]
        public VehicleCategory VehicleCategory { get; set; }

        public ICollection<DetailingOrder> DetailingOrders { get; set; }
        = new HashSet<DetailingOrder>();

    }
}
