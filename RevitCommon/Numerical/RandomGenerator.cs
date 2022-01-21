using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical
{
    public class RandomGenerator
    {
        private readonly Random _random;
        public RandomGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public NDArray<double> Normal(double a,double b,int count)
        {
            NDArray<double> array = new NDArray<double>(count);
            return array;
        }
    }
}
