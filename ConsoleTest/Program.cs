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
    struct Point
    {
        public double X, Y, Z;
        public Point(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Point operator+(Point left,double scalar)
        {
            return new Point(left.X+scalar, left.Y + scalar, left.Z + scalar);
        }
    }

    class Matrix
    {
        public int m0,m1,m2,m3,m4,m5,m6;
    }
    class Program
    {
        private const long COUNT = 10000_0000;
        private const string dllpath = @"D:\Code\Cpp\DllLib\cmake-build-debug\libDllLib.dll";
        [DllImport(dllpath,EntryPoint ="GetPoint")]
        public unsafe static extern Point* GetPoint();
        static unsafe void Main(string[] args)
        {
            Mat mat = new Mat(1000, 1000);
            double t = SysHelper.GetRunTime(() => {
                var r = mat* mat;
            });
            Console.WriteLine(t);
        }

        
    }
}
