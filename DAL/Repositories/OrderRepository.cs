using DAL.Models;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly OrdersContext _ordersContext;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(OrdersContext ordersContext, ILogger<OrderRepository> logger)
        {
            _ordersContext = ordersContext;
            _logger = logger;
        }

        public Order Create(Order order)
        {
            try
            {
                if (order != null)
                {
                    var obj = _ordersContext.Orders.Add(order);
                     _ordersContext.SaveChanges();
                    return obj.Entity;
                }
                else
                {
                    _logger.LogError("Order is null, return null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Create Order");
                throw;
            }
        }

        public void Delete(Order order)
        {
            try
            {
                if (order != null)
                {

                    var orderItems = order.OrderItems.Where(x => x.OrderId == order.Id);
                    
                    if(orderItems != null && orderItems.Count() > 0)
                    {
                        foreach (var item in orderItems)
                        {
                            _ordersContext.OrderItems.Remove(item);
                        }
                        _ordersContext.SaveChanges();
                    }
                    _ordersContext.Orders.Attach(order);
                    var obj = _ordersContext.Orders.Remove(order);
                    if (obj != null)
                    {
                        _ordersContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Delete Order");
                throw;
            }
        }

        public IQueryable<Order> GetAll()
        {
            try
            {
                IQueryable<Order> orders = _ordersContext.Orders.AsQueryable();
                if (orders != null)
                    return orders;
                else
                    _logger.LogError("IEnumerable<Order> is null, return null");
                return Enumerable.Empty<Order>().AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while GetAll Orders");
                throw;
            }
        }

        public Order GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Order? order = _ordersContext.Orders.FirstOrDefault(x => x.Id == id);
                    if (order != null)
                        return order;
                    else
                        _logger.LogError("Order GetById() is null, return null");
                    return null;
                }
                else
                {
                    _logger.LogError("Order id is incorrect, return null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Get Order by Id");
                throw;
            }
        }

        public void Update(Order order)
        {
            try
            {
                if (order != null)
                {
                    var obj = _ordersContext.Orders.Update(order);
                    if (obj != null)
                        _ordersContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Update Order");
                throw;
            }
        }
    }
}
