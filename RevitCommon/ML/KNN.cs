using RevitCommon.Numerical.Matrix.Normal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitCommon.Numerical.Matrix.Basic;

namespace RevitCommon.ML
{
    public class KNN : ISupervised
    {
        private int n_neighbors;
        private Mat X_train;
        private Mat Y_train;
        public KNN(int neighbors = 5)
        {
            this.n_neighbors = neighbors;
        }
        public void Fit(Mat X, Mat Y)
        {
            X_train = X;
            Y_train = Y;
        }

        public Mat Predict(Mat X)
        {
            Mat y_pred = new Mat(1,X.Shape.Row);
            for (int i = 0; i < X.Shape.Row; i++)
            {
                Mat ret = Mat.Sum(Mat.Pow(X_train - X[i, Axis.H]));
                Mat topks = ret[1, Enumerable.Range(0, n_neighbors).ToArray()];
            }
            return y_pred;
        }

        public double Score(Mat X, Mat Y)
        {
            throw new NotImplementedException();
        }
    }
}
