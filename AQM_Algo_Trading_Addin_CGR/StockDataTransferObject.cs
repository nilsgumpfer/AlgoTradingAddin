using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AQM_Algo_Trading_Addin_CGR;

namespace AQM_Algo_Trading_Addin_CGR
{
    public class StockDataTransferObject
    {
        private String isin { get; set; }
        private String wkn;
        private String symbol;
        private String sector;
        private double price;
        private double volume;
        private double trend_abs;
        private double trend_perc;
        private double day_high;
        private double day_low;
        private double preday_volume;
        private String timestamp_price;
        private String timestamp_volume;
        private String timestamp_metadata;
    }
}
