using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    public partial class Mat<T>
    {
        public static Mat<T> operator*(Mat<T> left,Mat<T> right)
        {
            return Dot(left, right);
        }

        private static Mat<T> Dot(Mat<T> left,Mat<T> right)
        {
            Mat<T> result = new Mat<T>(left.Shape.Row, right.Shape.Col);
            for (int i = 0; i < left.Shape.Row; i++)
            {
                for (int j = 0; j < right.Shape.Col; j++)
                {
                    T item = default(T), temp = default(T);
                    for (int k = 0; k < left.Shape.Col; k++)
                    {
                        temp = Operator.Mult<T>(left[i,k],right[j,k]);
                        item = Operator.Add<T>(item, temp);
                    }
                    result[i, j] = item;
                }
            }
            return result;
        }
    }
}
