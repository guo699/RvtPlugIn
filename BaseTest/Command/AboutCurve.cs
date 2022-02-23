using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitCommon.Extensions;
using RevitCommon.Utilitis;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class AboutCurve : IExternalCommand
    {
        private UIDocument UIDoc;
        private Document Doc;
        private const string path = @"C:\Users\IronBin\Desktop\points.csv";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDoc = commandData.Application.ActiveUIDocument;
            Doc = UIDoc.Document;

            Window window = new Window();
            RichTextBox box = new RichTextBox();
            box.Document = new System.Windows.Documents.FlowDocument();
            window.Content = box;
            window.Show();

            return Result.Succeeded;
        }
    }
}
