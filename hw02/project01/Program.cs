using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01
{
    internal class Program
    {
        static int Pre()
        {
            Console.WriteLine("Now, enter the Number plz:");
            string s = "";
            int n = 0;
            s = Console.ReadLine();
            n = Int32.Parse(s);
            return n;
        }
        static ArrayList Work01(int n)
        {
            ArrayList arr = new ArrayList();
            int i = 2;
            while (n > 1)
            {
                if (n % i == 0)
                {
                    //Console.Write(Convert.ToString(i) + " ");
                    arr.Add(i);
                    n /= i;
                    i = 2;
                }
                else
                {
                    i++;
                }
            }
            return arr;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("project01\n" +
                "Write a program to output all prime factors of data specified by the user.");
            int n = Pre();
            ArrayList arr = Work01(n);
            foreach(var item in arr)
                Console.WriteLine(Convert.ToString(item));
            Console.ReadKey();
        }
    }
}
