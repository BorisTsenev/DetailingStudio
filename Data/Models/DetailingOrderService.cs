using System.ComponentModel.DataAnnotations;

namespace DetailingStudio.Data.Models
{
    public class DetailingOrderService
    {
        
        public int DetailingOrderId { get; set; }

        
        public int ServiceId { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
