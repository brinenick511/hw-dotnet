using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var ods = new List<OrderDetail>();
            ods.Add(new OrderDetail( "aaa", 1.1));
            ods.Add(new OrderDetail( "aaa", 2.2));
            ods.Add(new OrderDetail( "aaa", 3.3));
            Order o = new Order("me", ods);

            OrderService os = new OrderService();
            os.RemoveAll();
            os.AddOrder(o);
            List<Order> orders = os.Orders;
            orders.ForEach(oo => Console.WriteLine(oo));

            Console.ReadKey();
        }
    }
}
