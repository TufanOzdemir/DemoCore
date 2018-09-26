using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Interface.Result;

namespace Interface.ServiceInterfaces
{
    public interface IProductService
    {
        Result<List<ProductDO>> GetAll();
        Result<List<ProductDO>> GetProductListByCategoryID(int id);

        Result<ProductDO> GetByID(int id);
        Result<string> Create(ProductDO productDO);
        Result<string> Edit(ProductDO productDO);
        Result<string> Delete(int id);
    }
}
