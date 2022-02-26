using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML
{
    public abstract class Contract
    {
        public static void EnsuresFitXY(Mat X,Mat Y)
        {
            if (X.Shape.Row != Y.Shape.Row || Y.Shape.Col != 1)
                throw new Exception("训练数据尺寸不匹配");
        }
    }
}
