using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Model
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public int productId { get; set; }
        public DateTime orderDate { get; set; }
        public int Quantity { get; set; }
        public float Amount { get; set; }

        [ForeignKey("productId")]
        public Product product { get; set; }
    }
}
