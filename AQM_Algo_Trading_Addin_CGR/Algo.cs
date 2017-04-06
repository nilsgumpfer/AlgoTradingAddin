using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class Algo : LiveConnectionSubscriber
    {
        public Algo(AlgoControlPanel algoControlPanel, string symbol, LiveConnectors variant)
        {
            DataManager dataManager = DataManager.getInstance();
            dataManager.subscribeForLiveConnection(symbol, this, variant);
            this.algoControlPanel = algoControlPanel;
            algoControlPanel.lblKS_Saldo.Text = (Convert.ToString(kontostand));
        }

        private double startaktienwert = 0;
        private double aktuelleraktienwert = 0;
        private double letzterwert = 0;
        private double gewinn;
        private int zahlVerlust = 0;
        private int initStart = 0;
        private string status;
        private AlgoControlPanel algoControlPanel;
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

                        status = "Verkauft";
                        gewinn = 0;
                        initStart = 0;
                        kontostand = kontostand + aktuelleraktienwert;
                        zahlVerlust = 0;
                        startaktienwert = 0;
                        setDataInRibbon(gewinn, kontostand, System.Drawing.Color.Black);
                    }              
                }
                else
                    initStart = 0;
            }
        }

        private void setDataInRibbon(double aktuellergewinn, double aktuellerkontostand, System.Drawing.Color statusfarbe)
        {
            algoControlPanel.lblAlgoStatus.Text = status;
            algoControlPanel.lblAlgoStatus.ForeColor = statusfarbe;

            algoControlPanel.lblGewinn.Text = aktuellergewinn.ToString("0.000") + " %";
            algoControlPanel.lblKS_Saldo.Text = aktuellerkontostand.ToString("0.00") + " €";
            algoControlPanel.lbl_EinstPreis.Text = startaktienwert.ToString() + " €";
        }
    }
}
