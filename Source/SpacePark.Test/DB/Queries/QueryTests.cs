using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpacePark.DB.Interfaces;
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

        [TestMethod]
        public void CreateCustomer_saves_a_Customer_via_context()
        {
            var customer = new Customer
            {
                ID = 1500,
                Name = "test"
            };
            var mockSet = new Mock<DbSet<Customer>>();

            var mockContext = new Mock<SpaceParkDbContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
            var service = new Query(mockContext.Object);
            service.CreateCustomer(customer);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

        }



        [TestMethod()]
        public void GetCustomerTest()
        {
            var data = new List<Customer>
            {
                new Customer {  ID = 3 },
                 new Customer {  ID = 2 },
                  new Customer {  ID = 4, Name="test" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SpaceParkDbContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            var service = new Query(mockContext.Object);

            var customer = service.GetCustomer("test");
            Assert.AreEqual("test", customer.Name);

        }



        [TestMethod()]
        public void GetAllParkingStatusTest()
        {
            var data = new List<ParkingStatus>
            {
                new ParkingStatus {  ID = 1, ArrivalTime= new DateTime(), DepartureAt= new DateTime(), CustomerID= 1, SpotID= 1 },
                 new ParkingStatus {  ID = 2, ArrivalTime= new DateTime(), DepartureAt= new DateTime(), CustomerID= 2, SpotID= 2 },
                  new ParkingStatus {  ID = 2,ArrivalTime= new DateTime(), DepartureAt= new DateTime(), CustomerID= 3, SpotID= 3 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ParkingStatus>>();
            mockSet.As<IQueryable<ParkingStatus>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ParkingStatus>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ParkingStatus>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ParkingStatus>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SpaceParkDbContext>();
            mockContext.Setup(m => m.ParkingStatuses).Returns(mockSet.Object);

            var service = new Query(mockContext.Object);

            var parkingStatuses = service.GetAllParkingStatus();


            Assert.AreEqual(3, parkingStatuses.Count);

        }


    }
}