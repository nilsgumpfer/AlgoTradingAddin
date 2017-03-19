using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class StockDataTransferObject
    {
        private String isin;
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

        public string Isin
        {
            get
            {
                return isin;
            }

            set
            {
                isin = value;
            }
        }

        public string Wkn
        {
            get
            {
                return wkn;
            }

            set
            {
                wkn = value;
            }
        }

        public string Symbol
        {
            get
            {
                return symbol;
            }

            set
            {
                symbol = value;
            }
        }

        public string Sector
        {
            get
            {
                return sector;
            }

            set
            {
                sector = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public double Volume
        {
            get
            {
                return volume;
            }

            set
            {
                volume = value;
            }
        }

        public double Trend_abs
        {
            get
            {
                return trend_abs;
            }

            set
            {
                trend_abs = value;
            }
        }

        public double Trend_perc
        {
            get
            {
                return trend_perc;
            }

            set
            {
                trend_perc = value;
            }
        }

        public double Day_high
        {
            get
            {
                return day_high;
            }

            set
            {
                day_high = value;
            }
        }

        public double Day_low
        {
            get
            {
                return day_low;
            }

            set
            {
                day_low = value;
            }
        }

        public double Preday_volume
        {
            get
            {
                return preday_volume;
            }

            set
            {
                preday_volume = value;
            }
        }

        public string Timestamp_price
        {
            get
            {
                return timestamp_price;
            }

            set
            {
                timestamp_price = value;
            }
        }

        public string Timestamp_volume
        {
            get
            {
                return timestamp_volume;
            }

            set
            {
                timestamp_volume = value;
            }
        }

        public string Timestamp_metadata
        {
            get
            {
                return timestamp_metadata;
            }

            set
            {
                timestamp_metadata = value;
            }
        }
    }
}
