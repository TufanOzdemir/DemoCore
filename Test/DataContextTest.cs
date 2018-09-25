using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Test
{
    [TestClass]
    public class DataContextTest
    {
        [TestMethod]
        public void CategoryTest()
        {
            DataContext dataContext = new DataContext(new Microsoft.EntityFrameworkCore.DbContextOptions<DataContext>());
            var k = dataContext.Category.Include(i=>i.Product).Include(i => i.Parent).Last();
        }
    }
}
