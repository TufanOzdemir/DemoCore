using AutoMapper;
using Data.Models;
using Domain.Models;
using Interface.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.IdentityServices;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ProductTest
    {
        ProductService productService;

        [TestInitialize()]
        public void Initialize()
        {
            MapperInitializer.MapperConfiguration();
            productService = new ProductService(new DataContext(new DbContextOptions<DataContext>()));
        }

        [TestMethod]
        public void GetByID()
        {
            var k = productService.GetByID(1);
        }

        [TestMethod]
        public void GetAll()
        {
            var k = productService.GetAll();
        }

        [TestMethod]
        public void GetProductListByCategoryID()
        {
            var k = productService.GetProductListByCategoryID(3);
        }

        [TestMethod]
        public void Create()
        {
            var k = productService.Create(new ProductDO() { CategoryId = 2, Description = "deniyorum", ImageUrl = "asd", IsActive = true, Price = 340, Title = "Gümüþ 5" });
        }

        [TestMethod]
        public void Edit()
        {
            var k = productService.Edit(new ProductDO() { CategoryId = 2, Description = "den", ImageUrl = "sasd", IsActive = true, Price = 340, Title = "Gümüþ 4", Id = 2 });
        }

        [TestMethod]
        public void Delete()
        {
            var k = productService.Delete(2);
        }
    }
}
