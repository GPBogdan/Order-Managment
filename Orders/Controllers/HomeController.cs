using Microsoft.AspNetCore.Mvc;
using Orders.Models;
using System.Diagnostics;
using BLL.Services;
using BLL.Models;
using DAL.Models;
using Orders.ViewModels;

namespace Orders.Controllers
{
    public class HomeController : Controller
    {
        #region Fields announcement
        private readonly ILogger<HomeController> _logger;
        private readonly OrderService _orderService;
        private readonly ProviderService _providerService;
        private readonly OrderItemService _orderItemService;              
        private readonly ILogger<FilterService> _filterLogger;
        private readonly FilterService _filterService;
        #endregion

        public HomeController(ILogger<HomeController> logger, OrderService orderService, ProviderService providerService, 
                                    OrderItemService orderItemService)
        {
            _logger = logger;
            _orderService = orderService;
            _providerService = providerService;
            _orderItemService = orderItemService;
            _filterService = new FilterService(_filterLogger, _orderService, _providerService, _orderItemService);
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            var uniqueOrders = _filterService.GetUniqueOrders();
            var uniqueOrderItems = _filterService.GetUniqueOrderItems();
            var uniqueProviders = _filterService.GetUniqueProviders();
            var uniqueOrderItemsQuantity = _filterService.GetUniqueOrderItemsQuantity();
            var uniqueOrderItemsUnits = _filterService.GetUniqueOrderItemsUnits();
            FilterDetailsViewModel fIlterDetailsViewModel = new FilterDetailsViewModel(orders, uniqueOrders, uniqueOrderItems, uniqueProviders, 
                                                                            uniqueOrderItemsQuantity, uniqueOrderItemsUnits);

            return View(fIlterDetailsViewModel);
        }

        [HttpPost]
        public IActionResult Index(FilterDetailsViewModel filterDetails)
        {                      

            DataFilterModel filter = new DataFilterModel(filterDetails.OrderNumber, filterDetails.ProviderId, filterDetails.OrderItemName,
                filterDetails.OrderItemQuantity, filterDetails.OrderItemUnit);

            if (filter != null)
            {
                IQueryable<Order> resultOrders = _filterService.FilterData(filter);
                var uniqueOrders = _filterService.GetUniqueOrders();
                var uniqueOrderItems = _filterService.GetUniqueOrderItems();
                var uniqueProviders = _filterService.GetUniqueProviders();
                var uniqueOrderItemsQuantity = _filterService.GetUniqueOrderItemsQuantity();
                var uniqueOrderItemsUnits = _filterService.GetUniqueOrderItemsUnits();

                FilterDetailsViewModel fIlterDetailsViewModel = new FilterDetailsViewModel(resultOrders, uniqueOrders, uniqueOrderItems, uniqueProviders,
                                                                                uniqueOrderItemsQuantity, uniqueOrderItemsUnits);

                return View(fIlterDetailsViewModel);
            }

            return View("Index");
        }

       

        public IActionResult Create()
        {
            ViewBag.Providers = _providerService.GetProvidersLikeSelectListItems();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
         {
            if (ModelState.IsValid)
            {

                if (order != null && _orderService.IsOrderUnique(order))
                {
                    _orderService.Create(order);
                }

                TempData["ResultOk"] = "Order Added Successfully!";
                return RedirectToAction("Index");
            }

            return View(order);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var order = _orderService.GetOrderById(id);
                if (order != null)
                {
                    _orderService.Delete(order);
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
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}