using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class ProductCreateViewModel
    {
        public ProductDO ProductDO { get; set; }
        public List<CategoryDO> CategoryList { get; set; }
    }
}
