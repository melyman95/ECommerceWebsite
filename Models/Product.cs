using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebsite.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// consumer facing name of the product
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// US currency retail price (dollars)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// e.g. Electronics, food and drink, hardware
        /// </summary>
        public string Category { get; set; }
    }
}
