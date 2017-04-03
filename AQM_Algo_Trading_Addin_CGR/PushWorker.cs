using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class PushWorker
    {
        private LiveConnector liveConnector;
        private List<LiveConnectionSubscriber> listOfSubscribers = new List<LiveConnectionSubscriber>();
        public String symbol { get; }
        private bool doThreading = false;
        private Thread thread;
        private bool doPause = false;
        public LiveConnectors variant { get; }
        private int debugCount = 0;

        private PushWorker()
        {
            //no public constructor without parameters available for this class!
        }

        public PushWorker(LiveConnectors variant, String symbol)
        {
            this.symbol = symbol;
            this.variant = variant;

            switch(variant)
            {
                case LiveConnectors.OnVista:
                    liveConnector = new OnVistaConnector(symbol);
                    break;

                case LiveConnectors.OnVistaDummy:
                    liveConnector = new OnVistaDummyConnector(symbol);
                    break;
            }
        }

        public void subscribe(LiveConnectionSubscriber subscriber)
        {
            listOfSubscribers.Add(subscriber);
        }

        public void unsubscribe(LiveConnectionSubscriber subscriber)
        {
            listOfSubscribers.Remove(subscriber);
        }

        private void doWork()
        {
            while (doThreading)
            {
                while (doPause)
                    Thread.Sleep(1000);

                StockDataTransferObject sdtObject = liveConnector.getStockData();

                if (liveConnector.checkChange())
                    updateSubscribers(sdtObject);
            }
        }

        public void stopWork()
        {
            doThreading = false;
            thread.Abort();
        }

        public void startWork()
        {
            if (doThreading == false)
            {
                doThreading = true;

                //create and start thread for doWork()
                thread = new Thread(this.doWork);
                thread.Name = "PushWorker (" + symbol + ")";
                thread.Start();
                Logger.log("Started PushWorker (" + symbol + ")");
            }
        }

        private void updateSubscribers(StockDataTransferObject record)
        {
            foreach(LiveConnectionSubscriber subscriber in listOfSubscribers)
            {
                debugCount++;
                Logger.log("UpdateSubscribers: " + debugCount);
                subscriber.updateMeWithNewData(record);
            }
        }

        public void pauseWork()
        {
            doPause = !doPause;

            if(doPause)
                Logger.log("Paused PushWorker (" + symbol + ")");
            else
                Logger.log("Resumed PushWorker (" + symbol + ")");
        }
    }
}
