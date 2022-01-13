using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTest.Projects
{
    class ElementToStlModel
    {
        public UIDocument UIDoc { get; set; }
        public IEnumerable<Element> ExportElements { get; set; }
        public Document Doc { get; set; }

        /// <summary>
        /// 将Revit的模型导出成为STL
        /// </summary>
        public ElementToStlModel(UIDocument uidoc)
        {
            this.UIDoc = uidoc;
            this.Doc = uidoc.Document;
            this.Select();
        }

        private void Select()
        {
            IList<Reference> refes = this.UIDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
            this.ExportElements = refes.Select(n => Doc.GetElement(n));
        }
    }
}
