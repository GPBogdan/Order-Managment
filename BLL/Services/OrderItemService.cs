using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class OrderItemService
    {
        public readonly IRepository<OrderItem> _repository;
        public readonly ILogger<OrderItemService> _logger;
        public OrderItemService(IRepository<OrderItem> repository, ILogger<OrderItemService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public OrderItem Create(OrderItem orderItem)
        {
            try
            {
                if (orderItem.Order.Number.ToLower() != orderItem.Name.ToLower())
                {
                    //defaultValue
                    orderItem.Id = 0;
                    return _repository.Create(orderItem);
                }
                else
                {
                    _logger.LogWarning("Can not create OrderItem with that name");
                    throw new ArgumentNullException(nameof(orderItem));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Create OrderItem with error: {ex.Message}");
                throw;
            }
        }

        public void Delete(OrderItem orderItem)
        {
            try
            {
                    if (orderItem != null) { _repository.Delete(orderItem); }
                    else
                    {
                    _logger.LogError("Can not delete OrderItem");
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Delete OrderItem with error: {ex.Message}");
                throw;
            }
        }

        public void Update(OrderItem orderItem)

        {
            try
            {

                    if (orderItem != null) { _repository.Update(orderItem); }
                    else
                    {
                        _logger.LogError("Can not Update OrderItem");
                    }               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Update OrderItem with error: {ex.Message}");
                throw;
            }

        }

        public void UpdateOrderItems(IQueryable<OrderItem> orderItems)
        {
            try
            {
                if(orderItems.Count() > 0)
                {
                    foreach (var item in orderItems)
                    {
                        _repository.Update(item);
                    }
                }
                else
                {
                    _logger.LogError("No OrderItems to Update");
                }
               
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while UpdateOrderItems() with error: {ex.Message}");
            }
        }

        public IQueryable<OrderItem> GetAll()
        {
            try
            {
                IQueryable<OrderItem> orderItem = _repository.GetAll().AsQueryable();

                if (orderItem != null && orderItem.Count() > 0)
                {
                    return orderItem;
                }
                else
                {
                    _logger.LogError("No OrderItems to GET");
                    return Enumerable.Empty<OrderItem>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while GetAll() with error: {ex.Message}");
                throw;
            }
        }

        public OrderItem GetOrderItemById(int id)
        {
            try
            {
                if (id > 0)
                {
                    OrderItem orderItem = _repository.GetById(id);
                    if (orderItem != null)
                        return orderItem;
                    else
                        _logger.LogError("GetById is null, return null");
                    return new OrderItem();
                }
                else
                {
                    return new OrderItem();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while GetById OrderItem");
                throw;
            }
        }

        public IQueryable<OrderItem> GetOrderItemsListByOrderId(int id)
        {
            try
            {
                if (id > 0)
                {
                    IQueryable<OrderItem> orderItemsList = _repository.GetAll().Where(x => x.OrderId == id);
                    if (orderItemsList != null)
                        return orderItemsList;
                    else
                        _logger.LogError("GetById is null, return null");
                    return Enumerable.Empty<OrderItem>().AsQueryable(); ;
                }
                else
                {
                    return Enumerable.Empty<OrderItem>().AsQueryable(); ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while GetById OrderItem");
                throw;
            }
        }
    }
}
