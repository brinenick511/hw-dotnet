using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService
{
    public class OrderService
    {
        public List<Order> Orders { get; set; }
        public OrderService() 
        {
            Orders= new List<Order>();
            Orders.Add(new Order());
            var od = new OrderDetail("Egg", 1.14);
            Orders[0].Data.Add(od);
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
                var order = ctx.Orders.Include("Data")
                  .SingleOrDefault(o => o.Id == orderId);
                if (order == null) return;
                ctx.OrderDetails.RemoveRange(order.Data);
                ctx.Orders.Remove(order);
                ctx.SaveChanges();
            }
        }
        public Order GetOrder(string id)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders
                  //.Include(o => o.Data.Select(d => d.Name))
                  //.Include(o => o.Customer)
                  .SingleOrDefault(o => o.Id == id);
            }
        }
        public List<Order> QueryOrdersByGoodsName(string goodsName)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders
                  .Include(o => o.Data.Select(d => d.Name))
                  .Include(o => o.Customer)
                  .Where(order => order.Data.Any(item => item.Name == goodsName))
                  .ToList();
            }
        }
    }
}
