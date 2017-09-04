using System.Threading.Tasks;
using MatOrderingService2.Domain;
using MatOrderingService2.Models;

namespace MatOrderingService2.Services.Storage
{
    public interface IOrderingService
    {
        Task<OrderInfo[]> GetAll();
        Task<OrderInfo> Get(int id);
        Task<Order> Create(NewOrder order); //map view models
        Task<Order> Update(int id, EditOrder order);//map view models
        Task<bool> Delete(int id);
        Task<OrderStatisticItem[]> GetStatistic();
        Task<OrderStatisticItem[]> GetStatisticDapper();
    }
}
