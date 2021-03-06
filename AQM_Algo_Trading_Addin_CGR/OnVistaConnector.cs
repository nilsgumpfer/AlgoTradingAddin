﻿using System;
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
    class OnVistaConnector : LiveConnector
    {
        private string url;
        private WebClient webClient;
        private string sourceHTML;
        private string onVista_stockID;
        private string symbol;
        private string provider                     = "OnVista.de";
        private string urlMainPart                  = "http://www.onvista.de/aktien/";
        private string urlSuffix                    = "";
        private string timestampFormat              = "yyyy-MM-dd HH:mm:ss";
        private string errorPlaceholder             = "0.0";
        private int lastVolume                      = 0;
        private StockDataTransferObject lastRecord  = new StockDataTransferObject();
        private StockDataTransferObject newRecord   = new StockDataTransferObject();

        public OnVistaConnector()
        {
            initWebClient();
        }

        public OnVistaConnector(string symbol)
        {
            this.symbol = symbol;
            init();
        }

        public StockDataTransferObject getStockData()
        {
            StockDataTransferObject stdTransferObject = new StockDataTransferObject();

            if (urlSuffix != "")
            {
                initWebClient();
                loadHtmlData();
                lastRecord = newRecord;

                stdTransferObject.isin = extractIsin();
                stdTransferObject.wkn = extractWkn();
                stdTransferObject.symbol = extractSymbol();
                stdTransferObject.name = extractName();
                stdTransferObject.sector = extractSector();

                stdTransferObject.price = extractPrice();
                stdTransferObject.timestamp_price = extractTimestampPrice();

                stdTransferObject.volume = extractVolume();
                stdTransferObject.timestamp_volume = extractTimestampVolume();

                stdTransferObject.high = extractDayHigh();
                stdTransferObject.low = extractDayLow();
                stdTransferObject.open = extractDayOpen();

                stdTransferObject.preday_close = extractPredayClose();
                stdTransferObject.total_volume = extractDayVolume();

                stdTransferObject.trend_abs = extractTrendAbs();
                stdTransferObject.trend_perc = extractTrendPerc();

                stdTransferObject.timestamp_otherdata = extractTimestampOtherData();

                stdTransferObject.trading_floor = extractTradingFloor();
                stdTransferObject.currency = extractCurrency();
                stdTransferObject.provider = provider;

                stdTransferObject.suffix_onvista = extractUrlSuffix();

                newRecord = stdTransferObject;
            }

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
            webClient = new WebClient();
            webClient.Headers.Add("user-agent", "AGENT");
        }

        private void loadMetaData()
        {
            if (urlSuffix == "")
            {
                try
                {
                    MySQLConnector mySQLConnector = new MySQLConnector();
                    StockDataTransferObject record = mySQLConnector.getMasterDataForSymbol(symbol);

                    if (record == null)
                        throw new Exception("Es konnten keine Stammdaten für dieses Symbol in Ihrer Datenbank gefunden werden. Bitte laden Sie diese zunächst nach und versuchen es dann erneut.");

                    urlSuffix = record.suffix_onvista;
                }
                catch (Exception e)
                {
                    ExceptionHandler.handle(e);
                    urlSuffix = "";
                }
            }
        }

        private void updateMasterData(StockDataTransferObject record)
        {
            DBUpdater updater = new DBUpdater();

            if (updater.updateMasterData(record) > 0)
                MessageBox.Show(
                                "Folgende Stammdaten wurden gespeichert: " + 
                                '\n' +
                                "ISIN: " +
                                record.isin +
                                '\n' +
                                "WKN: " +
                                record.wkn +
                                '\n' +
                                "Symbol: " +
                                record.symbol +
                                '\n' +
                                "Name: " +
                                record.name +
                                '\n' +
                                "Sektor: " +
                                record.sector +
                                '\n' +
                                "URL-Suffix: " +
                                record.suffix_onvista
                                );
            else
                MessageBox.Show("Stammdaten für " + record.name + " konnten nicht gespeichert werden.");

        }

        private void loadHtmlData()
        {
            try
            {
                sourceHTML = webClient.DownloadString(url);
                sourceHTML = HttpUtility.HtmlDecode(sourceHTML);
            }
            catch( Exception e )
            {
                ExceptionHandler.handle(e);
            }
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
            return url.Substring(urlMainPart.Length);
        }

        private string extractPrice()
        {
            return useExtractionVariant1("last");
        }

        private string extractVolume()
        {
            try
            {
                if (lastVolume == 0)
                {
                    lastVolume = Convert.ToInt32(extractDayVolume());
                    return errorPlaceholder;
                }
                else
                {
                    int deltaVolume = Convert.ToInt32(extractDayVolume()) - lastVolume;
                    lastVolume = Convert.ToInt32(extractDayVolume());
                    return deltaVolume.ToString();
                }
            }
            catch (Exception e)
            {
                return errorPlaceholder;
            }
        }

        private string extractDayVolume()
        {
            return useExtractionVariant1("totalVolume").Replace(".","");
        }

        private string useExtractionVariant1(string keyWord)
        {
            string pattern =    @"data-push=\d*:" + keyWord + @":\d{1}:\d{1}:Stock>(.?\d*[\.,\,]\d*)";
            /*
                                @"data-push=                //static part
                                \d*                         //0-n digits
                                :" + keyWord + @":          //this part differs, so the caller has to hand over the relevant keyword
                                \d{1}                       //1 digit
                                :                           //static part
                                \d{1}                       //1 digit
                                :Stock>                     //static part
                                (                           //this bracket initiates a "group". it specifies the relevant part, which should be extracted
                                .?                          //in some cases, here we expect one blankspace - .? means: here comes exactly 0 or 1 character (inlcudes space, too)
                                \d*                         //0-n digits
                                [\.,\,]                     //this means: here can be either one point or one comma - nothing else
                                \d*                         //0-n digits
                                )                           //this bracket finally closes the group
                                ";
             */

            //in some cases the returned string would contain a blankspace at its front - so we eliminate it
            return getItemUsingRegEx(pattern).Replace(" ", "");
        }

        private string useExtractionVariant2(string keyWord)
        {
            string pattern = @"data-push=\d*:" + keyWord + @":\d{1}:\d{1}:Stock>(.?[\-,\+]\d*[\.,\,]\d*)";
            
            //in some cases the returned string would contain a blankspace at its front - so we eliminate it
            return getItemUsingRegEx(pattern).Replace(" ", "");
        }

        private string useExtractionVariant3(string keyWord)
        {
            string pattern = @"data-push=\d*:" + keyWord + @":\d{1}:\d{1}:Stock>(.?\d*.\d*.\d*,.?\d*:\d*:\d*)";
            
            //in some cases the returned string would contain a blankspace at its front - so we eliminate it
            return getItemUsingRegEx(pattern).Replace(" ", "");
        }

        private string useExtractionVariant4(string keyWord)
        {
            string pattern = @"data-push=\d*:" + keyWord + @":\d{1}:\d{1}:Stock>.?\d*[\.,\,]\d*.?\/.?(\d*[\.,\,]\d*)";

            //in some cases the returned string would contain a blankspace at its front - so we eliminate it
            return getItemUsingRegEx(pattern).Replace(" ", "");
        }

        private string extractDayHigh()
        {
            return useExtractionVariant1("high");
        }

        private string extractDayLow()
        {
            //return useExtractionVariant1("data-push=", ":high:1:1:Stock> " + extractDayHigh() + " /", " <");
            //return useExtractionVariant2();
            return useExtractionVariant4("high");
            //TODO: Extract Low-Price
            //return "LOW";
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
            return "PREDAYVOLUME";
        }

        private string extractTrendAbs()
        {
            return useExtractionVariant2("performanceAbsolute");
        }

        private string extractTrendPerc()
        {
            return useExtractionVariant2("performanceRelative");
        }

        private string extractTimestampPrice()
        {
            return parseDateTimeToTimestamp(extractDataAndTimeGeneral());
        }

        private string parseDateTimeToTimestamp(string dateTime)
        {
            try
            {
                return DateTime.ParseExact(
                    dateTime,
                    "dd.MM.yyyy,HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture)
                    .ToString(timestampFormat);
            }
            catch (Exception e)
            {
                return errorPlaceholder;
            }
        }

        private string extractDataAndTimeGeneral()
        {
            return useExtractionVariant3("lastTime");
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

        private string getItemBetweenTags(string sourceString, string startTag, string endTag)
        {
            int start = 0;
            int end = 0;

            start = sourceString.IndexOf(startTag, 0);

            start += startTag.Length;
            end = sourceString.IndexOf(endTag, start);

            try
            {
                return sourceString.Substring(start, end - start).Replace('\n', ' ');
            }
            catch (Exception e)
            {
                ExceptionHandler.handle("Tag cannot be found in HTML-source: \"" + endTag);
                return errorPlaceholder;
            }
        }

        private string getItemBetweenTags(string startTag, string endTag)
        {
            return getItemBetweenTags(sourceHTML, startTag, endTag);
        }

        public void grabMetaDataAndFillDatabase(string url)
        {
            this.url = url;
            urlSuffix = extractUrlSuffix();
            updateMasterData(getStockData());
        }

        private string getItemUsingRegEx(string pattern)
        {
            return getItemUsingRegEx(sourceHTML, pattern);
        }

        private string getItemUsingRegEx(string source, string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.Compiled);

            //get matches (these are the substrings which match the pattern)
            MatchCollection matches = regex.Matches(source);

            try
            {
                //get groups (these are the RELEVANT substring out of the substrings above we want to extract basically)
                //hint: we assume that only one match and only one group are found, so we can access them via index
                GroupCollection groups = matches[0].Groups;
                //hint: index 0 contains the match again, index 1 contains the first group!
                return groups[1].Value;
            }
            catch (Exception e)
            {
                //maybe nothing was found and we get an index out of bounds exception or sth. - so we catch it, go on and pass back a placeholder
                return errorPlaceholder;
            }
        }

        private string getItemsUsingRegEx(string source, string pattern)
        {
            string result = "";
            Regex regex = new Regex(pattern, RegexOptions.Compiled);

            //get matches (these are the substrings which match the pattern)
            MatchCollection matches = regex.Matches(source);

            try
            {
                //get groups (these are the RELEVANT substring out of the substrings above we want to extract basically)
                foreach(Match match in matches)
                {
                    //skip first group
                    for(int i = 1; i< match.Groups.Count; i++)
                    {
                        result += "http://www.onvista.de" + match.Groups[i].Value + '\n';
                    }
                }
            }
            catch (Exception e)
            {
                //maybe nothing was found
            }

            return result;
        }

        public string loadURLsFormSite(string url)
        {
            string pattern = "(\\/aktien\\/.*?)\"";
            this.url = url;
            loadHtmlData();
            return getItemsUsingRegEx(sourceHTML, pattern);
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
