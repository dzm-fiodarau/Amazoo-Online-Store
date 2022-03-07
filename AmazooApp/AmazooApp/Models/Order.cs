using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Models
{
    public class Order
    {

        [Key]
        public int Id { get; set; }

        public string Customer { get; set; }

        public string Status { get; set; }

        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        [Column(TypeName="Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Total Paid")]
        public float TotalPaid { get; set; }

    }
}
