using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

namespace AQM_Algo_Trading_Addin_CGR

{
    public class BackgroundCrawler : Observable<Aktienwert>
    {
        private bool                        doWork;
        private Aktienwert                  aktienwert;
        private List<Observer<Aktienwert>>  observerList;

        public BackgroundCrawler(Aktienwert aktienwert)
        {
            doWork              = true;
            this.aktienwert     = new Aktienwert(aktienwert.getAktienSymbol(), false);
            observerList        = new List<Observer<Aktienwert>>();
        }

        public void stopWork()
        {
            doWork = false;
        }

        public void loadStockData()
        {
            //bool messageThrown = false;
            while (doWork)
            {
                try
                {
                    aktienwert.update();
                    notifyObeservers();
                }
                catch (Exception ex)
                {
                    /*if(messageThrown == false)
                        MessageBox.Show(ex.Message);

                    messageThrown = true;*/
                }
            }
        }

        public void subscribe(Observer<Aktienwert> observer)
        {
            observerList.Add(observer);
        }

        public void unsubscribe(Observer<Aktienwert> observer)
        {
            observerList.Remove(observer);
        }

        public Aktienwert getMessage()
        {
            return aktienwert;
        }

        private void notifyObeservers()
        {
            foreach (var item in observerList)
            {
                item.notify(this);
            }
        }
    }
}
