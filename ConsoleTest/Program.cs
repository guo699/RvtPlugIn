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
            {
                Console.WriteLine(GetMem());
                using (Mat M = new Mat(128 * 20, 1024 * 40))
                {

                }

                //List<double> ls = new List<double>(128 * 20 * 40 * 1024);
                Console.WriteLine(GetMem());
                M.Dispose();
                Console.WriteLine(GetMem());
            }
        } 

        static long GetMem()
        {
            return  Process.GetCurrentProcess().PrivateMemorySize64 / 1024/1024;
        }
        
    }
}
