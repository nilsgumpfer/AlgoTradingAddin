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
    //public delegate void UpdateListViewItem(Aktienwert aktie);
    public partial class AlgoTradingRibbon //: Observer<Aktienwert>
    {
        public int selectedOptions = 0;
        //private List<Aktienwert> aktuelleAktienwerte;
        //private List<Thread> workerThreads;
        //private List<BackgroundCrawler> workerObjects;
        //private List<MySQLStockDataConnector> dbConnections;
        //private string alteAktiensymbole;
        //private string[] aktienSymbole = { "" };

        private Excel.Application app;
        private Workbook wb;
        private Worksheet ws;
        private Excel.Range range;
        private TableObject tableObject;

        MySqlConnection mySQLVerbindung;
        MySqlCommand mySQLCommand;
        MySqlDataReader mySQLDataReader;

        /*public AlgoTradingRibbon()
        {

        }*/

        //test123karle
        private void AlgoTradingRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            ////InitializeComponent();
            //aktuelleAktienwerte = new List<Aktienwert>();
            //workerThreads = new List<Thread>();
            //workerObjects = new List<BackgroundCrawler>();
            //dbConnections = new List<MySQLStockDataConnector>();
            //alteAktiensymbole = "";

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



        //private void button1_Click(object sender, RibbonControlEventArgs e)
        //{
        //    String aktiensymbole_eingabe = Microsoft.VisualBasic.Interaction.InputBox("Bitte Aktienwerte mit Leerzeichen getrennt eingeben!", "Aktienwerte eingeben", "");

        //    aktienSymbole = null;
        //    aktienSymbole = aktiensymbole_eingabe.Split(' ');
        //    generiereAktienwerte();
        //    erstelleWorkerThreads();
        //    starteWorkerThreads();
        //}

        ////private void button1_Click(object sender, RibbonControlEventArgs e)
        ////{
        ////    String aktiensymbole_eingabe = Microsoft.VisualBasic.Interaction.InputBox("Bitte Aktienwerte mit Leerzeichen getrennt eingeben!", "Aktienwerte eingeben", "");

        ////    aktienSymbole = null;
        ////    aktienSymbole = aktiensymbole_eingabe.Split(' ');
        ////    generiereAktienwerte();
        ////    erstelleWorkerThreads();
        ////    starteWorkerThreads();
        ////}

        //private void generiereAktienwerte()
        //{
        //    aktuelleAktienwerte.Clear();

        //    foreach (var item in aktienSymbole)
        //    {
        //        Aktienwert aktienwert = new Aktienwert(item, false);
        //        aktuelleAktienwerte.Add(aktienwert);
        //    }
        //}

        //private void erstelleWorkerThreads()
        //{
        //    workerObjects.Clear();
        //    workerThreads.Clear();

        //    foreach (var item in aktuelleAktienwerte)
        //    {
        //        MySQLStockDataConnector dbConnection = new MySQLStockDataConnector();
        //        BackgroundCrawler worker = new BackgroundCrawler(item);
        //        Thread thread = new Thread(worker.loadStockData);
        //        thread.Name = item.getAktienSymbol();
        //        dbConnections.Add(dbConnection);
        //        workerObjects.Add(worker);
        //        workerThreads.Add(thread);
        //        //worker.subscribe(this);
        //        worker.subscribe(dbConnection);
        //    }
        //}

        //private void starteWorkerThreads()
        //{
        //    foreach (var item in workerThreads)
        //    {
        //        item.Start();
        //    }
        //}

        //private void stoppeWorkerThreads()
        //{
        //    foreach (var item in workerObjects)
        //    {
        //        item.stopWork();
        //    }
        //}
        //public void notify(Observable<Aktienwert> caller)
        //{
        //    Aktienwert aktie = caller.getMessage();

        //    foreach (var item in aktuelleAktienwerte)
        //    {
        //        if (item.getAktienSymbol() == aktie.getAktienSymbol())
        //        {
        //            item.setAktienKurs(aktie.getAktienKurs());
        //            //Control.Invoke(new UpdateListViewItem(updateListViewItem), new object[] { aktie });
        //            break;
        //        }
        //    }
        //}

        //private void updateListViewItem(Aktienwert aktie)
        //{
        //    bool found = false;
        //    int iAktienSymbol = 0;
        //    int iAktienName = 1;
        //    int iAktienKurs = 2;
        //    int iVolumen = 3;
        //    int iTimestampGehandelt = 4;
        //    int iHandelsPlatz = 5;
        //    int iProvider = 6;
        //    /*
        //    foreach (ListViewItem item in listView1.Items)
        //    {
        //        if (item.SubItems[iAktienSymbol].Text == aktie.getAktienSymbol())
        //        {
        //            found = true;
        //            item.SubItems[iAktienSymbol].Text = aktie.getAktienSymbol();
        //            item.SubItems[iAktienName].Text = aktie.getAktienName();
        //            item.SubItems[iAktienKurs].Text = aktie.getAktienKurs();
        //            item.SubItems[iVolumen].Text = aktie.getAktienVolumen();
        //            item.SubItems[iTimestampGehandelt].Text = aktie.getTimestampGehandelt();
        //            item.SubItems[iHandelsPlatz].Text = aktie.getHandelsPlatz();
        //            item.SubItems[iProvider].Text = aktie.getProvider();
        //            break;
        //        }
        //    }

        //    if (found == false)
        //        listView1.Items.Add(new ListViewItem(new string[]
        //        {
        //            aktie.getAktienSymbol(),
        //            aktie.getAktienName(),
        //            aktie.getAktienKurs(),
        //            aktie.getAktienVolumen(),
        //            aktie.getTimestampGehandelt(),
        //            aktie.getHandelsPlatz(),
        //            aktie.getProvider()
        //        }));

        //    listView1.Update();*/
        //}



        private void verbindungAufbauen(string datasource, string port, string username, string password)
        {
            string verbindungsParameter = "datasource=" + datasource + ";port=" + port + ";username=" + username + ";password=" + password;
            mySQLVerbindung = new MySqlConnection(verbindungsParameter);
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
                if (CB_Quelle_Onvista.Checked == true)
                {
                    CB_Quelle_Yahoo.Enabled = false;
                }
                else
                {
                    CB_Quelle_Yahoo.Enabled = true;
                }
                if (CB_Quelle_Yahoo.Checked == true)
                {
                    CB_Quelle_Onvista.Enabled = false;
                }
                else
                {
                    CB_Quelle_Onvista.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;
            }

            else
            {
                CB_Typ_Historisch.Enabled = true;
                CB_Quelle_Lokal.Enabled = false;
                CB_Quelle_Onvista.Enabled = false;
                CB_Quelle_Yahoo.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }

        }

        private void CB_Ziel_NeuesTB_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_NeuesTB.Checked == true)
            {
                CB_Ziel_AktuellesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                if(CB_Visualisierung_Diagramm.Checked == true)
                {
                    CB_Visualisierung_Diagramm.Enabled = true;
                }
                else if(CB_Visualisierung_Tabelle.Checked == true)
                {
                    CB_Visualisierung_Tabelle.Enabled = true;
                }
                else
                {
                    CB_Visualisierung_Tabelle.Enabled = true;
                    CB_Visualisierung_Diagramm.Enabled = true;
                }
                
                selectedOptions = selectedOptions + 1;


            }
            else
            {
                CB_Ziel_AktuellesTB.Enabled = true;
                CB_Ziel_Cursor.Enabled = true;
                CB_Visualisierung_Tabelle.Enabled = false;
                CB_Visualisierung_Diagramm.Enabled = false;
                selectedOptions = selectedOptions - 1;


            }
        }

        private void CB_Ziel_AktuellesTB_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Ziel_AktuellesTB.Checked == true)
            {
                CB_Ziel_NeuesTB.Enabled = false;
                CB_Ziel_Cursor.Enabled = false;
                if (CB_Visualisierung_Diagramm.Checked == true)
                {
                    CB_Visualisierung_Diagramm.Enabled = true;
                }
                else if (CB_Visualisierung_Tabelle.Checked == true)
                {
                    CB_Visualisierung_Tabelle.Enabled = true;
                }
                else
                {
                    CB_Visualisierung_Tabelle.Enabled = true;
                    CB_Visualisierung_Diagramm.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;


            }
            else
            {
                CB_Ziel_NeuesTB.Enabled = true;
                CB_Ziel_Cursor.Enabled = true;
                CB_Visualisierung_Tabelle.Enabled = false;
                CB_Visualisierung_Diagramm.Enabled = false;
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
                }
                else if (CB_Ziel_NeuesTB.Checked == true)
                {
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = false;
                    CB_Ziel_AktuellesTB.Enabled = false;
                }
                else if (CB_Ziel_Cursor.Checked == true)
                {
                    CB_Ziel_Cursor.Enabled = true;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    CB_Ziel_NeuesTB.Enabled = false;
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
                //CB_Quelle_Onvista.Enabled = true;
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
                if (CB_Ziel_AktuellesTB.Checked == true)
                {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = false;
                    CB_Ziel_Cursor.Enabled = false;
                }
                else if (CB_Ziel_NeuesTB.Checked == true)
                {
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = false;
                    CB_Ziel_AktuellesTB.Enabled = false;
                }
                else if (CB_Ziel_Cursor.Checked == true)
                {
                    CB_Ziel_Cursor.Enabled = true;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    CB_Ziel_NeuesTB.Enabled = false;
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
                if (CB_Typ_Historisch.Checked == true)
                {
                    CB_Quelle_Lokal.Enabled = true;
                }
                //CB_Quelle_Lokal.Enabled = true;
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
                if (CB_Ziel_AktuellesTB.Checked == true)
                {
                    CB_Ziel_AktuellesTB.Enabled = true;
                    CB_Ziel_NeuesTB.Enabled = false;
                    CB_Ziel_Cursor.Enabled = false;
                }
                else if (CB_Ziel_NeuesTB.Checked == true)
                {
                    CB_Ziel_NeuesTB.Enabled = true;
                    CB_Ziel_Cursor.Enabled = false;
                    CB_Ziel_AktuellesTB.Enabled = false;
                }
                else if (CB_Ziel_Cursor.Checked == true)
                {
                    CB_Ziel_Cursor.Enabled = true;
                    CB_Ziel_AktuellesTB.Enabled = false;
                    CB_Ziel_NeuesTB.Enabled = false;
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
                if (CB_Visualisierung_Diagramm.Checked == true)
                {
                    CB_Visualisierung_Diagramm.Enabled = true;
                }
                else if (CB_Visualisierung_Tabelle.Checked == true)
                {
                    CB_Visualisierung_Tabelle.Enabled = true;
                }
                else
                {
                    CB_Visualisierung_Tabelle.Enabled = true;
                    CB_Visualisierung_Diagramm.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;


            }
            else
            {
                CB_Ziel_AktuellesTB.Enabled = true;
                CB_Ziel_NeuesTB.Enabled = true;
                CB_Visualisierung_Tabelle.Enabled = false;
                CB_Visualisierung_Diagramm.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Visualisierung_Diagramm_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Visualisierung_Diagramm.Checked == true)
            {
                CB_Visualisierung_Tabelle.Enabled = false;
                if (CB_Algo_Trend_Kurs.Checked == true)
                {
                    CB_Algo_Trend_Kurs.Enabled = true;
                }
                else if (CB_Algo_Trend_Volumen.Checked == true)
                {
                    CB_Algo_Trend_Volumen.Enabled = true;
                }
                else
                {
                    CB_Algo_Trend_Kurs.Enabled = true;
                    CB_Algo_Trend_Volumen.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Visualisierung_Tabelle.Enabled = true;
                CB_Algo_Trend_Kurs.Enabled = false;
                CB_Algo_Trend_Volumen.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Visualisierung_Tabelle_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Visualisierung_Tabelle.Checked == true)
            {
                CB_Visualisierung_Diagramm.Enabled = false;
                if (CB_Algo_Trend_Kurs.Checked == true)
                {
                    CB_Algo_Trend_Kurs.Enabled = true;
                }
                else if (CB_Algo_Trend_Volumen.Checked == true)
                {
                    CB_Algo_Trend_Volumen.Enabled = true;
                }
                else
                {
                    CB_Algo_Trend_Kurs.Enabled = true;
                    CB_Algo_Trend_Volumen.Enabled = true;
                }
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Visualisierung_Diagramm.Enabled = true;
                CB_Algo_Trend_Kurs.Enabled = false;
                CB_Algo_Trend_Volumen.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Algo_Trend_Kurs_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Algo_Trend_Kurs.Checked == true)
            {
                CB_Algo_Trend_Volumen.Enabled = false;
                BTN_Aktionen_Ausfuehren.Enabled = true;
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Algo_Trend_Volumen.Enabled = true;
                BTN_Aktionen_Ausfuehren.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void CB_Algo_Trend_Volumen_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Algo_Trend_Volumen.Checked == true)
            {
                CB_Algo_Trend_Kurs.Enabled = false;
                BTN_Aktionen_Ausfuehren.Enabled = true;
                selectedOptions = selectedOptions + 1;
            }
            else
            {
                CB_Algo_Trend_Kurs.Enabled = true;
                BTN_Aktionen_Ausfuehren.Enabled = false;
                selectedOptions = selectedOptions - 1;
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            if (CB_Algo_Trend_Kurs.Enabled == false & CB_Algo_Trend_Volumen.Enabled == false)
            {
                BTN_Aktionen_Ausfuehren.Enabled = false;
            }
            if (selectedOptions == 5)
            {
                //DataManager erzeugen
            }

            //Aktives Tabellenblatt}
            else {
                MessageBox.Show("Es sind nicht alle nötigen Optionen ausgefüllt! Bitte Auswahl überprüfen!");
                return;

            }
        }

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

        private void BTN_Test3_Click(object sender, RibbonControlEventArgs e)
        {
            wb = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook);

            String aktiensymbole_eingabe = Microsoft.VisualBasic.Interaction.InputBox("Bitte Tabellenblattnamen eingeben!", "Tabellenblattname eingeben", "");
 
            foreach (Excel.Worksheet sheet in wb.Worksheets)
            {
                if (sheet.Name.ToString() == aktiensymbole_eingabe)
                {
                    MessageBox.Show("Tabellenblattname bereits vorhanden! Bitte erneut ausführen!");
                    return;
                }
            }

            ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add());
            ws.Name = aktiensymbole_eingabe;

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

        private void TableObjektTest_Click(object sender, RibbonControlEventArgs e)
        {
            //List<string> headline = new List<string>();
            //headline.Add("Wurscht");
            //headline.Add("Stulle");

            //List<List<string>> content = new List<List<string>>();

            //List<string> line1 = new List<string>();
            //line1.Add("Mortadella");
            //line1.Add("Graubrot");

            //List<string> line2 = new List<string>();
            //line2.Add("Mettwurscht");
            //line2.Add("Saatenbrot");

            List<string> headline = new List<string>();
            headline.Add("Obst");
            headline.Add("Anzahl");

            List<List<string>> content = new List<List<string>>();

            List<string> line1 = new List<string>();
            line1.Add("Banane");
            line1.Add("5");

            List<string> line2 = new List<string>();
            line2.Add("Apfel");
            line2.Add("3");

            List<string> line3 = new List<string>();
            line3.Add("Orange");
            line3.Add("12");

            List<string> line4 = new List<string>();
            line4.Add("Mango");
            line4.Add("1");

            List<string> line5 = new List<string>();
            line5.Add("Gurke");
            line5.Add("7");

            List<string> line6 = new List<string>();
            line6.Add("Avocado");
            line6.Add("5");

            content.Add(line1);
            content.Add(line2);
            content.Add(line3);
            content.Add(line4);
            content.Add(line5);
            content.Add(line6);

            List<int> columns = new List<int>();
            columns.Add(1);
            columns.Add(2);

            tableObject = new TableObject(
                                Globals.Factory.GetVstoObject(
                                Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                Globals.ThisAddIn.Application.Cells, 
                                headline, 
                                content,
                                columns);
            tableObject.drawOnlyRelevantColumns();
            //tableObject.deleteDraw();
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            //tableObject.deleteDraw();
            /*tableObject.drawAtPosition(Globals.Factory.GetVstoObject(
                                            Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                       Globals.ThisAddIn.Application.ActiveCell);*/
            tableObject.deleteOnlyRelevantColumns();
            tableObject.drawRelevantColumnsAtPosition(Globals.Factory.GetVstoObject(
                                                            Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                                       Globals.ThisAddIn.Application.ActiveCell);
        }

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            DataManager dataManager = DataManager.getInstance();

            TableObject myTable = new TableObject(
                                Globals.Factory.GetVstoObject(
                                    Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                Globals.ThisAddIn.Application.ActiveCell);

            dataManager.subscribeForLiveConnection("",myTable);
        }

        private void button5_Click(object sender, RibbonControlEventArgs e)
        {
            Konfigurator view = new Konfigurator();
            view.ShowDialog();

            ProgressIndicator progress = new ProgressIndicator();
            progress.progressBar1.Maximum = 100;
            progress.progressBar1.Minimum = 0;
            progress.progressBar1.Value = 20;
            progress.Show();
            progress.progressBar1.Value = 30;

            if (view.hasBeenCancelled == false)
            {
                DataManager dataManager = DataManager.getInstance();
                
                progress.progressBar1.Value = 40;

                TableObject myTable = new TableObject(
                                        Globals.Factory.GetVstoObject
                                        (
                                            Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet
                                        ),
                                        Globals.ThisAddIn.Application.ActiveCell,
                                        /*dataManager.getHistoricalStockData
                                        (
                                            view.comboBox1.SelectedItem.ToString(),
                                            view.dateTimePicker1.Value, 
                                            view.dateTimePicker2.Value, 
                                            YahooFinanceAPI_Resolution.Daily
                                        ),*/
                                        dataManager.getLocallySavedStockData
                                        (
                                            view.comboBox1.SelectedItem.ToString(),
                                            view.dateTimePicker1.Value,
                                            view.dateTimePicker2.Value
                                        ),
                                        dataManager.getColumnsToDraw_forHistoricalStockData()
                                      );

                progress.progressBar1.Value = 80;

                myTable.drawOnlyRelevantColumns();

                progress.progressBar1.Value = 100;
                progress.Close();
            }            
        }

        private void button6_Click(object sender, RibbonControlEventArgs e)
        {
            List<string> headline = new List<string>();
            headline.Add("Obst");
            headline.Add("Anzahl");

            List<List<string>> content = new List<List<string>>();

            List<string> line1 = new List<string>();
            line1.Add("Banane");
            line1.Add("5");

            List<string> line2 = new List<string>();
            line2.Add("Apfel");
            line2.Add("3");

            List<string> line3 = new List<string>();
            line3.Add("Orange");
            line3.Add("12");

            List<string> line4 = new List<string>();
            line4.Add("Mango");
            line4.Add("1");

            List<string> line5 = new List<string>();
            line5.Add("Gurke");
            line5.Add("7");

            List<string> line6 = new List<string>();
            line6.Add("Avocado");
            line6.Add("5");

            content.Add(line1);
            content.Add(line2);
            content.Add(line3);
            content.Add(line4);
            content.Add(line5);
            content.Add(line6);

            List<int> columns = new List<int>();
            columns.Add(1);
            columns.Add(2);

            tableObject = new TableObject(
                                Globals.Factory.GetVstoObject(
                                    Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                Globals.ThisAddIn.Application.Cells[1, 1],
                                headline,
                                content,
                                columns);
            tableObject.drawOnlyRelevantColumns();

            ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);

            object misValue = System.Reflection.Missing.Value;   
        
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)ws.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            //TODO: Location des Diagramms setzen!

            //myChart.TopLeftCell.Cells[1, 3];




            //Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(range(ws.Cells[1, columns.Count+1], ws.Cells[, 1]
            Excel.Chart chartPage = myChart.Chart;

            Excel.Range chartRange;
            chartRange = ws.get_Range(ws.Cells[1, 1], ws.Cells[content.Count+1, columns.Count]);
            chartPage.SetSourceData(chartRange, misValue);

            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
        }

        private void button7_Click(object sender, RibbonControlEventArgs e)
        {
            ////Historische Daten auswählen
            //Konfigurator view = new Konfigurator();
            //view.ShowDialog();

            //ProgressIndicator progress = new ProgressIndicator();
            //progress.progressBar1.Maximum = 100;
            //progress.progressBar1.Minimum = 0;
            //progress.progressBar1.Value = 20;
            //progress.Show();
            //progress.progressBar1.Value = 30;

            //if (view.hasBeenCancelled == false)
            //{
                DataManager dataManager = DataManager.getInstance();

                //progress.progressBar1.Value = 40;

                //TableObject historicalDataTable = new TableObject(
                //                        Globals.Factory.GetVstoObject
                //                        (
                //                            Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet
                //                        ),
                //                        Globals.ThisAddIn.Application.Cells[1, 1],
                //                        dataManager.getHistoricalStockData
                //                        (
                //                            view.comboBox1.SelectedItem.ToString(),
                //                            view.dateTimePicker1.Value,
                //                            view.dateTimePicker2.Value,
                //                            YahooFinanceAPI_Resolution.Daily
                //                        ),
                //                        dataManager.getColumnsToDraw_forHistoricalStockData()
                //                      );

                //historicalDataTable.createNewWorksheet("Historische Daten");

                //progress.progressBar1.Value = 80;

                //historicalDataTable.drawOnlyRelevantColumns();

                //progress.progressBar1.Value = 100;
                //progress.Close();

                //Livedaten
                TableObject liveDataTable = new TableObject(
                                Globals.Factory.GetVstoObject(
                                    Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                Globals.ThisAddIn.Application.Cells[1, 1]);

                liveDataTable.changeWorkbookName("OnVista-Livedaten");
            dataManager.subscribeForLiveConnection("", liveDataTable);

            //Thread.Sleep(2000);

            DiagramObject myDiagram = new DiagramObject(liveDataTable);

            //}
        }

        private void button8_Click(object sender, RibbonControlEventArgs e)
        {
            DataManager dataManager = DataManager.getInstance();

            TableObject myTable = new TableObject(
                                Globals.Factory.GetVstoObject(
                                    Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet),
                                Globals.ThisAddIn.Application.ActiveCell);

            dataManager.subscribeForLiveConnection("", myTable);
            Algo algorithmus = new Algo(Globals.ThisAddIn.ac);
            Globals.ThisAddIn.SharePane.Visible = true;

        }

        private void button9_Click(object sender, RibbonControlEventArgs e)
        {
            OnVistaDummyConnector dummy = new OnVistaDummyConnector("");

            while (true)
            {
                MessageBox.Show(dummy.getRandomPrice(85, 0.5, 0.5));
            }
        }

        
    }
}
 