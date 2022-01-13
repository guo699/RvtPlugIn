using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using Autodesk.Revit.DB.ExternalService;

namespace DirectGaphic
{
    class DirectContextServerBase : IDirectContext3DServer
    {
        protected VertexBuffer _vertexBuffer;
        protected int _vertexBufferCount;
        protected IndexBuffer _indexBuffer;
        protected int _indexBufferCount;
        protected Guid m_guid;
        public DirectContextServerBase()
        {
            m_guid = Guid.NewGuid();
        }
        public bool CanExecute(View dBView)
        {
            return true;
        }

        public string GetApplicationId()
        {
            return "123";
        }

        public Outline GetBoundingBox(View dBView)
        {
            return null;
        }

        public string GetDescription()
        {
            return "098";
        }

        public string GetName()
        {
            return "345";
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
            return "456";
        }

        public string GetVendorId()
        {
            return "567";
        }

        public virtual void RenderScene(View dBView, DisplayStyle displayStyle)
        {
            
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
