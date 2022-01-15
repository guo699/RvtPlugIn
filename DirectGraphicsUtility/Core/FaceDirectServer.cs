using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DirectGraphicsUtility.Model;

namespace DirectGraphicsUtility.Core
{
    class FaceDirectServer:DirectContextServer
    {
        private VertexBuffer _vertexBuffers;
        private int _vertexBufferCount;
        private IndexBuffer _indexBuffers;
        private int _indexBufferCount;
        private EffectInstance _effect;
        private VertexFormat _format;
        private int _primitiveCount;

        private List<CTriangular> _triangulars;
        public FaceDirectServer(CTriangular tface)
        {
            _triangulars = new List<CTriangular>() { tface };
        }
        public FaceDirectServer(List<CTriangular> triangulars)
        {
            _triangulars = triangulars;
        }
        public FaceDirectServer(CMesh mesh)
        {
            _triangulars = mesh.Triangulars;
        }
        public override void RenderScene(View dBView, DisplayStyle displayStyle)
        {
            _vertexBufferCount = CTriangular.PointNum * _triangulars.Count;
            int sizefloat = VertexPositionNormalColored.GetSizeInFloats();
            _vertexBuffers = new VertexBuffer(sizefloat * _vertexBufferCount);
            _vertexBuffers.Map(sizefloat * _vertexBufferCount);
            {
                VertexStreamPositionNormalColored vertexStream = _vertexBuffers.GetVertexStreamPositionNormalColored();
                foreach (var triangular in _triangulars)
                {
                    vertexStream.AddVertex(new VertexPositionNormalColored(triangular.PointA, triangular.Normal, triangular.TColor));
                    vertexStream.AddVertex(new VertexPositionNormalColored(triangular.PointB, triangular.Normal, triangular.TColor));
                    vertexStream.AddVertex(new VertexPositionNormalColored(triangular.PointC, triangular.Normal, triangular.TColor));
                }
            }
            _vertexBuffers.Unmap();

            sizefloat = IndexTriangle.GetSizeInShortInts();
            _indexBufferCount = sizefloat * _vertexBufferCount;
            _indexBuffers = new IndexBuffer(_indexBufferCount);
            _indexBuffers.Map(_indexBufferCount);
            {
                IndexStreamTriangle streamTriangle = _indexBuffers.GetIndexStreamTriangle();
                for (int i = 0; i < _triangulars.Count; i++)
                {
                    streamTriangle.AddTriangle(new IndexTriangle(i*3,i*3+1,i*3+2));
                }
            }
            _indexBuffers.Unmap();

            _effect = new EffectInstance(VertexFormatBits.PositionNormalColored);
            _format = new VertexFormat(VertexFormatBits.PositionNormalColored);
            _primitiveCount = CTriangular.PointNum * _triangulars.Count;

            DrawContext.FlushBuffer(
                _vertexBuffers,
                _vertexBufferCount,
                _indexBuffers,
                _indexBufferCount,
                _format, _effect,
                PrimitiveType.TriangleList,
                0,
                _primitiveCount);
            base.RenderScene(dBView, displayStyle);
        }
    }
}
