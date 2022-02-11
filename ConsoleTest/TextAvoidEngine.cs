using NumSharp;
using NumSharp.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**************************************************************
 * 利用遗传算法解决文本标签避让问题
 * 适应度评估函数：X,Y轴方向方差平方和
 * 
 * 
 * 
 * 
 * 
 * 
 * *************************************************************/

namespace ConsoleTest
{
    internal readonly struct GAParameters
    {
        public static readonly int PopAmount = 100;
        public static readonly (double, double) XAxisRange = (0, 1.0);
        public static readonly (double, double) YAxisRange = (0, 1.0);
        public static readonly int EvolutionCount = 200;
    }
    internal readonly struct Point3d
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public Point3d(in double x,in double y,in double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double DistanceTo(in Point3d other)
        {
            return Math.Sqrt(Math.Pow(this.X - other.X, 2) + Math.Pow(this.Y - other.Y, 2) + Math.Pow(this.Z - other.Z, 2));
        }
    }
    internal sealed class TextAvoidEngine
    {
        private IList<Point3d> _source;
        private IList<Point3d> _result;
        private NDArray<Point3d> _population;

        public IList<Point3d> Result => _result;
        public TextAvoidEngine(IList<Point3d> source)
        {
            this._source = source;
        }

        public IList<Point3d> Generate()
        {
            InitialPop();
            for (int i = 0; i < GAParameters.EvolutionCount; i++)
            {
                CrossOver();
                Select();
                Variation();
            }

            return _result;
        }

        /// <summary>
        /// 初始化种群
        /// </summary>
        private void InitialPop()
        {
            _population = new NDArray<Point3d>(new Shape(GAParameters.PopAmount, _source.Count));
        }

        /// <summary>
        /// 选择
        /// </summary>
        private void Select()
        {

        }

        /// <summary>
        /// 交叉
        /// </summary>
        private void CrossOver()
        {

        }

        /// <summary>
        /// 变异
        /// </summary>
        private void Variation()
        {

        }

        /// <summary>
        /// 适应度函数
        /// </summary>
        private double FitValues(IList<Point3d> points)
        {
            return 0.0;
        }
    }
}
