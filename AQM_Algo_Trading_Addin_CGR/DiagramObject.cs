﻿using System;
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
        private int foundColumnKurs = 0;
        private int foundColumnVolumen = 0;
        private int foundColumnTimestamp = 0;
        //Tabellenblatt, aus welchem die Daten gezogen werden aus Table-Object auslesen
        private Excel.Worksheet wsLiveData;
        private Microsoft.Office.Interop.Excel.Chart chartPageAktienkurs;
        private Microsoft.Office.Interop.Excel.Chart chartPageVolumen;
        private TableObject historicalTableObject;
        private string selectedTicker; 

        public DiagramObject(TableObject tableObject, TableObject historicalTableObject, string selectedTicker)
        {
            this.historicalTableObject = historicalTableObject;
            this.selectedTicker = selectedTicker;

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

        //public void setHistoricalDataTable(TableObject historicalDataTable)
        //{
        //    this.historicalDataTable = historicalDataTable;
        //}

        public void updateMeWithNewData()
        {   
            //Update Aktienkursdiagramm
            updateDiagram(wsLiveData, chartPageAktienkurs, StockDataTransferObject.posTimestampOther, StockDataTransferObject.posPrice, tableobject.getContentCount());
            //Update Volumendiagramm
            updateDiagram(wsLiveData, chartPageVolumen, StockDataTransferObject.posTimestampOther, StockDataTransferObject.posVolume, tableobject.getContentCount());
        }


        public void initDiagram()
        {
            //Aktienkurs

            //Spalte Aktienkurs setzen
            foundColumnKurs = StockDataTransferObject.posPrice;

            //Column für Timestamp setzen
            foundColumnTimestamp = StockDataTransferObject.posTimestampPrice;

            //Diagramm erstellen
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsAktienkurs =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartAktienkurs =
                (Excel.ChartObject)xlChartsAktienkurs.Add(10, 10, 500, 280);
            //Microsoft.Office.Interop.Excel.Chart chartPageAktienkurs = myChartAktienkurs.Chart;
            chartPageAktienkurs = myChartAktienkurs.Chart;

            chartPageAktienkurs.ChartType = Excel.XlChartType.xlLine;

            //Titel für Aktienkursdiagramm setzen
            myChartAktienkurs.Chart.HasTitle = true;
            myChartAktienkurs.Chart.ChartTitle.Text = "Akienkurs der " + selectedTicker + " Aktie";

            //Tabellenblatt, aus welchem die Daten gezogen werden aus Table-Object auslesen
            wsLiveData = (Excel.Worksheet)workbook.Worksheets["OnVista-Livedaten"];

            //Aktienkursdiagramm zu Beginn einmal updaten
            updateDiagram(wsLiveData, chartPageAktienkurs, foundColumnTimestamp, foundColumnKurs, tableobject.getContentCount());



            //Volumen

            //Spalte Aktienkurs setzen
            foundColumnVolumen = StockDataTransferObject.posVolume;

            //Column für Timestamp setzen
            foundColumnTimestamp = StockDataTransferObject.posTimestampVolume;

            //Diagramm erstellen
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsVolumen =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartVolumen =
                (Excel.ChartObject)xlChartsVolumen.Add(530, 10, 500, 280);
            chartPageVolumen = myChartVolumen.Chart;

            chartPageVolumen.ChartType = Excel.XlChartType.xlColumnClustered;

            //Titel für Volumendiagramm setzen
            myChartVolumen.Chart.HasTitle = true;
            myChartVolumen.Chart.ChartTitle.Text = "Volumen der " + selectedTicker + " Aktie";

            //Tabellenblatt, aus welchem die Daten gezogen werden aus Table-Object auslesen
            wsLiveData = (Excel.Worksheet)workbook.Worksheets["OnVista-Livedaten"];

            //Volumendiagramm zu Beginn einmal updaten
            updateDiagram(wsLiveData, chartPageVolumen, foundColumnTimestamp, foundColumnVolumen, tableobject.getContentCount());


            //Historische Daten
            Microsoft.Office.Interop.Excel.Range chartRangeHistoDaten;
            Microsoft.Office.Interop.Excel.ChartObjects xlChartsHistoDaten =
                (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Microsoft.Office.Interop.Excel.ChartObject myChartHistoDaten =
                (Excel.ChartObject)xlChartsHistoDaten.Add(10, 300, 1020, 270);
            Microsoft.Office.Interop.Excel.Chart chartPageHistoDaten = myChartHistoDaten.Chart;

            //Excel.Worksheet wsHistoricalData = (Excel.Worksheet)historicalTableObject.getWorksheetOfTableObject();
            Excel.Worksheet wsHistoricalData = (Excel.Worksheet)workbook.Worksheets["Historische Daten"];
            chartRangeHistoDaten = wsHistoricalData.Range[wsHistoricalData.Cells[1, 1], wsHistoricalData.Cells[historicalTableObject.getContentCount() + 1, historicalTableObject.getColumnsToDrawCount()]];


            chartPageHistoDaten.SetSourceData(chartRangeHistoDaten, misValue);
            chartPageHistoDaten.ChartType = Excel.XlChartType.xlLine;

            //Titel das Diagramms für historische Daten setzen
                myChartHistoDaten.Chart.HasTitle = true;
                myChartHistoDaten.Chart.ChartTitle.Text = "Historische Daten der " + selectedTicker + " Aktie";
         
          
        }

        public void updateDiagram(Excel.Worksheet wsLiveData, Microsoft.Office.Interop.Excel.Chart chartPage, int foundColumnTimestamp, int foundColumn, int ColumnCount)
        {
            ////Startrangecolumn
            //Microsoft.Office.Interop.Excel.Range rng = (Excel.Range)wsLiveData.Cells[1, foundColumn];
            //string startrangecolumn = rng.Address.ToString();
 
            //EndRangeColumn
            Microsoft.Office.Interop.Excel.Range rng2 = (Excel.Range)wsLiveData.Cells[ColumnCount + 1, foundColumn];
            string endRangeColumn = rng2.Address.ToString();

            ////Zusammenbauen von Start- und Endrange für Column
            //Excel.Range rangeColumn = wsLiveData.get_Range(startrangecolumn, endRangeColumn);
            //string rangeColumnText = rangeColumn.Address.ToString();

            //StartRangeTimestamp
            Microsoft.Office.Interop.Excel.Range rng3 = (Excel.Range)wsLiveData.Cells[1, foundColumnTimestamp];
            string startRangeTimestamp = rng3.Address.ToString();

            ////EndRangeTimpestamp
            //Microsoft.Office.Interop.Excel.Range rng4 = (Excel.Range)wsLiveData.Cells[ColumnCount + 1, foundColumnTimestamp];
            //string endRangeTimpestamp = rng4.Address.ToString();

            ////Zusammenbauen von Start- und Endrange für Timestamp
            //Excel.Range rangeColumnTimestamp = wsLiveData.get_Range(startRangeTimestamp, endRangeTimpestamp);
            //string rangeColumnTimestampText = rangeColumnTimestamp.Address.ToString();

            //Zusammenbauen von Start- und Endrange für Timestamp
            //Excel.Range rangeColumn3 = wsLiveData.get_Range(rangeColumn, rangeColumnTimestamp);

            //Excel.Range chartRangeAktienkurs =
            Excel.Range finalRange = wsLiveData.get_Range(startRangeTimestamp + ":" + endRangeColumn);


            //Excel.Range chartRangeAktienkurs =
            //wsLiveData.Range[wsLiveData.Cells[1, foundColumn], wsLiveData.Cells[ColumnCount+1, foundColumn]];


            //Excel.Range range = app.Union(rangeColumn, rangeColumn2);
            //chartPage.SetSourceData(rangeColumn, misValue);


            //Func<object, Excel.XlDataSeriesType, Excel.XlDataSeriesDate, object, object, object, dynamic> kursSeries = rangeColumn.DataSeries;
            //chartPage.SeriesCollection.
            //MessageBox.Show("finalRange: " + finalRange.Address.ToString());
            chartPage.SetSourceData(finalRange, misValue);
        }

    }
}
