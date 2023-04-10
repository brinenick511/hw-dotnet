using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hw06
{
    
    public partial class Form1 : Form
    {
        private bool _processingSelectionChange = false;
        public OrderService OS = new OrderService();
        public List<Order> ans1 = new List<Order>();
        public List<OrderDetail> ans2 = new List<OrderDetail>();
        public Form1()
        {
            
            InitializeComponent();
            List<OrderDetail> lod1 = new List<OrderDetail>();
            lod1.Add(new OrderDetail("a", "b", 11));
            lod1.Add(new OrderDetail("a", "b", 12));
            List<OrderDetail> lod2 = new List<OrderDetail>();
            lod2.Add(new OrderDetail("c", "c", 21));
            lod2.Add(new OrderDetail("b", "c", 22));
            lod2.Add(new OrderDetail("b", "c", 23));
            OS.Add(lod1, 101);
            OS.Add(lod2, 202);
            reset();
        }
        public void refresh()
        {
            //dataGridView1.DataSource = null;
            //dataGridView2.DataSource = null;
            dataGridView1.DataSource = ans1;
            dataGridView2.DataSource = ans2;
        }
        public void reset()
        {
            ans1 = OS.list;
            ans2 = ans1.Count > 0 ? ans1[0].Data : new List<OrderDetail>();
            refresh();
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            if (dataGridView1.Rows.Count > 0) dataGridView1.Rows[0].Selected = true;
            if (dataGridView2.Rows.Count > 0 && dataGridView2.Rows[0].Cells.Count > 0) dataGridView2.Rows[0].Cells[0].Selected = true;
        }
        public void SelectOrder(object sender, EventArgs e)
        {
            if (_processingSelectionChange) return;
            try
            {
                _processingSelectionChange = true;
                // 处理选中状态的修改
                //选中Order显示OrderDetail
                //dataGridView2.DataSource = OS.list[0].Data;return;
                if (dataGridView1.SelectedRows.Count <= 0 || dataGridView1.Rows.Count <= 0) return;
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                if (selectedRow.Cells.Count <= 0 || selectedRow.Cells[0].Value == null) return;
                var x = selectedRow.Cells[0].Value.ToString();
                if (string.IsNullOrEmpty(x))
                {
                    reset();
                    return;
                }
                int id = int.Parse(x);
                var y = OS.QId(id, true);
                //var tmp = ans1;
                ans2 = y.Count > 0 ? y[0].Data : new List<OrderDetail>();
                refresh();
            }
            finally
            {
                _processingSelectionChange = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //查询订单
            bool x = radioButton1.Checked;
            string y = textBox1.Text;
            if (string.IsNullOrEmpty(y))
            {
                reset();
                return;
            }
            if (x)
            {
                //id
                int z;
                if (Int32.TryParse(y, out z))
                {
                    // 转换成功，执行需要执行的操作
                    ans1 = OS.QId(z, true);
                    ans2 = ans1.Count > 0 ? ans1[0].Data : new List<OrderDetail>();
                    refresh();
                }
                else
                {
                    // 转换失败，不执行任何操作
                    reset();
                    return;
                }
                
                return;
            }
            //text
            ans1 = OS.QFuzzy(y, true);
            ans2 = ans1.Count > 0 ? ans1[0].Data : new List<OrderDetail>();
            refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //新增订单：新建窗口
            Form2 newForm = new Form2();

            newForm.ShowDialog();
            var data = newForm.returnOD();
            var id = newForm.returnID();
            OS.Add(data, id);
            ans1 = OS.list;
            ans2 = ans1.Count > 0 ? ans1[0].Data : new List<OrderDetail>();
            refresh();
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView1.DataSource = ans1;
            dataGridView2.DataSource = ans2;
            if (dataGridView1.Rows.Count > 0) dataGridView1.Rows[0].Selected = true;
            if (dataGridView2.Rows.Count > 0 && dataGridView2.Rows[0].Cells.Count > 0) dataGridView2.Rows[0].Cells[0].Selected = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //删除
            if (dataGridView1.SelectedRows.Count <= 0 || dataGridView1.Rows.Count <= 0) return;
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == false) continue;
                //MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
                int id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                OS.Delete(id);
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView1.DataSource = ans1;
                dataGridView2.DataSource = ans2;
            }
        }
    }


    public class OrderService
    {
        public List<Order> list;
        public List<Order> QId(int id, bool b)
        {
            var query = from o in list
                        where o.Id == id
                        select o;
            //var query = list.Where(o => o.Id == id);
            var ans = new List<Order>();
            foreach (var x in query)
                ans.Add(x);
            return ans;
        }
        public List<Order> QFuzzy(string s, bool b)
        {
            var query = list.Where(o => o.Data.Exists(od => od.Name.IndexOf(s) != -1 || od.Customer.IndexOf(s) != -1||od.Amount.ToString().IndexOf(s)!=-1)).OrderBy(o => o.Id);
            var ans = new List<Order>();
            foreach (var x in query)
                ans.Add(x);
            return ans;
        }
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
                ans += (x.ToString()) + "\n";
            return (ans == "") ? "None" : ans;
        }
        public string QName(string s)
        {
            var query = list.Where(o => o.Data.Exists(od => od.Name == s)).OrderBy(o => o.Id);
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
            var query = list.Where(o => o.Data.Exists(od => od.Name.IndexOf(s) != -1 || od.Customer.IndexOf(s) != -1)).OrderBy(o => o.Id);
            string ans = "";
            foreach (var x in query)
                ans += (x.ToString()) + "\n";
            return (ans == "") ? "None" : ans;
        }
    }
    public class Student
    {
        public string A { get; set; }
        public string B { get; set; }
        public Student()
        {
            A = "A"; B = "B";
        }
        public Student(string aa, string bb)
        {
            A = aa; B = bb;
        }
    }
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
            //foreach (var dod in Data) if (dod.Equals(od)) throw new SystemException("OrderDetail.Add失败:订单明细OrderDetail不能重复");
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
}
