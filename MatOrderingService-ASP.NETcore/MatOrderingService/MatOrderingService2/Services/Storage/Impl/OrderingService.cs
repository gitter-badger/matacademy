using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MatOrderingService2.Domain;
using MatOrderingService2.Exceptions;
using MatOrderingService2.Models;
using Microsoft.EntityFrameworkCore;

namespace MatOrderingService2.Services.Storage.Impl
{
    public class OrderingService : IOrderingService
    {
        private readonly OrdersDbContext _context;
        private readonly IMapper _imapper;
        private readonly ICodeGenerationService _codeGenerator;

        public OrderingService(OrdersDbContext context, IMapper imapper, ICodeGenerationService codeGenerator)
        {
            _context = context;
            _imapper = imapper;
            _codeGenerator = codeGenerator;
        }

        public async Task<Order> Create(NewOrder newOrder)
        {
            if (newOrder == null)
                throw new EntityNotFoundException();

            var order = _imapper.Map<Order>(newOrder);
            order.OrderCode = new Guid().ToString();
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            
            order.OrderCode = await _codeGenerator.GetCodeForOrder(order.Id);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> Delete(int id)
        {
            var toDelete = await _context.Orders.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (toDelete == null)
                return false;
            toDelete.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderInfo> Get(int id)
        {
            var order = await _context.Orders.AsNoTracking().Include(i => i.OrderItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            return _imapper.Map<OrderInfo>(order);
        }

        public async Task<OrderInfo[]> GetAll()
        {
            var orders = await _context.Orders.AsNoTracking().Include(i => i.OrderItems).ThenInclude(i => i.Product).Where(p => !p.IsDeleted).ToArrayAsync();

            return orders.Select(order => _imapper.Map<OrderInfo>(order)).ToArray();
        }

        public async Task<Order> Update(int id, EditOrder order)
        {
            var toBeUpdated = await _context.Orders.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (toBeUpdated == null)
                return null;
            toBeUpdated.OrderItems = order.OrderItems;
            await _context.SaveChangesAsync();

            return toBeUpdated;
        }

        public async Task<OrderStatisticItem[]> GetStatistic()
        {
            return await _context
                .Orders
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .GroupBy(g => g.CreatorId)
                .Select(p => new OrderStatisticItem {CreatorId = p.Key, NumberOfOrders = p.Count()})
                .ToArrayAsync();
        }

        public async Task<OrderStatisticItem[]> GetStatisticDapper()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                var ordersStatisticItems =
                    await connection.QueryAsync<OrderStatisticItem>(
                        @"SELECT CreatorId, COUNT(*) AS NumberOfOrders FROM Orders GROUP BY CreatorId");
                return ordersStatisticItems.ToArray();
            }

        }
    }
}
