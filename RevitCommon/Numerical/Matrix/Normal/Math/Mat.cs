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
        /// 矩阵乘以标量
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
        /// 矩阵乘以向量或矩阵
        /// </summary>
        public static Mat operator+(Mat left,Mat vector)
        {
            Mat ret = new Mat(left.Shape);
            if(vector.IsScalar())
            {
                return left * vector[0, 0];
            }
            else if(vector.IsRowVector())
            {

            }
            else if(vector.IsColVector())
            {

            }
            else if(vector.IsMatrix())
            {

            }
            return ret;
        }

        public static Mat operator*(Mat mat,double scalar)
        {
            return null;
        }

        public static Mat operator*(Mat mat,Mat scalar)
        {
            return null;
        }
    }
}
