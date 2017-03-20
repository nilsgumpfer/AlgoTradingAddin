using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class PushWorker
    {
        private PushConnector pushConnector;
        private List<LiveConnectionSubscriber> listOfSubscribers = new List<LiveConnectionSubscriber>();
        public String symbol { get; }

        private PushWorker()
        {
            //no public constructor without parameters available
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
    }
}
