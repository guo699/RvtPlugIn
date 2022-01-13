using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.DirectContext3D;
using Autodesk.Revit.DB.ExternalService;

namespace DirectGaphic
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class DirectCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            XYZ p1 = uidoc.Selection.PickPoint("拾取第一个点");
            XYZ p2 = uidoc.Selection.PickPoint("拾取第二个点");

            ExternalService directContext3DService = ExternalServiceRegistry.GetService(ExternalServices.BuiltInExternalServices.DirectContext3DService);

            Arc arc = Arc.Create(new XYZ(-5, 0, 0), new XYZ(0, -5, 0), new XYZ(7.1, 7.1, 0));
            arc = Arc.Create(p1, p2, XYZ.Zero);
            Line line = Line.CreateBound(p1, p2);
            CurveContextServer revitServer = new CurveContextServer(line);

            //FaceContextServer revitServer = new FaceContextServer();
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
                return Result.Cancelled; 

            foreach (var registeredServerId in externalDrawerService.GetActiveServerIds())
            {
                var externalDrawServer = externalDrawerService.GetServer(registeredServerId);
                if (externalDrawServer == null)
                    continue;
                externalDrawerService.RemoveServer(registeredServerId);
            }
            uidoc.UpdateAllOpenViews();
            return Result.Succeeded;
        }
    }
}
