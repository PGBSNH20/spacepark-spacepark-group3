using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpacePark.DB.Models;
using SpacePark.Networking;

namespace SpacePark.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void APIWorkingTest()
        {
            StarWarsAPI api = new();
            List<Ship> emptyShipList = new();
            Assert.AreNotSame(api.GetStarWarsShips(), emptyShipList);
        }

        [TestMethod]
        public void APINameIsStarWars()
        {
            StarWarsAPI api = new();
            Assert.AreEqual(api.UserFromStarWars("Darth Vader"), true);
        }

        [TestMethod]
        public void APINameIsNotStarWars()
        {
            StarWarsAPI api = new();
            Assert.AreEqual(api.UserFromStarWars("Michael Jackson"), false);
        }
    }
}
