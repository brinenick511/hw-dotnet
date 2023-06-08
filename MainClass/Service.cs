using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp
{
    public class OrderService
    {
        public List<Order> Orders
        {
            get
            {
                using (var ctx = new OrderContext())
                {
                    return ctx.Orders
                      //.Include(o => o.Details.Select(d => d.GoodsItem))
                      .Include(o => o.Details)
                      .ToList<Order>();
                }
            }
        }

        public OrderService()
        {
            //using (var ctx = new OrderContext())
            //{
            //    ctx.Orders.Add(new Order("Me"));
            //}
        }
        public Order GetOrder(string id)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders
                  .Include(o => o.Details)
                  //.Include(o => o.Customer)
                  .SingleOrDefault(o => o.OrderId == id);
            }
        }
        public void AddOrder(Order order)
        {
            using (var ctx = new OrderContext())
            {
                ctx.Entry(order).State = EntityState.Added;
                ctx.SaveChanges();
            }
        }
        public void RemoveOrder(string orderId)
        {
            using (var ctx = new OrderContext())
            {
                var order = ctx.Orders.Include("Details")
                  .SingleOrDefault(o => o.OrderId == orderId);
                if (order == null) return;
                ctx.OrderDetails.RemoveRange(order.Details);
                ctx.Orders.Remove(order);
                ctx.SaveChanges();
            }
        }
        public void AddDetail(string orderId, OrderDetail od)
        {
            using (var ctx = new OrderContext())
            {
                var order = ctx.Orders.Include(o => o.Details).SingleOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    od.OrderId = order.OrderId;
                    od.Order = order;
                    order.Details.Add(od);
                    ctx.SaveChanges();
                }
            }
        }
        public void RemoveDetail(string id)
        {
            using (var ctx = new OrderContext())
            {
                var orderDetail = ctx.OrderDetails
                  .SingleOrDefault(od => od.Id == id);
                if (orderDetail == null) return;
                ctx.OrderDetails.Remove(orderDetail);
                ctx.SaveChanges();
            }
        }
        public void UpdateOrder(Order newOrder)
        {
            RemoveOrder(newOrder.OrderId);
            AddOrder(newOrder);
        }
        public void RemoveAll()
        {
            //Orders.ForEach(o => RemoveOrder(o.OrderId));
            using (var ctx = new OrderContext()) 
            {
                ctx.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 0");
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE orderdetails");
                ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE orders");
                ctx.Database.ExecuteSqlCommand("SET FOREIGN_KEY_CHECKS = 1");
                ctx.SaveChanges();
            }
        }
        public List<Order> QueryOrdersByGoodsName(string goodsName)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders
                  .Include(o => o.Details)
                  .Where(order => order.Details.Any(item => item.Good == goodsName))
                  .ToList();
            }
        }

        public List<Order> QueryOrdersByCustomerName(string customerName)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders
                  .Include(o => o.Details)
                  .Where(order => order.Customer == customerName)
                  .ToList();
            }
        }
    }
}
