using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQM_Algo_Trading_Addin_CGR
{
    class Logger
    {
        private static Logger instance;
        private List<String> logs = new List<string>();

        public static void log(string item)
        {
            Logger.getInstance().addToList(item);
        }

        public void addToList(string item)
        {
            string timestamp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss:fff] : ");
            logs.Add(timestamp + item);
        }

        public static string printLogs()
        {
            return Logger.getInstance().getLogs();
        }

        public string getLogs()
        {
            string result = "";

            foreach (string item in logs)
                result += item + '\n';

            return result;

        }

        private Logger()
        {
            //i´m a singleton! i have no public constructor.
        }

        public static Logger getInstance()
        {
            if (instance == null)
                instance = new Logger();

            return instance;
        }
    }
}
