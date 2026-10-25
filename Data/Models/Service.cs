using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DetailingStudio.Common;
using DetailingStudio.Data.Models.Enums;

namespace DetailingStudio.Data.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.ServiceNameMaxLength, MinimumLength = ValidationConstants.ServiceNameMinLength)]
        public string Name { get; set; } = null!;

        [MaxLength(ValidationConstants.ServiceDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public ServiceType ServiceType { get; set; } = ServiceType.Main;

        [Required]
        public bool IsActive { get; set; } = true;

        public ICollection<ServicePrice> ServicePrices { get; set; }
        = new HashSet<ServicePrice>();

        public ICollection<DetailingOrderService> DetailingOrderServices { get; set; }
            = new HashSet<DetailingOrderService>();


    }
}
