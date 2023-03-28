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
    public class OrderServiceTests
    {
        [TestMethod()]
        public void AddTest()
        {
            List<OrderDetail> lod1=new List<OrderDetail>();
            lod1.Add(new OrderDetail("a", "b", 1));
            List<OrderDetail> lod2 = new List<OrderDetail>();
            lod2.Add(new OrderDetail("a", "b", 2));
            OrderService OS = new OrderService();
            OS.Add(lod1, 1);
            OS.Add(lod2, 2);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            List<OrderDetail> lod1 = new List<OrderDetail>();
            lod1.Add(new OrderDetail("a", "b", 1));
            List<OrderDetail> lod2 = new List<OrderDetail>();
            lod2.Add(new OrderDetail("a", "b", 2));
            OrderService OS = new OrderService();
            OS.Add(lod1, 1);
            OS.Add(lod2, 2);
            OS.Delete(1);
            //OS.Delete(1);
            //Assert.Fail();
        }

        [TestMethod()]
        public void AlterTest()
        {
            List<OrderDetail> lod1 = new List<OrderDetail>();
            lod1.Add(new OrderDetail("a", "b", 1));
            List<OrderDetail> lod2 = new List<OrderDetail>();
            lod2.Add(new OrderDetail("a", "b", 2));
            OrderService OS = new OrderService();
            OS.Add(lod1, 1);
            OS.Alter(1,lod2);
            //Assert.Fail();
        }

        [TestMethod()]
        public void SortTest()
        {
            List<OrderDetail> lod1 = new List<OrderDetail>();
            lod1.Add(new OrderDetail("a", "b", 1));
            List<OrderDetail> lod2 = new List<OrderDetail>();
            lod2.Add(new OrderDetail("a", "b", 2));
            OrderService OS = new OrderService();
            OS.Add(lod2, 2);
            OS.Add(lod1, 1);
            OS.Sort();
            Assert.IsTrue(OS.list[0].Id == 1 && OS.list[1].Id == 2);
            //Assert.Fail();
        }
        [TestMethod()]
        public void QTest()
        {

        }
    }
}