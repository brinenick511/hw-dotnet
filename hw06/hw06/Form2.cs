using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace hw06
{
    public partial class Form2 : Form
    {
        //public int maxnum;
        public List<OrderDetail> od;
        public int id;
        public Form2()
        {
            InitializeComponent();
            od = new List<OrderDetail>();
            od.Add(new OrderDetail());
            dataGridView1.DataSource = od;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new
            od.Add(new OrderDetail());
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = od;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //delete
            if (od.Count <= 0) return;
            od.RemoveAt(od.Count - 1);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = od;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Save
            this.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            textBox1.Text = Regex.Replace(text, "[^0-9]", "");
        }
        public List<OrderDetail> returnOD()
        {
            return od;
        }
        public int returnID()
        {
            if (textBox1.Text == "") return -1;
            return int.Parse(textBox1.Text);
        }
    }
}
