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
    class DirectServerRegister
    {
        public static void AddDirect3DServer(IDirectContext3DServer server)
        {
            Guid guid = server.GetServerId();
            ExternalServiceId serviceId = ExternalServices.BuiltInExternalServices.DirectContext3DService;
            MultiServerService serverService = ExternalServiceRegistry.GetService(serviceId) as MultiServerService;
            serverService.AddServer(server);
            IList<Guid> guids = serverService.GetActiveServerIds();
            guids.Add(guid);
            serverService.SetActiveServers(guids);
        }

        public static void RemoveDirect3DServer(Guid guid)
        {
            ExternalServiceId serviceId = ExternalServices.BuiltInExternalServices.DirectContext3DService;
            MultiServerService serverService = ExternalServiceRegistry.GetService(serviceId) as MultiServerService;
            IDirectContext3DServer server = serverService.GetServer(guid) as IDirectContext3DServer;
            if (server != null)
                serverService.RemoveServer(guid);
        }

        public static void ClearDirect3DServer()
        {
            ExternalServiceId serviceId = ExternalServices.BuiltInExternalServices.DirectContext3DService;
            MultiServerService serverService = ExternalServiceRegistry.GetService(serviceId) as MultiServerService;
            IList<Guid> guids = serverService.GetRegisteredServerIds();
            foreach (var id in guids)
            {
                serverService.RemoveServer(id);
            }
        }
    }
}
