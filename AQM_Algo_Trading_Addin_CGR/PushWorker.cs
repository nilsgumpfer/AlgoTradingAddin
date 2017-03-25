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
        private PushConnector pushConnector;
        private List<LiveConnectionSubscriber> listOfSubscribers = new List<LiveConnectionSubscriber>();
        public String symbol { get; }
        private bool doThreading = false;
        private Thread thread;

        private PushWorker()
        {
            //no public constructor without parameters available for this class!
        }

        public PushWorker(PushConnectors variant, String symbol)
        {
            this.symbol = symbol;

            switch(variant)
            {
                case PushConnectors.OnVista:
                    pushConnector = new OnVistaConnector(symbol);
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
                updateSubscribers(pushConnector.getStockData());
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
