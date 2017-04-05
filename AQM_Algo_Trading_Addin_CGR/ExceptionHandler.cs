using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    class ExceptionHandler
    {
        private static ExceptionHandler instance;
        private List<String> thrownMessages = new List<string>();

        public static void handle(Exception e)
        {
            ExceptionHandler.getInstance().showMessageInMessageBox(e.Message);
        }

        public static void handle(string message)
        {
            ExceptionHandler.getInstance().showMessageInMessageBox(message);
        }

        private void showMessageInMessageBox(string message)
        {
            thrownMessages.Add(message);
            MessageBox.Show(message);
        }

        private ExceptionHandler()
        {
            //i´m a singleton! i have no public constructor.
        }

        public static ExceptionHandler getInstance()
        {
            if (instance == null)
                instance = new ExceptionHandler();

            return instance;
        }
    }
}
