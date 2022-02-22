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
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Mat mat = Mat.RandInt(0, 100, (1,20));
            Mat indices = Mat.ArgSort(mat, Axis.H);

            Console.WriteLine(mat);
            Console.WriteLine(indices);
            Console.WriteLine(mat);
        } 

        static long GetMem()
        {
            return Process.GetCurrentProcess().PrivateMemorySize64 / 1024/1024;
        }
        
    }
}
