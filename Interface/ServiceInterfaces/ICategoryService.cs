using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using Interface.Result;

namespace Interface.ServiceInterfaces
{
    public interface ICategoryService
    {
        Result<List<CategoryDO>> GetAll();
        Result<List<CategoryDO>> GetCategoryTree();
        Result<List<CategoryDO>> GetAllWithoutSubCategories();
        Result<CategoryDO> GetByID(int id);
        Result<CategoryDO> GetByIDNaturalMode(int id);
        Result<string> Create(CategoryDO categoryDO);
        Result<string> Edit(CategoryDO categoryDO);
        Result<string> Delete(int id);
    }
}
