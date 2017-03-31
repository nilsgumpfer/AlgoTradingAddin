using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    class DiagramObject : LiveConnectionSubscriber
    {
        private Workbook workbook;
        private Worksheet worksheet;
        //private Excel.Range startPosition;
        //private int drawPosition = 1;
        private List<int> tableColumnsToDraw;
        private List<string> tableHeadline;
        private List<List<string>> tableContent;


        public DiagramObject(TableObject tableObject)
        {
            workbook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add());
            worksheet.Name = "Diagramm OnVista-Livedaten";

            this.tableColumnsToDraw = tableObject.getColumnsToDraw();
            this.tableHeadline = tableObject.getHeadline();
            this.tableContent = tableObject.getContent();

            drawDiagram();

        }


        public void drawDiagram()
        {
            ////worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);
            object misValue = System.Reflection.Missing.Value;
            //Excel.ChartObjects xlCharts = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
            //Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            //Excel.Chart chartPage = myChart.Chart;

            //Excel.Range chartRange;
            //chartRange = ws.get_Range(ws.Cells[1, 1], ws.Cells[content.Count + 1, columns.Count]);
            //chartPage.SetSourceData(chartRange, misValue);

            //chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
            

            //Aktienkurs
            Microsoft.Office.Interop.Excel.Range chartRange;
            Microsoft.Office.Interop.Excel.ChartObjects xlCharts =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChart =
                (Excel.ChartObject)xlCharts.Add(10, 10, 500, 300);
            Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;

            //Excel.Range range = (Excel.Range) worksheet.get_Range

            Excel.Worksheet wsLiveData = (Excel.Worksheet)workbook.Worksheets["OnVista-Livedaten"];

            chartRange = wsLiveData.get_Range("E1", "E20");
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;



            //Volumen
            Microsoft.Office.Interop.Excel.Range chartRange2;
            Microsoft.Office.Interop.Excel.ChartObjects xlCharts2 =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChart2 =
                (Excel.ChartObject)xlCharts2.Add(530, 10, 500, 300);
            Microsoft.Office.Interop.Excel.Chart chartPage2 = myChart2.Chart;

            chartRange2 = wsLiveData.get_Range("F1", "F30");
            chartPage2.SetSourceData(chartRange2, misValue);
            chartPage2.ChartType = Excel.XlChartType.xlColumnClustered;



        }

        public void updateMeWithNewData(StockDataTransferObject newRecord)
        {
            tableHeadline = newRecord.getHeadlineAsList();
            tableContent.Add(newRecord.getLineAsList());
            //TODO: nicht alles neu zeichnen, sondern nur letzte Zeile!
            //draw();
        }


    }
}
