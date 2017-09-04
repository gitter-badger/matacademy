using System;

namespace MatOrderingService2.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public OrderItemInfo[] OrderItems { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }

    }
}
