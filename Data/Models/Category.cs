using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public Category Parent { get; set; }
        public ICollection<Category> InverseParent { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
