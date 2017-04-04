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
        public static int posISIN               = 1;
        public static int posWKN                = 2;
        public static int posSymbol             = 3;
        public static int posSector             = 4;
        public static int posPrice              = 5;
        public static int posVolume             = 6;
        public static int posTrendAbs           = 7;
        public static int posTrendPerc          = 8;
        public static int posOpen               = 9;
        public static int posPreDayClose        = 10;
        public static int posAdjClose           = 11;
        public static int posHigh               = 12;
        public static int posLow                = 13;
        public static int posTotalVolume        = 14;
        public static int posTimestampPrice     = 15;
        public static int posTimestampVolume    = 16;
        public static int posTimestampOther     = 17;
        public static int posProvider           = 18;
        public static int posTradingFloor       = 19;
        public static int posName               = 20;
        public static int posCurrency           = 21;
        public static int posClose              = 22;

        public StockDataTransferObject()
        {
        }

        public static List<int> getStandardColumnsToDraw()
        {
            List<int> columnList = new List<int>();

            columnList.Add(posISIN);
            columnList.Add(posWKN);
            columnList.Add(posSymbol);
            columnList.Add(posSector);
            columnList.Add(posTimestampPrice);
            columnList.Add(posPrice);
            columnList.Add(posTimestampVolume);
            columnList.Add(posVolume);
            columnList.Add(posTrendAbs);
            columnList.Add(posTrendPerc);
            columnList.Add(posOpen);
            columnList.Add(posPreDayClose);
            columnList.Add(posAdjClose);
            columnList.Add(posHigh);
            columnList.Add(posLow);
            columnList.Add(posTotalVolume);
            columnList.Add(posTimestampOther);
            columnList.Add(posProvider);
            columnList.Add(posTradingFloor);
            columnList.Add(posName);
            columnList.Add(posCurrency);
            columnList.Add(posClose);

            return columnList;
        }
        public string isin { get; set; }
        public string wkn { get; set; }
        public string symbol { get; set; }
        public string sector { get; set; }
        public string price { get; set; }
        public string volume { get; set; }
        public string trend_abs { get; set; }
        public string trend_perc { get; set; }
        public string open { get; set; }
        public string preday_close { get; set; }
        public string adj_close { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string total_volume { get; set; }
        public string timestamp_price { get; set; }
        public string timestamp_volume { get; set; }
        public string timestamp_otherdata { get; set; }
        public string provider { get; set; }
        public string trading_floor { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public string close { get; set; }
        public string suffix_onvista { get; set; }


        public static List<string> getHeadlineAsList()
        {
            List<string> headline = new List<string>();

            //this order has to be kept equal to data-export/column-order!
            headline.Add("ISIN");
            headline.Add("WKN");
            headline.Add("Symbol");
            headline.Add("Sektor");
            headline.Add("Timestamp Kurs");
            headline.Add("Kurs");
            headline.Add("Timestamp Volumen");
            headline.Add("Volumen");
            headline.Add("Trend (abs.)");
            headline.Add("Trend (%)");
            headline.Add("Eröffnungskurs");
            headline.Add("Schlusskurs Vortag");
            headline.Add("Adj.Close");
            headline.Add("Hoch");
            headline.Add("Tief");
            headline.Add("Volumen gesamt");
            headline.Add("Timestamp");
            headline.Add("Provider");
            headline.Add("Handelsplatz");
            headline.Add("Name");
            headline.Add("Currency");
            headline.Add("Schlusskurs");

            return headline;
        }

        public List<string> getLineAsList()
        {
            List<string> line = new List<string>();

            //this order has to be kept equal to headline/column-order!
            line.Add(isin);
            line.Add(wkn);
            line.Add(symbol);
            line.Add(sector);
            line.Add(timestamp_price);
            line.Add(price);
            line.Add(timestamp_volume);
            line.Add(volume);
            line.Add(trend_abs);
            line.Add(trend_perc);
            line.Add(open);
            line.Add(preday_close);
            line.Add(adj_close);
            line.Add(high);
            line.Add(low);
            line.Add(total_volume);
            line.Add(timestamp_otherdata);
            line.Add(provider);
            line.Add(trading_floor);
            line.Add(name);
            line.Add(currency);
            line.Add(close);

            return line;
        }
    }
}
