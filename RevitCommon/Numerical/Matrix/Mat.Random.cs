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
