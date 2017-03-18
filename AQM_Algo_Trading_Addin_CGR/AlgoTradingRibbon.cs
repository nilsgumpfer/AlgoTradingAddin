﻿using System;
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

namespace AQM_Algo_Trading_Addin_CGR
{
    public delegate void UpdateListViewItem(Aktienwert aktie);
    public partial class AlgoTradingRibbon : Observer<Aktienwert>
    {
        private List<Aktienwert> aktuelleAktienwerte;
        private List<Thread> workerThreads;
        private List<BackgroundCrawler> workerObjects;
        private List<MySQLStockDataConnector> dbConnections;
        private string alteAktiensymbole;
        private string[] aktienSymbole = { "" };

        private Workbook wb;
        private Worksheet ws;
        private Excel.Range range;

        MySqlConnection mySQLVerbindung;
        MySqlCommand mySQLCommand;
        MySqlDataReader mySQLDataReader;

        /*public AlgoTradingRibbon()
        {

        }*/

            //test123karle
        private void AlgoTradingRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //InitializeComponent();
            aktuelleAktienwerte = new List<Aktienwert>();
            workerThreads = new List<Thread>();
            workerObjects = new List<BackgroundCrawler>();
            dbConnections = new List<MySQLStockDataConnector>();
            alteAktiensymbole = "";

            //Checkboxen deaktivieren
            CB_Quelle_Onvista.Enabled = false; 
            CB_Quelle_Yahoo.Enabled = false;
            CB_Quelle_Lokal.Enabled = false;
            CB_Ziel_AktuellesTB.Enabled = false;
            CB_Ziel_NeuesTB.Enabled = false;
            CB_Ziel_Cursor.Enabled = false;
            CB_Visualisierung_Diagramm.Enabled = false;
            CB_Visualisierung_Tabelle.Enabled = false;
            CB_Algo_Trend_Kurs.Enabled = false;
            CB_Algo_Trend_Volumen.Enabled = false;

            //Button deaktivieren
            BTN_Aktionen_Ausfuehren.Enabled = false;

        }



        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            String aktiensymbole_eingabe = Microsoft.VisualBasic.Interaction.InputBox("Bitte Aktienwerte mit Leerzeichen getrennt eingeben!", "Aktienwerte eingeben", "");

