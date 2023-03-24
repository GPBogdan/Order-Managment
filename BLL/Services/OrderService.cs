using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class OrderService
    {
        public readonly IRepository<Order> _repository;
        public readonly ILogger<OrderService> _logger;
        public OrderService(IRepository<Order> repository, ILogger<OrderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Order Create(Order order)
        {
            try
            {
                if (order != null)
                {
                    return _repository.Create(order);
                }
                else
                {
                    _logger.LogError("Order is null when Create");
                    throw new ArgumentNullException(nameof(order));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Create Order with error: {ex.Message}");
                throw;
            }
        }

        public void Delete(Order order)

        {
            try
            {
                    if (order != null) { _repository.Delete(order); }
                    else
                    {
                        _logger.LogError("Order is null when Delete");
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Delete Order with error: {ex.Message}");
                throw;
            }
        }

        public void Update(Order order)

        {
            try
            {
                if(order != null)
                {
                    _repository.Update(order);
                }
                else
                {
                    _logger.LogError("Order is null when Update");
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while Update Order with error: {ex.Message}");
                throw;
            }

        }

        public IQueryable<Order> GetAll()
        {
            try
            {
                IQueryable<Order> orders = _repository.GetAll().AsQueryable();

                if (orders != null && orders.Count() > 0)
                {
                    return orders;
                }
                else
                {
                    _logger.LogError("Order is null or empty when GetAll");
                    return Enumerable.Empty<Order>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while GetAll Orders with error: {ex.Message}");
                throw;
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Order order = _repository.GetById(id);
                    if (order != null)
                        return order;
                    else
                        _logger.LogError("Order is null while GetOrderById");
                    return new Order();
                }
                else
                {
                    _logger.LogError("Incorrect Id while GetOrderById");
                    return new Order();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while GetOrderById with error: {ex.Message}");
                throw;
            }
        }

        public IQueryable<Order> GetOrdersByListOfIds(List<int> ids)
        {
            try
            {
                if (ids != null && ids.Count() > 0)
                {
                    IQueryable<Order> orders = _repository.GetAll();
                    var result = orders.Where(order => ids.Contains(order.Id));

                    if (result != null && result.Count() > 0)
                    {
                        return result;
                    }
                    else
                    {
                        _logger.LogError("Orders is null or empty when GetOrdersByListOfIds");
                        return new List<Order>().AsQueryable();
                    }
                }
                else
                {
                    _logger.LogError("List of Ids is null or empty when GetOrdersByListOfIds");
                    return new List<Order>().AsQueryable();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while GetOrdersByListOfIds with error: {ex.Message}");
                return new List<Order>().AsQueryable();
            }
        }

        public bool IsOrderUnique(Order order)
        {
            try
            {
                IQueryable<Order> orders = _repository.GetAll().Where(x => x.Number == order.Number && x.ProviderId == order.ProviderId);

                if(orders!= null && orders.Count() == 0)
                {
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while IsOrderUnique with error: {ex.Message}");
                return false;
            }
        }

    }
}
