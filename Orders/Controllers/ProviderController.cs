using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Controllers
{
    public class ProviderController : Controller
    {
        private readonly ProviderService _providerService;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(ILogger<ProviderController> logger, ProviderService providerService)
        {
            _providerService = providerService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var orderList = _providerService.GetAll();

            return View(orderList);
        }
        public IActionResult Create()
        {  
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Provider provider)
        {
            if (ModelState.IsValid)
            {
                if (provider != null)
                {
                    _providerService.Create(provider);
                }

                TempData["ResultOk"] = "Order Added Successfully!";
                return RedirectToAction("Index");
            }

            return View(provider);
        }

        public IActionResult Edit(int id)
        {
            if (id > 0)
            {
                var order = _providerService.GetProviderById(id);
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
        public IActionResult Edit(Provider provider)
        {
            if (ModelState.IsValid)
            {
                _providerService.Update(provider);

                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(provider);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var provider = _providerService.GetProviderById(id);
                if (provider != null)
                {
                    _providerService.Delete(provider);
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
