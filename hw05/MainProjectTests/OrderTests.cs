using Microsoft.VisualStudio.TestTools.UnitTesting;
using MainProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Tests
{
    [TestClass()]
    public class OrderTests
    {
        [TestMethod()]
        public void AddEqualsTest()
        {
            Order o1 = new Order();
            o1.Id = 1;
            o1.Add(new OrderDetail("a", "b", 1));
            o1.Add(new OrderDetail("c", "d", 2));
            Order o2 = new Order();
            o2.Id = 2;
            o2.Add(new OrderDetail("a", "b", 1));
            o2.Add(new OrderDetail("c", "d", 2));
            Assert.IsFalse(o1.Equals(o2));
            //Assert.Fail();
        }
    }
}