using Autodesk.Revit.DB;
using DirectGraphicsUtility.Numbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Model
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
            _xnums = 50;
            _ynums = 50;
            _xthicks = new double[_xnums];
            _ythicks = new double[_ynums];
            _points = new XYZ[_xnums, _ynums];
            Triangulars = new List<CTriangular>();
            Initial();
        }
        private void Initial()
        {
            _xthicks = nc.linspace(_xdomain.Item1, _xdomain.Item2, _xnums);
            _ythicks = nc.linspace(_ydomain.Item1, _ydomain.Item2, _ynums);
            for (int i = 0; i < _xthicks.Length; i++)
            {
                for (int j = 0; j < _ythicks.Length; j++)
                {
                    _points[i, j] = new XYZ(_xthicks[i], _ythicks[j], Function(_xthicks[i], _ythicks[j]));
                }
            }

            CRectangle rect;
            ColorWithTransparency color;
            for (int i = 0; i < _xnums-1; i++)
            {
                for (int j = 0; j < _ynums-1; j++)
                {
                    color = new ColorWithTransparency(255, 255, 0, 0);
                    rect = new CRectangle(_points[i, j], _points[i, j + 1], _points[i + 1, j+1], _points[i + 1, j],color);
                    Triangulars.AddRange(rect.Triangulars);
                }
            }
        }
        private double Function(double x,double y)
        {
            return (Math.Pow(x, 3) + Math.Pow(y, 2)) / 50.0;
            //return 0;
            //return Math.Pow(Math.Sin(x), 2) + Math.Pow(Math.Cos(y), 2);
            //return Math.Sin(x) + Math.Cos(y);
        }
    }
}
