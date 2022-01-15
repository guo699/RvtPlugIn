using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Numbers
{
    public static partial class nc
    {
        public static double[] linspace(double min,double max,int count)
        {
            double[] result = new double[count];
            double delta = (max - min) / (count - 1);
            for (int i = 0; i < count; i++)
            {
                result[i] = min + delta * i;
            }
            return result;
        }
    }
}
