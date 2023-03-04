using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace project01
{
    internal class Program
    {
        static void Pre(ref double a,ref double b,ref string s)
        {
            Console.WriteLine("Now, input the first number plz:");
            string tmp = "";
            a = 0.0;
            b = 0.0;
            tmp = Console.ReadLine();
            a = Double.Parse(tmp);
            Console.WriteLine("Now, input the operator plz:");
            s = Console.ReadLine();
            Console.WriteLine("Now, input the second number plz:");
            tmp = Console.ReadLine();
            b = Double.Parse(tmp);
        }
        static double Work01(ref double a, ref double b, ref string s)
        {
            double ans = 0.0;
            switch (s)
            {
                case "+":
                    ans = a + b; break;
                case "-":
                    ans = a - b; break;
                case "*":
                    ans = a * b; break;
                case "/":
                    ans = a / b; break;
            }
            return ans;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("project01: " +
                "accepts two numbers and an operator as input, " +
                "and prints out the calculation result");
            double a = 0.0;
            double b = 0.0;
            string s = "";
            Pre(ref a, ref b, ref s);
            double ans = Work01(ref a, ref b, ref s);
            Console.WriteLine($"{a} {s} {b} = {ans}");
            Console.ReadKey();
        }
    }
}
