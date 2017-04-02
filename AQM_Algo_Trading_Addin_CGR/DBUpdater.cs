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
                mySQLConnector.updateMasterData(newRecord);
                mySQLConnector.enrichTransactionData(newRecord);
            }
            catch (Exception e)
            {
                mySQLConnector.closeConnection();
                ExceptionHandler.handle(e);
            }
        }
    }
}
