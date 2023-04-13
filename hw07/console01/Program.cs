using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCrawler
{
    class SimpleCrawler
    {
        private Hashtable urls = new Hashtable();
        private int count = 0;
        private int cnt_2 = 0;
        //private string targetUrl = "http://www.cnblogs.com/dstang2000/"; // 指定要爬取的网站
        private string[] allowedExtensions = { ".htm", ".html", ".aspx", ".php", ".jsp" }; // 指定要解析的文件扩展名
        private string host = "";

        static void Main(string[] args)
        {
            SimpleCrawler myCrawler = new SimpleCrawler();
            string targetUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) targetUrl = args[0];
            Uri turi = new Uri(targetUrl);
            myCrawler.host = turi.Host;
            myCrawler.urls.Add(targetUrl, false);//加入初始页面
            //new Thread(myCrawler.Crawl).Start();
            myCrawler.Crawl();
            Console.ReadKey();
        }

        private void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                    break;
                }

                if (current == null || count > 10) break;
                Uri uri = new Uri(current);
                urls[current] = true;
                count++;
                //Console.WriteLine(uri.Host);
                //Console.WriteLine(host);
                if (uri.Host != host)
                {
                    Console.WriteLine(uri.Host + "\t---非指定网站，跳过---");
                    continue;
                }
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                Parse(html,current);//解析,并加入新的链接
                current = $"{uri.Scheme}://{uri.Host}";
                //Console.WriteLine("爬行结束");
                Console.WriteLine($"\t因非htm等而跳过了{cnt_2}个网页");
                cnt_2 = 0;
            }
        }


        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }


        private void Parse(string html, string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');

                if (strRef.Length == 0) continue;

                // 判断链接的扩展名是否在 allowedExtensions 中
                string ext = Path.GetExtension(strRef).ToLowerInvariant();
                if (allowedExtensions.Contains(ext))
                {
                    if (!Uri.IsWellFormedUriString(strRef, UriKind.Absolute)) // 如果链接不是绝对地址，则将其转换为完整的URL
                    {
                        Uri baseUri = new Uri(current);
                        Uri absoluteUri = new Uri(baseUri, strRef);
                        strRef = absoluteUri.ToString();
                    }
                    if (urls[strRef] == null) urls[strRef] = false;
                }
                else
                {
                    cnt_2++;
                }
            }
        }


    }
}