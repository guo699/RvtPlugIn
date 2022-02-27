using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.ML.Datasets
{
    public class DataLoader
    {
        /// <summary>
        /// 加载鸢尾花数据集
        /// 特征数据集 150*4
        /// </summary>
        /// <returns></returns>
        public static Bunch LoadIris()
        {
            KeyValuePair<Mat,Mat> ret = CsvReader.ImportData(FileSourcePaths.Iris);

            string[] targetnames = 
                new string[3] { "setosa", "versicolor", "virginica" };
            string[] featurenames = 
                new string[4] 
                { "sepal length (cm)", "sepal width (cm)", "petal length (cm)", "petal width (cm)" };
            Bunch result = new Bunch(ret.Key, ret.Value, featurenames, targetnames);
            return result;
        }
        /// <summary>
        /// 加载红酒数据集
        /// </summary>
        /// <returns></returns>
        public static Bunch LoadWine()
        {
            KeyValuePair<Mat, Mat> ret = CsvReader.ImportData(FileSourcePaths.Wine);
            string[] targetnames = new string[3] { "class_0", "class_1", "class_2" };
            string[] featurenames = new string[13]
            {   "alcohol",
                "malic_acid",
                "ash",
                "alcalinity_of_ash",
                "magnesium",
                "total_phenols",
                "flavanoids",
                "nonflavanoid_phenols",
                "proanthocyanins",
                "color_intensity",
                "hue",
                "od280/od315_of_diluted_wines",
                "proline"};
            Bunch result = new Bunch(ret.Key, ret.Value, featurenames, targetnames);
            return result;
        }
        /// <summary>
        /// 加载波士顿房价数据集
        /// </summary>
        /// <returns></returns>
        public static Bunch LoadBoston()
        {
            KeyValuePair<Mat, Mat> ret = CsvReader.ImportData(FileSourcePaths.Boston);
            string[] targetnames = new string[1] {"house prices"};
            string[] featurenames = new string[13]
            {   "CRIM",
                "ZN",
                "INDUS",
                "CHAS",
                "NOX",
                "RM",
                "AGE",
                "DIS",
                "RAD",
                "TAX",
                "PTRATIO",
                "B",
                "LSTAT"};
            Bunch result = new Bunch(ret.Key, ret.Value, featurenames, targetnames);
            return result;
        }
        /// <summary>
        /// 加载乳腺癌数据集
        /// </summary>
        /// <returns></returns>
        public static Bunch LoadBreastCancer()
        {
            KeyValuePair<Mat, Mat> ret = CsvReader.ImportData(FileSourcePaths.BreastCancer);
            string[] targetnames = new string[2] { "malignant", "benign" };
            string[] featurenames = new string[30]
            { "mean radius", "mean texture", "mean perimeter", "mean area",
                "mean smoothness", "mean compactness", "mean concavity",
                "mean concave points", "mean symmetry", "mean fractal dimension",
                "radius error", "texture error", "perimeter error", "area error",
                "smoothness error", "compactness error", "concavity error",
                "concave points error", "symmetry error",
                "fractal dimension error", "worst radius", "worst texture",
                "worst perimeter", "worst area", "worst smoothness",
                "worst compactness", "worst concavity", "worst concave points",
                "worst symmetry", "worst fractal dimension"
            };
            Bunch result = new Bunch(ret.Key, ret.Value, featurenames, targetnames);
            return result;
        }
        /// <summary>
        /// 加载手写数字数据集
        /// </summary>
        /// <returns></returns>
        public static Bunch LoadDigits()
        {
            KeyValuePair<Mat, Mat> ret = CsvReader.ImportData(FileSourcePaths.Digits, 1797, 64);

            string[] targetnames =
                new string[1] {"Number from 0 to 9"};
            string[] featurenames =
                new string[1] { "Gray Color Value" };
            Bunch result = new Bunch(ret.Key, ret.Value, featurenames, targetnames);
            return result;
        }
    }
}
