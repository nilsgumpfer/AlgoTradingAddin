﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    interface LiveConnector
    {
        StockDataTransferObject getStockData();
        bool checkChange();
    }
}
