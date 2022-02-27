using System;

namespace RevitCommon.Numerical.Matrix
{
    public partial class Mat
    {
        public static Mat Ones(int rows,int cols)
        {
            Mat ret = new Mat(rows, cols, 1);
            return ret;
        }
        public static Mat Eye(int dim)
        {
            Mat ret = new Mat(dim, dim);
            for (int i = 0; i < dim; i++)
            {
                ret[i, i] = 1.0;
            }
            return ret;
        }
        public static Mat Array(double[,] array)
        {
            int row = array.GetLength(0);
            int col = array.GetLength(1);
            Mat ret = new Mat(row, col);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    ret[i, j] = array[i, j];
                }
            }
            return ret;
        }
        /// <summary>
        /// 生成一个从start开始，end结尾,步长为step的行向量
        /// </summary>
        /// <param name="start">起始值</param>
        /// <param name="end">终点值（不包括此值）</param>
        /// <param name="step">步长</param>
        /// <returns>行向量</returns>
        public static Mat Arange(double start,double end,double step)
        {
            if (start < end && step < (end - start))
            {
                int count = (int)Math.Ceiling((end - start) / step);
                Mat ret = new Mat(1, count);

                double value = start;
                for (int i = 0; i < count; i++)
                {
                    ret[1, i] = value;
                    value += step;
                }
                return ret;
            }
            else
                throw new NotSupportedException();
        }
        public static Mat LinSpace(double start,double end,int count)
        {
            Mat mat = new Mat(1, count);

            double delta = (end - start) / (count - 1);
            double value = start;
            for (int i = 0; i < count; i++)
            {
                mat[0,i] = value;
                value += delta;
            }
            return mat;
        }

        public static Mat Empty(int rows,int cols)
        {
            Mat ret = new Mat(rows, cols, double.MinValue);
            return ret;
        }
    }
}
