using AutoMapper;
using Data.Models;
using Domain.Models;
using Interface.Result;
using Interface.ServiceInterfaces;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Service.IdentityServices
{
    public class ProductService : IProductService
    {
        DataContext _dataContext;
        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public Result<List<ProductDO>> GetAll()
        {
            Result<List<ProductDO>> result;
            try
            {
                List<Product> product = _dataContext.Product.AsNoTracking().ToList();
                    
                List<ProductDO> productDO = Mapper.Map<List<Product>, List<ProductDO>>(product);
                result = new Result<List<ProductDO>>(productDO);
            }
            catch (Exception ex)
            {
                result = new Result<List<ProductDO>>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<List<ProductDO>> GetProductListByCategoryID(int id)
        {
            Result<List<ProductDO>> result;
            try
            {
                List<Product> product = _dataContext.Product
                    .Include(i=>i.Category)
                    .Where(i=>i.CategoryId == id)
                    .AsNoTracking()
                    .ToList();

                List<ProductDO> productDO = Mapper.Map<List<Product>, List<ProductDO>>(product);
                result = new Result<List<ProductDO>>(productDO);
            }
            catch (Exception ex)
            {
                result = new Result<List<ProductDO>>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<ProductDO> GetByID(int id)
        {
            Result<ProductDO> result;
            try
            {
                Product product = _dataContext.Product
                    .Include(i => i.Category)
                    .Single(i => i.Id == id);
                ProductDO productDO = Mapper.Map<Product, ProductDO>(product);
                result = new Result<ProductDO>(productDO);
            }
            catch (Exception ex)
            {
                result = new Result<ProductDO>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }
        
        public Result<string> Create(ProductDO productDO)
        {
            Result<string> result;
            try
            {
                Product product = Mapper.Map<ProductDO, Product>(productDO);
                _dataContext.Product.Add(product);
                _dataContext.SaveChanges();
                result = new Result<string>(true, ResultTypeEnum.Success, "İşlem Başarılı!");
            }
            catch (Exception ex)
            {
                result = new Result<string>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }

        public Result<string> Edit(ProductDO productDO)
        {
            Result<string> result;
            try
            {
                Product product = Mapper.Map<ProductDO, Product>(productDO);
                _dataContext.Product.Update(product);
                _dataContext.SaveChanges();
                result = new Result<string>(true, ResultTypeEnum.Success, "İşlem Başarılı!");
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
                Product product = _dataContext.Product.First(i => i.Id == id);
                _dataContext.Product.Remove(product);
                _dataContext.SaveChanges();
                result = new Result<string>(true, ResultTypeEnum.Success, "İşlem Başarılı!");
            }
            catch (Exception ex)
            {
                result = new Result<string>(false, ResultTypeEnum.Error, ex.ToString());
            }
            return result;
        }
    }
}