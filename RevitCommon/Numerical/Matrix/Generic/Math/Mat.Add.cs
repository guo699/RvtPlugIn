using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    public partial class Mat<T>
    {
        public static Mat<T> operator +(Mat<T> source,T scalar)
        {
            Mat<T> result = new Mat<T>(source.Shape);
            for (int i = 0; i < source.Shape.Row; i++)
            {
                for (int j = 0; j < source.Shape.Col; j++)
                {
                    result[i, j] = Operator.Add<T>(source[i, j], scalar);
                }
            }
            return result;
        }

        public static Mat<T> operator+(Mat<T> left,Mat<T> right)
        {
            Contracts.AssertAddShape(left, right);
            Mat<T> result = new Mat<T>(left.Shape);
            for (int i = 0; i < left.Shape.Row; i++)
            {
                for (int j = 0; j < left.Shape.Col; j++)
                {
                    result[i, j] = Operator.Add<T>(left[i, j], right[i,j]);
                }
            }
            return result;
        }
    }
}
