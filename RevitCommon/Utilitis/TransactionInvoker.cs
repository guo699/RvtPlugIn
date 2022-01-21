using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Utilitis
{
    public static class TransactionInvoker
    {
        public static void Action(Document doc,string transactionName,Action action)
        {
            using(Transaction ts = new Transaction(doc))
            {
                ts.Start(transactionName);
                action();
                ts.Commit();
            }
        }
    }
}
