using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MatOrderingService2.Domain;

namespace MatOrderingService2.Models
{
    public class EditOrder
    {
        [Required]
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
