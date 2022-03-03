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

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Display(Name = "In Stock")]
        public int QuantityInStock { get; set; }

        public float Price { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }

        public string Brand { get; set; }

    }
}
