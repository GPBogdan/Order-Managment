using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Orders.ViewModels
{
    public class FilterDetailsViewModel
    {
        public IQueryable<Order> Orders { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SelectListItem> OrdersFilteredList { get; set; }
        public List<SelectListItem> OrderItemsFilteredList { get; set; }
        public List<SelectListItem> ProvidersFilteredList { get; set; }
        public List<SelectListItem> OrderItemsQuantityFilteredList { get; set; }
        public List<SelectListItem> OrderItemsUnitsFilteredList { get; set; }

        [BindProperty]
        public List<string> OrderNumber { get; set; }
        [BindProperty]
        public List<string> ProviderId { get; set; }
        [BindProperty]
        public List<string> OrderItemName { get; set; }
        [BindProperty]
        public List<string> OrderItemQuantity { get; set; }
        [BindProperty]
        public List<string> OrderItemUnit { get; set; }

        public FilterDetailsViewModel() { }

        public FilterDetailsViewModel(IQueryable<Order> orders, List<SelectListItem> ordersFilteredList, List<SelectListItem> orderItems, List<SelectListItem> providers,
                                    List<SelectListItem> orderItemsQuantity, List<SelectListItem> orderItemsUnits)
        {
            Orders = orders;
            OrdersFilteredList = ordersFilteredList;
            OrderItemsFilteredList = orderItems;
            ProvidersFilteredList = providers;
            OrderItemsQuantityFilteredList = orderItemsQuantity;
            OrderItemsUnitsFilteredList = orderItemsUnits;
            StartDate = DateTime.Now.AddMonths(-1);
            EndDate = DateTime.Now;
        }
    }
}
