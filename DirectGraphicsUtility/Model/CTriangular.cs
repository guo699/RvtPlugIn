using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Model
{
    class CTriangular
    {
        public XYZ PointA { get; set; }
        public XYZ PointB { get; set; }
        public XYZ PointC { get; set; }
        public XYZ Normal { get; set; }
        public static int PointNum { get; set; } = 3;
        public ColorWithTransparency TColor { get; set; }
        public CTriangular()
        {

        }
        public CTriangular(XYZ point1,XYZ point2,XYZ point3,ColorWithTransparency color)
        {
            PointA = point1;
            PointB = point2;
            PointC = point3;
            TColor = color;
            Normal = (point1 - point2).CrossProduct(point3 - point2).Normalize();
        }
        public CTriangular(XYZ point1,XYZ point2,XYZ point3,XYZ normal)
        {
            PointA = point1;
            PointB = point2;
            PointC = point3;
            Normal = normal;
        }
    }
}
