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
    }
}
