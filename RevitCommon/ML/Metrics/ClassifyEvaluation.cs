using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML.Metrics
{
    public static class ClassifyEvaluation
    {
        public static double Accuracy(Mat Y_real,Mat Y_pred)
        {
            if (Y_real.Kind != MatTypeCode.ColVector || Y_pred.Kind != MatTypeCode.ColVector || Y_real.Shape.Row != Y_pred.Shape.Row)
                throw new NotSupportedException("两个输入矩阵必须为相同大小的列向量");

            int sames = 0;
            for (int i = 0; i < Y_real.Shape.Row; i++)
            {
                if (Math.Abs(Y_real[i, 0] - Y_pred[i, 0]) < 0.1)
                    sames += 1;
            }
            return (double)sames / Y_real.Shape.Row;
        }
    }
}
