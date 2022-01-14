using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using Autodesk.Revit.DB.ExternalService;

namespace DirectGraphicsUtility.Core
{
    class CurveDirectServer:DirectContextServer
    {
        private List<XYZ> _points;

        private VertexBuffer _vertexBuffers;
        private int _vertexBufferCount;
        private IndexBuffer _indexBuffers;
        private int _indexBufferCount;
        private EffectInstance _effect;
        private VertexFormat _format;
        private int _primitiveCount;
        public CurveDirectServer(Curve curve)
        {
            if(curve is Line line)
            {
                _points = new List<XYZ>();
                for (int i = 0; i < 100; i++)
                {
                    _points.Add(line.Evaluate(i / 100.0,true));
                }
            }
            _points = curve.Tessellate().ToList();
        }
        public override void RenderScene(View dBView, DisplayStyle displayStyle)
        {
            //顶点缓冲
            _vertexBufferCount = _points.Count;
            int sizefloat = VertexPosition.GetSizeInFloats();
            _vertexBuffers = new VertexBuffer(sizefloat * _vertexBufferCount);
            _vertexBuffers.Map(sizefloat * _vertexBufferCount);
            {
                VertexStreamPosition streamPosition = _vertexBuffers.GetVertexStreamPosition();
                foreach (var point in _points)
                {
                    streamPosition.AddVertex(new VertexPosition(point));
                }
            }
            _vertexBuffers.Unmap();

            //索引缓冲
            sizefloat = IndexLine.GetSizeInShortInts();
            _indexBufferCount = sizefloat * _vertexBufferCount;
            _indexBuffers = new IndexBuffer(_indexBufferCount);
            _indexBuffers.Map(_indexBufferCount);
            {
                IndexStreamLine streamLine = _indexBuffers.GetIndexStreamLine();
                for (int i = 0; i < _points.Count - 1; i++)
                {
                    streamLine.AddLine(new IndexLine(i, i + 1));
                }
            }
            _indexBuffers.Unmap();

            _format = new VertexFormat(VertexFormatBits.Position);
            _effect = new EffectInstance(VertexFormatBits.Position);
            _primitiveCount = _points.Count - 1;
            DrawContext.FlushBuffer(
                _vertexBuffers,
                _vertexBufferCount,
                _indexBuffers,
                _indexBufferCount,
                _format, _effect,
                PrimitiveType.LineList,
                0,
                _primitiveCount);
            base.RenderScene(dBView, displayStyle);
        }
    }
}
