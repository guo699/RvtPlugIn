using RevitCommon.Numerical.Matrix.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    public partial class Mat
    {
        public Mat Abs(Mat source) => MathOpr(source, Math.Abs);
        public Mat Sin(Mat source) => MathOpr(source, Math.Sin);
        public Mat Cos(Mat source) => MathOpr(source, Math.Cos);
        public Mat Tan(Mat source) => MathOpr(source, Math.Tan);
        public Mat Log(Mat source) => MathOpr(source, Math.Log);
        public Mat Min(Mat source,Axis axis = Axis.H)
        {
            //计算每行的最小值，返回一个列向量
            if(axis == Axis.H)
            {
                Mat ret = new Mat(source.Shape.Row, 1);
                for (int i = 0; i < source.Shape.Row; i++)
                {
                    double minvalue = double.MaxValue;
                    for (int j = 0; j < source.Shape.Col; j++)
                    {
                        if (minvalue < source[i, j])
                            minvalue = source[i, j];
                    }
                    ret[i, 1] = minvalue;
                }
                return ret;
            }
            //计算每列的最小值，返回一个行向量
            else
            {
                Mat ret = new Mat(1, source.Shape.Col);
                for (int i = 0; i < source.Shape.Col; i++)
                {
                    double minvalue = double.MaxValue;
                    for (int j = 0; j < source.Shape.Row; j++)
                    {
                        if (minvalue < source[j, i])
                            minvalue = source[j, i];
                    }
                    ret[1, i] = minvalue;
                }
                return ret;
            }
        }
        public Mat Max(Mat source, Axis axis = Axis.H)
        {
            //计算每行的最大值，返回一个列向量
            if (axis == Axis.H)
            {
                Mat ret = new Mat(source.Shape.Row, 1);
                for (int i = 0; i < source.Shape.Row; i++)
                {
                    double minvalue = double.MinValue;
                    for (int j = 0; j < source.Shape.Col; j++)
                    {
                        if (minvalue > source[i, j])
                            minvalue = source[i, j];
                    }
                    ret[i, 1] = minvalue;
                }
                return ret;
            }
            //计算每列的最大值，返回一个行向量
            else
            {
                Mat ret = new Mat(1, source.Shape.Col);
                for (int i = 0; i < source.Shape.Col; i++)
                {
                    double minvalue = double.MinValue;
                    for (int j = 0; j < source.Shape.Row; j++)
                    {
                        if (minvalue > source[j, i])
                            minvalue = source[j, i];
                    }
                    ret[1, i] = minvalue;
                }
                return ret;
            }
        }
        public Mat Mean(Mat source, Axis axis = Axis.H)
        {
            //计算每行的平均值，返回一个列向量
            if (axis == Axis.H)
            {
                Mat ret = new Mat(source.Shape.Row, 1);
                for (int i = 0; i < source.Shape.Row; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < source.Shape.Col; j++)
                    {
                        sum += source[i, j];
                    }
                    ret[i, 1] = sum / source.Shape.Col;
                }
                return ret;
            }
            //计算每列的平均值，返回一个行向量
            else
            {
                Mat ret = new Mat(1, source.Shape.Col);
                for (int i = 0; i < source.Shape.Col; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < source.Shape.Row; j++)
                    {
                        sum += source[j, i];
                    }
                    ret[1, i] = sum / source.Shape.Row;
                }
                return ret;
            }
        }
        public Mat Std(Mat source,Axis axis = Axis.H)
        {
            //每行求方差，返回一个列向量
            if(axis == Axis.H)
            {
                Mat ret = new Mat(source.Shape.Row, 1);
                for (int i = 0; i < source.Shape.Row; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < source.Shape.Col; j++)
                    {
                        sum += source[i, j];
                    }
                    double mean = sum / source.Shape.Col;
                    double stdsum = 0.0;
                    for (int j = 0; j < source.Shape.Col; j++)
                    {
                        stdsum += Math.Pow(source[i, j] - mean, 2);
                    }
                    ret[i, 0] = stdsum;
                }
                return ret;
            }
            //每列求方差，返回一个行向量
            else
            {
                return default(Mat);
            }
        }
        public Mat Var(Mat source,Axis axis = Axis.H)
        {
            return default(Mat);
        }
        /// <summary>
        /// 协方差
        /// </summary>
        public Mat Cov(Mat source)
        {
            return default(Mat);
        }
        /// <summary>
        /// 计算行列式
        /// </summary>
        public double? Det(Mat source)
        {
            return null;
        }
        /// <summary>
        /// 计算逆矩阵
        /// </summary>
        public Mat Inv(Mat source)
        {
            return default(Mat);
        }
        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public (double,Mat)[] Eig(Mat source)
        {
            return null;
        }
        private Mat MathOpr(Mat source,Func<double,double> func)
        {
            Mat ret = new Mat(source.Shape);
            for (int i = 0; i < source.Shape.Row; i++)
            {
                for (int j = 0; j < source.Shape.Col; j++)
                {
                    ret[i, j] = func(source[i,j]);
                }
            }
            return ret;
        }
    }
}
