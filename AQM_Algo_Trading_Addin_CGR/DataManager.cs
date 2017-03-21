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

        public void subscribeForLiveConnection(String symbol, LiveConnectionSubscriber subscriber)
        {

        }

        public void findOrCreateWorker(String symbol, LiveConnectionSubscriber subscriber)
        {
            Boolean found = false;

            foreach (PushWorker worker in listOfPushWorkers)
            {
                if (worker.symbol == symbol)
                {
                    worker.subscribe(subscriber);
                    found = true;
                    break;
                }
            }

            if(found == false)
            {
                listOfPushWorkers.Add(new PushWorker(PushConnectors.OnVista, symbol));
            }
        }
    }
}
