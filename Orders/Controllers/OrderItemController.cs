using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly OrderItemService _orderItemService;
        private readonly ILogger<OrderItemController> _logger;

        public OrderItemController(ILogger<OrderItemController> logger, OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var orderList = _orderItemService.GetAll();

            return View(orderList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                if (orderItem != null)
                {
                     _orderItemService.Create(orderItem);
                }

                TempData["ResultOk"] = "Order Added Successfully!";
                return RedirectToAction("Index");
            }

            return View(orderItem);
        }

        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                OrderItem order = _orderItemService.GetOrderItemById(id);
                if (order != null)
                {
                    return View(order);
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _orderItemService.Update(orderItem);

                TempData["ResultOk"] = "Data Updated Successfully!";
                return RedirectToAction("Index");
            }

            return View(orderItem);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                OrderItem orderItem = _orderItemService.GetOrderItemById(id);
                if (orderItem != null)
                {
                    _orderItemService.Delete(orderItem);
                    return RedirectToAction("Index");
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
