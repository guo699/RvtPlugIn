using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RevitCommon.Extensions;
using RevitCommon.ML;
using RevitCommon.ML.Datasets;
using RevitCommon.ML.ModelSelection;
using RevitCommon.ML.Neighbors;
using RevitCommon.Numerical.Matrix;
using RevitCommon.Utilitis;


namespace ConsoleTest1
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            List<int> ls = new List<int>();
            for (int i = 0; i < 10000_0000; i++)
            {
                ls.Add(i);
            }

            double t0 = SysUtils.GetRunTime(() =>
            {
                for (int i = 0; i < 10000_0000; i++)
                {
                    ls[i] = ls[i] * 2;
                }
            });
        }    

        static void Test(int start)
        {

        }
    }
}
