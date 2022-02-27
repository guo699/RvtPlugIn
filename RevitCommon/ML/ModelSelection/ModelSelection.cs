using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML.ModelSelection
{
    public abstract class ModelSelection
    {
        public static (Mat X_train,Mat X_test,Mat Y_train,Mat Y_test) TrainTestSplit(Mat X,Mat Y,double train_ratio = 0.8,int random_seed = 666)
        {
            int[] indices = Mat.DisorderArray(0, X.Shape.Row-1,random_seed);

            int train_rowns = (int)(X.Shape.Row * train_ratio);
            int test_rowns = X.Shape.Row - train_rowns;

            Mat _X_train = new Mat(train_rowns, X.Shape.Col);
            Mat _X_test = new Mat(test_rowns, X.Shape.Col);
            Mat _Y_train = new Mat(train_rowns, 1);
            Mat _Y_test = new Mat(test_rowns, 1);

            int k, i;
            for (i = 0; i < train_rowns; i++)
            {
                k = indices[i];
                _X_train[i, Axis.H] = X[k,Axis.H];
                _Y_train[i, Axis.H] = Y[k, Axis.H];
            }

            for (i=train_rowns; i < X.Shape.Row; i++)
            {
                k = indices[i];
                _X_test[i-train_rowns, Axis.H] = X[k, Axis.H];
                _Y_test[i-train_rowns, Axis.H] = Y[k, Axis.H];
            }

            return (_X_train, _X_test, _Y_train, _Y_test);
        }
    }
}
