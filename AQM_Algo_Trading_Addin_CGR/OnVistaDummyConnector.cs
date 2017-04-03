using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace AQM_Algo_Trading_Addin_CGR
{
    class OnVistaDummyConnector : LiveConnector
    {
        private string symbol;
        private string isin;
        private string wkn;
        private string sector;
        private string name;
        private string provider                     = "OnVista.de";
        private string urlMainPart                  = "http://www.onvista.de/aktien/";
        private string urlSuffix                    = "";
        private string timestampFormat              = "yyyy-MM-dd HH:mm:ss";
        private double lastGeneratedPrice           = 85.0;
        private int totalVolume                     = 0;
        private int runCount                        = 5;
        private double trend;
        private double trendAbs;
        private StockDataTransferObject lastRecord  = new StockDataTransferObject();
        private StockDataTransferObject newRecord   = new StockDataTransferObject();

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
            Thread.Sleep(750);

            lastRecord = newRecord;

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

            return stdTransferObject;
        }

        private void init()
        {
            loadMetaData();
        }

        private void loadMetaData()
        {
            try
            {
                MySQLConnector mySQLConnector = new MySQLConnector();
                StockDataTransferObject record = mySQLConnector.getMasterDataForSymbol(symbol);

                if (record == null)
                    throw new Exception("Es konnten keine Stammdaten für dieses Symbol in Ihrer Datenbank gefunden werden. Bitte laden Sie diese zunächst nach und versuchen es dann erneut.");

                urlSuffix   = record.suffix_onvista;
                isin        = record.isin;
                wkn         = record.wkn;
                name        = record.name;
                sector      = record.sector;

            }
            catch (Exception e)
            {
                ExceptionHandler.handle(e);
                urlSuffix   = "";
                isin        = "";
                wkn         = "";
                name        = "";
                sector      = "";
            }
        }

        private void updateMetaData()
        {
        }

        private void loadHtmlData()
        {
        }

        private string extractIsin()
        {
            return isin;
        }

        private string extractWkn()
        {
            return wkn;
        }

        private string extractSymbol()
        {
            return symbol;
        }

        private string extractName()
        {
            return name;
        }

        private string extractSector()
        {
            return sector;
        }

        private string extractUrlSuffix()
        {
            return urlSuffix;
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
            return trendAbs.ToString();
        }

        private string extractTrendPerc()
        {
            return trend.ToString() + "%";
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

            if (runCount < -2)
                runCount = 0;

            if (runCount > 2)
                runCount = 0;

            trend = (lastGeneratedPrice / result) - 1;
            trendAbs = result - lastGeneratedPrice;

            lastGeneratedPrice = result;

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
