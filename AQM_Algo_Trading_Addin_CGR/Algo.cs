using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class Algo : LiveConnectionSubscriber
    {
        public Algo(AlgoControl ac, string symbol)
        {
            DataManager dataManager = DataManager.getInstance();
            dataManager.subscribeForLiveConnection("BMW", this, LiveConnectors.OnVistaDummy);
            this.ac = ac;
            ac.setKontostand(Convert.ToString(kontostand));
        }

        private double startaktienwert = 0;
        private double aktuelleraktienwert = 0;
        private double letzterwert = 0;
        private double gewinn;
        private int zahlVerlust = 0;
        private int initStart = 0;
        private string status;
        private AlgoControl ac;
        private double kontostand = 10000.00;

        public void updateMeWithNewData(StockDataTransferObject newRecord)
        {
            letzterwert = aktuelleraktienwert;
            aktuelleraktienwert = Convert.ToDouble(newRecord.price);

            if (letzterwert != 0 && aktuelleraktienwert > letzterwert)
            {
                initStart = initStart + 1;
                zahlVerlust = 0;
                if (initStart == 2)
                {
                    startaktienwert = aktuelleraktienwert;
                    status = "Kaufen";
                    kontostand = kontostand - startaktienwert;
                    setDataInRibbon(0,kontostand, System.Drawing.Color.Green);
                }
                if (initStart > 2)
                {
                    gewinn = ((aktuelleraktienwert - startaktienwert) / startaktienwert) * 100;
                    kontostand = kontostand + (aktuelleraktienwert - startaktienwert);
                    status = "Behalten";
                    setDataInRibbon(gewinn, kontostand,System.Drawing.Color.DarkOrange);

                }
            }

            if (letzterwert != 0 && aktuelleraktienwert < letzterwert)
            {
                if (initStart >= 2)
                {
                    zahlVerlust = zahlVerlust + 1;
                    gewinn = ((aktuelleraktienwert - startaktienwert) / startaktienwert) * 100;
                    kontostand = kontostand + (aktuelleraktienwert - startaktienwert);
                    setDataInRibbon(gewinn, kontostand,System.Drawing.Color.DarkOrange);

                    if (gewinn < 2 && zahlVerlust >= 3 || gewinn > 2 && zahlVerlust >= 5)
                    {
                        status = "Verkaufen";
                        setDataInRibbon(gewinn, kontostand,System.Drawing.Color.Red);
                    }
                                     
                }
                else
                {
                    initStart = 0;
                }

            }
        }

        private void setDataInRibbon(double aktuellergewinn, double aktuellerkontostand, System.Drawing.Color statusfarbe)
        {
            ac.setStatus(status, statusfarbe);
            ac.setGewinn(aktuellergewinn.ToString("0.000") + " %");
            ac.setKontostand(aktuellerkontostand.ToString("0.00") + " €");
            ac.setStartwert(startaktienwert.ToString() + " €");
        }
    }
}
