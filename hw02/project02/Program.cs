using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace project02
{
    internal class Program
    {
        static ArrayList Pre()
        {
            Console.WriteLine("Now, enter the Int Array plz:(separate numbers with spaces)");
            int n = 0;
            string s = Console.ReadLine();
            bool minus = false;
            ArrayList arr = new ArrayList();
            foreach (char ch in s)
            {
                if (ch >= '0' && ch <= '9')
                {
                    n = n * 10 + ch - '0';
                }
                else if (ch == '-')
                {
                    minus = true;
                }
                else
                {
                    if (minus) arr.Add(0 - n);
                    else arr.Add(n);
                    n = 0;
                    minus = false;
                }
            }
            if (minus) arr.Add(0 - n);
            else arr.Add(n);
            return arr;
        }
        static void Work02(ArrayList arr,ref int min,ref int max,
            ref int sum,ref int cnt,ref double aver)
        {
            min = Int32.MaxValue;
            max = Int32.MinValue;
            sum = 0;
            cnt = 0;
            aver = 0.0;
            foreach (int x in arr)
            {
                //Console.Write(Convert.ToString(x)+" ");
                cnt++;
                sum += x;
                min = (min > x) ? x : min;
                max = (max < x) ? x : max;
            }
            aver = 1.0 * sum / cnt;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("project02\n" +
                "Find the maximum, minimum, average, and sum of all array elements of an integer array.");
            ArrayList arr= Pre();
            int min = 0;
            int max = 0;
            int sum = 0;
            int cnt = 0;
            double aver = 0.0;
            Work02(arr,ref min,ref max,ref sum,ref cnt,ref aver);
            Console.WriteLine($"min={min},max={max},sum={sum},cnt={cnt},aver={aver}");
            Console.ReadKey();
        }
    }
}
