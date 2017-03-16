using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace RealtimeStockDataCr4wl3r
{
    class MySQLStockDataConnector : Observer<Aktienwert>
    {
        private MySqlConnection mySQLVerbindung;
        private MySqlCommand    mySQLCommand;
        private MySqlDataReader mySQLDataReader;

        private void verbindungAufbauen(string datasource, string port, string username, string password)
        {
            string verbindungsParameter = "datasource=" + datasource + ";port=" + port + ";username=" + username + ";password=" + password;
            mySQLVerbindung = new MySqlConnection(verbindungsParameter);  
        }

        public void persistiereAktienwert(Aktienwert aktie)
        {
            string select_aktienwert_Query = "SELECT aktienid FROM aqm.aktienwerte WHERE timestamp='"
                            + aktie.getTimestampGehandelt()
                            + "' AND aktienid='"
                            + aktie.getAktienSymbol()
                            + "'";
            string select_volumen_Query = "SELECT volumen, timestamp, aktienid FROM aqm.aktienvolumen WHERE timestamp='"
                            + aktie.getTimestampVolumen()
                            + "' AND aktienid='"
                            + aktie.getAktienSymbol()
                            + "'";

            string insert_aktienwert_Query = "INSERT INTO aqm.aktienwerte(timestamp,aktienid,aktienwert) VALUES('"
                            + aktie.getTimestampGehandelt()
                            + "', '"
                            + aktie.getAktienSymbol() 
                            + "', '" 
                            + aktie.getAktienKurs() 
                            + "')";

            string insert_Volumen_Query = "INSERT INTO aqm.aktienvolumen(volumen,aktienid,timestamp) VALUES('"
                            + aktie.getAktienVolumen()
                            + "', '"
                            + aktie.getAktienSymbol()
                            + "', '"
                            + aktie.getTimestampVolumen()
                            + "')";

            try
            { 
                verbindungAufbauen("localhost", "3306", "root", "");
                mySQLVerbindung.Open();

                mySQLCommand    = new MySqlCommand(select_aktienwert_Query, mySQLVerbindung);
                mySQLDataReader = mySQLCommand.ExecuteReader();

                if (mySQLDataReader.HasRows == false)
                {
                    mySQLVerbindung.Close();
                    mySQLVerbindung.Open();

                    mySQLCommand    = new MySqlCommand(insert_aktienwert_Query, mySQLVerbindung);
                    mySQLDataReader = mySQLCommand.ExecuteReader();

                    mySQLVerbindung.Close();
                }

                mySQLVerbindung.Close();

                mySQLVerbindung.Open();
                mySQLCommand = new MySqlCommand(select_volumen_Query, mySQLVerbindung);
                mySQLDataReader = mySQLCommand.ExecuteReader();

                if (mySQLDataReader.HasRows == false)
                {
                    mySQLVerbindung.Close();
                    mySQLVerbindung.Open();

                    mySQLCommand = new MySqlCommand(insert_Volumen_Query, mySQLVerbindung);
                    mySQLDataReader = mySQLCommand.ExecuteReader();

                    mySQLVerbindung.Close();
                }

                mySQLVerbindung.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void notify(Observable<Aktienwert> caller)
        {
            persistiereAktienwert(caller.getMessage());
        }
    }
}
