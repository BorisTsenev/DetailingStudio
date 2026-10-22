using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailingStudio.Data.Models
{
    public class DetailingOrderService
    {

        [Required]
        public int DetailingOrderId { get; set; }

        [ForeignKey(nameof(DetailingOrderId))]
        public DetailingOrder DetailingOrder { get; set; } = null!;

        [Required]
        public int ServiceId { get; set; }

        [ForeignKey(nameof(ServiceId))]
        public Service Service { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
    }
}
