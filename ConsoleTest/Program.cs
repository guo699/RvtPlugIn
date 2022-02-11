using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using NumSharp;


namespace ConsoleTest
{
    class Program
    {
        static void Test()
        {

        }
        static void Main(string[] args)
        {

        }

        public static string GetMemory()
        {
            Process proc = Process.GetCurrentProcess();
            long b = proc.PrivateMemorySize64;
            for (int i = 0; i < 2; i++)
            {
                b /= 1024;
            }
            return b + "MB";
        }
    }
}
