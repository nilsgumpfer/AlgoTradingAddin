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

        private PushWorker()
        {
            //no public constructor without parameters available for this class!
        }

        public PushWorker(LiveConnectors variant, String symbol)
        {
            this.symbol = symbol;

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
                StockDataTransferObject sdtObject = liveConnector.getStockData();

                Logger.log("updateSubscribers START");
                if (liveConnector.checkChange())
                    updateSubscribers(sdtObject);
                Logger.log("updateSubscribers END");
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
            }
        }

        private void updateSubscribers(StockDataTransferObject record)
        {
            foreach(LiveConnectionSubscriber subscriber in listOfSubscribers)
            {
                subscriber.updateMeWithNewData(record);
            }
        }
    }
}
