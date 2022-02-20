using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.DirectGraphics.Model
{
    class CCude
    {
        public List<CTriangular> Triangulars { get; set; }
        private double _diaglength;
        private double _edgelength;
        private XYZ p1, p2, p3, p4, p5, p6, p7, p8, min, max;
        public CCude(XYZ min,XYZ max)
        {
            Triangulars = new List<CTriangular>();
            this.min = min;
            this.max = max;
            if (max.DistanceTo(min) < 1.0 / 304.8)
                throw new Exception("cube too small");
            XYZ diagonal = (max - min).Normalize();
            if (diagonal.IsAlmostEqualTo(XYZ.BasisX)
                || diagonal.IsAlmostEqualTo(XYZ.BasisY)
                || diagonal.IsAlmostEqualTo(XYZ.BasisZ))
                throw new Exception("Invalid Coord");
            if (diagonal[0] < 0 || diagonal[1] < 0 || diagonal[2] < 0)
                throw new Exception("Position is Invalid");

            _diaglength = max.DistanceTo(min);
            _edgelength = Math.Pow(_diaglength, 1 / 3.0);

            Initial();
        }

        private void Initial()
        {
            p1 = max;
            p2 = new XYZ(p1.X, p1.Y - _edgelength, p1.Z);
            p3 = new XYZ(p1.X - _edgelength, p1.Y - _edgelength, p1.Z);
            p4 = new XYZ(p1.X - _edgelength, p1.Y, p1.Z);

            p5 = new XYZ(p1.X, p1.Y, p1.Z - _edgelength);
            p6 = new XYZ(p5.X, p5.Y - _edgelength, p5.Z);
            p7 = new XYZ(p5.X - _edgelength, p5.Y - _edgelength, p5.Z);
            p8 = new XYZ(p5.X - _edgelength, p5.Y, p5.Z);

            ColorWithTransparency color1 = new ColorWithTransparency(255, 0, 0, 1);
            ColorWithTransparency color2 = new ColorWithTransparency(0, 255, 0, 1);
            ColorWithTransparency color3 = new ColorWithTransparency(0, 0, 255, 1);

            CRectangle rect1 = new CRectangle(p1, p2, p3, p4, color1);
            CRectangle rect2 = new CRectangle(p5, p6, p7, p8, color1);
            CRectangle rect3 = new CRectangle(p1, p2, p6, p5, color2);
            CRectangle rect4 = new CRectangle(p3, p4, p8, p7, color2);
            CRectangle rect5 = new CRectangle(p2, p3, p7, p6, color3);
            CRectangle rect6 = new CRectangle(p1, p4, p8, p5, color3);
            Triangulars.AddRange(rect1.Triangulars);
            Triangulars.AddRange(rect2.Triangulars);
            Triangulars.AddRange(rect3.Triangulars);
            Triangulars.AddRange(rect4.Triangulars);
            Triangulars.AddRange(rect5.Triangulars);
            Triangulars.AddRange(rect6.Triangulars);
        }
    }
}
