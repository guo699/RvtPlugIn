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
using RevitCommon.Numerical.Matrix.Basic.Datasets;
using RevitCommon.Numerical.Matrix.Normal;
using RevitCommon.Utilitis;


namespace ConsoleTest
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Bunch iris = DataLoader.LoadDigits();

            Console.WriteLine(iris[BunchKey.Data]);
            Console.WriteLine(iris[BunchKey.Target]);
        } 

        static long GetMem()
        {
            return Process.GetCurrentProcess().PrivateMemorySize64 / 1024/1024;
        }
        
    }
}
