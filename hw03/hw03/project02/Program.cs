using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project02
{
    class Program
    {
        interface If
        {
            void Is();
        }
        abstract class Shape:If
        {
            public abstract void Is();
            public abstract void Func();
        }
        class Rec : Shape
        {
            public override void Is()
            {
                Console.WriteLine("RecIs");
            }
            public new void Func()
            {
                Console.WriteLine("RecFunc");
            }
        }
        class Tri : Rec
        {
            public new void Is()
            {
                base.Is();
                Console.WriteLine("TriIs");
            }
            public new void Func()
            {
                Console.WriteLine("TriFunc");
            }
        }


        static void Main(string[] args)
        {
            Rec r = new Rec();
            Tri t = new Tri();
            r.Func();
            r.Is();
            t.Func();
            t.Is();
            Console.ReadKey();
        }
    }
}
