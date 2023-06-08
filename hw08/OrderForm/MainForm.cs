using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderApp;

namespace OrderForm
{
    public partial class MainForm : Form
    {
        OrderService os;
        public int selectedRowIndex = 0;
        public MainForm()
        {
            InitializeComponent();
            os = new OrderService();
            bso.DataSource = os.Orders;
            comboBox1.SelectedIndex = 0;
            //textBox1.DataBindings.Add("Text", this, "Keyword");
        }
        public void QueryAll()
        {
            bso.DataSource = os.Orders;
            bso.ResetBindings(false);
            bsod.DataSource = null;
            bso.ResetBindings(false);
        }
        public void SelectOd(int idx)
        {
            if (idx >= os.Orders.Count || idx < 0) return;
            bsod.DataSource = os.Orders[idx].Details;
            bsod.ResetBindings(false);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //add Order
            var ods = new List<OrderDetail>();
            //ods.Add(new OrderDetail( "aaa", 1.1));
            //ods.Add(new OrderDetail( "aaa", 2.2));
            //ods.Add(new OrderDetail( "aaa", 3.3));
            Order o = new Order("YHQ", ods);
            os.AddOrder(o);
            QueryAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //remove Order
            Order order = bso.Current as Order;
            if (order == null)
            {
                MessageBox.Show("No Order Existing!");
                return;
            }
            os.RemoveOrder(order.OrderId);
            QueryAll();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                SelectOd(selectedRowIndex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //add Detail
            Order oc = bso.Current as Order;
            if (oc == null)
            {
                MessageBox.Show("No Order Existing!");
                return;
            }
            os.AddDetail(oc.OrderId, new OrderDetail());
            QueryAll();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //remove Detail
            //Order oc = bso.Current as Order;
            OrderDetail od = bsod.Current as OrderDetail;
            if (od == null)
            {
                MessageBox.Show("No OrderDetail Existing!");
                return;
            }
            os.RemoveDetail(od.Id);
            QueryAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Query
            //All
            //Order Id
            //Cutomer
            //Good
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    QueryAll();
                    break;
                case 1://根据ID查询
                    Order order = os.GetOrder(textBox1.Text);
                    List<Order> result = new List<Order>();
                    if (order != null) result.Add(order);
                    bso.DataSource = result;
                    break;
                case 2://根据客户查询
                    bso.DataSource = os.QueryOrdersByCustomerName(textBox1.Text);
                    break;
                case 3://根据货物查询
                    bso.DataSource = os.QueryOrdersByGoodsName(textBox1.Text);
                    break;
            }
            bso.ResetBindings(false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var o = bso.Current as Order;
            var od = bsod.Current as OrderDetail;
            using (var ctx = new OrderContext())
            {
                var existingOrder = ctx.Orders.Include("Details").SingleOrDefault(order => order.OrderId == o.OrderId);

                if (existingOrder != null)
                {
                    // 更新Order对象的属性
                    existingOrder.Customer = o.Customer;

                    // 更新OrderDetail对象的属性
                    var existingOrderDetail = existingOrder.Details.FirstOrDefault(detail => detail.Id == od.Id);
                    if (existingOrderDetail != null)
                    {
                        //existingOrderDetail.Index = od.Index;
                        existingOrderDetail.Good = od.Good;
                        existingOrderDetail.Price = od.Price;
                    }
                }
                ctx.SaveChanges();
                MessageBox.Show("Successfully Saved!");
            }
        }
    }
}
