using System.ComponentModel.DataAnnotations;

namespace MatOrderingService2.Models
{
    public class NewOrder
    {
        [Required]
        public NewOrderItem[] OrderItems { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatorId { get; set; }
    }
}
