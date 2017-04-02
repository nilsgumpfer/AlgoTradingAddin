using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    public partial class LogView : Form
    {
        public LogView()
        {
            InitializeComponent();
            richTextBox1.Text = Logger.printLogs();
        }

        private void onklickReload_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Logger.printLogs();
        }
    }
}
