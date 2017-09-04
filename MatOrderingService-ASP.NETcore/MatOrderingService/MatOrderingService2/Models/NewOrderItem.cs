using System.ComponentModel.DataAnnotations;

namespace MatOrderingService2.Models
{
    public class NewOrderItem
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
