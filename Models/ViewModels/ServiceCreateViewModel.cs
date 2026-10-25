using System.ComponentModel.DataAnnotations;

namespace DetailingStudio.Models.ViewModels
{
    public class ServiceCreateViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public List<ServicePriceInputModel> ServicePrices { get; set; }
            = new List<ServicePriceInputModel>();
    }
}
