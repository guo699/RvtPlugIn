using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitCommon.ML.Base;
using RevitCommon.Numerical.Matrix;

namespace RevitCommon.ML.Neighbors
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
        /// <summary>
        /// X = m * n，Y = m * 1
        /// </summary>
        /// <param name="X">训练数据</param>
        /// <param name="Y">训练标签</param>
        public void Fit(Mat X, Mat Y)
        {
            Contract.EnsuresFitXY(X, Y);
            X_train = X;
            Y_train = Y;
        }

        public Mat Predict(Mat X_pred)
        {
            Mat y_pred = new Mat(X_pred.Shape.Row, 1);

            Mat row;
            for (int i = 0; i < X_pred.Shape.Row; i++)
            {
                row = X_pred[$"{i},:"];
                Mat dists = (X_train - row).Pow(2).Sum(Axis.H).Pow(0.5); // row vector

                //距离排序，获取最小的前k个索引
                int[] indices = Enumerable.Range(0, dists.Shape.Row).ToArray();
                int index;
                double temp;
                for (int m = dists.Shape.Row-1; m > dists.Shape.Row-1-n_neighbors; m--)
                {
                    for (int n = 0; n < m; n++)
                    {
                        if(dists[0,n] > dists[0,n+1])
                        {
                            temp = dists[0, n];
                            dists[0, n] = dists[0, n + 1];
                            dists[0, n + 1] = temp;

                            index = indices[n];
                            indices[n] = indices[n + 1];
                            indices[n + 1] = index;
                        }
                    }
                }
                SortedList<double, int> ls = new SortedList<double, int>();
                double y; 
                for (int k = 0; k < n_neighbors; k++)
                {
                    y = Y_train[indices[k], 0];
                    if (!ls.ContainsKey(y))
                        ls.Add(y, 1);
                    else
                        ls[y] += 1;
                }
                double maxy = ls.First().Key;
                int maxcount = ls.First().Value;
                foreach (var item in ls)
                {
                    if(item.Value > maxcount)
                    {
                        maxy = item.Key;
                        maxcount = item.Value;
                    }
                }
                y_pred[i, 0] = maxy;
            }
            return y_pred;
        }

        public double Score(Mat X_pred, Mat Y_real)
        {
            Mat y_pred = Predict(X_pred);

            int rightn = 0;
            for (int i = 0; i < y_pred.Shape.Row; i++)
            {
                if ((int)y_pred[i, 0] == (int)Y_real[i, 0])
                    rightn += 1;
            }
            return rightn / (double)y_pred.Shape.Row;
        }
    }
}
