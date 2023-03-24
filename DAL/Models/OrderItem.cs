using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public partial class OrderItem
    {
        [DisplayName(@"Order Item Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName(@"Order Id")]
        public int? OrderId { get; set; }
        [DisplayName(@"Order Item Name")]
        public string Name { get; set; }
        [DisplayName(@"Order Item Quantity")]
        public decimal Quantity { get; set; }
        [DisplayName(@"Order Item Unit")]
        public string Unit { get; set; }

        public virtual Order Order { get; set; }
    }
}
