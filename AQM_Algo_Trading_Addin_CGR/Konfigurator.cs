﻿using System;
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
        public bool hasBeenCancelled { get; set; } = true;
        public Konfigurator()
        {
            InitializeComponent();

            try
            {
                comboBox1.DataSource = DataManager.getInstance().getAvailableSymbols();
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                ExceptionHandler.handle("Stammdaten konnten nicht geladen werden! Bitte überprüfen Sie die Verbindung zu Ihrer MySQL-Datenbank.");
                this.Close();
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            TimeSpan span = dateTimePicker2.Value - dateTimePicker1.Value;

            if (groupBox1.Visible == false || (dateTimePicker1.Value < dateTimePicker2.Value))
            {
                this.hasBeenCancelled = false;
                this.Close();
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
        }
    }
}
