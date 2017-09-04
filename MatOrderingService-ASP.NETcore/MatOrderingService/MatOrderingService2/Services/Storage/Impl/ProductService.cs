using System.Threading.Tasks;
using MatOrderingService2.Domain;
using Microsoft.EntityFrameworkCore;

namespace MatOrderingService2.Services.Storage.Impl
{
    public class ProductService : IProductService
    {
        private readonly OrdersDbContext _context;

        // add code gen in constructor
        public ProductService(OrdersDbContext context) //Imapper
        {
            _context = context;
        }

        public async Task<Product[]> GetAll()
        {
            var products = await _context.Products.AsNoTracking().ToArrayAsync();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }
    }
}
