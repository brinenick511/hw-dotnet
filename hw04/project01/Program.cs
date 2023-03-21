using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01
{
    class Program
    {
        //为示例中的泛型链表类添加类似于List<T>类的ForEach(Action<T> action)方法。
        //通过调用这个方法打印链表元素，求最大值、最小值和求和（使用lambda表达式实现）
        public class Node<T>
        {
            public Node<T> Next { get; set; }
            public T Data { get; set; }
            public Node(T t)
            {
                Next = null;
                Data = t;
            }
        }
        public class GenericList<T>
        {
            private Node<T> head;
            private Node<T> tail;
            public GenericList()
            {
                tail = head = null;
            }
            public Node<T> Head { get => head; }
            public void Add (T t)
            {
                Node<T> n = new Node<T>(t);
                if (tail == null) head = tail = n;
                else
                {
                    tail.Next = n;
                    tail = n;
                }
            }
            public void Foreach(Action<T> action)
            {
                for (Node<T> node = Head; node != null; node = node.Next)
                    action(node.Data);
            }
            public void Print()
            {
                Foreach(m => Console.WriteLine(m));
            }
            public T Max(Func<T,T,bool> bge)
            {
                T ans = head.Data;
                Foreach(m => ans = bge(m,ans) ? m : ans);
                return ans;
            }
            public T Min(Func<T, T, bool> bge)
            {
                T ans = head.Data;
                Foreach(m => ans = bge(m, ans) ? ans : m);
                return ans;
            }
            public T Sum(Func<T,T,T> add, Func<T, T, T> minus)
            {
                T ans = head.Data;
                Foreach(m => ans = add(m, ans));
                return minus(ans, head.Data);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("hw4-1: Foreach/Max/Min/Sum of Link List");
            Random rd = new Random();
            GenericList<int> intlist = new GenericList<int>();
            for (int x = 0; x < 10; x++)
                intlist.Add(rd.Next(10));
            //for (Node<int> node = intlist.Head; node != null; node = node.Next) Console.WriteLine(node.Data);

            intlist.Print();
            Func<int, int, bool> bge = (a, b) => (a >= b);
            Func<int, int, int> add = (a, b) => a + b;
            Func<int, int, int> minus = (a, b) => a - b;
            int maxnum = intlist.Max(bge);
            int minnum = intlist.Min(bge);
            int sumnum = intlist.Sum(add, minus);
            Console.WriteLine($"max={maxnum}, min={minnum}, sum={sumnum}.");
            Console.ReadKey();
        }
    }
}
