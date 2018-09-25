using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ProductDO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }

        public CategoryDO Category { get; set; }
    }
}
