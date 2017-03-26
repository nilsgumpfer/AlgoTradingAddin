using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR

{
    public interface Observable<MessageType>
    {
        void subscribe(Observer<MessageType> observer);
        void unsubscribe(Observer<MessageType> observer);
        MessageType getMessage();
    }
}
