using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    public partial class Mat
    {
        public double this[int row, int col]
        {
            get { return MemoryStorage[_shape.Col * row + col]; }
            set { MemoryStorage[_shape.Col * row + col] = value; }
        }
        public Mat this[int index, Axis axis = Axis.H]
        {
            get
            {
                if (axis == Axis.H)
                {
                    Mat ret = new Mat(1, Shape.Col);
                    for (int i = 0; i < Shape.Col; i++)
                        ret[0, i] = this[index, i];
                    return ret;
                }
                else
                {
                    Mat ret = new Mat(Shape.Row, 1);
                    for (int i = 0; i < Shape.Row; i++)
                        ret[i, 0] = this[i, index];
                    return ret;
                }
            }
            set
            {
                if(axis == Axis.H)
                {
                    for (int i = 0; i < Shape.Col; i++)
                        this[index, i] = value[0, i];
                }
                else
                {
                    for (int i = 0; i < Shape.Row; i++)
                        this[i, index] = value[i, 0];
                }
            }
        }
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
                        int rows = string.IsNullOrEmpty(rowse[0]) ? 0 : int.Parse(rowse[0]);
                        rows = rows < 0 ? Shape.Row + rows : rows;
                        int rowe = string.IsNullOrEmpty(rowse[1]) ? Shape.Row : int.Parse(rowse[1]);
                        rowe = rowe < 0 ? Shape.Row + rowe : rowe;

                        string[] colse = rcs[1].Split(':');
                        int cols = string.IsNullOrEmpty(colse[0]) ? 0 : int.Parse(colse[0]);
                        cols = cols < 0 ? Shape.Col + cols : cols;
                        int cole = string.IsNullOrEmpty(colse[1]) ? Shape.Col : int.Parse(colse[1]);
                        cole = cole < 0 ? Shape.Col + cole : cole;

                        int rownum = rowe - rows;
                        int colnum = cole - cols;
                        Mat ret = new Mat(rownum, colnum);
                        for (int i = rows; i < rowe; i++)
                        {
                            for (int j = cols; j < cole; j++)
                            {
                                ret[i-rows,j-cols] = this[i, j];
                            }
                        }
                        return ret;
                    }
                    else if (!rcs[0].Contains(':') && rcs[1].Contains(':'))
                    {
                        int row = int.Parse(rcs[0]);
                        string[] colse = rcs[1].Split(':');
                        int cols = string.IsNullOrEmpty(colse[0]) ? 0 : int.Parse(colse[0]);
                        int cole = string.IsNullOrEmpty(colse[1]) ? Shape.Col : int.Parse(colse[1]);

                        int colnum = cole - cols;
                        Mat ret = new Mat(1, colnum);
                        for (int i = cols; i < cole; i++)
                        {
                            ret[0, i - cols] = this[row, i];
                        }
                        return ret;
                    }
                    else if (rcs[0].Contains(':') && !rcs[1].Contains(':'))
                    {
                        string[] rowse = rcs[0].Split(':');
                        int rows = string.IsNullOrEmpty(rowse[0]) ? 0 : int.Parse(rowse[0]);
                        int rowe = string.IsNullOrEmpty(rowse[1]) ? Shape.Row : int.Parse(rowse[1]);

                        int col = int.Parse(rcs[1]);

                        int rownum = rowe - rows;
                        Mat ret = new Mat(rownum, 1);
                        for (int i = rows; i < rowe; i++)
                        {
                            ret[i - rows, 0] = this[i, col];
                        }
                        return ret;
                    }
                    else
                        throw new ArgumentException("切片参数异常");
                }
                catch(Exception ex)
                {
                    throw new ArgumentException(ex.Message + "切片参数异常");
                }
            }
        }
    }
}
