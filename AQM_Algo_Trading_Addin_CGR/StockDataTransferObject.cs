using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AQM_Algo_Trading_Addin_CGR;

namespace AQM_Algo_Trading_Addin_CGR
{
    class StockDataTransferObject
    {
        private List<string> headline = new List<string>();

        public StockDataTransferObject()
        {
            //this order has to be kept equal to data-export-order!
            headline.Add("ISIN");
            headline.Add("WKN");
            headline.Add("Symbol");
            headline.Add("Sektor");
            headline.Add("Kurs");
            headline.Add("Volumen");
            headline.Add("Trend (abs.)");
            headline.Add("Trend (%)");
            headline.Add("Eröffnungskurs");
            headline.Add("Schlusskurs");
            headline.Add("Adj.Close");
            headline.Add("Tageshoch");
            headline.Add("Tagestief");
            headline.Add("Vortagesvolumen");
            headline.Add("Timestamp Kurs");
            headline.Add("Timestamp Volumen");
            headline.Add("Timestamp");
            headline.Add("Provider");
            headline.Add("Handelsplatz");
            headline.Add("Name");
        }
        public string isin { get; set; }
        public string wkn { get; set; }
        public string symbol { get; set; }
        public string sector { get; set; }
        public string price { get; set; }
        public string volume { get; set; }
        public string trend_abs { get; set; }
        public string trend_perc { get; set; }
        public string day_open { get; set; }
        public string day_close { get; set; }
        public string day_adj_close { get; set; }
        public string day_high { get; set; }
        public string day_low { get; set; }
        public string preday_volume { get; set; }
        public string timestamp_price { get; set; }
        public string timestamp_volume { get; set; }
        public string timestamp_otherdata { get; set; }
        public string provider { get; set; }
        public string trading_floor { get; set; }
        public string name { get; set; }

        public List<string> getHeadlineAsList()
        {
           return headline;
        }

        public List<string> getLineAsList()
        {
            List<string> line = new List<string>();

            //this order has to be kept equal to headline-order!
            line.Add(isin);
            line.Add(wkn);
            line.Add(symbol);
            line.Add(sector);
            line.Add(price);
            line.Add(volume);
            line.Add(trend_abs);
            line.Add(trend_perc);
            line.Add(day_open);
            line.Add(day_close);
            line.Add(day_adj_close);
            line.Add(day_high);
            line.Add(day_low);
            line.Add(preday_volume);
            line.Add(timestamp_price);
            line.Add(timestamp_volume);
            line.Add(timestamp_otherdata);
            line.Add(provider);
            line.Add(trading_floor);
            line.Add(name);

            return line;
        }
    }
}
