using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    public partial class Mat
    {
        /// <summary>
        /// 返回第 row 行，cols若干列的数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public Mat this[int row,params int[] cols]
        {
            get
            {
                Mat ret = new Mat(1, cols.Length);
                for (int i = 0; i < cols.Length; i++)
                {
                    ret[0, i] = this[row, cols[i]];
                }
                return ret;
            }
        }

        /// <summary>
        /// 切片，形如 "0:2,4:8" 表示0、1行中的4、5、6、7列的数据
        /// "1,3:6" 表示第一行中的3、4、5列
        /// "2:4,8" 表示2、3行中的第8列
        /// </summary>
        /// <param name="slice"></param>
        /// <returns></returns>
        public Mat this[string slice]
        {
            get
            {
                try
                {
                    string[] rcs = slice.Split(',');
                    if (rcs[0].Contains(':') && rcs[1].Contains(':'))
                    {
                        string[] rowse = rcs[0].Split(':');
                        int rows = int.Parse(rowse[0]);
                        int rowe = int.Parse(rowse[1]);
                        string[] colse = rcs[1].Split(':');
                        int cols = int.Parse(colse[0]);
                        int cole = int.Parse(colse[1]);

                        int rownum = rowe - rows;
                        int colnum = cole - cols;
                        Mat ret = new Mat(rownum, colnum);
                        for (int i = rows; i < rowe; i++)
                        {
                            for (int j = cols; j < cole; j++)
                            {
                                ret[i-rownum,j-colnum] = this[i, j];
                            }
                        }
                        return ret;
                    }
                    else if (!rcs[0].Contains(':') && rcs[1].Contains(':'))
                    {
                        return null;
                    }
                    else if (rcs[0].Contains(':') && !rcs[1].Contains(':'))
                    {
                        return null;
                    }
                    else
                        throw new ArgumentException("切片参数异常");
                }
                catch(Exception ex)
                {
                    throw new ArgumentException("切片参数异常");
                }
            }
        }
    }
}
