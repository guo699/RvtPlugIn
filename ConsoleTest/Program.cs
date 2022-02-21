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
            Mat mat = new double[1, 10] { {9,4,7,1,3,5,2,6,0,8 } };

            Mat indices = Mat.ArgSort(mat);

            Console.WriteLine(mat.ToString());
            Console.WriteLine(indices.ToString());

            string ss = "";
            for (int i = 0; i < 10; i++)
            {
                ss += mat[0, (int)indices[0,i]];
                ss += ",";
            }
            Console.WriteLine(ss);
        }       
    }
}
