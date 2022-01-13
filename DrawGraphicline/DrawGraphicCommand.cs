using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;

namespace DrawGraphicline
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class DrawGraphicCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            ExternalService directContext3DService = ExternalServiceRegistry.GetService(ExternalServices.BuiltInExternalServices.DirectContext3DService);
            GraphiclineDrawingServer revitServer = new GraphiclineDrawingServer(uidoc);
            directContext3DService.AddServer(revitServer);

            MultiServerService msDirectContext3DService = directContext3DService as MultiServerService;

            IList<Guid> serverIds = msDirectContext3DService.GetActiveServerIds();
            serverIds.Add(revitServer.GetServerId());

            // Add the new server to the list of active servers.
            msDirectContext3DService.SetActiveServers(serverIds);
            uidoc.UpdateAllOpenViews();

            return Result.Succeeded;
        }
    }
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ClearBufferCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            ExternalServiceId externalDrawerServiceId = ExternalServices.BuiltInExternalServices.DirectContext3DService;
            var externalDrawerService = ExternalServiceRegistry.GetService(externalDrawerServiceId) as MultiServerService;
            if (externalDrawerService == null)
                return Result.Succeeded;

            foreach (var registeredServerId in externalDrawerService.GetRegisteredServerIds())
            {
                var externalDrawServer = externalDrawerService.GetServer(registeredServerId) as GraphiclineDrawingServer;
                if (externalDrawServer == null)
                    continue;
                externalDrawerService.RemoveServer(registeredServerId);
            }

            return Result.Succeeded;
        }
    }
}
