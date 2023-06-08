using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using OrderApi.Models;

namespace OrderApi.Models
{

    /**
     * The service class to manage orders
     * */
    public class OrderService
    {

        OrderContext dbContext;

        public OrderService(OrderContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Order> GetAllOrders()
        {
            return dbContext.Orders
                .Include(o => o.Details)
                .Include(o => o.Customer)
                .ToList<Order>();
        }

        public Order GetOrder(string id)
        {
            return dbContext.Orders
                .Include(o => o.Details)
                .Include(o => o.Customer)
                .SingleOrDefault(o => o.OrderId == id);
        }

        public void AddOrder(Order order)
        {
            dbContext.Entry(order).State = EntityState.Added;
            dbContext.SaveChanges();
        }

        public void RemoveOrder(string orderId)
        {
            var order = dbContext.Orders
                .Include(o => o.Details)
                .SingleOrDefault(o => o.OrderId == orderId);
            if (order == null) return;
            dbContext.OrderDetails.RemoveRange(order.Details);
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }

        public List<Order> QueryOrdersByGoodsName(string goodsName)
        {
            var query = dbContext.Orders
                .Include(o => o.Details)
                .Include(o => o.Customer)
                .Where(order => order.Details.Any(item => item.Good == goodsName));
            return query.ToList();
        }

        public List<Order> QueryOrdersByCustomerName(string customerName)
        {
            return dbContext.Orders
                .Include(o => o.Details)
                .Include("Customer")
              .Where(order => order.Customer == customerName)
              .ToList();
        }

        public void UpdateOrder(Order newOrder)
        {
            RemoveOrder(newOrder.OrderId);
            AddOrder(newOrder);
        }
    }
}
