using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpacePark.DB.Models;
using SpacePark.DB.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark.DB.Queries.Tests
{
    [TestClass()]
    public class QueryTests
    {
        [TestMethod()]
        public void GetCustomerTest()
        {
            var mockSet = new Mock<DbSet<Customer>>();

            var mockContext = new Mock<SpaceParkDbContext>();
            mockContext.Setup(m => m.Customer).Returns(mockSet.Object);

            var service = new Query(mockContext.Object);
           
            var customer = service.GetCustomer("AAA");

            Assert.AreEqual("AAA", customer.Name);

        }
    }
}