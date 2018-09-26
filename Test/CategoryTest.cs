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
    public class CategoryTest
    {
        CategoryService categoryService;

        [TestInitialize()]
        public void Initialize()
        {
            MapperInitializer.MapperConfiguration();
            categoryService = new CategoryService(new DataContext(new DbContextOptions<DataContext>()));
        }

        [TestMethod]
        public void GetByID()
        {
            var k = categoryService.GetByID(1);
        }

        [TestMethod]
        public void GetAll()
        {
            var k = categoryService.GetAll();
        }

        [TestMethod]
        public void Create()
        {
            var k = categoryService.Create(new CategoryDO() { Name = "asd", ParentId = 1 });
        }

        [TestMethod]
        public void Edit()
        {
            var k = categoryService.Edit(new CategoryDO() { Name = "Beyaz Eþya", ParentId = 1, Id = 4 });
        }

        [TestMethod]
        public void Delete()
        {
            var k = categoryService.Delete(4);
        }
    }
}
