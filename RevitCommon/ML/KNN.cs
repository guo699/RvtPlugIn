using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitCommon.Numerical.Matrix;

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
            return null;
        }

        public double Score(Mat X, Mat Y)
        {
            throw new NotImplementedException();
        }
    }
}
