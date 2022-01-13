using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGaphic
{
    class CurveContextServer: DirectContextServerBase
    {
        private Curve _curve;
        private List<XYZ> points;
        public CurveContextServer(Curve curve)
        {
            points = curve.Tessellate().ToList();
            //points = new List<XYZ>();
            //Random r = new Random();
            //for (int i = 0; i < 10000; i++)
            //{
            //    double x = r.NextDouble();
            //    double y = r.NextDouble();
            //    double z = r.NextDouble();
            //    points.Add(new XYZ(x, y, z));
            //}
        }
        public override void RenderScene(View dBView, DisplayStyle displayStyle)
        {
            //Arc arc = Arc.Create(new XYZ(5, 0, 0), new XYZ(0, 5, 0), new XYZ(7.1, 7.1, 0));
            //List<XYZ> points = arc.Tessellate().ToList();

            _vertexBufferCount = points.Count;
            int buffercount = VertexPosition.GetSizeInFloats() * _vertexBufferCount;
            _vertexBuffer = new VertexBuffer(buffercount);
            _vertexBuffer.Map(buffercount);
            {
                VertexStreamPosition vertexStream = _vertexBuffer.GetVertexStreamPosition();
                foreach (var item in points)
                {
                    vertexStream.AddVertex(new VertexPosition(item));
                }
            }
            _vertexBuffer.Unmap();

            _indexBufferCount = (points.Count - 0) * IndexLine.GetSizeInShortInts();
            _indexBuffer = new IndexBuffer(_indexBufferCount);
            _indexBuffer.Map(_indexBufferCount);
            {
                IndexStreamLine indexStream = _indexBuffer.GetIndexStreamLine();
                for (int i = 0; i < points.Count - 1; i++)
                {
                    indexStream.AddLine(new IndexLine(i,i+1));
                }
            }
            _indexBuffer.Unmap();

            VertexFormat vertexFormat = new VertexFormat(VertexFormatBits.Position);
            EffectInstance effectInstance = new EffectInstance(VertexFormatBits.Position);
            DrawContext.FlushBuffer(
                _vertexBuffer,
                _vertexBufferCount,
                _indexBuffer,
                _indexBufferCount,
                vertexFormat,
                effectInstance,
                PrimitiveType.LineList,
                0,
                _vertexBufferCount - 0);
            base.RenderScene(dBView, displayStyle);
        }
    }
}
