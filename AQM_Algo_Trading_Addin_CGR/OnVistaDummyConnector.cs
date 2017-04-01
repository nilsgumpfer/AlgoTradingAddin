using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AQM_Algo_Trading_Addin_CGR
{
    class OnVistaDummyConnector : LiveConnector
    {
        private string symbol;
        private string provider         = "OnVista.de";
        private string urlMainPart      = "http://www.onvista.de/aktien/";
        private string urlSuffix;
        private string timestampFormat  = "yyyy-MM-dd HH:mm:ss";
        private string errorPlaceholder = "N/A";
        private double lastVolume       = 0.0;
        private StockDataTransferObject lastRecord = new StockDataTransferObject();
        private StockDataTransferObject newRecord = new StockDataTransferObject();

        private string url;

        private bool checkMetaData = true;

        private WebClient webClient;

        private string sourceHTML;

        private string onVista_stockID;

        private OnVistaDummyConnector()
        {
            //no public constructor without parameters available!
        }

        public OnVistaDummyConnector(string symbol)
        {
            this.symbol = symbol;
            init();
        }

        public StockDataTransferObject getStockData()
        {
            initWebClient();
            loadHtmlData();
            lastRecord = newRecord;

            if(checkMetaData)
            {
                updateMetaData();
                checkMetaData = false;
            }

            StockDataTransferObject stdTransferObject = new StockDataTransferObject();

            stdTransferObject.isin                  = extractIsin();
            stdTransferObject.wkn                   = extractWkn();
            stdTransferObject.symbol                = extractSymbol();
            stdTransferObject.name                  = extractName();
            stdTransferObject.sector                = extractSector();

            stdTransferObject.price                 = extractPrice();
            stdTransferObject.timestamp_price       = extractTimestampPrice();

            stdTransferObject.volume                = extractVolume();
            stdTransferObject.timestamp_volume      = extractTimestampVolume();

            stdTransferObject.high                  = extractDayHigh();
            stdTransferObject.low                   = extractDayLow();
            stdTransferObject.open                  = extractDayOpen();

            stdTransferObject.preday_close          = extractPredayClose();
            stdTransferObject.total_volume          = extractDayVolume();
            
            stdTransferObject.trend_abs             = extractTrendAbs();
            stdTransferObject.trend_perc            = extractTrendPerc();

            stdTransferObject.timestamp_otherdata   = extractTimestampOtherData();

            stdTransferObject.trading_floor         = extractTradingFloor();
            stdTransferObject.currency              = extractCurrency();
            stdTransferObject.provider              = provider;

            newRecord = stdTransferObject;

            return stdTransferObject;
        }

        private void init()
        {
            loadMetaData();
            url = urlMainPart + urlSuffix;
            initWebClient();
        }

        private void initWebClient()
        {
        }

        private void loadMetaData()
        {
            urlSuffix = "BMW-Aktie-DE0005190003";
        }

        private void updateMetaData()
        {
        }

        private void loadHtmlData()
        {
        }

        private string extractIsin()
        {
            return "DE00000000TEST";
        }

        private string extractWkn()
        {
            return "4711";
        }

        private string extractSymbol()
        {
            return "TEST";
        }

        private string extractName()
        {
            return "Name";
        }

        private string extractSector()
        {
            return "Sector";
        }

        private string extractUrlSuffix()
        {
            return url.Substring(urlMainPart.Length);
        }

        private string extractPrice()
        {
            return "99,99";
        }

        private string extractVolume()
        {
            return "9999";
        }

        private string extractDayVolume()
        {
            return "99999999";
        }

        private string extractDayHigh()
        {
            return "100";
        }

        private string extractDayLow()
        {
            return "50";
        }

        private string extractDayOpen()
        {
            return "50";
        }

        private string extractPredayClose()
        {
            return "100";
        }

        private string extractPredayVolume()
        {
            return "999999999";
        }

        private string extractTrendAbs()
        {
            return "-10";
        }

        private string extractTrendPerc()
        {
            return "-10%";
        }

        private string extractTimestampPrice()
        {
            return DateTime.Now.ToString(timestampFormat);
        }

        private string extractDataAndTimeGeneral()
        {
            return DateTime.Now.ToString(timestampFormat);
        }

        private string extractTimestampVolume()
        {
            return DateTime.Now.ToString(timestampFormat);
        }

        private string extractTimestampOtherData()
        {
            return DateTime.Now.ToString(timestampFormat);
        }

        private string extractTradingFloor()
        {
            return "Tradegate";
        }

        private string extractCurrency()
        {
            return "EUR";
        }

        public void grabMetaDataAndFillDatabase(string url)
        {
            this.url = url;
            loadHtmlData();
            updateMetaData();
        }

        public bool checkChange()
        {
            bool changed = false;

            changed = !(
                        newRecord.isin          == lastRecord.isin 
                        &&
                        newRecord.wkn           == lastRecord.wkn
                        &&
                        newRecord.symbol        == lastRecord.symbol
                        &&
                        newRecord.name          == lastRecord.name
                        &&
                        newRecord.sector        == lastRecord.sector
                        &&
                        newRecord.price         == lastRecord.price
                        &&
                        newRecord.volume        == lastRecord.volume
                        &&
                        newRecord.high          == lastRecord.high
                        &&
                        newRecord.low           == lastRecord.low
                        &&
                        newRecord.open          == lastRecord.open
                        &&
                        newRecord.preday_close  == lastRecord.preday_close
                        &&
                        newRecord.total_volume  == lastRecord.total_volume
                        &&
                        newRecord.trend_abs     == lastRecord.trend_abs
                        &&
                        newRecord.trend_perc    == lastRecord.trend_perc
                        &&
                        newRecord.trading_floor == lastRecord.trading_floor
                        &&
                        newRecord.currency      == lastRecord.currency
                        &&
                        newRecord.provider      == lastRecord.provider
                       );

            return changed;
        }
    }
}
