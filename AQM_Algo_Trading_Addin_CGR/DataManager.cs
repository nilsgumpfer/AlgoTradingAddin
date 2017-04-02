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
        private List<DBUpdater> listOfDBUpdaters = new List<DBUpdater>();
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

        public List<StockDataTransferObject> getHistoricalStockData(string stockSymbol, DateTime dateFrom, DateTime dateTo, YahooFinanceAPI_Resolution resolution)
        {
            YahooFinanceAPIConnector yahooFinanceAPIConnector = new YahooFinanceAPIConnector();

            return yahooFinanceAPIConnector.getHistoricalStockData(stockSymbol, dateFrom, dateTo, resolution);
        }

        public List<int> getColumnsToDraw_forHistoricalStockData()
        {
            List<int> columnsToDraw = new List<int>();

            columnsToDraw.Add(StockDataTransferObject.posHigh);
            columnsToDraw.Add(StockDataTransferObject.posTotalVolume);
            columnsToDraw.Add(StockDataTransferObject.posAdjClose);
            columnsToDraw.Add(StockDataTransferObject.posLow);
            columnsToDraw.Add(StockDataTransferObject.posClose);
            columnsToDraw.Add(StockDataTransferObject.posOpen);

            return columnsToDraw;
        }

        private void findOrCreateWorker(String symbol, LiveConnectionSubscriber subscriber)
        {
            //search for worker which already loads the relevant data
            PushWorker worker = findWorker(symbol);

            //create worker only in case of new symbol
            if(worker == null)
            {
                //**********************************************!!!TESTING!!!*****************************************************
                //****************************************************************************************************************

                worker = new PushWorker(LiveConnectors.OnVistaDummy, symbol); //TODO: This is just for development and test-usage!
                //worker = new PushWorker(LiveConnectors.OnVista, symbol);

                //****************************************************************************************************************
                //**********************************************!!!TESTING!!!*****************************************************

                DBUpdater dbUpdater = new DBUpdater();

                //stash objects for later use
                listOfPushWorkers.Add(worker);
                listOfDBUpdaters.Add(dbUpdater);

                //subscribe dedicated mySQL-Connector to keep DB up-to-date
                worker.subscribe(dbUpdater);
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
