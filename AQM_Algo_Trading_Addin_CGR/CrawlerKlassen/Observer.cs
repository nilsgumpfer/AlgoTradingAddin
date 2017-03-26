using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    public interface Observer<MessageType>
    {
        void notify(Observable<MessageType> caller);
    }
}
