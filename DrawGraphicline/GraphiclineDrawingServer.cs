using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;

namespace DrawGraphicline
{
    class GraphiclineDrawingServer : IDirectContext3DServer
    {
        private Guid m_guid;
        public GraphiclineDrawingServer(UIDocument uidoc)
        {
            m_guid = Guid.NewGuid();
        }
        public bool CanExecute(View dBView)
        {
            return true;
        }

        public string GetApplicationId()
        {
            return "";
        }

        public Outline GetBoundingBox(View dBView)
        {
            return null;
        }

        public string GetDescription()
        {

            return "To Draw Line using Direct";
        }

        public string GetName()
        {
            return "Draw Graphic Line";
        }

        public Guid GetServerId()
        {
            return m_guid;
        }

        public ExternalServiceId GetServiceId()
        {
            return ExternalServices.BuiltInExternalServices.DirectContext3DService;
        }

        public string GetSourceId()
        {
            return "";
        }

        public string GetVendorId()
        {
            return "Iron BIN";
        }

        public void RenderScene(View dBView, DisplayStyle displayStyle)
        {
            LineBufferStorage lineBuffer = new LineBufferStorage();
            ProcessEdges(lineBuffer);
            DrawContext.FlushBuffer(lineBuffer.VertexBuffer,
                                    lineBuffer.VertexBufferCount,
                                    lineBuffer.IndexBuffer,
                                    lineBuffer.IndexBufferCount,
                                    lineBuffer.VertexFormat,
                                    lineBuffer.EffectInstance,
                                    PrimitiveType.LineList,
                                    0,
                                    lineBuffer.PrimitiveCount
                                    );
        }

        private void ProcessEdges(LineBufferStorage bufferStorage)
        {
            List<IList<XYZ>> edges = bufferStorage.EdgeXYZs;
            if (edges.Count == 0)
                return;

            bufferStorage.VertexBufferCount = bufferStorage.EdgeXYZs.Sum(n => n.Count);

            // Edges are encoded as line segment primitives whose vertices contain only position information.
            bufferStorage.FormatBits = VertexFormatBits.Position;

            int edgeVertexBufferSizeInFloats = VertexPosition.GetSizeInFloats() * bufferStorage.VertexBufferCount;
            List<int> numVerticesInEdgesBefore = new List<int>();
            numVerticesInEdgesBefore.Add(0);

            bufferStorage.VertexBuffer = new VertexBuffer(edgeVertexBufferSizeInFloats);
            bufferStorage.VertexBuffer.Map(edgeVertexBufferSizeInFloats);
            {
                VertexStreamPosition vertexStream = bufferStorage.VertexBuffer.GetVertexStreamPosition();
                foreach (IList<XYZ> xyzs in edges)
                {
                    foreach (XYZ vertex in xyzs)
                    {
                        vertexStream.AddVertex(new VertexPosition(vertex));
                    }

                    numVerticesInEdgesBefore.Add(numVerticesInEdgesBefore.Last() + xyzs.Count);
                }
            }
            bufferStorage.VertexBuffer.Unmap();

            bufferStorage.PrimitiveCount = bufferStorage.VertexBufferCount - 1;
            int edgeNumber = 0;
            bufferStorage.IndexBufferCount = bufferStorage.PrimitiveCount * IndexLine.GetSizeInShortInts();
            int indexBufferSizeInShortInts = 1 * bufferStorage.IndexBufferCount;
            bufferStorage.IndexBuffer = new IndexBuffer(indexBufferSizeInShortInts);
            bufferStorage.IndexBuffer.Map(indexBufferSizeInShortInts);
            {
                IndexStreamLine indexStream = bufferStorage.IndexBuffer.GetIndexStreamLine();
                foreach (IList<XYZ> xyzs in edges)
                {
                    int startIndex = numVerticesInEdgesBefore[edgeNumber];
                    for (int i = 1; i < xyzs.Count; i++)
                    {
                        // Add two indices that define a line segment.
                        indexStream.AddLine(new IndexLine((int)(startIndex + i - 1),
                                                          (int)(startIndex + i)));
                    }
                    edgeNumber++;
                }
            }
            bufferStorage.IndexBuffer.Unmap();


            bufferStorage.VertexFormat = new VertexFormat(bufferStorage.FormatBits);
            bufferStorage.EffectInstance = new EffectInstance(bufferStorage.FormatBits);
        }

        public bool UseInTransparentPass(View dBView)
        {
            return true;
        }

        public bool UsesHandles()
        {
            return false;
        }
    }
}
