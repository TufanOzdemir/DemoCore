using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class CategoryCreateViewModel
    {
        public CategoryCreateViewModel()
        {
            CategoryList = new List<CategoryDO>();
        }

        public CategoryDO CategoryDO { get; set; }
        public List<CategoryDO> CategoryList { get; set; }
        public bool IsSubCategory { get; set; }
    }
}
