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

            //TODO: Fill Dropdown with Data from database.. only available symbols can be selected!
            comboBox1.SelectedIndex = 0;
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value <= dateTimePicker2.Value)
                this.Close();
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
