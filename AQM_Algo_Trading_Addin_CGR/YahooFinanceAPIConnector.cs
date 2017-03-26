using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class YahooFinanceAPIConnector
    {
        private List<StockDataTransferObject> parseHistoricData(string csvData)
        {
            List<StockDataTransferObject> records = new List<StockDataTransferObject>();
            bool firstRow = true;
            string temp;
            int i;

            string[] rows = csvData.Replace("\r", "").Split('\n');

            foreach (string row in rows)
            {
                i = 0;

                if (string.IsNullOrEmpty(row))
                    continue;

                string[] cols = row.Split(',');

                if (firstRow == true)
                {
                    firstRow = false;

                    //skip first row which contains column names
                }
                else
                {
                    StockDataTransferObject record = new StockDataTransferObject();

                    record.timestamp_otherdata = cols[i++];

                    temp = cols[i++].Replace('.', ',');
                    record.day_open = temp;

                    temp = cols[i++].Replace('.', ',');
                    record.day_high = temp;

                    temp = cols[i++].Replace('.', ',');
                    record.day_low = temp;

                    temp = cols[i++].Replace('.', ',');
                    record.preday_close = temp;

                    temp = cols[i++].Replace('.', ',');
                    record.volume = temp;

                    temp = cols[i++].Replace('.', ',');
                    record.day_adj_close = temp;

                    records.Add(record);
                }
            }

            return records;
        }

        public List<StockDataTransferObject> getHistoricalStockData(string stockSymbol, DateTime dateFrom, DateTime dateTo, YahooFinanceAPI_Resolution resolution)
        {
            string resolution_s = "";

            switch (resolution)
            {
                case YahooFinanceAPI_Resolution.Hourly:
                    resolution_s = "h";
                    break;
                case YahooFinanceAPI_Resolution.Daily:
                    resolution_s = "d";
                    break;
                case YahooFinanceAPI_Resolution.Monthly:
                    resolution_s = "m";
                    break;
                case YahooFinanceAPI_Resolution.Yearly:
                    resolution_s = "y";
                    break;
            }
            return getHistoricalStockData(
                                            stockSymbol,
                                            dateTo.Month.ToString(),
                                            dateTo.Day.ToString(),
                                            dateTo.Year.ToString(),
                                            dateFrom.Month.ToString(),
                                            dateFrom.Day.ToString(),
                                            dateFrom.Year.ToString(),
                                            resolution_s
                                          );
        }

        /// <summary>Extracts historical stockdata via YahooFinanceAPI using following parameters</summary>
        /// <param name="sNameID">Stock-Symbol</param>
        /// <param name="dToMonth">Top-Boundary Month: Values between 0 and 11</param>
        /// <param name="eToDay">Top-Boundary Day: Values between 1 and 31</param>
        /// <param name="fToYear">Top-Boundary Year</param>
        /// <param name="aFromMonth">Bottom-Boundary Month: Values between 0 and 11</param>
        /// <param name="bFromDay">Bottom-Boundary Day: Values between 1 and 31</param>
        /// <param name="cFromYear">Bottom-Boundary Year</param>
        /// <param name="gFormat">Value-Resolution: Hourly (h), Daily (d), Monthly (m), Yearly (y)</param>
        /// <returns>List of party filled StockDataTransferObjects (only relevant fields are filled)</returns>
        public List<StockDataTransferObject> getHistoricalStockData(string sNameID, string dToMonth, string eToDay, string fToYear, string aFromMonth, string bFromDay, string cFromYear, string gFormat)
        {
            string csvData;
            string ignoreString     = ".csv";    //escape sequence data
            string apiUrl           = "http://real-chart.finance.yahoo.com/table.csv?";

            /*
             * Example-value-assignment:
             * 
             /// sNameID                 = "YHOO";    //stock-symbol
             /// dToMonth                = "9";       //9 = October
             /// eToDay                  = "23";      //23 = 23.
             /// fToYear                 = "2016";    //2016 = 2016
             /// aFromMonth              = "3";       //3 = April
             /// bFromDay                = "12";      //12 = 12.
             /// cFromYear               = "1996";    //1996 = 1996
             /// gFormat                 = "d";       //d = on daily basis, m = monthly, etc.           
            */

            using (WebClient webClient = new WebClient())
            {
                apiUrl += "s="         + sNameID;
                apiUrl += "&a="        + aFromMonth;
                apiUrl += "&b="        + bFromDay;
                apiUrl += "&c="        + cFromYear;
                apiUrl += "&d="        + dToMonth;
                apiUrl += "&e="        + eToDay;
                apiUrl += "&f="        + fToYear;
                apiUrl += "&g="        + gFormat;
                apiUrl += "&ignore="   + ignoreString;

                csvData = webClient.DownloadString(apiUrl);
            }

            List<StockDataTransferObject> prices = parseHistoricData(csvData);

            return prices;
        }
    }
}
