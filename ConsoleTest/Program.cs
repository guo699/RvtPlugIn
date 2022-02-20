using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RevitCommon.Numerical.Matrix;
using RevitCommon.Numerical.Matrix.Basic;
using RevitCommon.Numerical.Matrix.Normal;
using RevitCommon.Utilitis;


namespace ConsoleTest
{
    public static unsafe class Heap<T> where T:unmanaged
    {
        public static T* New()
        {
            T* ELEM = (T*)Marshal.AllocHGlobal(sizeof(T)).ToPointer();
            return ELEM;
        }
    }

    public struct Point
    {
        public double X;
        public double Y;
        public double Z;
        public Point(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    class Program
    {
        static unsafe void Main(string[] args)
        {

        }       
    }
}
