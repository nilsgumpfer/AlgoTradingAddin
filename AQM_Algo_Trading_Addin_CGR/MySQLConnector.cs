using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    class MySQLConnector
    {
        private MySqlConnection mySQLConnection;
        private MySqlCommand mySQLCommand;
        private MySqlDataReader mySQLDataReader;
        private string stockID;

        public MySQLConnector()
        {
            establishConnection();
        }

        public void openConnection()
        {
            mySQLConnection.Open();
        }

        public void closeConnection()
        {
            mySQLConnection.Close();
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

        private int updateExistingMasterData(StockDataTransferObject record)
        {
            bool isinSet = record.isin != null;
            bool wknSet = record.wkn != null;
            bool symbolSet = record.symbol != null;
            bool nameSet = record.name != null;
            bool suffixSet = record.suffix_onvista != null;
            bool sectorSet = record.sector != null;

            bool firstItem = true;

            string query =
                            "UPDATE " +
                            "aqm.tbl_stock_masterdata " +
                            "SET ";

            if (isinSet)
            {
                if (firstItem)
                    firstItem = false;

                query += "at_isin = '" + record.isin + "'";
            }

            if (wknSet)
            {
                if (firstItem == false)
                    query += ", ";
                else
                    firstItem = false;

                query += "at_wkn = '" + record.wkn + "'";
            }
            if (symbolSet)
            {
                if (firstItem == false)
                    query += ", ";
                else
                    firstItem = false;

                query += "at_symbol = '" + record.symbol + "'";
            }
            if (nameSet)
            {
                if (firstItem == false)
                    query += ", ";
                else
                    firstItem = false;

                query += "at_name = '" + record.name + "'";
            }
            if (suffixSet)
            {
                if (firstItem == false)
                    query += ", ";
                else
                    firstItem = false;

                query += "at_suffix_onvista = '" + record.suffix_onvista + "'";
            }
            if (sectorSet)
            {
                if (firstItem == false)
                    query += ", ";
                else
                    firstItem = false;

                query += "at_sector = '" + record.sector + "'";
            }

            query +=
                            " WHERE " +
                            "pk_id " +
                            "= '" +
                            getStockID(record) +
                            "'";

            return executeQueryStandalone(query);
        }

        public List<StockDataTransferObject> getHistoricalStockData(string stockSymbol, DateTime dateFrom, DateTime dateTo)
        {
            List<StockDataTransferObject> result = new List<StockDataTransferObject>();

            string from = dateFrom.ToString("yyyy-MM-dd HH:mm:ss");
            string to = dateTo.ToString("yyyy-MM-dd HH:mm:ss");

            string query = "SELECT " +
                                "at_price, " +
                                "at_volume, " +
                                "at_trend_abs, " +
                                "at_trend_perc, " +
                                "at_open, " +
                                "at_close, " +
                                "at_high, " +
                                "at_low, " +
                                "at_adj_close, " +
                                "at_preday_close, " +
                                "at_totalvolume, " +
                                "at_timestamp_price, " +
                                "at_timestamp_volume, " +
                                "at_timestamp_otherdata, " +
                                "at_provider, " +
                                "at_trading_floor, " +
                                "at_currency " +
                            "FROM " +
                                "aqm.tbl_stock_transactiondata " +
                            "WHERE " +
                                "fk_stock_id " +
                                "= '" +
                                getStockIDBySymbol(stockSymbol) +
                                "' " + 
                                "AND " +
                                "at_timestamp_otherdata " +
                                ">= '" +
                                from +
                                "' AND " +
                                "at_timestamp_otherdata " +
                                "<= '" +
                                to +
                                "'";
            try
            {
                openConnection();
                executeQuery(query);

                while(mySQLDataReader.Read())
                {
                    StockDataTransferObject record = new StockDataTransferObject();

                    record.price                = mySQLDataReader.GetString("at_price");
                    record.volume               = mySQLDataReader.GetString("at_volume");
                    record.trend_abs            = mySQLDataReader.GetString("at_trend_abs");
                    record.trend_perc           = mySQLDataReader.GetString("at_trend_perc");
                    record.open                 = mySQLDataReader.GetString("at_open");
                    record.close                = mySQLDataReader.GetString("at_close");
                    record.high                 = mySQLDataReader.GetString("at_high");
                    record.low                  = mySQLDataReader.GetString("at_low");
                    record.adj_close            = mySQLDataReader.GetString("at_adj_close");
                    record.preday_close         = mySQLDataReader.GetString("at_preday_close");
                    record.total_volume         = mySQLDataReader.GetString("at_totalvolume");
                    record.timestamp_price      = mySQLDataReader.GetString("at_timestamp_price");
                    record.timestamp_volume     = mySQLDataReader.GetString("at_timestamp_volume");
                    record.timestamp_otherdata  = mySQLDataReader.GetString("at_timestamp_otherdata");
                    record.provider             = mySQLDataReader.GetString("at_provider");
                    record.trading_floor        = mySQLDataReader.GetString("at_trading_floor");
                    record.currency             = mySQLDataReader.GetString("at_currency");

                    result.Add(record);
                }

                closeConnection();

                return result;
            }
            catch(Exception e)
            {
                ExceptionHandler.handle(e);
                return result;
            }

        }

        private int insertNewMasterData(StockDataTransferObject record)
        {
            string query =
                            "INSERT INTO " +
                            "aqm.tbl_stock_masterdata " +
                            "(" +
                                "at_isin, " +
                                "at_wkn, " +
                                "at_symbol, " +
                                "at_name, " +
                                "at_suffix_onvista, " +
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

            return executeQueryStandalone(query);
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
            return getStockIDByX("at_wkn", record.wkn);
        }

        private string getStockIDByISIN(StockDataTransferObject record)
        {
            return getStockIDByX("at_isin", record.isin);
        }

        private string getStockIDBySymbol(StockDataTransferObject record)
        {
            return getStockIDBySymbol(record.symbol);
        }

        private string getStockIDBySymbol(string symbol)
        {
            return getStockIDByX("at_symbol", symbol);
        }

        private string getStockIDByX(string column, string parameter)
        {
            string result = null;
            string query =
                             "SELECT " +
                             "pk_id " +
                             "FROM " +
                             "aqm.tbl_stock_masterdata " +
                             "WHERE " +
                             column +
                             "= '" +
                             parameter +
                             "'";

            openConnection();
            executeQuery(query);
            if (mySQLDataReader.Read())
                result = mySQLDataReader.GetString(0);
            closeConnection();

            return result;
        }

        public int enrichTransactionData(StockDataTransferObject record)
        {
            string query =
                            "INSERT INTO " +
                            "aqm.tbl_stock_transactiondata " +
                            "(" +
                                "fk_stock_id, " +
                                "at_price, " +
                                "at_volume, " +
                                "at_trend_abs, " +
                                "at_trend_perc, " +
                                "at_open, " +
                                "at_close, " +
                                "at_high, " +
                                "at_low, " +
                                "at_adj_close, " +
                                "at_preday_close, " +
                                "at_totalvolume, " +
                                "at_timestamp_price, " +
                                "at_timestamp_volume, " +
                                "at_timestamp_otherdata, " +
                                "at_provider, " +
                                "at_trading_floor, " +
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

            return executeQueryStandalone(query);
        }

        private int executeQuery(string query)
        {
            createAndInvokeCommand(query);
            return mySQLDataReader.RecordsAffected;
        }
        private int executeQueryStandalone(string query)
        {
            openConnection();
            int affected = executeQuery(query);
            closeConnection();
            return affected;
        }

        private void createAndInvokeCommand(string query)
        {
            mySQLCommand = new MySqlCommand(query, mySQLConnection);
            mySQLDataReader = mySQLCommand.ExecuteReader();
        }
    }
}
