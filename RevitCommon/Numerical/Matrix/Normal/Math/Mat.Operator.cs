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
        /// <summary>
        /// 矩阵加标量
        /// </summary>
        public static Mat operator+(Mat left,double scalar)
        {
            Mat ret = new Mat(left.Shape);
            for (int i = 0; i < left.Shape.Row; i++)
            {
                for (int j = 0; j < left.Shape.Col; j++)
                {
                    ret[i, j] = left[i, j] + scalar;
                }
            }
            return ret;
        }
        /// <summary>
        /// 矩阵加向量或矩阵
        /// </summary>
        public static Mat operator+(Mat left,Mat right)
        {
            Mat ret = new Mat(left.Shape);
            if(right.IsScalar())
            {
                return left + right[0, 0];
            }
            else if(right.IsRowVector())
            {
                if (left.Shape.Col != right.Shape.Col)
                    throw new NotSupportedException("矩阵列数不相符");
                for (int i = 0; i < left.Shape.Row; i++)
                {
                    for (int j = 0; j < left.Shape.Col; j++)
                    {
                        ret[i, j] = left[i, j] + right[0, j];
                    }
                }
            }
            else if(right.IsColVector())
            {
                if (left.Shape.Row != right.Shape.Row)
                    throw new NotSupportedException("矩阵行数不相符");
                for (int i = 0; i < left.Shape.Row; i++)
                {
                    for (int j = 0; j < left.Shape.Col; j++)
                    {
                        ret[i, j] = left[i, j] + right[i, 0];
                    }
                }
            }
            else if(right.IsMatrix())
            {
                if (left.Shape.Equals(right.Shape))
                    throw new NotSupportedException();
                for (int i = 0; i < left.Shape.Row; i++)
                {
                    for (int j = 0; j < left.Shape.Col; j++)
                    {
                        ret[i, j] = left[i, j] + right[i, j];
                    }
                }
            }
            return ret;
        }
        public static Mat operator -(Mat left, double scalar)
        {
            Mat ret = new Mat(left.Shape);
            for (int i = 0; i < left.Shape.Row; i++)
            {
                for (int j = 0; j < left.Shape.Col; j++)
                {
                    ret[i, j] = left[i, j] - scalar;
                }
            }
            return ret;
        }
        public static Mat operator -(Mat left, Mat right)
        {
            Mat ret = new Mat(left.Shape);
            if (right.IsScalar())
            {
                return left - right[0, 0];
            }
            else if (right.IsRowVector())
            {
                if (left.Shape.Col != right.Shape.Col)
                    throw new NotSupportedException("矩阵列数不相符");
                for (int i = 0; i < left.Shape.Row; i++)
                {
                    for (int j = 0; j < left.Shape.Col; j++)
                    {
                        ret[i, j] = left[i, j] - right[0, j];
                    }
                }
            }
            else if (right.IsColVector())
            {
                if (left.Shape.Row != right.Shape.Row)
                    throw new NotSupportedException("矩阵行数不相符");
                for (int i = 0; i < left.Shape.Row; i++)
                {
                    for (int j = 0; j < left.Shape.Col; j++)
                    {
                        ret[i, j] = left[i, j] - right[i, 0];
                    }
                }
            }
            else if (right.IsMatrix())
            {
                if (left.Shape.Equals(right.Shape))
                    throw new NotSupportedException();
                for (int i = 0; i < left.Shape.Row; i++)
                {
                    for (int j = 0; j < left.Shape.Col; j++)
                    {
                        ret[i, j] = left[i, j] - right[i, j];
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 矩阵乘以标量
        /// </summary>
        public static Mat operator*(Mat mat,double scalar)
        {
            Mat ret = new Mat(mat.Shape);
            for (int i = 0; i < mat.Shape.Row; i++)
            {
                for (int j = 0; j < mat.Shape.Col; j++)
                {
                    ret[i, j] = mat[i, j] * scalar;
                }
            }
            return ret;
        }
        /// <summary>
        /// 矩阵乘以矩阵
        /// </summary>
        public static Mat operator*(Mat left,Mat right)
        {
            Mat ret = new Mat(left.Shape.Row, right.Shape.Col);
            for (int i = 0; i < left.Shape.Row; i++)
            {
                for (int j = 0; j < right.Shape.Col; j++)
                {
                    double item = 0.0;
                    for (int k = 0; k < left.Shape.Col; k++)
                    {
                        item += left[i, k] * right[j, k];
                    }
                    ret[i, j] = item;
                }
            }
            return ret;
        }
        public static Mat Pow(Mat source)
        {
            Mat ret = new Mat(source.Shape);

            for (int i = 0; i < source.Shape.Row; i++)
            {
                for (int j = 0; j < source.Shape.Col; j++)
                {
                    ret[i, j] = Math.Pow(source[i, j], 2);
                }
            }

            return ret;
        }
        /// <summary>
        /// 横向求和或纵向求和
        /// </summary>
        /// <param name="source">矩阵</param>
        /// <param name="axis">若axis == Axis.H,结果返回一个列向量</param>
        /// <returns></returns>
        public static Mat Sum(Mat source, Axis axis = Axis.H)
        {
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
                    ret[i, 0] = sum;
                }
                return ret;
            }
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
                    ret[0, i] = sum;
                }
                return ret;
            }
        }
        /// <summary>
        /// 若干个矩阵水平方向叠加
        /// </summary>
        public Mat HStack(params Mat[] mats)
        {
            int[] rowls= mats.Select(n => n.Shape.Row).ToArray();
            int[] colls = mats.Select(n => n.Shape.Col).ToArray();
            if (!rowls.All(n => n == rowls[0]))
                throw new NotSupportedException("矩阵行数不一致");
            int sumcol = colls.Sum();

            Mat ret = new Mat(rowls[0],sumcol);
            int col = 0;
            int catcol = 0;
            for (int i = 0; i < colls.Length; i++)
            {
                col = colls[i];
                for (int j = 0; j < rowls[0]; j++)
                {
                    for (int k = 0; k < col; k++)
                    {
                        ret[j, catcol + k] = mats[i][j, k];
                    }
                }
                catcol += col;
            }
            return ret;
        }
        public double[] ToArray()
        {
            double[] array = new double[_shape.Size];
            for (int i = 0; i < _shape.Size; i++)
            {
                array[i] = MemoryStorage[i];
            }
            return array;
        }
        /// <summary>
        /// 拷贝副本
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public Mat Copy()
        {
            Mat clone = new Mat(this.Shape);
            UnmgdMemoryBlock<double>.Copy(this.MemoryStorage, clone.MemoryStorage);
            return clone;
        }
    }
}
