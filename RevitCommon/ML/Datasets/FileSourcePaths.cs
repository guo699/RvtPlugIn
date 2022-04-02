namespace RevitCommon.ML.Datasets
{
    internal readonly struct FileSourcePaths
    {
        private const string _path = "..\\..\\..\\Datasets\\CsvData\\";
        public  const string Iris = _path + "iris.csv";
        public  const string Wine = _path + "wine_data.csv";
        public  const string Boston = _path + "boston_house_prices.csv";
        public  const string BreastCancer = _path + "breast_cancer.csv";
        public  const string Digits = _path + "digits.csv";
    }
}
