using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGaphic
{
    class FaceContextServer:DirectContextServerBase
    {
        public FaceContextServer()
        {

        }
        public override void RenderScene(View dBView, DisplayStyle displayStyle)
        {
            List<XYZ> points = new List<XYZ>()
            {
                new XYZ(0,0,0),
                new XYZ(20,0,0),
                new XYZ(0,10,0),
            };

            ColorWithTransparency CWT = new ColorWithTransparency(255, 0, 255, 1);
            _vertexBufferCount = 3;
            int bufferCount = _vertexBufferCount * VertexPositionNormalColored.GetSizeInFloats();
            _vertexBuffer = new VertexBuffer(bufferCount);
            _vertexBuffer.Map(bufferCount);
            {
                VertexStreamPositionNormalColored vertexStream = _vertexBuffer.GetVertexStreamPositionNormalColored();
                foreach (var item in points)
                {
                    vertexStream.AddVertex(new VertexPositionNormalColored(item, XYZ.BasisZ, CWT));
                }
            }
            _vertexBuffer.Unmap();

            _indexBufferCount = (points.Count - 1) * IndexTriangle.GetSizeInShortInts();
            _indexBuffer = new IndexBuffer(_indexBufferCount);
            _indexBuffer.Map(_indexBufferCount);
            {
                IndexStreamTriangle indexStream = _indexBuffer.GetIndexStreamTriangle();
                indexStream.AddTriangle(new IndexTriangle(0,1,2));
            }
            _indexBuffer.Unmap();

            VertexFormat vertexFormat = new VertexFormat(VertexFormatBits.PositionNormalColored);
            EffectInstance effectInstance = new EffectInstance(VertexFormatBits.PositionNormalColored);
            DrawContext.FlushBuffer(
                _vertexBuffer,
                _vertexBufferCount,
                _indexBuffer,
                _indexBufferCount,
                vertexFormat,
                effectInstance,
                PrimitiveType.TriangleList,
                0,
                _vertexBufferCount);
            base.RenderScene(dBView, displayStyle);
        }
    }
}
