using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace project04
{
    internal class Program
    {
        static void Get_line(int num,ref double[] arr)
        {
            Console.WriteLine($"Now, enter next line ({num} numbers) of the double Array plz:\n" +
                $"(separate numbers with spaces)");
            string s = Console.ReadLine();
            arr = new double[num];
            int i = 0;
            int l = 0;
            int r = 0;
            while(r< s.Length)
            {
                while (r < s.Length && (s[r]=='.' || s[r] == '-' || (s[r] >= '0' && s[r] <= '9')))
                {
                    r++;
                }
                if( r == s.Length )
                {
                    r--;
                }
                string subs = s.Substring(l, r-l+1);
                //Console.WriteLine(subs);
                double d = Double.Parse(subs);
                arr[i++] = d;
                //do (l, r);
                r++;
                l = r;
            }
            foreach (double d in arr)
            {
                //Console.WriteLine(Convert.ToString(d));
            }
        }
        static double[][] Pre(ref int m,ref int n)
        {
            string s;
            Console.Write("Enter m plz: ");
            s = Console.ReadLine();
            m = Int32.Parse(s);
            Console.Write("Enter n plz: ");
            s = Console.ReadLine();
            n = Int32.Parse(s);
            double[][] arr = new double[m][];
            for (int i = 0; i < m; i++)
            {
                Get_line(n, ref arr[i]);
            }
            return arr;
        }
        static bool Work04(double[][]arr,int m,int n)
        {
            double[] toep = new double[m + n - 1];
            bool[] vis = new bool[m + n - 1];
            for (int i = 0; i < m + n - 1; i++)
                vis[i] = false;
            //i-j=[1-n,m-1]
            //i-j+n-1=[0,n+m-2]
            bool ans = true;
            for (int i = 0; i < m; i++)
            {
                if (ans == false) break;
                for (int j = 0; j < n; j++)
                {
                    if (ans == false) break;
                    if (vis[i - j + n - 1] == false)
                    {
                        vis[i - j + n - 1] = true;
                        toep[i - j + n - 1] = arr[i][j];
                        continue;
                    }
                    if (arr[i][j] == toep[i - j + n - 1]) continue;
                    ans = false; break;
                }
            }
            return ans;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("project04\n"+
                "m*n Toeplitz matrix");
            int m=0, n=0;
            double[][] arr = Pre(ref m, ref n);
            Console.WriteLine(Work04(arr,m,n));
            Console.ReadKey();
        }
    }
}
