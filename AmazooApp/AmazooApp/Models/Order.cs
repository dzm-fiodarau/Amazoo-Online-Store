using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int Customer { get; set; }

        public string Status { get; set; }

        [Display(Name = "Delivery Date")]
        public string DeliveryDate { get; set; }

        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }

        [Display(Name = "Total Paid")]
        public float TotalPaid { get; set; }
    }
}
