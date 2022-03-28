using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string Customer { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public int ProductId { get; set; }
    }
}
