using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    public partial class Mat<T>
    {
        public static Mat<T> Empty(Shape shape)
        {
            return new Mat<T>(shape);
        }

        public static Mat<T> One(int row)
        {
            return null;
        }
    }
}
