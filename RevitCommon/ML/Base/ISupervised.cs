using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML.Base
{
    public interface ISupervised
    {
        void Fit(Mat X, Mat Y);
        Mat Predict(Mat X);
        double Score(Mat X, Mat Y);
    }
}
