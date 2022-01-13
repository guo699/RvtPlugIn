using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawGraphicline
{
    class LineBufferStorage
    {
        public VertexBuffer VertexBuffer { get; set; }
        public int VertexBufferCount { get; set; }
        public IndexBuffer IndexBuffer { get; set; }
        public int IndexBufferCount { get; set; }
        public VertexFormat VertexFormat { get; set; }
        public EffectInstance EffectInstance { get; set; }
        public int PrimitiveCount { get; set; }
        public VertexFormatBits FormatBits { get; set; }
        public List<IList<XYZ>> EdgeXYZs { get; set; }
        public LineBufferStorage()
        {
            EdgeXYZs = new List<IList<XYZ>>();
            XYZ p1 = new XYZ(0, 0, 0);
            XYZ p2 = new XYZ(1, 0, 0);
            XYZ p3 = new XYZ(1, 1, 0);
            XYZ p4 = new XYZ(0, 1, 0);
            Line l1 = Line.CreateBound(p1, p2);
            Line l2 = Line.CreateBound(p2, p3);
            Line l3 = Line.CreateBound(p3, p4);
            Line l4 = Line.CreateBound(p4, p1);
            EdgeXYZs.Add(l1.Tessellate());
            EdgeXYZs.Add(l2.Tessellate());
            EdgeXYZs.Add(l3.Tessellate());
            EdgeXYZs.Add(l4.Tessellate());

            Arc c = Arc.Create(new XYZ(1, 0, 0), new XYZ(0, 1, 0), new XYZ(0.71, 0.71, 0));
            EdgeXYZs.Add(c.Tessellate());
        }
    }
}
