using System;
using System.Collections.Generic;
using RevitCommon.ML;
using RevitCommon.Numerical.Matrix;
using RevitCommon.Numerical.Matrix.Datasets;


namespace ConsoleTest
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Bunch iris = DataLoader.LoadIris();
            Mat X = iris[BunchKey.Data];
            Mat Y = iris[BunchKey.Target];
            Y = Y.T();

            (Mat X_train, Mat X_test, Mat Y_train, Mat Y_test) = ModelSelection.TrainTestSplit(X, Y);

            KNN knn = new KNN();
            knn.Fit(X_train, Y_train);
            double score = knn.Score(X_test, Y_test);
            Console.WriteLine(score);
        } 
        
    }
}
