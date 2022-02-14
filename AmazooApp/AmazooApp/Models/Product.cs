using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int ProductName { get; set; }

        public int Description { get; set; }

        public int QuantityInStock { get; set; }

        public int Price { get; set; }

        public int Category { get; set; }

        public int Image { get; set; }

        public int Brand { get; set; }
    }
}
