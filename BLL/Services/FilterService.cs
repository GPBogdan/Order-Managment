using DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class FilterService
    {
        private readonly OrderService _orderService;
        private readonly ProviderService _providerService;
        private readonly OrderItemService _orderItemService;
        private readonly ILogger<FilterService> _logger;

        public FilterService(ILogger<FilterService> logger, OrderService orderService, ProviderService providerService, OrderItemService orderItemService)
        {
            _logger = logger;
            _orderService = orderService;
            _providerService = providerService;
            _orderItemService = orderItemService;
        }

        public List<SelectListItem> GetUniqueOrders()
        {
            try
            {
                var response = _orderService.GetAll().GroupBy(x => x.Number).Select(y => y.FirstOrDefault());

                if (response != null)
                {
                    List<SelectListItem> uniqueOrderItems = new List<SelectListItem>();

                    foreach (var orderItem in response)
                    {
                        SelectListItem selectListItem = new SelectListItem()
                        {
                            Text = orderItem.Number,
                            Value = orderItem.Id.ToString()
                        };
                        uniqueOrderItems.Add(selectListItem);
                    }

                    return uniqueOrderItems;
                }
                else
                {
                    _logger.LogWarning("GetUniqueOrders() is null or empty");
                    return new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while GetUniqueOrders with error: {ex.Message}");
                return new List<SelectListItem>();
            }

        }

        public List<SelectListItem> GetUniqueOrderItems()
        {
            try
            {
                var response = _orderItemService.GetAll().GroupBy(x => x.Name).Select(y => y.FirstOrDefault());
                if (response != null)
                {
                    List<SelectListItem> uniqueOrderItems = new List<SelectListItem>();

                    foreach (var orderItem in response)
                    {
                        SelectListItem selectListItem = new SelectListItem()
                        {
                            Text = orderItem.Name,
                            Value = orderItem.Id.ToString()
                        };
                        uniqueOrderItems.Add(selectListItem);
                    }

                    return uniqueOrderItems;
                }
                else
                {
                    _logger.LogWarning("GetUniqueOrderItems() is null or empty");
                    return new List<SelectListItem>();
                }

            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while GetUniqueOrderItems() with error: {ex.Message}");
                return new List<SelectListItem>();
            }
        }

        public List<SelectListItem> GetUniqueProviders()
        {
            try
            {
                var response = _providerService.GetAll().GroupBy(x => x.Name).Select(y => y.FirstOrDefault());

                if (response != null)
                {
                    List<SelectListItem> uniqueProviders = new List<SelectListItem>();

                    foreach (var provider in response)
                    {
                        SelectListItem selectListItem = new SelectListItem()
                        {
                            Text = provider.Name,
                            Value = provider.Id.ToString()
                        };
                        uniqueProviders.Add(selectListItem);
                    }

                    return uniqueProviders;
                }
                else
                {
                    _logger.LogWarning("GetUniqueProviders() is null or empty");
                    return new List<SelectListItem>();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while GetUniqueProviders() with error: {ex.Message}");
                return new List<SelectListItem>();
            }

        }

        public List<SelectListItem> GetUniqueOrderItemsQuantity()
        {
            try
            {
                var response = _orderItemService.GetAll().GroupBy(x => x.Quantity).Select(y => y.FirstOrDefault());
                if (response != null)
                {
                    List<SelectListItem> uniqueQuantities = new List<SelectListItem>();

                    foreach (var orderItem in response)
                    {
                        SelectListItem selectListItem = new SelectListItem()
                        {
                            Text = orderItem.Quantity.ToString(),
                            Value = orderItem.Id.ToString()
                        };

                        uniqueQuantities.Add(selectListItem);
                    }

                    return uniqueQuantities;
                }
                else
                {
                    _logger.LogWarning("GetUniqueOrderItemsQuantity() is null or empty");
                    return new List<SelectListItem>();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed while GetUniqueOrderItemsQuantity() with error: {ex.Message}");
                return new List<SelectListItem>();
            }

        }

        public List<SelectListItem> GetUniqueOrderItemsUnits()
        {
            try
            {
                var response = _orderItemService.GetAll().GroupBy(x => x.Unit).Select(y => y.FirstOrDefault());

                if (response != null)
                {
                    List<SelectListItem> uniqueUnits = new List<SelectListItem>();

                    foreach (var orderItem in response)
                    {
                        SelectListItem selectListItem = new SelectListItem()
                        {
                            Text = orderItem.Unit.ToString(),
                            Value = orderItem.Id.ToString()
                        };

                        uniqueUnits.Add(selectListItem);
                    }

                    return uniqueUnits;
                }
                else
                {
                    _logger.LogWarning("GetUniqueOrderItemsUnits() is null or empty");
                    return new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while GetUniqueOrderItemsUnits() with error: {ex.Message}");
                return new List<SelectListItem>();
            }
        }

        public IQueryable<Order> FilterData(DataFilterModel filter)
        {
            try
            {
                var orders = _orderService.GetAll();
                var providers = _providerService.GetAll();
                var orderItems = _orderItemService.GetAll();

                List<int> resultOrderNumber = new List<int>();
                List<int> ProviderIds = new List<int>();
                List<int> OrderItemNames = new List<int>();
                List<int> OrderItemQuantities = new List<int>();
                List<int> OrderItemUnits = new List<int>(); 

                if (filter != null)
                {
                    if (filter.OrderNumber != null && filter.OrderNumber.Count() > 0)
                    {
                        foreach (var orderNumber in filter.OrderNumber)
                        {
                            IQueryable<int> list = from o in orders
                                                   where o.Number.Contains(orderNumber)
                                                   select o.Id;

                            foreach (var item in list) { resultOrderNumber.Add(item); }
                        }
                    }
                    if (filter.ProviderId != null && filter.ProviderId.Count() > 0)
                    {
                        foreach (var orderNumber in filter.ProviderId)
                        {
                            IQueryable<int> list = from o in orders
                                                   join p in providers on o.ProviderId equals p.Id
                                                   where p.Name.Contains(orderNumber)
                                                   select o.Id;

                            foreach (var item in list) { ProviderIds.Add(item); }
                        }
                    }
                    if (filter.OrderItemName != null && filter.OrderItemName.Count() > 0)
                    {
                        foreach (var orderItem in filter.OrderItemName)
                        {
                            IQueryable<int> itemName = from o in orders
                                                       join oi in orderItems on o.Id equals oi.OrderId
                                                       where oi.Name.Contains(orderItem)
                                                       select o.Id;

                            foreach (var item in itemName) { OrderItemNames.Add(item); }
                        }
                    }
                    if (filter.OrderItemQuantity != null && filter.OrderItemQuantity.Count() > 0)
                    {
                        foreach (var orderItem in filter.OrderItemQuantity)
                        {
                            IQueryable<int> itemName = from o in orders
                                                       join oi in orderItems on o.Id equals oi.OrderId
                                                       where oi.Quantity.Equals(decimal.Parse(orderItem))
                                                       select o.Id;

                            foreach (var item in itemName) { OrderItemQuantities.Add(item); }
                        }
                    }
                    if (filter.OrderItemUnit != null && filter.OrderItemUnit.Count() > 0)
                    {
                        foreach (var orderItem in filter.OrderItemUnit)
                        {
                            IQueryable<int> itemName = from o in orders
                                                       join oi in orderItems on o.Id equals oi.OrderId
                                                       where oi.Unit.Equals(orderItem)
                                                       select o.Id;

                            foreach (var item in itemName) { OrderItemUnits.Add(item); }
                        }
                    }

                    var filteredOrderIds = Intersect(resultOrderNumber, ProviderIds, OrderItemNames, OrderItemQuantities, OrderItemUnits);

                    IQueryable<Order> resultOrders = _orderService.GetOrdersByListOfIds(filteredOrderIds.Distinct().ToList());
                    if(resultOrders != null && resultOrders.Count() > 0) 
                        return resultOrders;
                    return new List<Order>().AsQueryable();
                }
                else
                {
                    _logger.LogWarning("Filters is null return all orders");
                    return orders;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed while GetUniqueOrderItemsUnits() with error: {ex.Message}");
                return new List<Order>().AsQueryable();
            }
        }

        IEnumerable<T> Intersect<T>(params IEnumerable<T>[] lists)
        {
            return lists.Where(l => l.Any()).Aggregate((l1, l2) => l1.Intersect(l2));
        }

    }
}
