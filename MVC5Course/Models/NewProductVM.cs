using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class NewProductVM
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(1.0, 999.9)]
        public Nullable<decimal> Price { get; set; }
    }
}