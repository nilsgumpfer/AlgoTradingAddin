using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    public class OnVistaConnector : PushConnector
    {
        private String wkn;
        private String isin;
        private String symbol;

        private OnVistaConnector()
        {

        }

        public OnVistaConnector(String symbol)
        {
            this.symbol = symbol;

            if (init() == false)
                return null;
        }

        private StockDataTransferObject getStockData()
        {
            StockDataTransferObject stdTransferObject = new StockDataTransferObject();

        }

        private bool init()
        {
            //search for symbol in db, assign wkn and isin
        }
    }
}
