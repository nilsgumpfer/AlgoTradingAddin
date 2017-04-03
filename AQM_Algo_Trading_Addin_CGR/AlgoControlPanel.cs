using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    public partial class AlgoControlPanel : UserControl
    {

        public AlgoControlPanel()
        {
            InitializeComponent();
        }

        private void AlgoControl_Load(object sender, EventArgs e)
        {
           Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
