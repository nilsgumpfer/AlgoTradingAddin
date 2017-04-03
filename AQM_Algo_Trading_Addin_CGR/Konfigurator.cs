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
    public partial class Konfigurator : Form
    {
        public bool hasBeenCancelled { get; set; } = false;
        public Konfigurator()
        {
            InitializeComponent();

            comboBox1.DataSource = DataManager.getInstance().getAvailableSymbols();
            comboBox1.SelectedIndex = 0;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            TimeSpan span = dateTimePicker2.Value - dateTimePicker1.Value;
            int tage = 40;

            if (dateTimePicker1.Value <= dateTimePicker2.Value)
            {
                //if (span.Days > tage)
                    this.Close();
                //else
                    //MessageBox.Show("Zwischen Start- und Endzeitpunkt müssen mindestens " + tage + " Tage liegen!");
            }
            else
            {
                MessageBox.Show("Der Startzeitpunkt muss vor dem Endzeitpunkt liegen!");
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.hasBeenCancelled = true;
        }
    }
}
