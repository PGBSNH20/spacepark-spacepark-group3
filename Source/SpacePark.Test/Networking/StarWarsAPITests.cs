using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpacePark.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark.Networking.Tests
{
    [TestClass()]
    public class StarWarsAPITests
    {
        StarWarsAPI starWarsAPI = new StarWarsAPI();

        [TestMethod()]
        public void UserFromStarWarsTestShouldFail()
        {
            var result = starWarsAPI.UserFromStarWars("test");
            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void UserFromStarWarsTestShouldPass()
        {
            var result = starWarsAPI.UserFromStarWars("Luke Skywalker");
            Assert.AreEqual(true, result);
        }
    }
}