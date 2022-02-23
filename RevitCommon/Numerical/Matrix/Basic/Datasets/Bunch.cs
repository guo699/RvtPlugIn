using RevitCommon.Numerical.Matrix.Normal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Basic.Datasets
{
    public enum BunchKey:ushort
    {
        Data = 0,
        Target = 1,
        FeatureName = 2,
        TargetName = 3,
        Introduction = 4,
    }

    internal readonly struct FileSourcePaths
    {
        private const string _path = "..\\..\\..\\Datasets\\CsvData\\";
        public  const string Iris = _path + "iris.csv";
        public  const string Wine = _path + "wine_data.csv";
        public  const string Boston = _path + "boston_house_prices.csv";
        public  const string BreastCancer = _path + "breast_cancer.csv";
        public  const string Digits = _path + "digits.csv";
    }
    public sealed class Bunch
    {
        private Mat _data;
        private Mat _target;
        private string[] feature_names;
        private string[] target_names;
        private string[] introduction = new string[] { "" };

        public Mat this[BunchKey key]
        {
            get
            {
                if (key == BunchKey.Data)
                    return _data;
                else if (key == BunchKey.Target)
                    return _target;
                else
                    throw new ArgumentException();
            }
        }

        public string[] Information(BunchKey key)
        {
            if (key == BunchKey.FeatureName)
                return feature_names;
            else if (key == BunchKey.TargetName)
                return target_names;
            else if (key == BunchKey.Introduction)
                return introduction;
            else
                throw new ArgumentException();
        }

        public Bunch(Mat data,Mat target,string[] feature_names,string[] target_names)
        {
            this._data = data;
            this._target = target;
            this.feature_names = feature_names;
            this.target_names = target_names;
        }
    }
}
