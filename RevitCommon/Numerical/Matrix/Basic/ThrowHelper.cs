using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Basic
{
    internal sealed class ThrowHelper
    {
        public static void ThrowIndexOutRange()
        {
            throw new IndexOutOfRangeException();
        }
    }
}
