using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FK_Order")]
        public int OrderId { get; set; }

        [ForeignKey("FK_Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
