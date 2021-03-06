﻿using System;
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
            //implement singleton
            if (instance == null)
                instance = new DataManager();

            return instance;
        }

        private DataManager()
        {
            //i´m a singleton! i have no public constructor.
        }

        public void subscribeForLiveConnection(String symbol, LiveConnectionSubscriber subscriber, LiveConnectors variant)
        {
            findOrCreateWorker(symbol, subscriber, variant);
        }

        public List<StockDataTransferObject> getHistoricalStockData(string stockSymbol, DateTime dateFrom, DateTime dateTo, YahooFinanceAPI_Resolution resolution)
        {
            YahooFinanceAPIConnector yahooFinanceAPIConnector = new YahooFinanceAPIConnector();

            return yahooFinanceAPIConnector.getHistoricalStockData(stockSymbol, dateFrom, dateTo, resolution);
        }

        public List<StockDataTransferObject> getLocallySavedStockData(string stockSymbol, DateTime dateFrom, DateTime dateTo)
        {
            MySQLConnector mySQLConnector = new MySQLConnector();

            return mySQLConnector.getHistoricalStockData(stockSymbol, dateFrom, dateTo);
        }

        public List<int> getColumnsToDraw_forYahooHistoricalData()
        {
            List<int> columnsToDraw = new List<int>();

            columnsToDraw.Add(StockDataTransferObject.posTimestampOther);
            columnsToDraw.Add(StockDataTransferObject.posHigh);
            columnsToDraw.Add(StockDataTransferObject.posAdjClose);
            columnsToDraw.Add(StockDataTransferObject.posLow);
            columnsToDraw.Add(StockDataTransferObject.posClose);
            columnsToDraw.Add(StockDataTransferObject.posOpen);

            return columnsToDraw;
        }

        public List<int> getColumnsToDraw_LiveStockData()
        {
            List<int> columnsToDraw = StockDataTransferObject.getStandardColumnsToDraw();
            
            columnsToDraw.Remove(StockDataTransferObject.posAdjClose);
            columnsToDraw.Remove(StockDataTransferObject.posTimestampPrice);
            columnsToDraw.Remove(StockDataTransferObject.posTimestampVolume);
            columnsToDraw.Remove(StockDataTransferObject.posClose);

            return columnsToDraw;
        }

        private void findOrCreateWorker(String symbol, LiveConnectionSubscriber subscriber, LiveConnectors variant)
        {
            //search for worker which already loads the relevant data
            PushWorker worker = findWorker(symbol, variant);

            //create worker only in case of new symbol
            if(worker == null)
            {
                worker = new PushWorker(variant, symbol);                                 
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

        private PushWorker findWorker(string symbol, LiveConnectors variant)
        {
            //search for worker
            foreach (PushWorker worker in listOfPushWorkers)
            {
                if (worker.symbol == symbol && worker.variant == variant)
                {
                    //return found worker
                    return worker;
                }
            }

            //if never found, return null
            return null;
        }

        public void pausePushWorkers()
        {
            foreach (PushWorker worker in listOfPushWorkers)
            {
                worker.pauseWork();
            }
        }

        public void stopPushWorkers()
        {
            foreach (PushWorker worker in listOfPushWorkers)
            {
                worker.stopWork();
            }

            listOfPushWorkers.Clear();
            listOfDBUpdaters.Clear();
        }

        public List<string> getAvailableSymbols()
        {
            MySQLConnector mySQLConnector = new MySQLConnector();
            return mySQLConnector.getAvailableSymbols();
        }

        public List<int> getColumnsToDraw_Standard()
        {
            return StockDataTransferObject.getStandardColumnsToDraw();
        }
    }
}
