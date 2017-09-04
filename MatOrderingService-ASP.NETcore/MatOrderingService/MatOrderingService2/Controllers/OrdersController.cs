using System;
using System.Threading.Tasks;
using MatOrderingService2.Exceptions;
using MatOrderingService2.Models;
using MatOrderingService2.Services.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatOrderingService2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderingService _orderList;

        public OrdersController(IOrderingService orderList)
        {
            _orderList = orderList;
        }

        // GET api/orders
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            OrderInfo[] orders = await _orderList.GetAll();
            if (orders.Length == 0)
                throw new EntityNotFoundException();
            return Ok(orders);
        }

        // GET api/orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult>  Get(int id)
        {
            var order = await _orderList.Get(id);
            if (order == null)
                throw new NotSupportedException();
            return Ok(order);
        }

        // POST api/orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewOrder order)
        {
            var newOrder =  await _orderList.Create(order);
            if (newOrder != null)
               return Ok(newOrder);
            throw new EntityNotFoundException();
        }

        // PUT api/orders/5
        /// <summary>
        /// Updates an Order's Details by unique id
        /// </summary>
        /// <param name="id">ID of updated order</param>
        /// <param name="order">New Properties of the order</param>
        /// <response code="200">Order created</response>
        /// <response code="404">Order is not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EditOrder order)
        {
           var updated = await _orderList.Update(id, order);
            if (updated == null)
                throw new EntityNotFoundException();
            return Ok(updated);
        }

        // DELETE api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _orderList.Delete(id))
                return Ok();
            throw new EntityNotFoundException();
        }

        [HttpGet("statistic")]
        [ProducesResponseType(typeof(OrderStatisticItem[]), 200)]
        public async Task<IActionResult> GetStatistic()
        {
            var orderStatisticsItems = await _orderList.GetStatistic();
            return Ok(orderStatisticsItems);
        }

        [HttpGet("statistic/dapper")]
        [ProducesResponseType(typeof(OrderStatisticItem[]), 200)]
        public async Task<IActionResult> GetStatisticDapper()
        {
            var orderStatisticsItems = await _orderList.GetStatisticDapper();
            return Ok(orderStatisticsItems);
        }
    }
}
