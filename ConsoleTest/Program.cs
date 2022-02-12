using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RevitCommon.Numerical.Matrix.Normal;
using RevitCommon.Utilitis;


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            double m1 = SysHelper.GetMemory();
            Mat mat = new Mat(10000, 10000, 233);

            double m2 = SysHelper.GetMemory();
            mat.Dispose();
            double m3 = SysHelper.GetMemory();

            Console.WriteLine($"{m1}MB\n{m2}MB\n{m3}MB");
        }
    }
}
