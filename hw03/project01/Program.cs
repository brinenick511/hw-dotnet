using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project01
{
    class Program
    {
        interface HasEdge
        {
            int NumEdge { get; set; }
            double this[int index] { get; set; }
        }
        interface IfError
        {
            bool IsError();
        }
        interface Computable
        {
            double ComputeSize();
        }
        interface Printable
        {
            void Print();
        }

        abstract class Shape : HasEdge, IfError, Computable
        {
            public abstract bool IsError();
            public abstract double ComputeSize();
            public abstract void Print();
            public abstract int NumEdge { get; set; }
            public abstract double this[int index] { get; set; }
        }

        class Triangle : Shape
        {
            public double[] edges = new double[10];
            public override int NumEdge { get; set; }
            public Triangle()
            {
                NumEdge = 0;
                edges = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            public Triangle(double[] arr)
            {
                NumEdge = arr.Length;
                for (int i = 0; i < arr.Length; i++)
                {
                    this[i] = arr[i];
                }
            }
            public override double this[int index]
            {
                get
                {
                    return edges[index];
                }
                set
                {
                    edges[index] = value;
                }
            }
            public override bool IsError()
            {

                if (NumEdge != 3) return true;
                for (int i = 0; i < NumEdge; i++)
                    if (this[i] <= 0) return true;
                return (edges[0] + edges[1] + edges[2] <= 2 * edges.Max());
            }
            public override double ComputeSize()
            {
                double p = (edges[0] + edges[1] + edges[2]) / 2;
                return Math.Sqrt(p * (p - this[0]) * (p - this[1]) * (p - this[2]));
            }
            public override void Print()
            {
                Console.Write("Triangle: ___ ");
                for (int i = 0; i < NumEdge; i++)
                    Console.Write(Convert.ToString(this[i]) + " ");
                Console.WriteLine("___");
            }
        }

        class Rectangle : Shape
        {
            public double[] edges = new double[10];
            public override int NumEdge { get; set; }
            public Rectangle()
            {
                NumEdge = 0;
                edges = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            public Rectangle(double[] arr)
            {
                NumEdge = arr.Length;
                for (int i = 0; i < arr.Length; i++)
                {
                    this[i] = arr[i];
                }
            }
            public override double this[int index]
            {
                get
                {
                    return edges[index];
                }
                set
                {
                    edges[index] = value;
                }
            }
            public override bool IsError()
            {
                if (NumEdge != 4) return true;
                for (int i = 0; i <= 3; i++)
                    if (this[i] <= 0.0) return true;
                for (int i = 1; i <= 3; i++)
                {
                    if (this[i] != this[0]) continue;
                    return (this[1] - this[2] + this[3] + Math.Pow(-1, i) * this[i]) != 0;
                }
                //Console.Write("a");
                return true;
            }
            public override double ComputeSize()
            {
                for (int i = 1; i < 3; i++)
                    if (this[i] != this[0]) return (this[i] * this[0]);
                return (this[0] * this[0]);
            }
            public override void Print()
            {
                Console.Write("___ ");
                for (int i = 0; i < NumEdge; i++)
                    Console.Write(Convert.ToString(this[i]) + " ");
                Console.WriteLine("___");
            }
        }
        class Square : Rectangle
        {
            public Square()
            {
                NumEdge = 0;
                edges = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
            public Square(double[] arr)
            {
                NumEdge = arr.Length;
                for (int i = 0; i < arr.Length; i++)
                {
                    this[i] = arr[i];
                }
            }
            public new bool IsError()
            {
                if (base.IsError()) return true;
                for (int i = 1; i < 4; i++)
                    if (this[i] != this[0]) return true;
                return false;
            }
        }
        static Shape ShapeFac(double[] arr)
        {
            int i = 0;
            for (i = 0; i < arr.Length; ++i)
                if (arr[i] <= 0) break;
            if (i == 3)
            {
                Shape ans = new Triangle(arr);
                if(ans.IsError()==false) return ans;
            }
            else if (i == 4)
            {
                Shape ans = new Square(arr);
                if(ans.IsError()==false) return ans;
                ans = new Rectangle(arr);
                if(ans.IsError()==false) return ans;
            }
            return null;
        }
        static void Main(string[] args)
        {
            double cnt = 0;
            Random rd = new Random();
            for (int i = 0; i < 10; ++i){
                int n = rd.Next() % 2 + 3;
                double[] arr = new double[n];
                for(int j=0;j<n; ++j)
                    Console.Write(Convert.ToString(arr[j] = rd.Next() % 2 + 1)+" ");
                Shape ashape = ShapeFac(arr);
                if (ShapeFac(arr) == null)
                {
                    Console.WriteLine("Wrong");
                    continue;
                }
                cnt += ashape.ComputeSize();
                Console.WriteLine("Right");
            }
            Console.WriteLine("The Total Size Is: "+Convert.ToString(cnt));
            Console.ReadKey();
        }
    }
}
