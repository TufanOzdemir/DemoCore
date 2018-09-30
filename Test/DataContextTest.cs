using AutoMapper;
using Data.Models;
using Domain.Models;
using Interface.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Test
{
    [TestClass]
    public class DataContextTest
    {
        DataContext dataContext;
        [TestInitialize()]
        public void Initialize()
        {
            dataContext = new DataContext(new DbContextOptions<DataContext>());
            MapperInitializer.MapperConfiguration();
        }


        [TestMethod]
        public void DeleteAllProduct()
        {
            //Test methodu asenkron olmadýðý için bu þekilde procedure çaðýrdým kod patlýyor ama procedure çalýþýyor.
            //Bu procedure tüm ürünleri silmek için kullanýlmaktadýr.
            dataContext.Product.FromSql("DeleteDatabaseTableItems").ToArray();
        }

        [TestMethod]
        public void CategoryTest()
        {
            var k = dataContext.Category.Include(i=>i.Product).Include(i => i.Parent).Include(i=>i.InverseParent).First();
            CategoryDO categoryDO = Mapper.Map<Category, CategoryDO>(k);
        }
    }
}
