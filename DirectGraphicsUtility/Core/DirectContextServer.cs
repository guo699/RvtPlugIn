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
    class DirectContextServer : IDirectContext3DServer
    {
        private Guid _serverId;

        public Guid ServerID
        {
            get { return _serverId; }
            protected set { _serverId = value; }
        }

        public DirectContextServer()
        {
            _serverId = Guid.NewGuid();
        }
        public bool CanExecute(View dBView)
        {
            return dBView is View3D;
        }

        public string GetApplicationId()
        {
            return "";
        }

        public Outline GetBoundingBox(View dBView)
        {
            BoundingBoxXYZ box = dBView.get_BoundingBox(null);
            XYZ min = box.Min;
            XYZ max = box.Max;
            return new Outline(min, max);
        }

        public string GetDescription()
        {
            return "IronBIN Direct3DContextServer Class";
        }

        public string GetName()
        {
            return "IronBIN Server";
        }

        public Guid GetServerId()
        {
            return _serverId;
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
            return "IronBIN";
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
