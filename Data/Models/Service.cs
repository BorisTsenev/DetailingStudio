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
        public bool IsActive { get; set; } = true;

        public ICollection<ServicePrice> ServicePrices { get; set; }
        = new HashSet<ServicePrice>();

        public ICollection<DetailingOrderService> DetailingOrderServices { get; set; }
            = new HashSet<DetailingOrderService>();


    }
}
