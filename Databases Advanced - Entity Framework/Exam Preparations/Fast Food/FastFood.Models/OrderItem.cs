﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class OrderItem
    {
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public int ItemId { get; set; }

        public Item Item { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}