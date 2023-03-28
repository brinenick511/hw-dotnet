using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Program.Tests
{
    [TestClass()]
    public class OrderDetailTests
    {
        [TestMethod()]
        public void EqualsTest()
        {
            OrderDetail od1 = new OrderDetail("a","b",1);
            OrderDetail od2 = new OrderDetail("a","b",1);
            Assert.IsTrue(od1.Equals(od2));
            //Assert.Fail();
        }

        [TestMethod()]
        public void CreateToStringTest()
        {
            OrderDetail od1 = new OrderDetail("a", "b", 1);
            string ans = "OrderDetail:\tname=a\tcustomer=b\tamount=1";
            string output = od1.ToString();
            Assert.AreEqual<string>(ans, output);
            //Assert.Fail();
        }
    }
}