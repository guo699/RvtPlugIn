using RevitCommon.ML.Base;
using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML.LinearModel
{
    public class LinearRegression : ISupervised
    {
        private Mat X_train;
        private Mat Y_train;
        private Mat X_b;

        /// <summary>
        /// X = m*n  Y = m*1  X_b = m*(n+1)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public LinearRegression(Mat X,Mat Y)
        {
            this.X_train = X;
            this.Y_train = Y;
            this.X_b = Mat.HStack(Mat.Ones(X_train.Shape.Row, 1), X_train);
        }
        public void Fit(Mat X, Mat Y)
        {
            throw new NotImplementedException();
        }

        public Mat Predict(Mat X)
        {
            throw new NotImplementedException();
        }

        public double Score(Mat X, Mat Y)
        {
            throw new NotImplementedException();
        }

        private double J(Mat thetas)
        {
            double j = (Y_train - X_b * thetas).Pow(2).CumSum() / X_b.Shape.Row;
            return checked(j);
        }

        private Mat DJ(Mat thates)
        {
            Mat res = Mat.Empty(thates.Shape.Row,thates.Shape.Col);

            res[0, 0] = (X_b * thates - Y_train).CumSum();
            for (int i = 1; i < res.Shape.Row; i++)
            {
                res[i, 0] = ((X_b * thates - Y_train) * (X_b[i, Axis.V]))[0, 0];
            }

            return res *2 * (1/thates.Shape.Row);
        }
    }
}
