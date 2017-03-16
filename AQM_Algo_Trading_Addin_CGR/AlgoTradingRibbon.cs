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


        private void AlgoTradingRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //InitializeComponent();
            aktuelleAktienwerte = new List<Aktienwert>();
            workerThreads = new List<Thread>();
            workerObjects = new List<BackgroundCrawler>();
            dbConnections = new List<MySQLStockDataConnector>();
            alteAktiensymbole = "";
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

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            //wb = Globals.ThisAddIn.Application.ActiveWorkbook;
            ws = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveWorkbook.ActiveSheet);

            range = Globals.ThisAddIn.Application.ActiveCell;


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
                                    ws.Cells[i, j+1] = reader[j].ToString();
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

        private void verbindungAufbauen(string datasource, string port, string username, string password)
        {
            string verbindungsParameter = "datasource=" + datasource + ";port=" + port + ";username=" + username + ";password=" + password;
            mySQLVerbindung = new MySqlConnection(verbindungsParameter);
        }
    }
}