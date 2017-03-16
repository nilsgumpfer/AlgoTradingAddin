using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeStockDataCr4wl3r
{
    public interface Observable<MessageType>
    {
        void subscribe(Observer<MessageType> observer);
        void unsubscribe(Observer<MessageType> observer);
        MessageType getMessage();
    }
}
