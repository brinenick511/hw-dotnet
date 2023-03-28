using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderDetail> Data;
        public Order()
        {
            Data = new List<OrderDetail>();
        }
        public void Add(OrderDetail od)
        {
            foreach (var dod in Data)
                if (dod.Equals(od))
                    throw new SystemException("OrderDetail.Add失败:订单明细OrderDetail不能重复");
            Data.Add(new OrderDetail(od));
        }
        public void Delete()
        {
            Data.Clear();
        }
        public override string ToString()
        {
            string ans = $"Order:\tid={Id}\n";
            foreach (var od in Data)
                ans += od.ToString() + "\n";
            return ans;
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            Order o = (Order)obj;
            return (o.Id == this.Id);
            //if (o.Id == this.Id)
            //    return true;
            //var d = o.Data;
            //if (d.Count != Data.Count)
            //    return false;
            //for (int i = 0; i < d.Count; i++)
            //    if (d[i].Equals(Data[i]) == false)
            //        return false;
            //return true;
        }

    }
    public class OrderDetail
    {
        public string Name { get; set; }
        public string Customer { get; set; }
        public double Amount { get; set; }
        public OrderDetail()
        {
            Amount = -1;
            Name = Customer = "Default";
        }
        public OrderDetail(string n, string c, double a)
        {
            Name = n; Customer = c; Amount = a;
        }
        public OrderDetail(OrderDetail od)
        {
            Name = od.Name; Customer = od.Customer; Amount = od.Amount;
        }
        public override string ToString()
        {
            //Console.WriteLine($"id={Id}\tname={Name}\tcustomer={Customer}\tamount={Amount}");
            return ($"OrderDetail:\tname={Name}\tcustomer={Customer}\tamount={Amount}");
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            OrderDetail od = (OrderDetail)obj;
            return (Name == od.Name) && (Customer == od.Customer) && (Amount == od.Amount);
        }
    }
    public class OrderService
    {
        public List<Order> list;
        public OrderService() { list = new List<Order>(); }
        public void Add(List<OrderDetail> lod, int id)
        {
            Order o = new Order();
            o.Data = new List<OrderDetail>(lod);
            o.Id = id;
            foreach (var x in list)
                if (x.Equals(o))
                    throw new SystemException("Order.Add失败:订单Order不能重复");
            list.Add(o);
        }
        public void Delete(int id)
        {
            bool flag = true;
            for (int i = 0; i < list.Count; i++)
                if (list[i].Id == id)
                {
                    list.RemoveAt(i);
                    flag = false;
                }
            if (flag)
                throw new SystemException("Delete失败:没有找到指定id的订单Order");
        }
        public void Alter(int id, List<OrderDetail> data)
        {
            bool flag = true;
            for (int i = 0; i < list.Count; i++)
                if (list[i].Id == id)
                {
                    flag = false;
                    list[i].Data = new List<OrderDetail>(data);
                }
            if (flag)
                throw new SystemException("Alter失败:没有找到指定id的订单Order");
        }
        public void Sort()
        {
            list.Sort((a, b) => a.Id.CompareTo(b.Id));
        }
        public string QId(int id)
        {
            var query = from o in list
                        where o.Id == id
                        select o;
            //var query = list.Where(o => o.Id == id);
            string ans = "";
            foreach (var x in query)
                ans+=(x.ToString())+"\n";
            return (ans == "") ? "None" : ans;
        }
        public string QName(string s)
        {
            var query = list.Where(o=>o.Data.Exists(od => od.Name==s)).OrderBy(o=>o.Id);
            string ans = "";
            foreach (var x in query)
                ans += (x.ToString()) + "\n";
            return (ans == "") ? "None" : ans;
        }
        public string QCus(string s)
        {
            var query = list.Where(o => o.Data.Exists(od => od.Customer == s)).OrderBy(o => o.Id);
            string ans = "";
            foreach (var x in query)
                ans += (x.ToString()) + "\n";
            return (ans == "") ? "None" : ans;
        }
        public string QAmount(double a)
        {
            var query = list.Where(o => o.Data.Exists(od => od.Amount == a)).OrderBy(o => o.Id);
            string ans = "";
            foreach (var x in query)
                ans += (x.ToString()) + "\n";
            return (ans == "") ? "None" : ans;
        }
        public string QFuzzy(string s)
        {
            var query = list.Where(o => o.Data.Exists(od=>od.Name.IndexOf(s)!=-1||od.Customer.IndexOf(s)!=-1)).OrderBy(o => o.Id);
            string ans = "";
            foreach (var x in query)
                ans += (x.ToString()) + "\n";
            return (ans == "") ? "None" : ans;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<OrderDetail> lod1 = new List<OrderDetail>();
            lod1.Add(new OrderDetail("a", "b", 11));
            lod1.Add(new OrderDetail("a", "b", 12));
            List<OrderDetail> lod2 = new List<OrderDetail>();
            lod2.Add(new OrderDetail("a", "c", 21));
            lod2.Add(new OrderDetail("b", "c", 22));
            OrderService OS = new OrderService();
            OS.Add(lod1, 1);
            OS.Add(lod2, 2);
            //Console.WriteLine(OS.QCus("b"));
            //Console.WriteLine(OS.QAmount(21));
            Console.WriteLine(OS.QFuzzy("a"));

            Console.ReadKey();
        }
    }
}
