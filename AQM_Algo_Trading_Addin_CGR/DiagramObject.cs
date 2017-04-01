using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using System.Windows.Forms;
using System.Reflection;

namespace AQM_Algo_Trading_Addin_CGR
{
    class DiagramObject : LiveConnectionSubscriber
    {
        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        private Workbook workbook;
        private Worksheet worksheet;
        //private Excel.Range startPosition;
        //private int drawPosition = 1;
        private List<int> tableColumnsToDraw;
        private List<string> tableHeadline;
        private List<List<string>> tableContent;
        private TableObject tableobject;
        int foundedColumn = 0;


        public DiagramObject(TableObject tableObject)
        {
            workbook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add());
            worksheet.Name = "Management-Cockpit";

            this.tableobject = tableObject;
            this.tableColumnsToDraw = tableObject.getColumnsToDraw();
            this.tableHeadline = tableObject.getHeadline();
            this.tableContent = tableObject.getContent();

            drawDiagram();

        }


        public void drawDiagram()
        {
            object misValue = System.Reflection.Missing.Value;

            //Aktienkurs

            //Spalte Aktienkurs finden
            
            for (int i = 0; i < tableHeadline.Count; i++)
            {
                if (tableHeadline[i] == "Kurs")
                {
                    foundedColumn = i;
                }

            }

            foundedColumn = StockDataTransferObject.posPrice;


            //Diagramm erstellen
            Microsoft.Office.Interop.Excel.Range chartRangeAktienkurs;
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsAktienkurs =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartAktienkurs =
                (Excel.ChartObject)xlChartsAktienkurs.Add(10, 10, 500, 280);
            Microsoft.Office.Interop.Excel.Chart chartPageAktienkurs = myChartAktienkurs.Chart;

            Excel.Worksheet wsLiveData = (Excel.Worksheet)workbook.Worksheets["OnVista-Livedaten"];

            //chartRangeAktienkurs = wsLiveData.get_Range("E1", "E20");
            chartRangeAktienkurs = wsLiveData.get_Range(wsLiveData.Cells[1, foundedColumn+1], wsLiveData.Cells[10, foundedColumn+1]);
            chartPageAktienkurs.SetSourceData(chartRangeAktienkurs, misValue);
            chartPageAktienkurs.ChartType = Excel.XlChartType.xlColumnClustered;



            ////Spalte finden
            //Excel.Range foundValue = null;

            //Excel.Range headline = wsLiveData.get_Range("A1", "A20");
            //foundValue = headline.Find("Kurs", misValue,
            //    Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
            //    Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext, false,
            //    misValue, misValue);
            //foundValue.get_Address(Excel.XlReferenceStyle.xlA1);
            //MessageBox.Show(foundValue.get_Address(Excel.XlReferenceStyle.xlA1));




















            //Volumen
            Microsoft.Office.Interop.Excel.Range chartRangechartRangeVolumen;
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsVolumen =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartVolumen =
                (Excel.ChartObject)xlChartsVolumen.Add(530, 10, 500, 280);
            Microsoft.Office.Interop.Excel.Chart chartPageVolumen = myChartVolumen.Chart;

            chartRangechartRangeVolumen = wsLiveData.get_Range("F1", "F30");
            chartPageVolumen.SetSourceData(chartRangechartRangeVolumen, misValue);
            chartPageVolumen.ChartType = Excel.XlChartType.xlColumnClustered;


            //Historische Daten
            Microsoft.Office.Interop.Excel.Range chartRangeHistoDaten;
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsHistoDaten =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartHistoDaten =
                (Excel.ChartObject)xlChartsHistoDaten.Add(10, 300, 1020, 270);
            Microsoft.Office.Interop.Excel.Chart chartPageHistoDaten = myChartHistoDaten.Chart;

            chartRangeHistoDaten = wsLiveData.get_Range("F1", "F30");
            chartPageHistoDaten.SetSourceData(chartRangeHistoDaten, misValue);
            chartPageHistoDaten.ChartType = Excel.XlChartType.xlColumnClustered;

            //Tests
            //MessageBox.Show(this.tableColumnsToDraw.ToString());

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
