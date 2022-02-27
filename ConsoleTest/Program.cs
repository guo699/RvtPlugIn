using System;
using System.Collections.Generic;
using RevitCommon.ML;
using RevitCommon.ML.Datasets;
using RevitCommon.ML.ModelSelection;
using RevitCommon.ML.Neighbors;
using RevitCommon.Numerical.Matrix;


namespace ConsoleTest
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Mat m1 = Mat.Ones(5, 3);
            Mat m2 = Mat.Ones(5, 6) * 3;

            Mat m3 = Mat.HStack(m1, m2);
            Console.WriteLine(m3.ToString());
        } 
        
    }
}
