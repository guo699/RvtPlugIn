using Autodesk.Revit.DB;
using RevitCommon.Numerical.Matrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.DirectGraphics.Model
{
    class CSurface
    {
        public List<CTriangular> Triangulars { get; set; }
        private (double, double) _xdomain;
        private (double, double) _ydomain;
        private XYZ[,] _points;
        private double[] _xthicks;
        private double[] _ythicks;
        private int _xnums;
        private int _ynums;
        public CSurface()
        {
            _xdomain = (-10, 10);
            _ydomain = (-10, 10);
            _xnums = 100;
            _ynums = 100;
            _xthicks = new double[_xnums];
            _ythicks = new double[_ynums];
            _points = new XYZ[_xnums, _ynums];
            Triangulars = new List<CTriangular>();
            Initial();
        }
        private void Initial()
        {
            _xthicks = Mat.LinSpace(_xdomain.Item1, _xdomain.Item2, _xnums).ToArray();
            _ythicks = Mat.LinSpace(_ydomain.Item1, _ydomain.Item2, _ynums).ToArray();
            for (int i = 0; i < _xthicks.Length; i++)
            {
                for (int j = 0; j < _ythicks.Length; j++)
                {
                    _points[i, j] = new XYZ(_xthicks[i], _ythicks[j], Function(_xthicks[i], _ythicks[j]));
                }
            }

            CRectangle rect;
            ColorWithTransparency color_w = new ColorWithTransparency(0, 0, 0, 1);
            ColorWithTransparency color_b = new ColorWithTransparency(255, 255, 255, 1);
            for (int i = 0; i < _xnums-1; i++)
            {
                for (int j = 0; j < _ynums-1; j++)
                {
                    rect = new CRectangle(_points[i, j], _points[i, j + 1], _points[i + 1, j + 1], _points[i + 1, j], (i + j) % 2 == 0 ? color_w : color_b);
                    Triangulars.AddRange(rect.Triangulars);
                }
            }
        }
        private double Function(double x,double y)
        {
            return Math.Pow(Math.Sin(x), 2) + Math.Pow(Math.Cos(y), 2);
        }
    }
}
