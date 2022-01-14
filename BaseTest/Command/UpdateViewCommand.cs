using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Windows;

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class UpdateViewCommand : IExternalCommand
    {
        private UIDocument uidoc;
        private Document doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;

            Reference refe = uidoc.Selection.PickObject(ObjectType.Element);
            TextNote note = doc.GetElement(refe) as TextNote; 

            View view = doc.ActiveView;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                using(Transaction ts = new Transaction(doc))
                {
                    ts.Start("Update");
                    note.Text = (int.Parse(note.Text) + 1).ToString();
                    doc.Regenerate();
                    ts.Commit();
                }
                uidoc.UpdateAllOpenViews();
            }


            return Result.Succeeded;
        }

        void Sleep(int n)
        {
            int m = 10000 * n;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < 10000; j++)
                {

                }
            }
        }

    }
}
