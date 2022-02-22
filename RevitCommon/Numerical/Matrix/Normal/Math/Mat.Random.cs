using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    public partial class Mat
    {
        private static Random _random = new Random(666);
        public void RandomSeed(int seed)
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
        public static void Shuffle(Mat source)
        {
        }
    }
}
