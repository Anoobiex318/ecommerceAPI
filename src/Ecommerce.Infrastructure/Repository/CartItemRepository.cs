using Dapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interface;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly EcommerceDataContext _dataContext;
        private readonly ILogger<CartItemRepository> _logger;

        public CartItemRepository(EcommerceDataContext dataContext, ILogger<CartItemRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<CartItemEntity> AddCartItem(CartItemEntity newCartItem)
        {
            try
            {
                _logger.LogInformation("Add Cart Item");

                var shopper = await _dataContext.Users.SingleOrDefaultAsync(u => u.UserId == newCartItem.ShopperId);
                var shopperOrder = await _dataContext.Orders.SingleOrDefaultAsync(u => u.UserPrimaryId == newCartItem.ShopperId && u.OrderStatus == 0);

                if (shopperOrder != null)
                {
                    newCartItem.OrderPrimaryId = shopperOrder.OrderId;
                    shopperOrder.CartItems.Add(newCartItem);
                    _dataContext.Orders.Update(shopperOrder);
                }
                else
                {
                    _logger.LogInformation("Create new Order");
                    var orderEntity = new OrderEntity()
                    {
                        UserPrimaryId = shopper.UserId,
                        OrderStatus = 0,
                        CartItems = new List<CartItemEntity>() {
                            newCartItem
                        }
                    };
                    _dataContext.Orders.Add(orderEntity);
                }

                await _dataContext.SaveChangesAsync();

                return newCartItem;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in creating cart item details: {ex}", ex);
                throw;
            }
        }

        public async Task DeleteCartItem(Guid cartItemId)
        {
            _logger.LogInformation("Delete Cart Item by Id");
            try
            {
                var cartItemEntity = await _dataContext.CartItems.FindAsync(cartItemId);

                if (cartItemEntity != null)
                {
                    _dataContext.CartItems.Remove(cartItemEntity);
                    await _dataContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in deleting cart item details: {ex}", ex);
                throw;
            }
        }

        public async Task<List<CartItemModel>> GetAllCartItems(Guid shopperId)
        {
            _logger.LogInformation("Get All Cart Items executing..");
            try
            {
                var query = @"
            SELECT ci.*
            FROM CartItems ci
            INNER JOIN Orders o 
            ON ci.OrderPrimaryId = o.OrderId
            WHERE o.OrderStatus = @orderStatus
            AND ci.ShopperId = @shopperId";

                var con = _dataContext.Database.GetDbConnection();
                var cartItems = await con.QueryAsync<CartItemModel>(query, new
                {
                    orderStatus = 0,
                    shopperId
                });

                return cartItems.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting all cart item details: {ex}", ex);
                throw;
            }
        }

        public async Task<CartItemEntity> UpdateCartItem(CartItemEntity cartItem)
        {
            try
            {
                var existingCartItem = await _dataContext.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItem.CartItemId)??throw new Exception($"Cart item with ID {cartItem.CartItemId} not found");
                existingCartItem.ProductName = cartItem.ProductName;

                await _dataContext.SaveChangesAsync();

                return existingCartItem;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating cart item with ID {cartItem.CartItemId}: {ex}", ex);
                throw;
            }
        }
    }

}
