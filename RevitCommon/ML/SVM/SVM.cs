using RevitCommon.ML.Base;
using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML.SVM
{
    class SVM : ISupervised
    {
        public SVM()
        {

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
    }
}
