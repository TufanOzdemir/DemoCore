﻿using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }

        public Category Category { get; set; }
    }
}
