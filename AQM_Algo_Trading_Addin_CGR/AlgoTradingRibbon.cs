using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;
using MySql.Data.MySqlClient;
using Microsoft.Office.Tools;

namespace AQM_Algo_Trading_Addin_CGR
{
    public partial class AlgoTradingRibbon
    {
        public int selectedOptions = 0;

        private void AlgoTradingRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //Checkboxen deaktivieren
            CB_Quelle_Onvista.Enabled = false;
            CB_Quelle_Yahoo.Enabled = false;
            CB_Quelle_Dummy.Enabled = false;
            CB_Quelle_Lokal.Enabled = false;
            CB_Ziel_AktuellesTB.Enabled = false;
            CB_Ziel_NeuesTB.Enabled = false;
            CB_Ziel_Cursor.Enabled = false;

            //Button deaktivieren
            BTN_Aktionen_Ausfuehren.Enabled = false;
        }

        private void CB_Typ_Live_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Lokal.Checked == true)
            {
                CB_Typ_Live.Checked = false;
                return;
            }
            if (CB_Typ_Live.Checked == true)
            {
                CB_Typ_Historisch.Enabled = false;
                CB_Quelle_Lokal.Enabled = false;
                selectedOptions = selectedOptions + 1;
                if (CB_Quelle_Onvista.Checked == true)
                {
                    CB_Quelle_Onvista.Enabled = true;
                    CB_Quelle_Yahoo.Enabled = false;
                    CB_Quelle_Dummy.Enabled = false;
                }
                else if (CB_Quelle_Dummy.Checked == true)
                {
                    CB_Quelle_Dummy.Enabled = true;
                    CB_Quelle_Onvista.Enabled = false;
                    CB_Quelle_Yahoo.Enabled = false;
                } 
                else
                {
                    CB_Quelle_Dummy.Enabled = true;
                    CB_Quelle_Onvista.Enabled = true;
                    CB_Quelle_Yahoo.Enabled = false;
                }
            }            
            else
            {
                CB_Typ_Historisch.Enabled = true;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Dummy.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Ziel_NeuesTB_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_NeuesTB.Checked == true)
            {
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                BTN_Aktionen_Ausfuehren.Enabled = true;        
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Ziel_AktuellesTB.Enabled = true;
                CB_Ziel_Cursor.Enabled = true;
                BTN_Aktionen_Ausfuehren.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Ziel_AktuellesTB_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_AktuellesTB.Checked == true)
            {
                CB_Ziel_NeuesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                BTN_Aktionen_Ausfuehren.Enabled = true;
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Ziel_NeuesTB.Enabled = true;
                CB_Ziel_Cursor.Enabled = true;
                BTN_Aktionen_Ausfuehren.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Typ_Historisch_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Onvista.Checked == true)
            {
                CB_Typ_Historisch.Checked = false;
                return;
            }
            if (CB_Typ_Historisch.Checked == true)
            {
                CB_Typ_Live.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;

                if (CB_Quelle_Lokal.Checked == true)
                {
                    CB_Quelle_Yahoo.Enabled = false;
                }
                else
                {
                    CB_Quelle_Yahoo.Enabled = true;
                }

                if (CB_Quelle_Yahoo.Checked == true)
                {
                    CB_Quelle_Lokal.Enabled = false;
                }
                else {
                    CB_Quelle_Lokal.Enabled = true;
                }

                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Typ_Live.Enabled = true;
                CB_Quelle_Onvista.Enabled = false;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Yahoo.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Quelle_Lokal_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Lokal.Checked == true)
            {
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
                if (CB_Ziel_AktuellesTB.Checked == true)
                {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = false;
                    CB_Ziel_Cursor.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_NeuesTB.Checked == true)
                {
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = false;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_Cursor.Checked == true)
                {
                    CB_Ziel_Cursor.Enabled = true;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    CB_Ziel_NeuesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Quelle_Yahoo.Enabled = true;
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_NeuesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Quelle_Onvista_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Onvista.Checked == true)
            {
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Dummy.Enabled = false;
                if (CB_Ziel_AktuellesTB.Checked == true)
                {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = false;
                    CB_Ziel_Cursor.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_NeuesTB.Checked == true)
                {
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = false;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_Cursor.Checked == true)
                {
                    CB_Ziel_Cursor.Enabled = true;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    CB_Ziel_NeuesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Dummy.Enabled = true;
                if (CB_Typ_Historisch.Checked == true)
                {
                    CB_Quelle_Lokal.Enabled = true;
                }
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_NeuesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Quelle_Yahoo_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Yahoo.Checked == true)
            {
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
                CB_Quelle_Dummy.Enabled = false;
                if (CB_Ziel_AktuellesTB.Checked == true)
                {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = false;
                    CB_Ziel_Cursor.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_NeuesTB.Checked == true)
                {
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = false;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_Cursor.Checked == true)
                {
                    CB_Ziel_Cursor.Enabled = true;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    CB_Ziel_NeuesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = true;   
                }
                selectedOptions = selectedOptions + 1;
            }
            else
            {   
                if(CB_Typ_Historisch.Checked == true)
                {
                    CB_Quelle_Lokal.Enabled = true;
                }
                else
                {
                    CB_Quelle_Onvista.Enabled = true;
                    CB_Quelle_Dummy.Enabled = true;
                }
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_NeuesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Ziel_Cursor_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_Cursor.Checked == true)
            {
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_NeuesTB.Enabled = false;
                BTN_Aktionen_Ausfuehren.Enabled = true;
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Ziel_AktuellesTB.Enabled = true;
                CB_Ziel_NeuesTB.Enabled = true;
                BTN_Aktionen_Ausfuehren.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }
        private void CB_Quelle_Dummy_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Dummy.Checked == true)
            {
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
                if (CB_Ziel_AktuellesTB.Checked == true)
                {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = false;
                    CB_Ziel_Cursor.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_NeuesTB.Checked == true)
                {
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = false;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else if (CB_Ziel_Cursor.Checked == true)
                {
                    CB_Ziel_Cursor.Enabled = true;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    CB_Ziel_NeuesTB.Enabled = false;
                    BTN_Aktionen_Ausfuehren.Enabled = true;
                }
                else
                {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Onvista.Enabled = true;
                if (CB_Typ_Historisch.Checked == true)
                {
                    CB_Quelle_Lokal.Enabled = true;
                }
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_NeuesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_NeuesTB.Enabled == false & CB_Ziel_AktuellesTB.Enabled == false & CB_Ziel_Cursor.Enabled == false)
            {
                BTN_Aktionen_Ausfuehren.Enabled = false;
            }
            if (selectedOptions == 3)
            {
                Konfigurator view = new Konfigurator();

                if (CB_Typ_Live.Checked)
                    view.groupBox1.Visible = false;

                view.ShowDialog();

                Workbook workBook;
                Worksheet workSheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);
                Excel.Range position = Globals.ThisAddIn.Application.Cells[1, 1];

                //TODO: Hier vielleicht noch nachbessern :)
                string stockSymbol = view.comboBox1.SelectedItem.ToString();
                DateTime dateFrom = view.dateTimePicker1.Value;
                DateTime dateTo = view.dateTimePicker2.Value;

                DataManager dataManager = DataManager.getInstance();
                TableObject tableObject = null;
                LiveConnectors liveVariant = LiveConnectors.OnVistaDummy;

                List<int> columnsToDraw = null;
                List<StockDataTransferObject> listOfRecords = null;

                if (view.hasBeenCancelled == false)
                {
                    //************************* Position *************************************
                    //Aktive Zelle
                    if (CB_Ziel_Cursor.Checked)
                    {
                        position = Globals.ThisAddIn.Application.ActiveCell;
                    }

                    //Aktuelles Tabellenblatt
                    else if (CB_Ziel_AktuellesTB.Checked)
                    {
                        //nutze Standards
                    }

                    //Neues Tabellenblatt
                    else if (CB_Ziel_NeuesTB.Checked)
                    {
                        workBook = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);
                        String tabellenblattName = Microsoft.VisualBasic.Interaction.InputBox("Bitte Tabellenblattnamen eingeben!", "Tabellenblattname eingeben", "");
                        foreach (Excel.Worksheet sheet in workBook.Worksheets)
                        {
                            if (sheet.Name.ToString() == tabellenblattName)
                            {
                                MessageBox.Show("Tabellenblattname bereits vorhanden! Bitte erneut ausführen!");
                                return;
                            }
                        }
                        workSheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add());
                        workSheet.Name = tabellenblattName;
                    }
                    //************************* Position *************************************

                    //************************* Historisch ***********************************
                    
                    //Yahoo API
                    if (CB_Quelle_Yahoo.Checked)
                    {
                        listOfRecords = dataManager.getHistoricalStockData(stockSymbol, dateFrom, dateTo, YahooFinanceAPI_Resolution.Daily);
                        columnsToDraw = dataManager.getColumnsToDraw_forYahooHistoricalData();
                    }

                    //Lokale Daten
                    else if(CB_Quelle_Lokal.Checked)
                    {
                        listOfRecords = dataManager.getLocallySavedStockData(stockSymbol, dateFrom, dateTo);
                        columnsToDraw = dataManager.getColumnsToDraw_Standard();
                    }

                    //************************* Historisch ***********************************

                    //************************* Live *****************************************

                    //OnVista Live
                    if (CB_Quelle_Onvista.Checked)
                    {
                        liveVariant = LiveConnectors.OnVista; 
                    }

                    //Dummy Live
                    else if(CB_Quelle_Dummy.Checked)
                    {
                        liveVariant = LiveConnectors.OnVistaDummy;
                    }

                    //************************* Live *****************************************

                    //************************* Zusammenführung ******************************

                    //Zusammenführung für historische Variante
                    if (CB_Quelle_Yahoo.Checked || CB_Quelle_Lokal.Checked)
                    {
                        tableObject = new TableObject(workSheet, position, listOfRecords, columnsToDraw);
                        tableObject.drawAll();
                    }

                    //Zusammenführung für Live-Variante
                    else if (CB_Quelle_Onvista.Checked || CB_Quelle_Dummy.Checked)
                    {
                        tableObject = new TableObject(workSheet, position, dataManager.getColumnsToDraw_LiveStockData());
                        dataManager.subscribeForLiveConnection(stockSymbol, tableObject, liveVariant);
                        tableObject.drawAll();
                    }

                    //************************* Zusammenführung ******************************
                }
            }
            else
            {
                MessageBox.Show("Es sind nicht alle nötigen Optionen ausgefüllt! Bitte Auswahl überprüfen!");
            }
        }

        private void button7_Click(object sender, RibbonControlEventArgs e)
        {
            //Historische Daten auswählen
            Konfigurator view = new Konfigurator();
            view.ShowDialog();

            LiveConnectors variant;
            DialogResult dummyOrNot = MessageBox.Show("Möchten Sie mit echten Live-Daten (Ja) oder mit Dummy-Daten (Nein) arbeiten?", "Datentyp auswählen", MessageBoxButtons.YesNo);

            switch (dummyOrNot)
            {
                case DialogResult.Yes:
                    variant = LiveConnectors.OnVista;
                    break;
                case DialogResult.No:
                    variant = LiveConnectors.OnVistaDummy;
                    break;
                default:
                    variant = LiveConnectors.OnVistaDummy;
                    break;
            }

            if (view.hasBeenCancelled == false)
            {

                ProgressIndicator progress = new ProgressIndicator();
                progress.progressBar1.Maximum = 100;
                progress.progressBar1.Minimum = 0;
                progress.progressBar1.Value = 20;
                progress.Show();
                progress.progressBar1.Value = 25;

                DataManager dataManager = DataManager.getInstance();

                progress.progressBar1.Value = 30;

                TableObject historicalDataTableObject = new TableObject(
                                        Globals.Factory.GetVstoObject
                                        (
                                            Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet
                                        ),
                                        Globals.ThisAddIn.Application.Cells[1, 1],
                                        dataManager.getHistoricalStockData
                                        (
                                            view.comboBox1.SelectedItem.ToString(),
                                            view.dateTimePicker1.Value,
                                            view.dateTimePicker2.Value,
                                            YahooFinanceAPI_Resolution.Daily
                                        ),
                                        dataManager.getColumnsToDraw_forYahooHistoricalData()
                                      );

                progress.progressBar1.Value = 45;
                historicalDataTableObject.changeSheetName("Historische Daten");
                progress.progressBar1.Value = 55;
                historicalDataTableObject.drawOnlyRelevantColumns();
                progress.progressBar1.Value = 60;
                
                //Livedaten
                TableObject liveDataTableObject = new TableObject(
                                    Globals.Factory.GetVstoObject(
                                        Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                    Globals.ThisAddIn.Application.Cells[1, 1]);

                progress.progressBar1.Value = 75;

                //liveDataTable.changeWorkbookName("OnVista-Livedaten");
                liveDataTableObject.createNewWorksheet("Livedaten");

                progress.progressBar1.Value = 80;
                dataManager.subscribeForLiveConnection(view.comboBox1.SelectedItem.ToString(), liveDataTableObject, variant);
                dataManager.pausePushWorkers(); //lege Worker schlafen
                progress.progressBar1.Value = 85;

                DiagramObject myDiagram = new DiagramObject(liveDataTableObject, historicalDataTableObject, view.comboBox1.SelectedItem.ToString());
                progress.progressBar1.Value = 90;
                
                Algo algorithmus = new Algo(Globals.ThisAddIn.ac, view.comboBox1.SelectedItem.ToString(), variant);
                Globals.ThisAddIn.SharePane.Visible = true;

                progress.progressBar1.Value = 95;
                progress.progressBar1.Value = 100;
                progress.Close();

                dataManager.pausePushWorkers(); //wecke Worker wieder auf
            }
        }

        private void button9_Click(object sender, RibbonControlEventArgs e)
        {
            LogView logView = new LogView();
            logView.Show();
        }

        private void onLoadPause_Click(object sender, RibbonControlEventArgs e)
        {
            DataManager.getInstance().pausePushWorkers();

            if(onLoadPause.Label == "Pausiere Laden")
                onLoadPause.Label = "Weiter laden";
            else
                onLoadPause.Label = "Pausiere Laden";
        }

        private void onStopLoad_Click(object sender, RibbonControlEventArgs e)
        {
            DataManager.getInstance().stopPushWorkers();
        }

        private void button10_Click(object sender, RibbonControlEventArgs e)
        {
            MasterDataDialog mdDialog = new MasterDataDialog();
            mdDialog.ShowDialog();
        }
    }
}
 