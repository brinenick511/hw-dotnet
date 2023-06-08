using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Customer { get; set; }
        public List<OrderDetail> Details { get; set; }
        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
            Details = new List<OrderDetail>();
        }
        public Order(string customer, List<OrderDetail> details) : this()
        {
            OrderId = Guid.NewGuid().ToString();
            Customer = customer;
            Details = details;
        }
        public override string ToString()
        {
            string ans = $"Id:{OrderId}, customer:{Customer}";
            //StringBuilder strBuilder = new StringBuilder();
            //strBuilder.Append($"Id:{OrderId}, customer:{Customer}");
            Details.ForEach(od => ans += ("\n\t" + od.ToString()));
            return ans;
        }
    }
    public class OrderDetail
    {
        public string Id { get; set; }
        //public int Index { get; set; }
        public string Good { get; set; }
        public double Price { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
        public OrderDetail(string good, double price)
        {
            Id = Guid.NewGuid().ToString();
            //Index = index;
            Good = good;
            Price = price;
        }
        public override string ToString()
        {
            return $"[Id.:{Id},goods:{Good},price:{Price}]";
        }
    }
}
