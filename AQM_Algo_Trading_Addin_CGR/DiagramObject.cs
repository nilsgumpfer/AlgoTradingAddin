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
    class DiagramObject
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
        private object misValue = System.Reflection.Missing.Value;
        int foundColumn = 0;
        //Tabellenblatt, aus welchem die Daten gezogen werden aus Table-Object auslesen
        private Excel.Worksheet wsLiveData;
        private Microsoft.Office.Interop.Excel.Chart chartPageAktienkurs;
        private Microsoft.Office.Interop.Excel.Chart chartPageVolumen;

        public DiagramObject(TableObject tableObject)
        {
            tableObject.subscribeForTableContent(this);
            workbook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
            worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add());
            worksheet.Name = "Management-Cockpit";

            this.tableobject = tableObject;
            this.tableColumnsToDraw = tableObject.getColumnsToDraw();
            this.tableHeadline = tableObject.getHeadline();
            this.tableContent = tableObject.getContent();

            initDiagram();

        }
  
        public void updateMeWithNewData()
        {   
            //Update Aktienkursdiagramm
            updateDiagram(wsLiveData, chartPageAktienkurs, StockDataTransferObject.posPrice, tableobject.getContentCount());
            //Update Volumendiagramm
            updateDiagram(wsLiveData, chartPageVolumen, StockDataTransferObject.posVolume, tableobject.getContentCount());
            //Update Historiendiagramm
            //updateDiagram(wsLiveData, chartPageAktienkurs, 5, tableobject.getContentCount());
        }


        public void initDiagram()
        {
            //Aktienkurs

            //Spalte Aktienkurs setzen
            foundColumn = StockDataTransferObject.posPrice;
            
            //Diagramm erstellen
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsAktienkurs =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartAktienkurs =
                (Excel.ChartObject)xlChartsAktienkurs.Add(10, 10, 500, 280);
            //Microsoft.Office.Interop.Excel.Chart chartPageAktienkurs = myChartAktienkurs.Chart;
            chartPageAktienkurs = myChartAktienkurs.Chart;

            chartPageAktienkurs.ChartType = Excel.XlChartType.xlLine;

            //Tabellenblatt, aus welchem die Daten gezogen werden aus Table-Object auslesen
            wsLiveData = (Excel.Worksheet)workbook.Worksheets["OnVista-Livedaten"];
            //Excel.Worksheet wsLiveData = (Excel.Worksheet)tableobject.getWorksheetOfTableObject();

            updateDiagram(wsLiveData, chartPageAktienkurs, foundColumn, tableobject.getContentCount());



            //Volumen

            //Spalte Aktienkurs setzen
            foundColumn = StockDataTransferObject.posVolume;

            //Diagramm erstellen
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsVolumen =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartVolumen =
                (Excel.ChartObject)xlChartsVolumen.Add(530, 10, 500, 280);
            //Microsoft.Office.Interop.Excel.Chart chartPageAktienkurs = myChartAktienkurs.Chart;
            chartPageVolumen = myChartVolumen.Chart;

            chartPageVolumen.ChartType = Excel.XlChartType.xlColumnClustered;

            //Tabellenblatt, aus welchem die Daten gezogen werden aus Table-Object auslesen
            wsLiveData = (Excel.Worksheet)workbook.Worksheets["OnVista-Livedaten"];
            //Excel.Worksheet wsLiveData = (Excel.Worksheet)tableobject.getWorksheetOfTableObject();

            updateDiagram(wsLiveData, chartPageVolumen, foundColumn, tableobject.getContentCount());


            ////Historische Daten
            //Microsoft.Office.Interop.Excel.Range chartRangeHistoDaten;
            //Microsoft.Office.Interop.Excel.ChartObjects xlChartsHistoDaten =
            //    (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            //Microsoft.Office.Interop.Excel.ChartObject myChartHistoDaten =
            //    (Excel.ChartObject)xlChartsHistoDaten.Add(10, 300, 1020, 270);
            //Microsoft.Office.Interop.Excel.Chart chartPageHistoDaten = myChartHistoDaten.Chart;

            //chartRangeHistoDaten = wsLiveData.get_Range("F1", "F30");
            //chartPageHistoDaten.SetSourceData(chartRangeHistoDaten, misValue);
            //chartPageHistoDaten.ChartType = Excel.XlChartType.xlColumnClustered;


        }

        public void updateDiagram(Excel.Worksheet wsLiveData, Microsoft.Office.Interop.Excel.Chart chartPageAktienkurs, int foundColumn, int ColumnCount)
        {
            //chartRangeAktienkurs = wsLiveData.get_Range("E1", "E20");

            Microsoft.Office.Interop.Excel.Range chartRangeAktienkurs = wsLiveData.Range[wsLiveData.Cells[1, foundColumn], wsLiveData.Cells[ColumnCount+1, foundColumn]];
            chartPageAktienkurs.SetSourceData(chartRangeAktienkurs, misValue);
        }





    }
}
