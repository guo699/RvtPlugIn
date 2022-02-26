using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
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
            if(right.Kind == MatTypeCode.Scalar)
            {
                return left + right[0, 0];
            }
            else if(right.Kind == MatTypeCode.RowVector)
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
            else if(right.Kind == MatTypeCode.ColVector)
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
            else if(right.Kind == MatTypeCode.Matrix)
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
            if (right.Kind == MatTypeCode.Scalar)
            {
                return left - right[0, 0];
            }
            else if (right.Kind == MatTypeCode.RowVector)
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
            else if (right.Kind == MatTypeCode.ColVector)
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
            else if (right.Kind == MatTypeCode.Matrix)
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
    }
}
