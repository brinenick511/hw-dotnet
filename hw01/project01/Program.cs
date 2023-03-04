using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace project01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("project01: " +
                "accepts two numbers and an operator as input, " +
                "and prints out the calculation result\n" +
                "Now, input the first number plz:");
            string s= "";
            int op = -1;
            double a=0.0;
            double b=0.0;
            double ans = 0.0;
            s=Console.ReadLine();
            a=Double.Parse(s);
            while (true)
            {
                Console.WriteLine("Now, input the operator plz:");
                s =Console.ReadLine();
                switch (s)
                {
                    case "+":
                        op=0;break;
                    case "-":
                        op=1;break;
                    case "*":
                        op=2;break;
                    case "/":
                        op=3;break;
                    default:
                        op=-1;break;
                }
                if (op != -1)
                    break;
            }
            Console.WriteLine("Now, input the second number plz:");
            s=Console.ReadLine();
            b=Double.Parse(s);
            switch (op)
            {
                case 0:
                    ans = a + b;break;
                case 1:
                    ans = a - b;break;
                case 2:
                    ans = a * b;break;
                case 3:
                    ans = a / b;break;
            }
            Console.WriteLine($"{a} op{op} {b} = {ans}");
            Console.ReadKey();
        }
    }
}
