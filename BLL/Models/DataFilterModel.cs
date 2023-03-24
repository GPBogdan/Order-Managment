using DAL.Models;

namespace BLL.Models
{
    public class DataFilterModel
    {
        public List<string> OrderNumber { get; set; } 
        public List<string> ProviderId { get; set; }
        public List<string> OrderItemName { get; set; }
        public List<string> OrderItemQuantity { get; set; }
        public List<string> OrderItemUnit { get; set; }

        public DataFilterModel() { }

        public DataFilterModel(List<string> orderNumber, List<string> providerId, List<string> orderItemName, List<string> orderItemQuantity, List<string> orderItemUnit)
        {
            OrderNumber = orderNumber;
            ProviderId = providerId;
            OrderItemName = orderItemName;
            OrderItemQuantity = orderItemQuantity;
            OrderItemUnit = orderItemUnit;
        }

    }
}
