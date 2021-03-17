using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpacePark;

namespace SpacePark.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 c = new Class1();
            Assert.AreEqual("Test", c.Test());
        }
    }
}
