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
            for (int m = 0; m < 300; m++)
            {
                Random r = new Random(m);
                int[] arr = Enumerable.Range(0, 50).ToArray();

                for (int i = arr.Length-1; i > 0; i--)
                {
                    int k = r.Next(0, i+1);
                    int temp = arr[k];
                    arr[k] = arr[i];
                    arr[i] = temp;
                }
                Console.WriteLine(String.Join(" ",arr));
            }

        }       
    }
}
