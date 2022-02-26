using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    public partial class Mat
    {
        public Mat Abs() => MathOpr(this, Math.Abs);
        public Mat Sin() => MathOpr(this, Math.Sin);
        public Mat Cos() => MathOpr(this, Math.Cos);
        public Mat Tan() => MathOpr(this, Math.Tan);
        public Mat Log() => MathOpr(this, Math.Log);
        /// <summary>
        /// 求矩阵每行或每列的最小值
        /// </summary>
        /// <param name="axis">指定为 Axis.H 时，针对每行取最小值，结果返回一个列向量。</param>
        /// <returns></returns>
        public Mat Min(Axis axis = Axis.H)
        {
            //计算每行的最小值，返回一个列向量
            if(axis == Axis.H)
            {
                Mat ret = new Mat(Shape.Row, 1);
                for (int i = 0; i < Shape.Row; i++)
                {
                    double minvalue = double.MaxValue;
                    for (int j = 0; j < Shape.Col; j++)
                    {
                        if (minvalue < this[i, j])
                            minvalue = this[i, j];
                    }
                    ret[i, 1] = minvalue;
                }
                return ret;
            }
            //计算每列的最小值，返回一个行向量
            else
            {
                Mat ret = new Mat(1, this.Shape.Col);
                for (int i = 0; i < this.Shape.Col; i++)
                {
                    double minvalue = double.MaxValue;
                    for (int j = 0; j < this.Shape.Row; j++)
                    {
                        if (minvalue < this[j, i])
                            minvalue = this[j, i];
                    }
                    ret[1, i] = minvalue;
                }
                return ret;
            }
        }
        public Mat Max(Axis axis = Axis.H)
        {
            //计算每行的最大值，返回一个列向量
            if (axis == Axis.H)
            {
                Mat ret = new Mat(this.Shape.Row, 1);
                for (int i = 0; i < this.Shape.Row; i++)
                {
                    double minvalue = double.MinValue;
                    for (int j = 0; j < this.Shape.Col; j++)
                    {
                        if (minvalue > this[i, j])
                            minvalue = this[i, j];
                    }
                    ret[i, 1] = minvalue;
                }
                return ret;
            }
            //计算每列的最大值，返回一个行向量
            else
            {
                Mat ret = new Mat(1, this.Shape.Col);
                for (int i = 0; i < this.Shape.Col; i++)
                {
                    double minvalue = double.MinValue;
                    for (int j = 0; j < this.Shape.Row; j++)
                    {
                        if (minvalue > this[j, i])
                            minvalue = this[j, i];
                    }
                    ret[1, i] = minvalue;
                }
                return ret;
            }
        }
        public Mat Mean(Axis axis = Axis.H)
        {
            //计算每行的平均值，返回一个列向量
            if (axis == Axis.H)
            {
                Mat ret = new Mat(this.Shape.Row, 1);
                for (int i = 0; i < this.Shape.Row; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < this.Shape.Col; j++)
                    {
                        sum += this[i, j];
                    }
                    ret[i, 1] = sum / this.Shape.Col;
                }
                return ret;
            }
            //计算每列的平均值，返回一个行向量
            else
            {
                Mat ret = new Mat(1, this.Shape.Col);
                for (int i = 0; i < this.Shape.Col; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < this.Shape.Row; j++)
                    {
                        sum += this[j, i];
                    }
                    ret[1, i] = sum / this.Shape.Row;
                }
                return ret;
            }
        }
        public Mat Std(Axis axis = Axis.H)
        {
            //每行求方差，返回一个列向量
            if(axis == Axis.H)
            {
                Mat ret = new Mat(this.Shape.Row, 1);
                for (int i = 0; i < this.Shape.Row; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < this.Shape.Col; j++)
                    {
                        sum += this[i, j];
                    }
                    double mean = sum / this.Shape.Col;
                    double stdsum = 0.0;
                    for (int j = 0; j < this.Shape.Col; j++)
                    {
                        stdsum += Math.Pow(this[i, j] - mean, 2);
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
        public Mat Var(Axis axis = Axis.H)
        {
            return default(Mat);
        }
        /// <summary>
        /// 协方差
        /// </summary>
        public Mat Cov()
        {
            return default(Mat);
        }
        /// <summary>
        /// 计算行列式
        /// </summary>
        public double? Det()
        {
            return null;
        }
        /// <summary>
        /// 计算逆矩阵
        /// </summary>
        public Mat Inv()
        {
            return default(Mat);
        }
        /// <summary>
        /// 计算特征值和特征向量
        /// </summary>
        public void Eig(out Mat vectors,out Mat values)
        {
            vectors = null;
            values = null;
        }
        private Mat MathOpr(Mat source,Func<double,double> func)
        {
            Mat ret = new Mat(source.Shape);
            for (int i = 0; i < source.MemoryStorage.ItemCount; i++)
            {
                ret.MemoryStorage[i] = func(source.MemoryStorage[i]);
            }
            return ret;
        }
        /// <summary>
        /// 所有元素的累加和
        /// </summary>
        /// <returns></returns>
        public double CumSum()
        {
            double result = 0.0;
            for (int i = 0; i < this.MemoryStorage.ItemCount; i++)
            {
                result += MemoryStorage[i];
            }
            return result;
        }
        /// <summary>
        /// 所有元素的累计积
        /// </summary>
        /// <returns></returns>
        public double CumProd()
        {
            double result = 1.0;
            for (int i = 0; i < this.MemoryStorage.ItemCount; i++)
            {
                result *= MemoryStorage[i];
            }
            return result;
        }
        public Mat Pow(double y)
        {
            Mat ret = new Mat(Shape);
            for (int i = 0; i < ret.MemoryStorage.ItemCount; i++)
            {
                ret.MemoryStorage[i] = Math.Pow(this.MemoryStorage[i], y);
            }
            return ret;
        }
        /// <summary>
        /// 横向求和或纵向求和
        /// </summary>
        /// <param name="source">矩阵</param>
        /// <param name="axis">若axis == Axis.H,结果返回一个列向量</param>
        /// <returns></returns>
        public Mat Sum(Axis axis = Axis.H)
        {
            if (axis == Axis.H)
            {
                Mat ret = new Mat(this.Shape.Row, 1);
                for (int i = 0; i < this.Shape.Row; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < this.Shape.Col; j++)
                    {
                        sum += this[i, j];
                    }
                    ret[i, 0] = sum;
                }
                return ret;
            }
            else
            {
                Mat ret = new Mat(1, this.Shape.Col);
                for (int i = 0; i < this.Shape.Col; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < this.Shape.Row; j++)
                    {
                        sum += this[j, i];
                    }
                    ret[0, i] = sum;
                }
                return ret;
            }
        }

    }
}
