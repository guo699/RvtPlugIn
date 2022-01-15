using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Model
{
    class CRectangle
    {
        public XYZ PointA { get; set; }
        public XYZ PointB { get; set; }
        public XYZ PointC { get; set; }
        public XYZ PointD { get; set; }
        public List<CTriangular> Triangulars { get; set; }
        private ColorWithTransparency _color;
        public CRectangle(XYZ point1,XYZ point2,XYZ point3,XYZ point4,ColorWithTransparency color)
        {
            PointA = point1;
            PointB = point2;
            PointC = point3;
            PointD = point4;
            _color = color;
            Triangulars = new List<CTriangular>();
            Initial();
        }

        private void Initial()
        {
            Triangulars.Add(new CTriangular(PointA, PointB, PointC,_color));
            Triangulars.Add(new CTriangular(PointA, PointC, PointD, _color));
        }
    }
}
