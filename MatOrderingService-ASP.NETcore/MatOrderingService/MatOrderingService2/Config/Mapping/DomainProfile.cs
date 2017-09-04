using AutoMapper;
using MatOrderingService2.Domain;
using MatOrderingService2.Models;

namespace MatOrderingService2.Config.Mapping
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Order, OrderInfo>()
                .ForMember(d => d.Status,
                opt => opt.MapFrom(s => s.Status.ToString()));
            CreateMap<OrderItem, OrderItemInfo>()
                .ForMember(d => d.ProductCode, opt => opt.MapFrom(s => s.Product.Code))
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name));

            CreateMap<NewOrder, Order>();
            CreateMap<NewOrderItem, OrderItem>();

            CreateMap<EditOrder, Order>();
        }
    }
}
