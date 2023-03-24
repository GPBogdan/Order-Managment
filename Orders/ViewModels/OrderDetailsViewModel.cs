using DAL.Models;


namespace Orders.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public Provider Provider { get; set; }
        public IQueryable<OrderItem> OrderItems { get; set; }

        public OrderDetailsViewModel() { }

        public OrderDetailsViewModel(Order order, Provider provider, IQueryable<OrderItem> orderItems) 
        { 
            Order = order;
            Provider = provider;
            OrderItems = orderItems;
        }


    }
}
