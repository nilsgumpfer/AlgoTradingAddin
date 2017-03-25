using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;

namespace AQM_Algo_Trading_Addin_CGR
{
    class OnVistaConnector : PushConnector
    {
        private string symbol;
        private string wkn;
        private string isin;
        private string name;
        private string sector;
        private string url_main_part = "http://www.onvista.de/aktien/";
        private string url_suffix;

        private string url;

        private bool checkMetaData = true;

        private WebClient webClient;

        private string sourceHTML;

        private string onVista_stockID;

        private OnVistaConnector()
        {
            //no public constructor without parameters available!
        }

        public OnVistaConnector(string symbol)
        {
            this.symbol = symbol;
            init();
        }

        public StockDataTransferObject getStockData()
        {
            initWebClient();
            loadHtmlData();

            if(checkMetaData)
            {
                updateMetaData();
                checkMetaData = false;
            }

            StockDataTransferObject stdTransferObject = new StockDataTransferObject();

            stdTransferObject.isin                  = isin;
            stdTransferObject.wkn                   = wkn;
            stdTransferObject.symbol                = symbol;
            stdTransferObject.name                  = name;
            stdTransferObject.sector                = sector;

            stdTransferObject.price                 = extractPrice();
            stdTransferObject.timestamp_price       = extractTimestampPrice();

            stdTransferObject.volume                = extractVolume();
            stdTransferObject.timestamp_volume      = extractTimestampVolume();

            stdTransferObject.day_high              = extractDayHigh();
            stdTransferObject.day_low               = extractDayLow();
            stdTransferObject.day_open              = extractDayOpen();
            stdTransferObject.day_close             = extractDayClose();

            stdTransferObject.preday_volume         = extractPredayVolume();
            
            stdTransferObject.trend_abs             = extractTrendAbs();
            stdTransferObject.trend_perc            = extractTrendPerc();

            stdTransferObject.timestamp_otherdata   = extractTimestampOtherData();

            stdTransferObject.trading_floor         = extractTradingFloor();
            stdTransferObject.provider              = "OnVista.de";

            return stdTransferObject;
        }

        private void init()
        {
            loadMetaData();
            url = url_main_part + url_suffix;
            initWebClient();
        }

        private void initWebClient()
        {
            webClient = new WebClient();
            webClient.Headers.Add("user-agent", "AGENT");
        }

        private void loadMetaData()
        {
            //TODO: search for symbol in db, assign wkn, isin, name, sector and url-suffix

            /*if (notfound)
                throw new Exception("No data available for this symbol! Please update your database first.");
            else {*/
            wkn = "wkn";
            isin = "isin";
            name = "name";
            sector = "sector";
            url_suffix = "BMW-Aktie-DE0005190003";
        }

        private void updateMetaData()
        {
            //TODO: compare db-data (isin, etc.) to loaded data from website
            //TODO: if not actual or not present, update data in database
        }

        private void loadHtmlData()
        {
            sourceHTML = webClient.DownloadString(url);
            //replace specialities like &auml with ä, etc.
            sourceHTML = HttpUtility.HtmlDecode(sourceHTML);
        }

        private string extractIsin()
        {
            //TODO: Extract Data from HTML Stream
            return "ISIN";
        }

        private string extractWkn()
        {
            //TODO: Extract Data from HTML Stream
            return "WKN";
        }

        private string extractName()
        {
            string startTag = "<h1 property=\"v:title\">";
            string endTag = "</h1>";

            return getItemBetweenTags(startTag, endTag)
                   .Replace('\n', ' ');
        }

        private string extractSector()
        {
            //TODO: Extract Data from HTML Stream
            return "SECTOR";
        }

        private string extractUrlSuffix()
        {
            return url.Substring(url_main_part.Length);
        }

        private string extractPrice()
        {
            return useExtractionVariant1("<span data-push=", ":last:1:1:Stock>", "</span>");
        }

        private string extractVolume()
        {
            return useExtractionVariant1("<span data-push=", ":totalVolume:1:1:Stock>", " Stk.</span>");
        }

        private string useExtractionVariant1(string startTag_part1, string startTag_part2, string endTag)
        {
            loadOnVistaStockID();

            string startTag = startTag_part1 + onVista_stockID + startTag_part2;

            return getItemBetweenTags(startTag, endTag)
                   .Replace('\n', ' ')
                   .Replace(" ", "");
        }

        private string extractDayHigh()
        {
            //TODO: Extract Data from HTML Stream
            return "HIGH";
        }

        private string extractDayLow()
        {
            //TODO: Extract Data from HTML Stream
            return "LOW";
        }

        private string extractDayOpen()
        {
            //TODO: Extract Data from HTML Stream
            return "OPEN";
        }

        private string extractDayClose()
        {
            //TODO: Extract Data from HTML Stream
            return "CLOSE";
        }

        private string extractPredayVolume()
        {
            //TODO: Extract Data from HTML Stream
            return "PREDAYVOLUME";
        }

        private string extractTrendAbs()
        {
            //TODO: Extract Data from HTML Stream
            return "TRENDABS";
        }

        private string extractTrendPerc()
        {
            //TODO: Extract Data from HTML Stream
            return "TRENDPERC";
        }

        private string extractTimestampPrice()
        {
            //TODO: Extract Data from HTML Stream
            return "2017-03-20 14:14:14";
        }

        private string extractTimestampVolume()
        {
            //TODO: Extract Data from HTML Stream
            return "2017-03-20 14:14:14";
        }

        private string extractTimestampOtherData()
        {
            //TODO: Extract Data from HTML Stream
            return "2017-03-20 14:14:14";
        }

        private string extractTradingFloor()
        {/*
            return getItemBetweenTags("<span class=\"ICON icon-realtime\" title=\"Realtime\"><span>Realtime:</span></span>", 
                                      "<span class=\"SELEKTOR\">Börsenplatz auswählen</span>");*/
         //TODO: Repair loading of trading-floor
            return "TRADINGFLOOR";
        }

        private void loadOnVistaStockID()
        {
            string startTag = "<meta name=\"og:image\" content=\"http://chartdata.onvista.de/image?granularity=year&type=Stock&id=";
            string endTag = "&";

            onVista_stockID = getItemBetweenTags(startTag, endTag)
                              .Replace('\n', ' ')
                              .Replace(" ", "");
        }

        private string getItemBetweenTags(string startTag, string endTag)
        {
            int start = 0;
            int end = 0;

            start = sourceHTML.IndexOf(startTag, 0);

            if (start < 0)
                throw new Exception("Tag not contained in HTML-source: \"" + startTag);

            start += startTag.Length;
            end = sourceHTML.IndexOf(endTag, start);

            if (end < 0)
                throw new Exception("Tag not contained in HTML-source: \"" + endTag);

            return sourceHTML.Substring(start, end - start);
        }

        public void grabMetaDataAndFillDatabase(string url)
        {
            this.url = url;
            loadHtmlData();
            updateMetaData();
        }
    }
}
