using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Data.Geometry
{
    public struct TrianglePatch
    {
        public Point3D PointA;
        public Point3D PointB;
        public Point3D PointC;
        public TrianglePatch(Point3D p1,Point3D p2,Point3D p3)
        {
            PointA = p1;
            PointB = p2;
            PointC = p3;
        }
    }
}
