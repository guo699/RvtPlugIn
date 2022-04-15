using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitCommon.Extensions;

namespace RevitCommon.Numerical.Matrix
{
    public partial class Mat
    {
        private static Random _random = new Random(666);
        public static void Seed(int seed)
        {
            _random = new Random(seed);
        }
        public static Mat Normal(double u,double std,Shape shape)
        {
            return default(Mat);
        }
        public static Mat Unique(double min,double max,Shape shape)
        {
            Mat ret = new Mat(shape);
            for (int i = 0; i < shape.Size; i++)
            {
                ret.MemoryStorage[i] = _random.NextDouble();
            }
            return ret;
        }
        public static Mat RandInt(int min,int max,Shape shape)
        {
            Mat ret = new Mat(shape);
            for (int i = 0; i < shape.Size; i++)
            {
                ret.MemoryStorage[i] = _random.Next(min, max);
            }
            return ret;
        }

        public static Mat Choice(Mat values,int size,params double[] probs)
        {
            Mat ret = new Mat(0, size);

            List<(double prob_b, double prob_u)> prob_ranges = new List<(double prob_b, double prob_u)>();
            double prob_b = 0, prob_u = 0;
            for (int i = 0; i < probs.Length; i++)
            {
                (double, double) prob_range;
                prob_range.Item1 = prob_b;
                prob_u = prob_b + probs[i] / probs.Sum();
                prob_range.Item2 = prob_u;
                prob_ranges.Add(prob_range);
                prob_b = prob_u;
            }

            return ret;
        }

        public static int[] DisorderArray(int start, int stop,int seed = 666)
        {
            Random random = new Random(seed);
            int Count = stop - start + 1;
            int[] numbers = Enumerable.Range(start, Count).ToArray();
            for (int i = Count; i > 0; i--)
            {
                int k = random.Next(0, Count-1);
                int temp = numbers[i - 1];
                numbers[i - 1] = numbers[k];
                numbers[k] = temp;
            }
            return numbers;
        }
    }
}
