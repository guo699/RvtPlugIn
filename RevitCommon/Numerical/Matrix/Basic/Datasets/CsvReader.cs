using System;
using System.Collections.Generic;
using System.IO;
using RevitCommon.Numerical.Matrix.Normal;

namespace RevitCommon.Numerical.Matrix.Basic.Datasets
{
    internal class CsvReader
    {
        /// <summary>
        /// 从CSV文件加载数据，默认第一行存储特殊信息，最后一列为特征列
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="separator">每个数据之间的·1分隔符</param>
        /// <returns>两个矩阵，键为特征矩阵，值为标签向量</returns>
        public static KeyValuePair<Mat,Mat> ImportData(string path, char separator = ',')
        {
            try
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    string line;
                    string[] cells;
                    
                    line = reader.ReadLine();
                    cells = line.Split(separator);
                    int.TryParse(cells[0], out int rownum);
                    int.TryParse(cells[1], out int colnum);

                    Mat data = new Mat(rownum, colnum);
                    Mat label = new Mat(1, rownum);
                    int rowindex = 0;
                    while(!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        cells = line.Split(separator);
                        for (int i = 0; i < colnum; i++)
                            data[rowindex, i] = double.Parse(cells[i]);
                        label[0, rowindex] = double.Parse(cells[colnum]);
                        rowindex += 1;
                    }
                    return new KeyValuePair<Mat, Mat>(data, label);
                }
            }
            catch(Exception ex)
            {
                throw new FormatException(ex.Message + "文件格式错误");
            }
        }
    }
}
