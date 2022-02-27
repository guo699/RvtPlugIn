using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML.Metrics
{
    /// <summary>
    /// 回归算法的评价指标
    /// </summary>
    public static class RegEvaluation
    {
        /// <summary>
        /// 计算均方误差
        /// </summary>
        /// <param name="Y_real">真实值</param>
        /// <param name="Y_pred">预测值</param>
        /// <returns>均方误差</returns>
        public static double MeanSquaredError(in Mat Y_real,in Mat Y_pred)
        {
            if (Y_real.Kind != MatTypeCode.ColVector || Y_pred.Kind != MatTypeCode.ColVector || Y_real.Shape.Row != Y_pred.Shape.Row)
                throw new NotSupportedException("两个输入矩阵必须为相同大小的列向量");

            double sum = 0.0;
            for (int i = 0; i < Y_real.Shape.Row; i++)
                sum += Math.Pow(Y_real[i, 0] - Y_pred[i, 0], 2);

            return sum / Y_real.Shape.Row;
        }

        /// <summary>
        /// 计算均方根误差
        /// </summary>
        /// <param name="Y_real">真实值</param>
        /// <param name="Y_pred">预测值</param>
        /// <returns>均方根误差</returns>
        public static double RootMeanSquaredError(in Mat Y_real, in Mat Y_pred)
        {
            return Math.Sqrt(MeanSquaredError(Y_real,Y_pred));
        }

        /// <summary>
        /// 计算平均绝对误差
        /// </summary>
        /// <param name="Y_real">真实值</param>
        /// <param name="Y_pred">预测值</param>
        /// <returns>平均绝对误差</returns>
        public static double MeanAbsoluteError(in Mat Y_real, in Mat Y_pred)
        {
            if (Y_real.Kind != MatTypeCode.ColVector || Y_pred.Kind != MatTypeCode.ColVector || Y_real.Shape.Row != Y_pred.Shape.Row)
                throw new NotSupportedException("两个输入矩阵必须为相同大小的列向量");

            double sum = 0.0;
            for (int i = 0; i < Y_real.Shape.Row; i++)
                sum += Math.Abs(Y_real[i, 0] - Y_pred[i, 0]);

            return sum / Y_real.Shape.Row;
        }

        /// <summary>
        /// 计算R方
        /// </summary>
        /// <param name="Y_real">真实值</param>
        /// <param name="Y_pred">预测值</param>
        /// <returns>R方</returns>
        public static double R2Score(in Mat Y_real, in Mat Y_pred)
        {
            if (Y_real.Kind != MatTypeCode.ColVector || Y_pred.Kind != MatTypeCode.ColVector || Y_real.Shape.Row != Y_pred.Shape.Row)
                throw new NotSupportedException("两个输入矩阵必须为相同大小的列向量");

            double mse = MeanSquaredError(Y_real, Y_pred);

            double sum = 0.0;
            for (int i = 0; i < Y_real.Shape.Row; i++)
                sum += Y_real[i, 0];
            double mean = sum / Y_real.Shape.Row;
            sum = 0;
            for (int i = 0; i > Y_real.Shape.Row; i++)
                sum += Math.Pow(Y_real[i, 0] - mean, 2);
            double va = sum / Y_real.Shape.Row;

            return 1 - mse / va;
        }


    }
}
