using Dapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interface;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDataContext _dataContext;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(EcommerceDataContext dataContext, ILogger<OrderRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        public async Task DeleteOrderById(Guid id)
        {
            try
            {
                var entityOrder = await FindIdOrder(id);
                _dataContext.Orders.Remove(entityOrder);

                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Deleting Order Details: {ex}", ex);
                throw;
            }
        }

        public async Task<OrderModel> GetOrderById(Guid id)
        {
            _logger.LogInformation("Get Order By Id executing..");

            try
            {
                var query = @"
            SELECT o.*, ci.* 
            FROM Orders o
            INNER JOIN CartItems ci ON o.OrderId = ci.OrderPrimaryId
            WHERE o.OrderId = @id";

                using var connection = _dataContext.Database.GetDbConnection();
                _logger.LogInformation("Connection established..");

                var orderDictionary = new Dictionary<Guid, OrderModel>();

                var order = await connection.QueryAsync<OrderModel, CartItemEntity, OrderModel>(
                    query,
                    (o, ci) =>
                    {
                        if (!orderDictionary.TryGetValue(o.OrderId, out var orderEntry))
                        {
                            orderEntry = o;
                            orderEntry.CartItemEntities = new List<CartItemEntity>();
                            orderDictionary.Add(o.OrderId, orderEntry);
                        }
                        orderEntry.CartItemEntities.Add(ci);
                        return orderEntry;
                    },
                    new { id },
                    splitOn: "OrderId, CartItemId");

                _logger.LogInformation("Fetch successfully..");

                return order.First();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOrderById");
                throw new Exception("Error in retrieving order by Id", ex);
            }
        }


        public async Task<List<OrderModel>> GetOrders(Guid userId)
        {
            var query = "SELECT o.* FROM Orders o WHERE UserPrimaryId = @UserId";
            try
            {
                var con = _dataContext.Database.GetDbConnection();
                var orders = await con.QueryAsync<OrderModel>(query, new { UserId = userId });
                var orderIds = orders.Select(o => o.OrderId).ToArray();
                var cartItems = await _dataContext.CartItems
                    .Where(ci => orderIds.Contains(ci.OrderPrimaryId)).ToListAsync();
                foreach (var order in orders)
                {
                    order.CartItemEntities = cartItems
                        .Where(c => c.OrderPrimaryId == order.OrderId)
                        .ToList();
                }
                _logger.LogInformation("GetAllOrder success..");
                return orders.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllOrderByStatus");
                throw new Exception("Error in retrieving all Order by Status", ex);
            }
        }


        public async Task<OrderModel> UpdateOrder(OrderModel orders)
        {
            var order = await _dataContext.Orders.FindAsync(orders.OrderId) ?? new OrderEntity();
            order.CartItems = order.CartItems;
            await _dataContext.SaveChangesAsync();
            return orders;

        }
        private async Task<OrderEntity> FindIdOrder(Guid id)
        {
            var foundOrder = await _dataContext.Orders.FindAsync(id)??throw new NullReferenceException();
            return foundOrder;

        }
    }
}
