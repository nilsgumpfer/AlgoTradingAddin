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
    public partial class MasterDataDialog : Form
    {
        public MasterDataDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OnVistaConnector ovConnector = new OnVistaConnector();
                richTextBox1.Text = ovConnector.loadURLsFormSite(richTextBox1.Text);
            }
            catch(Exception ex)
            {
                ExceptionHandler.handle("Das hat nicht funktioniert. Bitte versuchen Sie es erneut mit gültigen Werten.");
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OnVistaConnector ovConnector = new OnVistaConnector();
                List<string> list = richTextBox1.Text.Replace('\n', ' ').Split(' ').ToList();

                foreach (string item in list)
                {
                    if (checkBox1.Checked)
                    {
                        DialogResult result = MessageBox.Show("Soll diese URL zur Extraktion von Stammdaten genutzt werden? " + item, "Bestätigung", MessageBoxButtons.YesNo);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                ovConnector.grabMetaDataAndFillDatabase(item);
                                break;
                            case DialogResult.No:
                                break;
                        }
                    }
                    else
                        ovConnector.grabMetaDataAndFillDatabase(item);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.handle("Das hat nicht funktioniert. Bitte versuchen Sie es erneut mit gültigen Werten.");
            }
        }
    }
}
