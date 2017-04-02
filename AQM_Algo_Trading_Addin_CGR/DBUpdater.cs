using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    class DBUpdater : LiveConnectionSubscriber
    {
        private MySqlConnection mySQLConnection;
        private MySqlCommand mySQLCommand;
        private MySqlDataReader mySQLDataReader;
        private string stockID;

        public DBUpdater()
        {
            establishConnection();
        }

        public void updateMeWithNewData(StockDataTransferObject newRecord)
        {
            updateMasterData(newRecord);
            enrichTransactionData(newRecord);
        }

        private void openConnection()
        {
            mySQLConnection.Open();
        }

        private void closeConnection()
        {
            mySQLConnection.Close();
        }

        private void cleanUp()
        {
            closeConnection();
            openConnection();
        }

        private void establishConnection()
        {
            establishConnection("localhost", "3306", "root", "");
        }

        private void establishConnection(string datasource, string port, string username, string password)
        {
            string parameters = "datasource=" + datasource + ";port=" + port + ";username=" + username + ";password=" + password;
            mySQLConnection = new MySqlConnection(parameters);
        }

        public void updateMasterData(StockDataTransferObject record)
        {
            if (getStockID(record) == null)
                insertNewMasterData(record);
            else
                updateExistingMasterData(record);
        }

        private void updateExistingMasterData(StockDataTransferObject record)
        {
            //if abfrage, welcher wert gesetzt, diese spalte in query aufnehmen  - zunächst booleans für jeden wert setzen, dann diese für ifs nutzen 
            bool isinSet    = record.isin           != null;
            bool wknSet     = record.wkn            != null;
            bool symbolSet  = record.symbol         != null;
            bool nameSet    = record.name           != null;
            bool suffixSet  = record.suffix_onvista != null;
            bool sectorSet  = record.sector         != null;

            bool firstItem = true;

            string update_query =
                            "UPDATE " +
                            "aqm.tbl_stock_masterdata " +
                            "SET ";

            if (isinSet)
                update_query += "at_isin = " + record.isin;
            if (isinSet)
            {
                if (firstItem == false)
                    update_query += ", ";
                else
                    firstItem = false;

                update_query += "at_wkn = " + record.wkn;
            }
            if (isinSet)
            {
                if (firstItem == false)
                    update_query += ", ";
                else
                    firstItem = false;

                update_query += "at_symbol = " + record.symbol;
            }
            if (isinSet)
            {
                if (firstItem == false)
                    update_query += ", ";
                else
                    firstItem = false;

                update_query += "at_name = " + record.name;
            }
            if (isinSet)
            {
                if (firstItem == false)
                    update_query += ", ";
                else
                    firstItem = false;

                update_query += "at_suffix_onvista = " + record.suffix_onvista;
            }
            if (isinSet)
            {
                if (firstItem == false)
                    update_query += ", ";
                else
                    firstItem = false;

                update_query += "at_sector = " + record.sector;
            }

            update_query +=
                            " WHERE " +
                            "pk_id " +
                            "= " + 
                            getStockID(record);
            try
            {
                if (executeQuery(update_query) < 1)
                    throw new Exception("No rows affected by query: [" + update_query + "]");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void insertNewMasterData(StockDataTransferObject record)
        {
            string insert_query =
                            "INSERT INTO " +
                            "aqm.tbl_stock_masterdata " +
                            "(" +
                                "at_isin," +
                                "at_wkn," +
                                "at_symbol" +
                                "at_name" +
                                "at_suffix_onvista" +
                                "at_sector" +
                            ")" +
                            "VALUES" +
                            "('"
                                + record.isin
                                + "', '"
                                + record.wkn
                                + "', '"
                                + record.symbol
                                + "', '"
                                + record.name
                                + "', '"
                                + record.suffix_onvista
                                + "', '"
                                + record.sector + 
                            "')";
            try
            {
                if (executeQuery(insert_query) < 1)
                    throw new Exception("No rows affected by query: [" + insert_query + "]");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string getStockID(StockDataTransferObject record)
        {
            if (stockID == null)
            {
                stockID = getStockIDBySymbol(record);

                if (stockID != null)
                    return stockID;

                stockID = getStockIDByISIN(record);

                if (stockID != null)
                    return stockID;

                stockID = getStockIDByWKN(record);

                if (stockID != null)
                    return stockID;

                return null;
            }
            else
                return stockID;
        }

        private string getStockIDByWKN(StockDataTransferObject record)
        {
            string select_query =
                            "SELECT " +
                            "pk_id " +
                            "FROM " +
                            "aqm.tbl_stock_masterdata " +
                            "WHERE " +
                            "at_wkn " +
                            "= '" +
                            record.symbol +
                            "'";
            try
            {
                executeQuery(select_query);

                if (mySQLDataReader.HasRows)
                    return mySQLDataReader.GetString(0);
                else
                    return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private string getStockIDByISIN(StockDataTransferObject record)
        {
            string select_query =
                            "SELECT " +
                            "pk_id " +
                            "FROM " +
                            "aqm.tbl_stock_masterdata " +
                            "WHERE " +
                            "at_isin " +
                            "= '" +
                            record.symbol +
                            "'";
            try
            {
                executeQuery(select_query);

                if (mySQLDataReader.HasRows)
                    return mySQLDataReader.GetString(0);
                else
                    return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private string getStockIDBySymbol(StockDataTransferObject record)
        {
           string select_query =
                            "SELECT " +
                            "pk_id " +
                            "FROM " +
                            "aqm.tbl_stock_masterdata " +
                            "WHERE " +
                            "at_symbol " + 
                            "= '" + 
                            record.symbol +
                            "'";
            try
            {
                executeQuery(select_query);

                if (mySQLDataReader.HasRows)
                    return mySQLDataReader.GetString(0);
                else
                    return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public void enrichTransactionData(StockDataTransferObject record)
        {
            string insert_query =
                            "INSERT INTO " +
                            "aqm.tbl_stock_transactiondata " +
                            "(" +
                                "fk_stock_id," +
                                "at_price," +
                                "at_volume" +
                                "at_trend_abs" +
                                "at_trend_perc" +
                                "at_open" +
                                "at_close" +
                                "at_high" +
                                "at_low" +
                                "at_adj_close" +
                                "at_preday_close" +
                                "at_totalvolume" +
                                "at_timestamp_price" +
                                "at_timestamp_volume" +
                                "at_timestamp_otherdata" +
                                "at_provider" +
                                "at_trading_floor" +
                                "at_currency" +
                            ")" +
                            "VALUES" +
                            "('"
                                + getStockID(record)
                                + "', '"
                                + record.price
                                + "', '"
                                + record.volume
                                + "', '"
                                + record.trend_abs
                                + "', '"
                                + record.trend_perc
                                + "', '"
                                + record.open
                                + "', '"
                                + record.close
                                + "', '"
                                + record.high
                                + "', '"
                                + record.low
                                + "', '"
                                + record.adj_close
                                + "', '"
                                + record.preday_close
                                + "', '"
                                + record.total_volume
                                + "', '"
                                + record.timestamp_price
                                + "', '"
                                + record.timestamp_volume
                                + "', '"
                                + record.timestamp_otherdata
                                + "', '"
                                + record.provider
                                + "', '"
                                + record.trading_floor
                                + "', '"
                                + record.currency +
                            "')";
            try
            {
                if (executeQuery(insert_query) < 1)
                    throw new Exception("No rows affected by query: [" + insert_query + "]");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private int executeQuery(string query)
        {
            openConnection();
            createAndInvokeCommand(query);
            closeConnection();

            return mySQLDataReader.RecordsAffected;
        }

        private void createAndInvokeCommand(string query)
        {
            mySQLCommand = new MySqlCommand(query, mySQLConnection);
            mySQLDataReader = mySQLCommand.ExecuteReader();
        }
    }
}
