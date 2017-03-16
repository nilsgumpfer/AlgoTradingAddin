using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    public class Aktienwert
    {
        private string aktienName;
        private string aktienSymbol;
        private string aktienKurs;
        private string aktienVolumen;
        private string timestamp_geladen;
        private string timestamp_gehandelt;
        private string timestamp_volumen;
        private string handelsPlatz;
        private string provider;

        private RealTimePullObject myFunctions;
        
        public Aktienwert(string aktienSymbol, bool updateRelevant)
        {
            this.aktienSymbol = aktienSymbol;
            this.myFunctions = new RealTimePullObject_MOCKUP(this.aktienSymbol);

            if (updateRelevant)
                update();
        }

        public void update()
        {
            aktienName              = myFunctions.getAktienName(true);
            aktienKurs              = myFunctions.getAktienKurs(false);
            aktienVolumen           = myFunctions.getAktienVolumen(false);
            timestamp_gehandelt     = myFunctions.getTimestampGehandelt();
            timestamp_geladen       = myFunctions.getTimestampGeladen();
            timestamp_volumen       = myFunctions.getTimestampVolumen();
            handelsPlatz            = myFunctions.getHandelsPlatz(false);
            provider                = myFunctions.getProvider(false);
        }

        public string getAktienSymbol()
        {
            return aktienSymbol;
        }

        public string getAktienName()
        {
            aktienName = myFunctions.getAktienName(false);
            return aktienName;
        }

        public string getAktienName(bool updateRelevant)
        {
            if (updateRelevant)
                update();

            return getAktienName();
        }

        public string getAktienKurs()
        {
            aktienKurs = myFunctions.getAktienKurs(false).Replace(',', '.');
            return aktienKurs;
        }

        public string getTimestampVolumen()
        {
            return timestamp_volumen;

        }

        public string getAktienKurs(bool updateRelevant)
        {
            if (updateRelevant)
                update();

            return getAktienKurs();
        }

        public void setAktienKurs(string aktienKurs)
        {
            this.aktienKurs = aktienKurs;
        }

        public string getAktienVolumen()
        {
            aktienVolumen = myFunctions.getAktienVolumen(false).Replace(',', '.');
            return aktienVolumen;
        }

        public string getAktienVolumen(bool updateRelevant)
        {
            if (updateRelevant)
                update();

            return getAktienVolumen();
        }

        public string getHandelsPlatz()
        {
            handelsPlatz = myFunctions.getHandelsPlatz(false);
            return handelsPlatz;
        }

        public string getHandelsPlatz(bool updateRelevant)
        {
            if (updateRelevant)
                update();

            return getHandelsPlatz();
        }

        public string getProvider()
        {
            provider = myFunctions.getProvider(false);
            return provider;
        }

        public string getProvider(bool updateRelevant)
        {
            if (updateRelevant)
                update();

            return getProvider();
        }

        public string getTimestampGehandelt()
        {
            return timestamp_gehandelt;
        }

        public string getTimestampGeladen()
        {
            return timestamp_geladen;
        }
    }
}
