using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Windows.Forms;


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

            stdTransferObject.isin                  = extractIsin();
            stdTransferObject.wkn                   = extractWkn();
            stdTransferObject.symbol                = extractSymbol();
            stdTransferObject.name                  = extractName();
            stdTransferObject.sector                = extractSector();

            stdTransferObject.price                 = extractPrice();
            stdTransferObject.timestamp_price       = extractTimestampPrice();

            stdTransferObject.volume                = extractVolume();
            stdTransferObject.timestamp_volume      = extractTimestampVolume();

            stdTransferObject.day_high              = extractDayHigh();
            stdTransferObject.day_low               = extractDayLow();
            stdTransferObject.day_open              = extractDayOpen();

            stdTransferObject.preday_close          = extractPredayClose();
            stdTransferObject.day_volume            = extractDayVolume();
            
            stdTransferObject.trend_abs             = extractTrendAbs();
            stdTransferObject.trend_perc            = extractTrendPerc();

            stdTransferObject.timestamp_otherdata   = extractTimestampOtherData();

            stdTransferObject.trading_floor         = extractTradingFloor();
            stdTransferObject.currency              = extractCurrency();
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
            return getItemBetweenTags("<dt>ISIN</dt><dd property=\"schema:productID\">", "<");
        }

        private string extractWkn()
        {
            return getItemBetweenTags("<dt>WKN</dt><dd>", "<");
        }

        private string extractSymbol()
        {
            return getItemBetweenTags("<dt>Symbol</dt><dd>", "<");
        }

        private string extractName()
        {
            return getItemBetweenTags("<h1 property=\"v:title\">", "<");
        }

        private string extractSector()
        {
            return getItemBetweenTags("title=\"Branche: ", "\"");
        }

        private string extractUrlSuffix()
        {
            return url.Substring(url_main_part.Length);
        }

        private string extractPrice()
        {
            return useExtractionVariant1("<span data-push=", ":last:1:1:Stock>", "<");
        }

        private string extractVolume()
        {
            return "VOLUME";
        }

        private string extractDayVolume()
        {
            return useExtractionVariant1("<span data-push=", ":totalVolume:1:1:Stock>", " ");
        }

        private string useExtractionVariant1(string startTag_part1, string startTag_part2, string endTag)
        {
            loadOnVistaStockID();

            string startTag = startTag_part1 + onVista_stockID + startTag_part2;

            return getItemBetweenTags(startTag, endTag);
        }

        private string extractDayHigh()
        {
            return useExtractionVariant1("data-push=", ":high:1:1:Stock> ", " /");
        }

        private string extractDayLow()
        {
            return useExtractionVariant1("data-push=", ":high:1:1:Stock> " + extractDayHigh() + " /", " <");
        }

        private string extractDayOpen()
        {
            return getItemBetweenTags("<td headers=\"dataOpen\">", "<");
        }

        private string extractPredayClose()
        {
            return getItemBetweenTags("<td headers=\"dataClose\">", "<");
        }

        private string extractPredayVolume()
        {
            //TODO: Extract Data from HTML Stream
            return "PREDAYVOLUME";
        }

        private string extractTrendAbs()
        {
            return getItemBetweenTags("performanceAbsolute:1:1:Stock> ", " <");
        }

        private string extractTrendPerc()
        {
            return getItemBetweenTags("performanceRelative:1:1:Stock>XYZ ", " <");
        }

        private string extractTimestampPrice()
        {
            return parseDateTimeToTimestamp(extractDataAndTimeGeneral());
        }

        private string parseDateTimeToTimestamp(string dateTime)
        {
            return DateTime.ParseExact(
                dateTime,
                "dd.MM.yyyy, HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"
                );
        }

        private string extractDataAndTimeGeneral()
        {
            return getItemBetweenTags("lastTime:1:1:Stock> ", " <");
        }

        private string extractTimestampVolume()
        {
            return parseDateTimeToTimestamp(extractDataAndTimeGeneral());
        }

        private string extractTimestampOtherData()
        {
            return parseDateTimeToTimestamp(extractDataAndTimeGeneral());
        }

        private string extractTradingFloor()
        {
            return getItemBetweenTags("<meta property=\"schema:seller\" content=\"", "\"");
        }

        private string extractCurrency()
        {
            return getItemBetweenTags("<span property=\"schema:priceCurrency\">", "<");
        }

        private void loadOnVistaStockID()
        {
            onVista_stockID = getItemBetweenTags("<meta name=\"og:image\" content=\"http://chartdata.onvista.de/image?granularity=year&type=Stock&id=", "&");
        }

        private string getItemBetweenTags(string startTag, string endTag)
        {
            int start = 0;
            int end = 0;

            start = sourceHTML.IndexOf(startTag, 0);

            if (start < 0)
            {
                MessageBox.Show("Tag cannot be found in HTML-source: \"" + startTag);
                return "N/A";
            }

            start += startTag.Length;
            end = sourceHTML.IndexOf(endTag, start);

            if (end < 0)
            {
                MessageBox.Show("Tag cannot be found in HTML-source: \"" + endTag);
                return "N/A";
            }

            return sourceHTML.Substring(start, end - start).Replace('\n', ' ');
        }

        public void grabMetaDataAndFillDatabase(string url)
        {
            this.url = url;
            loadHtmlData();
            updateMetaData();
        }
    }
}
