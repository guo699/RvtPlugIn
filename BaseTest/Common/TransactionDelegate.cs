using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTest.Common
{
    class TransactionDelegate
    {
        public static void Invoke(Document doc,Action action)
        {
            using(Transaction ts = new Transaction(doc))
            {
                ts.Start("TS");
                action.Invoke();
                ts.Commit();
            }
        }
    }
}
