using System.Threading.Tasks;
using MatOrderingService2.Domain;

namespace MatOrderingService2.Services.Storage
{
    public interface IProductService
    {
        Task<Product[]> GetAll();
        Task<Product> GetById(int id);
    }
}
