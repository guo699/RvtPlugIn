using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.DirectGraphics.Model
{
    class CSphere
    {
        private XYZ _center;
        private double _radius;
        public List<CTriangular> Triangulars { get; set; }
        public CSphere(XYZ center,double radius)
        {
            _center = center;
            _radius = radius;
        }
    }
}
