using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project03
{
    internal class Program
    {
        static ArrayList Work03()
        {
            ArrayList arr = new ArrayList();
            bool[] isprime = new bool[101];
            for (int i = 0; i < 101; i++)
                isprime[i] = true;
            isprime[0] = isprime[1] = false;
            for (int i = 0; i < 101; i++)
            {
                if (isprime[i] == false) continue;
                //Console.WriteLine(Convert.ToString(i));
                arr.Add(i);
                int j = i;
                int k = 2;
                while (j * k <= 100)
                {
                    isprime[j * k] = false;
                    k++;
                }
            }
            return arr;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("project03\n" +
                "Find prime numbers within 2~100");
            ArrayList arr = Work03();
            foreach(var item in arr)
                Console.WriteLine(Convert.ToString(item));
            Console.ReadKey();
        }
    }
}
