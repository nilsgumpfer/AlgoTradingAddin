using System;

namespace AQM_Algo_Trading_Addin_CGR
{
    class RealTimePullObject_MOCKUP : RealTimePullObject
    {
        private string aktienSymbol;
        private DateTime timestamp_geladen;

        public RealTimePullObject_MOCKUP(string aktienSymbol)
        {
            this.aktienSymbol   = aktienSymbol;
        }

        public string getAktienName(bool updateRelevant)
        {
            return aktienSymbol;
        }

        public string getAktienKurs(bool updateRelevant)
        {
            return "77,77";
        }

        public string getAktienVolumen(bool updateRelevant)
        {
            return "8888";
        }
    

        public string getHandelsPlatz(bool updateRelevant)
        {
            return "MOCKUP";
        }

        public string getProvider(bool updateRelevant)
        {
            return "MOCKUP";
        }

        public string getTimestampGeladen()
        {
            return timestamp_geladen.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public string getUhrzeitGehandelt()
        {
            return "00:00:00";
        }

        public string getUhrzeitVolumen()
        {
            return "00:00:00";
        }

        public string getDatumGehandelt()
        {
            return "01.01.2017";
        }

        public string getTimestampGehandelt()
        {
            return timestamp_geladen.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string getTimestampVolumen()
        {
            return timestamp_geladen.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void updateSourceHTML(bool updateRelevant)
        {
            if (updateRelevant)
            {
                timestamp_geladen = DateTime.Now;
            }
        }
    }
}
