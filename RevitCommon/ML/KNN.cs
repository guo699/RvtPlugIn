using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NumSharp;
using NumSharp.Generic;

namespace RevitCommon.ML
{
    public sealed class KNN
    {
        private NDArray<double> _trainX;
        private NDArray<double> _trainY;
        private int _neighbors;
        public KNN(int n_neighbors)
        {
            _neighbors = n_neighbors;
        }

        public void Fit(NDArray<double> trainX,NDArray<double> trainY)
        {
            _trainX = trainX;
            _trainY = trainY;
        }

        public NDArray<double> Predict(NDArray<double> predData)
        {
            var row = predData["0,:"];

            return null;
        }
    }
}
