using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Utilitis
{
    public class SelectionFilter : ISelectionFilter
    {
        private Func<Element, bool> _func;
        public SelectionFilter(Func<Element,bool> func)
        {
            this._func = func;
        }
        public bool AllowElement(Element elem)
        {
            return _func(elem);
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
