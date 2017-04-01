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
            atr.lblKS_Saldo.Label = Convert.ToString(kontostand);
        }

        private double startaktienwert = 0;
        private double aktuelleraktienwert = 0;
        private double letzterwert = 0;
        private double gewinn;
        private int zahlVerlust = 0;
        private int initStart = 0;
        private string status;
        private AlgoTradingRibbon atr;
        private double kontostand = 10000.00;

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
                    kontostand = kontostand - startaktienwert;
                    setDataInRibbon(0,kontostand);
                }
                if (initStart > 2)
                {
                    double gewinn = ((aktuelleraktienwert - startaktienwert) / startaktienwert) * 100;
                    kontostand = kontostand + (aktuelleraktienwert - startaktienwert);
                    status = "Behalten";
                    setDataInRibbon(gewinn, kontostand);

                }
            }

            if (letzterwert != 0 & aktuelleraktienwert < letzterwert)
            {
                if (initStart >= 2)
                {
                    zahlVerlust = zahlVerlust + 1;
                    double gewinn = ((aktuelleraktienwert - startaktienwert) / startaktienwert) * 100;
                    if (gewinn < 2 & zahlVerlust >= 3)
                    {
                        status = "Verkaufen";
                        kontostand = kontostand + (aktuelleraktienwert - startaktienwert);
                        setDataInRibbon(gewinn, kontostand);
                    }
                    else if (gewinn > 2 & zahlVerlust >= 5)
                    {
                        status = "Verkaufen";
                        kontostand = kontostand + (aktuelleraktienwert - startaktienwert);
                        setDataInRibbon(gewinn, kontostand);
                    }
                    
                }
                else
                {
                    initStart = 0;
                }

            }
        }

        private void setDataInRibbon(double aktuellergewinn, double aktuellerkontostand)
        {
            String gewinnForLbl = Convert.ToString(aktuellergewinn) + " %";
            String kontostand_saldo = Convert.ToString(aktuellerkontostand) + " €";
            atr.lblAlgoStatus.Label = status;
            atr.lblGewinn.Label = gewinnForLbl;
            atr.lblKS_Saldo.Label = kontostand_saldo;
        }
    }
}
