using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockDataCr4wl3r
{
    public interface Observer<MessageType>
    {
        void notify(Observable<MessageType> caller);
    }
}
