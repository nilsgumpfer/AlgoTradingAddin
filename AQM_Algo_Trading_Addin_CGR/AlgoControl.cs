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
    public partial class AlgoControl : UserControl
    {

        private delegate void setKontostandCallback(String kontostand);
        private delegate void setGewinnCallback(String gewinn);
        private delegate void setStartwertCallback(String startwert);
        private delegate void setStatusCallback(String status, System.Drawing.Color farbe);

        public String gewStatus;

        public AlgoControl()
        {
            InitializeComponent();
        }

        private void AlgoControl_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void setKontostand (String kontostand)
        {
            lblKS_Saldo.Text = kontostand;
            
            
        }

        public void setGewinn(String gewinn)
        {
            lblGewinn.Text = gewinn;
            
        }

        public void setStartwert(String startwert)
        {
           lbl_EinstPreis.Text = startwert;
            
            
        }

        public void setStatus(String status, System.Drawing.Color farbe)
        {
            lblAlgoStatus.Text = status;
            lblAlgoStatus.ForeColor = farbe;
        }
            
           
        
    }
}
