using DAL.Models;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly OrdersContext _ordersContext;
        private readonly ILogger<OrderItemRepository> _logger;

        public OrderItemRepository(OrdersContext ordersContext, ILogger<OrderItemRepository> logger)
        {
            _ordersContext = ordersContext;
            _logger = logger;
        }

        public OrderItem Create(OrderItem orderItem)
        {
            try
            {
                if (orderItem != null)
                {
                    var obj = _ordersContext.Add(orderItem);
                    _ordersContext.SaveChanges();
                    return obj.Entity;
                }
                else
                {
                    _logger.LogError("Order item is null, return null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Create OrderItem");
                throw;
            }
        }

        public void Delete(OrderItem orderItem)
        {
            try
            { 
                if (orderItem != null)
                {
                    var response = _ordersContext.OrderItems.Remove(orderItem);
                    if (response != null)
                        _ordersContext.SaveChanges();
                    else
                        _logger.LogError("Failed in response while Delete OrderItem");
                }
                else
                _logger.LogError("Faild to Delete, OrderItem is null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Delete OrderItem");
                throw;
            }
        }

        public IQueryable<OrderItem> GetAll()
        {
            try
            {
                var orderItems = _ordersContext.OrderItems.AsQueryable();
                if (orderItems != null)
                    return orderItems;
                else
                    _logger.LogError("IEnumerable<OrderItem> is null, return null");
                return Enumerable.Empty<OrderItem>().AsQueryable(); 
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"Failed while GetAll OrderItems"); 
                throw; 
            }
        }

        public OrderItem GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    OrderItem? orderItem = _ordersContext.OrderItems.FirstOrDefault(x => x.Id == id);
                    if (orderItem != null)
                        return orderItem;
                    else
                        _logger.LogError("GetById is null, return null");
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex, $"Failed while GetById OrderItem"); 
                throw; 
            }
        }

        public void Update(OrderItem orderItem)
        {
            try
            {
                if (orderItem != null)
                {
                    var obj = _ordersContext.OrderItems.Update(orderItem);
                    if (obj != null)
                        _ordersContext.SaveChanges();
                    else
                        _logger.LogError("Failed in response while Update OrderItem");
                }
                else
                    _logger.LogError("Faild to Update, OrderItem is null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Update OrderItem");
                throw;
            }
        }
    }
}
