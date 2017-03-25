using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class DataManager 
    {
        private List<PushWorker> listOfPushWorkers = new List<PushWorker>();
        private List<MySQLConnector> listOfMySQLConnectors = new List<MySQLConnector>();
        private static DataManager instance;

        public static DataManager getInstance()
        {
            //implements singleton
            if (instance == null)
                instance = new DataManager();

            return instance;
        }

        private DataManager()
        {
            //i´m a singleton! i have no public constructor.
        }

        public void subscribeForLiveConnection(String symbol, LiveConnectionSubscriber subscriber)
        {
            findOrCreateWorker(symbol, subscriber);
        }

        private void findOrCreateWorker(String symbol, LiveConnectionSubscriber subscriber)
        {
            //search for worker which already loads the relevant data
            PushWorker worker = findWorker(symbol);

            //create worker only in case of new symbol
            if(worker == null)
            {
                worker = new PushWorker(PushConnectors.OnVista, symbol);
                MySQLConnector  mySQLConnector = new MySQLConnector();

                //stash objects for later use
                listOfPushWorkers.Add(worker);
                listOfMySQLConnectors.Add(mySQLConnector);

                //subscribe dedicated mySQL-Connector to keep DB up-to-date
                worker.subscribe(mySQLConnector);
                //initialize worker, tell him to load
                worker.startWork();
            }

            //subscribe interested Object (e.g. table, chart, etc.)
            worker.subscribe(subscriber);
        }

        private PushWorker findWorker(String symbol)
        {
            //search for worker
            foreach (PushWorker worker in listOfPushWorkers)
            {
                if (worker.symbol == symbol)
                {
                    //return found worker
                    return worker;
                }
            }

            //if never found, return null
            return null;
        }
    }
}
