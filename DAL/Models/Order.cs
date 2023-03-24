using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        
        [DisplayName(@"Order Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName(@"Order Number")]
        public string Number { get; set; }
        [DisplayName(@"Order Date")]
        public DateTime Date { get; set; }
        [DisplayName(@"Order Provider Id")]
        public int ProviderId { get; set; }

        [DisplayName(@"Provider Name")]
        public virtual Provider? Provider { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
