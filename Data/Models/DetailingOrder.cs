using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DetailingStudio.Common;

namespace DetailingStudio.Data.Models
{
    public class DetailingOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [ForeignKey(nameof(VehicleId))]
        public Vehicle Vehicle { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(ValidationConstants.detailingOrderStatusMaxLength, MinimumLength = ValidationConstants.detailingOrderStatusMinLength)]
        public string Status { get; set; } 

        [StringLength(ValidationConstants.detailingOrderNotesMaxLength)]
        public string? Notes { get; set; }

        public ICollection<DetailingOrderService> DetailingOrderServices { get; set; }
        = new HashSet<DetailingOrderService>();


    }
}
