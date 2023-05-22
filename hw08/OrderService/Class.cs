using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OrderService
{
    public class Order
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public List<OrderDetail> Data { get; set; }
        public Order() 
        {
            Id = Guid.NewGuid().ToString();
            Customer = "nullOrder";
            Data = new List<OrderDetail>();
        }
        public Order(string id, string customer, List<OrderDetail> data)
        {
            Id = id;
            Customer = customer;
            Data = data;
        }
        public void AddItem(OrderDetail orderItem)
        {
            Data.Add(orderItem);
        }
        public void RemoveDetail(OrderDetail orderItem)
        {
            Data.Remove(orderItem);
        }
    }
    public class OrderDetail
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public OrderDetail() 
        {
            Name = "nullOD";
            Price=0;
        }
        public OrderDetail(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
