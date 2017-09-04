using System.ComponentModel.DataAnnotations;

namespace MatOrderingService2.Models
{
    public class OrderStatisticItem
    {
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public int NumberOfOrders { get; set; }
    }
}