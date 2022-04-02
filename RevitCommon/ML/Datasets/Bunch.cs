using RevitCommon.Numerical.Matrix;
using System;

namespace RevitCommon.ML.Datasets
{
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
