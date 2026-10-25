using System.ComponentModel.DataAnnotations;

namespace DetailingStudio.Models.ViewModels
{
    public class ServicePriceInputModel
    {

        public int VehicleCategoryId { get; set; }

        public string VehicleCategoryName { get; set; } = null!;

        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }


    }
}
