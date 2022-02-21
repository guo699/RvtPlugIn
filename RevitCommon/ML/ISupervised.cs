using RevitCommon.Numerical.Matrix.Normal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML
{
    public interface ISupervised
    {
        void Fit(Mat X, Mat Y);
        Mat Predict(Mat X);
        double Score(Mat X, Mat Y);
    }
}
