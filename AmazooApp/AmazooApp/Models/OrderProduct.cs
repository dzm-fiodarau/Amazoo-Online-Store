using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
