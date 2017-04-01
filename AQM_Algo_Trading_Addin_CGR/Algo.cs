using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class Algo : LiveConnectionSubscriber
    {
        public Algo(AlgoTradingRibbon atr)
        {
            DataManager dataManager = DataManager.getInstance();
            dataManager.subscribeForLiveConnection("", this);
            this.atr = atr;
        }

        private double startaktienwert = 0;
        private double aktuelleraktienwert = 0;
        private double letzterwert = 0;
        private double gewinn = 0;
        private int zahlVerlust = 0;
        private int initStart = 0;
        private string status;
        private AlgoTradingRibbon atr;

        public void updateMeWithNewData(StockDataTransferObject newRecord)
        {
            letzterwert = aktuelleraktienwert;
            aktuelleraktienwert = Convert.ToDouble(newRecord.price);

            if (letzterwert != 0 & aktuelleraktienwert > letzterwert)
            {
                initStart = initStart + 1;
                zahlVerlust = 0;
                if (initStart == 2)
                {
                    startaktienwert = aktuelleraktienwert;
                    status = "Kaufen";
                    setDataInRibbon();
                }
                if (initStart > 2)
                {
                    double gewinn = ((aktuelleraktienwert - startaktienwert) * 100) - 100;
                    status = "Behalten";
                    setDataInRibbon();

                }
            }

            if (letzterwert != 0 & aktuelleraktienwert < letzterwert)
            {
                if (initStart >= 2)
                {
                    zahlVerlust = zahlVerlust + 1;
                    double gewinn = ((aktuelleraktienwert - startaktienwert) * 100) - 100;
                    if (gewinn < 2 & zahlVerlust >= 3)
                    {
                        status = "Verkaufen";
                        setDataInRibbon();
                    }
                    else if (gewinn > 2 & zahlVerlust >= 5)
                    {
                        status = "Verkaufen";
                        setDataInRibbon();
                    }
                    
                }
                else
                {
                    initStart = 0;
                }

            }
        }

        private void setDataInRibbon()
        {
            //AlgoTradingRibbon atr = new AlgoTradingRibbon();
            String gewinnForLbl = Convert.ToString(gewinn);
            atr.lblAlgoStatus.Label = status;
            atr.lblGewinn.Label = gewinnForLbl;
        }
    }
}
