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

namespace CrawlerForm
{
    public partial class Form1 : Form
    {
        Crawler crawler = new Crawler();
        public Form1()
        {
            InitializeComponent();
            bs = new BindingSource();
            dataGridView1.DataSource = bs;
            crawler.PageDownloaded += Crawler_PageDownloaded;
            crawler.CrawlerStopped += Crawler_CrawlerStopped;
        }
        private void Crawler_CrawlerStopped(Crawler obj)
        {
            Action action = () =>label1.Text = "爬虫已停止";
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void Crawler_PageDownloaded(Crawler crawler, string url, string info)
        {
            var pageInfo = new
            {
                Index = bs.Count + 1,
                URL = url,
                Status = info
            };
            Action action = () => { bs.Add(pageInfo); };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.Clear();
            crawler.StartURL = textBox1.Text;
            Match match = Regex.Match(crawler.StartURL, Crawler.urlParseRegex);
            if (match.Length == 0) return;
            string host = match.Groups["host"].Value;
            crawler.HostFilter = "^" + host + "$";
            crawler.FileFilter = ".(html?|aspx|jsp|php)$|^[^.]*$";
            Task.Run(crawler.Start);
            label1.Text = "爬虫已启动....";
        }
    }
}
