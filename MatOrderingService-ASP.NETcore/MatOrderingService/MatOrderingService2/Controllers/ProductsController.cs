using System.Threading.Tasks;
using MatOrderingService2.Domain;
using MatOrderingService2.Exceptions;
using MatOrderingService2.Services.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatOrderingService2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/orders
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Product[] products = await _productService.GetAll();
            if (products.Length == 0)
                throw new EntityNotFoundException();
            return Ok(products);
        }

        // GET api/orders
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product product = await _productService.GetById(id);
            if (product == null)
                throw new EntityNotFoundException();
            return Ok(product);
        }

    }
}
