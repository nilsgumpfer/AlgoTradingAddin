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
        private double lastGeneratedPrice  = 85.0;
        private int lastGeneratedVolume = 0;
        private double lastGeneratedTrend = 0.0;
        private int totalVolume = 0;
        private int runCount = 5;
        private StockDataTransferObject lastRecord = new StockDataTransferObject();
        private StockDataTransferObject newRecord = new StockDataTransferObject();

        private string url;

        private bool checkMetaData = true;

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
            Logger.log("getStockData() START");

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

            stdTransferObject.suffix_onvista        = extractUrlSuffix();

            newRecord = stdTransferObject;
            Logger.log("getStockData() END");
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
            return "DE0005190003";
        }

        private string extractWkn()
        {
            return "519000";
        }

        private string extractSymbol()
        {
            return "BMW";
        }

        private string extractName()
        {
            return "BMW Aktie";
        }

        private string extractSector()
        {
            return "Automobilindustrie";
        }

        private string extractUrlSuffix()
        {
            return url.Substring(urlMainPart.Length);
        }

        private string extractPrice()
        {
            return getRandomPrice(1, 1);
        }

        private string extractVolume()
        {
            return getRandomVolume();
        }

        private string extractDayVolume()
        {
            return totalVolume.ToString();
        }

        private string extractDayHigh()
        {
            return "86,00";
        }

        private string extractDayLow()
        {
            return "84,00";
        }

        private string extractDayOpen()
        {
            return "85,00";
        }

        private string extractPredayClose()
        {
            return "85,00";
        }

        private string extractPredayVolume()
        {
            return "30000";
        }

        private string extractTrendAbs()
        {
            return getRandomTrend(1,1);
        }

        private string extractTrendPerc()
        {
            return getRandomTrend(1, 1) + "%";
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

        public string getRandomPrice(double plusFactor, double minusFactor)
        {
            Random rand = new Random();
            double result;
            double pos_ten = rand.Next(0, 10);              //0-9
            double pos_one = rand.Next(0, 10) / 10.0;       //0-0,9
            double deviation = (pos_one + pos_ten) / 10.0;  //0-0,99

            int neg = rand.Next();                          //some number
            int positive_or_negative = neg % 2;             //without rest dividable by 2? 50/50-chance

            if(positive_or_negative == 0)
            {
                runCount++;
            }
            else
            {
                runCount--;
            }

            if(runCount >= 0)
            {
                result = lastGeneratedPrice + (plusFactor * deviation);
            }
            else
            {
                result = lastGeneratedPrice - (minusFactor * deviation);
            }

            if (runCount < -5)
                runCount = 5;

            if (runCount > 5)
                runCount = -5;

            lastGeneratedPrice = result;

            return result.ToString();
        }
        public string getRandomTrend(double plus, double minus)
        {
            Random rand = new Random();
            double result;
            double pos_ten = rand.Next(0, 10);              //0-9
            double pos_one = rand.Next(0, 10) / 10.0;       //0-0,9
            double deviation = (pos_one + pos_ten) / 10.0;  //0-0,99

            int neg = rand.Next();                          //some number
            int positive_or_negative = neg % 2;             //without rest dividable by 2? 50/50-chance

            if (positive_or_negative == 0)
            {
                result = plus * deviation;
            }
            else
            {
                result = - (minus * deviation);
            }

            return result.ToString();
        }

        public string getRandomVolume()
        {
            Random rand = new Random();
            int result = 1000 / rand.Next(1, 8);

            totalVolume += result;
            
            return result.ToString();
        }
    }
}
