using AutoMapper;
using Data.Models;
using Domain.Models;
using Interface.Result;
using Interface.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.IdentityServices
{
    public class CategoryService : ICategoryService
    {
        DataContext _dataContext;
        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Result<List<CategoryDO>> GetAll()
        {
            Result<List<CategoryDO>> result;
            try
            {
                List<Category> category = _dataContext.Category
                    .Include(i => i.InverseParent)
                    .Include(i => i.Parent)
                    .Include(i => i.Product)
                    .AsNoTracking()
                    .ToList();

                List<CategoryDO> categoryDO = Mapper.Map<List<Category>, List<CategoryDO>>(category);
                result = new Result<List<CategoryDO>>(categoryDO);
            }
            catch (Exception ex)
            {
                result = new Result<List<CategoryDO>>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<List<CategoryDO>> GetCategoryTree()
        {
            Result<List<CategoryDO>> result;
            try
            {
                List<Category> category = _dataContext.Category
                    .Include(i => i.InverseParent)
                    .Include(i => i.Parent)
                    .Include(i => i.Product)
                    .ToList();

                List<Category> categories = category.Where(i => i.ParentId == null).ToList();

                List<CategoryDO> categoryDO = Mapper.Map<List<Category>, List<CategoryDO>>(categories);
                result = new Result<List<CategoryDO>>(categoryDO);
            }
            catch (Exception ex)
            {
                result = new Result<List<CategoryDO>>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<List<CategoryDO>> GetAllWithoutSubCategories()
        {
            Result<List<CategoryDO>> result;
            try
            {
                List<Category> category = _dataContext.Category.AsNoTracking().ToList();
                List<CategoryDO> categoryDO = Mapper.Map<List<Category>, List<CategoryDO>>(category);
                result = new Result<List<CategoryDO>>(categoryDO);
            }
            catch (Exception ex)
            {
                result = new Result<List<CategoryDO>>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<CategoryDO> GetByID(int id)
        {
            Result<CategoryDO> result;
            try
            {
                Category category = _dataContext.Category
                    .Include(i => i.InverseParent)
                    .Include(i => i.Parent)
                    .Include(i => i.Product)
                    .First(i => i.Id == id);

                CategoryDO categoryDO = Mapper.Map<Category, CategoryDO>(category);
                result = new Result<CategoryDO>(categoryDO);
            }
            catch (Exception ex)
            {
                result = new Result<CategoryDO>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<CategoryDO> GetByIDNaturalMode(int id)
        {
            Result<CategoryDO> result;
            try
            {
                Category category = _dataContext.Category.First(i => i.Id == id);
                CategoryDO categoryDO = Mapper.Map<Category, CategoryDO>(category);
                result = new Result<CategoryDO>(categoryDO);
            }
            catch (Exception ex)
            {
                result = new Result<CategoryDO>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<string> Create(CategoryDO categoryDO)
        {
            Result<string> result;
            try
            {
                Category category = Mapper.Map<CategoryDO, Category>(categoryDO);
                _dataContext.Category.Add(category);
                _dataContext.SaveChanges();
                result = new Result<string>(true,ResultTypeEnum.Success,"Başarılı!");
            }
            catch (Exception ex)
            {
                result = new Result<string>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<string> Edit(CategoryDO categoryDO)
        {
            Result<string> result;
            try
            {
                Category category = Mapper.Map<CategoryDO, Category>(categoryDO);
                _dataContext.Category.Update(category);
                _dataContext.SaveChanges();
                result = new Result<string>(true, ResultTypeEnum.Success, "Başarılı!");
            }
            catch (Exception ex)
            {
                result = new Result<string>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<string> Delete(int id)
        {
            Result<string> result;
            try
            {
                Category category = _dataContext.Category.First(i=>i.Id == id);
                _dataContext.Category.Remove(category);
                _dataContext.SaveChanges();
                result = new Result<string>(true, ResultTypeEnum.Success, "Başarılı!");
            }
            catch (Exception ex)
            {
                result = new Result<string>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }
    }
}
