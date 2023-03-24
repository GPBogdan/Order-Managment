using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Orders.ViewModels;

namespace Orders.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProviderService _providerService;
        private readonly OrderItemService _orderItemService;
        private readonly ILogger<HomeController> _logger;

        public OrderDetailsController(ILogger<HomeController> logger, OrderService orderService, OrderItemService orderItemService, ProviderService providerService)
        {
            _logger = logger;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _providerService = providerService;
        }

        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                Order? order = _orderService.GetOrderById(id);
                IQueryable<OrderItem> orderItems = _orderItemService.GetOrderItemsListByOrderId(id);
                if (order != null && orderItems != null)
                {
                    Provider provider = _providerService.GetProviderById(order.ProviderId);
                    OrderDetailsViewModel orderDetails = new OrderDetailsViewModel(order, provider, orderItems);

                    return View(orderDetails);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Edit(OrderDetailsViewModel orderDetails)
        {

            if (orderDetails.Order != null && _orderService.IsOrderUnique(orderDetails.Order))
            {
                _orderService.Update(orderDetails.Order);
            }

            TempData["ResultOk"] = "Data Updated Successfully !";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddItemToOrder(int id)
        {
            OrderItem orderItem = new OrderItem();

            if (id > 0)
            {
                orderItem.OrderId = id;               
            }         

            return View(orderItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItemToOrder(OrderItem orderItem)
        {
            if(orderItem != null)
            {
                //Add new Item to Order
                orderItem.Order = _orderService.GetOrderById(orderItem.OrderId ?? 0);
                _orderItemService.Create(orderItem);
                return RedirectToAction("Edit", new { id = orderItem.OrderId });
            }
            else
            {
                return View();
            }
           
        }

        public IActionResult DeleteOrderItem(int id)
        {
            if (id > 0)
            {
                OrderItem orderItem = _orderItemService.GetOrderItemById(id);
                if (orderItem != null)
                {
                    _orderItemService.Delete(orderItem);
                    return RedirectToAction("Edit", new { id = orderItem.OrderId });
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
