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
    public class CategoryTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            MapperInitializer.MapperConfiguration();
        }

        [TestMethod]
        public void Test()
        {
            DataContext dataContext = new DataContext(new DbContextOptions<DataContext>());
            var k = dataContext.Category.Include(i=>i.Product).Include(i => i.Parent).Include(i=>i.InverseParent).First();
            CategoryDO categoryDO = Mapper.Map<Category, CategoryDO>(k);
        }
    }
}
