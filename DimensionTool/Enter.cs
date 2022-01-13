using DimensionTool.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DimensionTool
{
    class Enter
    {
        [STAThread]
        public static void Main()
        {         
            List<TableItemData> items = new List<TableItemData>()
            {
                new TableItemData("不锈钢","LDG-22-1","DN220","500",1,0),
                new TableItemData("不锈钢","LDG-22-2","DN220","500",1,0),
                new TableItemData("不锈钢","LDG-22-3","DN220","500",1,0),
                new TableItemData("不锈钢","LDG-22-4","DN220","500",1,0),
                new TableItemData("不锈钢","LDG-22-5","DN220","500",1,0),
                new TableItemData("不锈钢","LDG-22-6","DN220","500",1,0),
                new TableItemData("不锈钢","LDG-22-7","DN220","500",1,0),
                new TableItemData("不锈钢","LDG-22-8","DN220","500",1,0),

                new TableItemData("生铁钢","LDG-22-9","DN220","500",1,0),
                new TableItemData("生铁钢","LDG-22-10","DN220","500",1,0),
                new TableItemData("生铁钢","LDG-22-11","DN220","500",1,0),
                new TableItemData("生铁钢","LDG-22-12","DN220","500",1,0),
                new TableItemData("生铁钢","LDG-22-13","DN220","500",1,0),
                new TableItemData("生铁钢","LDG-22-14","DN220","500",1,0),
                new TableItemData("生铁钢","LDG-22-15","DN220","500",1,0),

                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
                new TableItemData("弯头","","DN220xDN220","/",1,0),
            };
            ExportTableData data = new ExportTableData("LDG-22",items);

            List<ExportTableData> datas = new List<ExportTableData>()
            {
                data,
                data,
                data,
                data,
                data,
                data,
                data,
                data,
                data,
                data,
            };

            StatisticTableExport export = new StatisticTableExport();
            export.TableDatas = datas;
            export.DoExport();
        }
    }
}
