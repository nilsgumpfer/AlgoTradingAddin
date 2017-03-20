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
        public string isin { get; set; }
        public string wkn { get; set; }
        public string symbol { get; set; }
        public string sector { get; set; }
        public double price { get; set; }
        public double volume { get; set; }
        public double trend_abs { get; set; }
        public double trend_perc { get; set; }
        public double day_open { get; set; }
        public double day_close { get; set; }
        public double day_adj_close { get; set; }
        public double day_high { get; set; }
        public double day_low { get; set; }
        public double preday_volume { get; set; }
        public string timestamp_price { get; set; }
        public string timestamp_volume { get; set; }
        public string timestamp_otherdata { get; set; }
    }
}
