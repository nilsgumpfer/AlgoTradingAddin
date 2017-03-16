﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    interface RealTimePullObject
    {
        string getAktienName(bool updateRelevant);
        string getAktienKurs(bool updateRelevant);
        string getAktienVolumen(bool updateRelevant);
        string getHandelsPlatz(bool updateRelevant);
        string getProvider(bool updateRelevant);
        string getTimestampGehandelt();
        string getTimestampGeladen();
        string getTimestampVolumen();
    }
}
