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
        private MySQLConnector mySQLConnector;

        public DBUpdater()
        {
            mySQLConnector = new MySQLConnector();
        }

        public void updateMeWithNewData(StockDataTransferObject newRecord)
        {
            try
            {
                updateMasterData(newRecord);
                updateTransactionData(newRecord);
            }
            catch (Exception e)
            {
                mySQLConnector.closeConnection();
                ExceptionHandler.handle(e);
            }
        }

        public int updateMasterData(StockDataTransferObject newRecord)
        {
            return mySQLConnector.updateMasterData(newRecord);
        }

        public void updateTransactionData(StockDataTransferObject newRecord)
        {
            mySQLConnector.enrichTransactionData(newRecord);
        }
    }
}
