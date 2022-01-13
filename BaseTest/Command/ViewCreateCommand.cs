using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class ViewCreateCommand : IExternalCommand
    {
        private UIDocument uidoc;
        private Document doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;

            View view = doc.ActiveView;

            var elems = GetElements(doc, view).ToList();

            TaskDialog.Show("XXX", string.Join("\n", elems.Select(n => n.Id.IntegerValue)));

            return Result.Succeeded;
        }

        public static IEnumerable<Element> GetElements(Document doc,View view)
        {
            return new FilteredElementCollector(doc, view.Id);
        }
    }
}
