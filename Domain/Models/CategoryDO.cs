using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CategoryDO
    {
        public CategoryDO()
        {
            SubCategoryList = new List<CategoryDO>();
            ProductList = new List<ProductDO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public CategoryDO ParentCategory { get; set; }
        public List<CategoryDO> SubCategoryList { get; set; }
        public List<ProductDO> ProductList { get; set; }
    }
}
