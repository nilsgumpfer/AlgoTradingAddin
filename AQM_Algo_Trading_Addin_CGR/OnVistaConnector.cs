using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class OnVistaConnector : PushConnector
    {
        private String wkn;
        private String isin;
        private String symbol;
        private String name;
        private String sector;
        private bool checkMetaData = true;
        private String htmlData;

        private OnVistaConnector()
        {
            //no public constructor without parameters available
        }

        public OnVistaConnector(String symbol)
        {
            this.symbol = symbol;
            init();
        }

        public StockDataTransferObject getStockData()
        {
            loadHtmlData();

            if(checkMetaData)
            {
                updateMetaData();
                checkMetaData = false;
            }

            StockDataTransferObject stdTransferObject = new StockDataTransferObject();

            stdTransferObject.price                 = extractPrice();
            stdTransferObject.timestamp_price       = extractTimestampPrice();

            stdTransferObject.volume                = extractVolume();
            stdTransferObject.timestamp_volume      = extractTimestampVolume();

            stdTransferObject.day_high              = extractDayHigh();
            stdTransferObject.day_low               = extractDayLow();
            stdTransferObject.preday_volume         = extractPredayVolume();
            
            stdTransferObject.trend_abs             = extractTrendAbs();
            stdTransferObject.trend_perc            = extractTrendPerc();

            stdTransferObject.timestamp_otherdata   = extractTimestampOtherData();

            return stdTransferObject;
        }

        private void init()
        {
            //TODO: search for symbol in db, assign wkn, isin, name and sector

            wkn = "XXXX";
            isin = "XXXX";
            name = "Blablubb Inc.";
            sector = "Bubble-Producer";
        }

        private void updateMetaData()
        {
            //TODO: compare db-data (isin, etc.) to loaded data from website
            //TODO: if not actual or not present, update data in database
        }

        private void loadHtmlData()
        {
            //TODO: load HTML from OnVista using special "Vorgaukeling"-mechanism predending to be a genuine WebBrowser ;)
        }

        private double extractPrice()
        {
            //TODO: Extract Data from HTML Stream
            return 0;
        }

        private double extractVolume()
        {
            //TODO: Extract Data from HTML Stream
            return 0;
        }

        private double extractDayHigh()
        {
            //TODO: Extract Data from HTML Stream
            return 0;
        }

        private double extractDayLow()
        {
            //TODO: Extract Data from HTML Stream
            return 0;
        }

        private double extractPredayVolume()
        {
            //TODO: Extract Data from HTML Stream
            return 0;
        }

        private double extractTrendAbs()
        {
            //TODO: Extract Data from HTML Stream
            return 0;
        }

        private double extractTrendPerc()
        {
            //TODO: Extract Data from HTML Stream
            return 0;
        }

        private String extractTimestampPrice()
        {
            //TODO: Extract Data from HTML Stream
            return "2017-03-20 14:14:14";
        }

        private String extractTimestampVolume()
        {
            //TODO: Extract Data from HTML Stream
            return "2017-03-20 14:14:14";
        }

        private String extractTimestampOtherData()
        {
            //TODO: Extract Data from HTML Stream
            return "2017-03-20 14:14:14";
        }
    }
}