            aktienSymbole = null;
            aktienSymbole = aktiensymbole_eingabe.Split(' ');
            generiereAktienwerte();
            erstelleWorkerThreads();
            starteWorkerThreads();
        }

        //private void button1_Click(object sender, RibbonControlEventArgs e)
        //{
        //    String aktiensymbole_eingabe = Microsoft.VisualBasic.Interaction.InputBox("Bitte Aktienwerte mit Leerzeichen getrennt eingeben!", "Aktienwerte eingeben", "");

        //    aktienSymbole = null;
        //    aktienSymbole = aktiensymbole_eingabe.Split(' ');
        //    generiereAktienwerte();
        //    erstelleWorkerThreads();
        //    starteWorkerThreads();
        //}

        private void generiereAktienwerte()
        {
            aktuelleAktienwerte.Clear();

            foreach (var item in aktienSymbole)
            {
                Aktienwert aktienwert = new Aktienwert(item, false);
                aktuelleAktienwerte.Add(aktienwert);
            }
        }

        private void erstelleWorkerThreads()
        {
            workerObjects.Clear();
            workerThreads.Clear();

            foreach (var item in aktuelleAktienwerte)
            {
                MySQLStockDataConnector dbConnection = new MySQLStockDataConnector();
                BackgroundCrawler worker = new BackgroundCrawler(item);
                Thread thread = new Thread(worker.loadStockData);
                thread.Name = item.getAktienSymbol();
                dbConnections.Add(dbConnection);
                workerObjects.Add(worker);
                workerThreads.Add(thread);
                //worker.subscribe(this);
                worker.subscribe(dbConnection);
            }
        }

        private void starteWorkerThreads()
        {
            foreach (var item in workerThreads)
            {
                item.Start();
            }
        }

        private void stoppeWorkerThreads()
        {
            foreach (var item in workerObjects)
            {
                item.stopWork();
            }
        }
        public void notify(Observable<Aktienwert> caller)
        {
            Aktienwert aktie = caller.getMessage();

            foreach (var item in aktuelleAktienwerte)
            {
                if (item.getAktienSymbol() == aktie.getAktienSymbol())
                {
                    item.setAktienKurs(aktie.getAktienKurs());
                    //Control.Invoke(new UpdateListViewItem(updateListViewItem), new object[] { aktie });
                    break;
                }
            }
        }

        private void updateListViewItem(Aktienwert aktie)
        {
            bool found = false;
            int iAktienSymbol = 0;
            int iAktienName = 1;
            int iAktienKurs = 2;
            int iVolumen = 3;
            int iTimestampGehandelt = 4;
            int iHandelsPlatz = 5;
            int iProvider = 6;
            /*
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems[iAktienSymbol].Text == aktie.getAktienSymbol())
                {
                    found = true;
                    item.SubItems[iAktienSymbol].Text = aktie.getAktienSymbol();
                    item.SubItems[iAktienName].Text = aktie.getAktienName();
                    item.SubItems[iAktienKurs].Text = aktie.getAktienKurs();
                    item.SubItems[iVolumen].Text = aktie.getAktienVolumen();
                    item.SubItems[iTimestampGehandelt].Text = aktie.getTimestampGehandelt();
                    item.SubItems[iHandelsPlatz].Text = aktie.getHandelsPlatz();
                    item.SubItems[iProvider].Text = aktie.getProvider();
                    break;
                }
            }

            if (found == false)
                listView1.Items.Add(new ListViewItem(new string[]
                {
                    aktie.getAktienSymbol(),
                    aktie.getAktienName(),
                    aktie.getAktienKurs(),
                    aktie.getAktienVolumen(),
                    aktie.getTimestampGehandelt(),
                    aktie.getHandelsPlatz(),
                    aktie.getProvider()
                }));

            listView1.Update();*/
        }

      

        private void verbindungAufbauen(string datasource, string port, string username, string password)
        {
            string verbindungsParameter = "datasource=" + datasource + ";port=" + port + ";username=" + username + ";password=" + password;
            mySQLVerbindung = new MySqlConnection(verbindungsParameter);
        }

        private void CB_Typ_Live_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Typ_Live.Checked == true)
            {
                CB_Typ_Historisch.Enabled = false;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Onvista.Enabled = true;
                CB_Quelle_Yahoo.Enabled = true;
            }
            else
            {
                CB_Typ_Historisch.Enabled = true;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
                CB_Quelle_Yahoo.Enabled = false;
            }

        }

        private void CB_Ziel_NeuesTB_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_NeuesTB.Checked == true)
            {
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
            }
            else
            {
                CB_Ziel_AktuellesTB.Enabled = true;
                CB_Ziel_Cursor.Enabled = true;
            }
        }

        private void CB_Ziel_AktuellesTB_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_AktuellesTB.Checked == true)
            {
                CB_Ziel_NeuesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
            }
            else
            {
                CB_Ziel_NeuesTB.Enabled = true;
                CB_Ziel_Cursor.Enabled = true;
            }
        }

        private void CB_Typ_Historisch_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Typ_Historisch.Checked == true)
            {
                CB_Typ_Live.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
                CB_Quelle_Lokal.Enabled = true;
                CB_Quelle_Yahoo.Enabled = true;
            }
            else
            {
                CB_Typ_Live.Enabled = true;
                CB_Quelle_Onvista.Enabled = false;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Yahoo.Enabled = false;
            }
        }

        private void CB_Quelle_Lokal_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Lokal.Checked == true)
            {
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
            }
            else
            {
                CB_Quelle_Yahoo.Enabled = true;
                CB_Quelle_Onvista.Enabled = true;
            }
        }

        private void CB_Quelle_Onvista_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Onvista.Checked == true)
            {
                CB_Quelle_Yahoo.Enabled = false;
                CB_Quelle_Lokal.Enabled = false;
            }
            else
            {
                CB_Quelle_Yahoo.Enabled = true;
                CB_Quelle_Lokal.Enabled = true;
            }
        }

        private void CB_Quelle_Yahoo_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Quelle_Yahoo.Checked == true)
            {
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
            }
            else
            {
                CB_Quelle_Lokal.Enabled = true;
                CB_Quelle_Onvista.Enabled = true;
            }
        }

        private void CB_Ziel_Cursor_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_Cursor.Checked == true)
            {
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_NeuesTB.Enabled = false;
            }
            else
            {
                CB_Ziel_AktuellesTB.Enabled = true;
                CB_Ziel_NeuesTB.Enabled = true;
            }
        }

        private void CB_Visualisierung_Diagramm_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Visualisierung_Diagramm.Checked == true)
            {
                CB_Visualisierung_Tabelle.Enabled = false;
            }
            else
            {
                CB_Visualisierung_Tabelle.Enabled = true;
            }
        }

        private void CB_Visualisierung_Tabelle_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Visualisierung_Tabelle.Checked == true)
            {
                CB_Visualisierung_Diagramm.Enabled = false;
            }
            else
            {
                CB_Visualisierung_Diagramm.Enabled = true;
            }
        }

        private void CB_Algo_Trend_Kurs_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Algo_Trend_Kurs.Checked == true)
            {
                CB_Algo_Trend_Volumen.Enabled = false;
            }
            else
            {
                CB_Algo_Trend_Volumen.Enabled = true;
            }
        }

        private void CB_Algo_Trend_Volumen_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Algo_Trend_Volumen.Checked == true)
            {
                CB_Algo_Trend_Kurs.Enabled = false;
            }
            else
            {
                CB_Algo_Trend_Kurs.Enabled = true;
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {

            //wb = Globals.ThisAddIn.Application.ActiveWorkbook;
            ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);
            range = Globals.ThisAddIn.Application.Cells[0, 0];
            //range = Globals.ThisAddIn.Application.ActiveCell;


            string select_aktienwert_Query = "SELECT * FROM aqm.aktienwerte LIMIT 100";

            try
            {
                verbindungAufbauen("localhost", "3306", "root", "");
                mySQLVerbindung.Open();

                mySQLCommand = new MySqlCommand(select_aktienwert_Query, mySQLVerbindung);
                //mySQLDataReader = mySQLCommand.ExecuteReader();

                int i = 0;

                using (MySqlDataReader reader = mySQLCommand.ExecuteReader())
                {
                    if (reader != null)
                    {

                        while (reader.Read())
                        {
                            i++;
                            for (int j = 0; j < reader.FieldCount; j++)
                            {
                                if (i == 1) //Beim ersten Durchlauf Spaltenbezeichnungen setzen
                                {
                                    ws.Cells[i, j + 1] = reader.GetName(j);
                                }
                                else //bei den restlichen Durchläufen Datensätze spaltenweise ausgeben
                                {
                                    ws.Cells[i, j + 1] = reader[j].ToString();
                                }
                            }
                        }
                    }
                }

                mySQLVerbindung.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //ws = wb.ActiveSheet;
            //ws.Cells[1,1] = "Test";
            //ws.Cells[1,2] = "1234";
            //Excel.Range cells = ws.Range["A1", "D8"];
            //Chart chart = ws.Controls.AddChart(cells, "emplyees");
            //chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;
            //chart.SetSourceData(cells);
        }

        //Aktives Tabellenblatt
        private void BTN_Test_Click(object sender, RibbonControlEventArgs e)
        {
            ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);
            //range = Globals.ThisAddIn.Application.Cells[0, 0];
            
            int row = Globals.ThisAddIn.Application.ActiveCell.Row;
            int column = Globals.ThisAddIn.Application.ActiveCell.Column;


            ////Aktive Zelle
            //ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);
            //range = Globals.ThisAddIn.Application.ActiveCell;

            ////Neues Tabellenblatt
            //ws = Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add();
            //range = Globals.ThisAddIn.Application.Cells[0, 0];


            //writeTable(ws, range);


            //button2_Click(sender, e);

            string select_aktienwert_Query = "SELECT * FROM aqm.aktienwerte LIMIT 100";

            try
            {
                verbindungAufbauen("localhost", "3306", "root", "");
                mySQLVerbindung.Open();

                mySQLCommand = new MySqlCommand(select_aktienwert_Query, mySQLVerbindung);
                //mySQLDataReader = mySQLCommand.ExecuteReader();

                //int i = row  = 5;
                //int j = column = 5;

                int rowTemp = row; 

                using (MySqlDataReader reader = mySQLCommand.ExecuteReader())
                {
                    if (reader != null)
                    {

                        while (reader.Read())
                        {
                            for (int i = 0;  i < reader.FieldCount; i++)
                            {
                                if (rowTemp == row) //Beim ersten Durchlauf Spaltenbezeichnungen setzen
                                {
                                    ws.Cells[row, column+i] = reader.GetName(i);
                                }
                                else //bei den restlichen Durchläufen Datensätze spaltenweise ausgeben
                                {
                                    ws.Cells[row, column+i] = reader[i].ToString();
                                }
                            }
                            row++;
                        }
                    }
                }

                mySQLVerbindung.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void BTN_Test2_Click(object sender, RibbonControlEventArgs e)
        {
            ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);
  
            string select_aktienwert_Query = "SELECT * FROM aqm.aktienwerte LIMIT 100";

            try
            {
                verbindungAufbauen("localhost", "3306", "root", "");
                mySQLVerbindung.Open();

                mySQLCommand = new MySqlCommand(select_aktienwert_Query, mySQLVerbindung);

                int i = 0;

                using (MySqlDataReader reader = mySQLCommand.ExecuteReader())
                {
                    if (reader != null)
                    {

                        while (reader.Read())
                        {
                            i++;
                            for (int j = 0; j < reader.FieldCount; j++)
                            {
                                if (i == 1) //Beim ersten Durchlauf Spaltenbezeichnungen setzen
                                {
                                    ws.Cells[i, j + 1] = reader.GetName(j);
                                }
                                else //bei den restlichen Durchläufen Datensätze spaltenweise ausgeben
                                {
                                    ws.Cells[i, j + 1] = reader[j].ToString();
                                }
                            }
                        }
                    }
                }

                mySQLVerbindung.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}