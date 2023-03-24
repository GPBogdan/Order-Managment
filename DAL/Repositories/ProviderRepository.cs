using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class ProviderRepository : IRepository<Provider>
    {
        private readonly OrdersContext _ordersContext;
        private readonly ILogger<ProviderRepository> _logger;

        public ProviderRepository(OrdersContext ordersContext, ILogger<ProviderRepository> logger)
        {
            _ordersContext = ordersContext;
            _logger = logger;
        }

        public Provider Create(Provider order)
        {
            try
            {
                if (order != null)
                {
                    var obj = _ordersContext.Add(order);
                     _ordersContext.SaveChanges();
                    return obj.Entity;
                }
                else
                {
                    _logger.LogError("Provider is null, return null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Create Provider");
                throw;
            }
        }

        public void Delete(Provider provider)
        {
            try
            {
                if (provider != null)
                {
                    var obj = _ordersContext.Remove(provider);
                    if (obj != null)
                    {
                        _ordersContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Delete Provider");
                throw;
            }
        }

        public IQueryable<Provider> GetAll()
        {
            try
            {
                var provider = _ordersContext.Providers.AsQueryable();
                if (provider != null)
                    return provider;
                else
                    _logger.LogError("IEnumerable<Order> is null, return null");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while GetAll Providers");
                throw;
            }
        }

        public Provider GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    Provider? provider = _ordersContext.Providers.FirstOrDefault(x => x.Id == id);
                    if (provider != null)
                        return provider;
                    else
                        _logger.LogError("Provider GetById() is null, return null");
                    return null;
                }
                else
                {
                    _logger.LogError("Provider id is incorrect, return null");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Get Provider by Id");
                throw;
            }
        }

        public void Update(Provider provider)
        {
            try
            {
                if (provider != null)
                {
                    var obj = _ordersContext.Update(provider);
                    if (obj != null)
                        _ordersContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed while Update Provider");
                throw;
            }
        }
    }
}
