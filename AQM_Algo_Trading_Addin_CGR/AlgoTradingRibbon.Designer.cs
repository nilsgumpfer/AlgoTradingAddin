namespace AQM_Algo_Trading_Addin_CGR
{
    partial class AlgoTradingRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AlgoTradingRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für Designerunterstützung -
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.CB_Typ_Historisch = this.Factory.CreateRibbonCheckBox();
            this.CB_Typ_Live = this.Factory.CreateRibbonCheckBox();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.CB_Quelle_Lokal = this.Factory.CreateRibbonCheckBox();
            this.CB_Quelle_Onvista = this.Factory.CreateRibbonCheckBox();
            this.CB_Quelle_Yahoo = this.Factory.CreateRibbonCheckBox();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.CB_Ziel_NeuesTB = this.Factory.CreateRibbonCheckBox();
            this.CB_Ziel_AktuellesTB = this.Factory.CreateRibbonCheckBox();
            this.CB_Ziel_Cursor = this.Factory.CreateRibbonCheckBox();
            this.group5 = this.Factory.CreateRibbonGroup();
            this.CB_Visualisierung_Diagramm = this.Factory.CreateRibbonCheckBox();
            this.CB_Visualisierung_Tabelle = this.Factory.CreateRibbonCheckBox();
            this.group6 = this.Factory.CreateRibbonGroup();
            this.CB_Algo_Trend_Kurs = this.Factory.CreateRibbonCheckBox();
            this.CB_Algo_Trend_Volumen = this.Factory.CreateRibbonCheckBox();
            this.group7 = this.Factory.CreateRibbonGroup();
            this.BTN_Aktionen_Ausfuehren = this.Factory.CreateRibbonButton();
            this.group8 = this.Factory.CreateRibbonGroup();
            this.BTN_1 = this.Factory.CreateRibbonButton();
            this.BTN_Test2 = this.Factory.CreateRibbonButton();
            this.BTN_Test3 = this.Factory.CreateRibbonButton();
            this.TableObjektTest = this.Factory.CreateRibbonButton();
            this.button3 = this.Factory.CreateRibbonButton();
            this.button4 = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.checkBox3 = this.Factory.CreateRibbonCheckBox();
            this.BTN_Test1 = this.Factory.CreateRibbonButton();
            this.button5 = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group3.SuspendLayout();
            this.group4.SuspendLayout();
            this.group5.SuspendLayout();
            this.group6.SuspendLayout();
            this.group7.SuspendLayout();
            this.group8.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Groups.Add(this.group3);
            this.tab1.Groups.Add(this.group4);
            this.tab1.Groups.Add(this.group5);
            this.tab1.Groups.Add(this.group6);
            this.tab1.Groups.Add(this.group7);
            this.tab1.Groups.Add(this.group8);
            this.tab1.Label = "Test AlgoTrading";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.CB_Typ_Historisch);
            this.group1.Items.Add(this.CB_Typ_Live);
            this.group1.Items.Add(this.button1);
            this.group1.Items.Add(this.button2);
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // CB_Typ_Historisch
            // 
            this.CB_Typ_Historisch.Label = "Historische Daten";
            this.CB_Typ_Historisch.Name = "CB_Typ_Historisch";
            this.CB_Typ_Historisch.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Typ_Historisch_Click);
            // 
            // CB_Typ_Live
            // 
            this.CB_Typ_Live.Label = "Live-Daten";
            this.CB_Typ_Live.Name = "CB_Typ_Live";
            this.CB_Typ_Live.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Typ_Live_Click);
            // 
            // button1
            // 
            this.button1.Label = "";
            this.button1.Name = "button1";
            // 
            // button2
            // 
            this.button2.Label = "";
            this.button2.Name = "button2";
            // 
            // group3
            // 
            this.group3.Items.Add(this.CB_Quelle_Lokal);
            this.group3.Items.Add(this.CB_Quelle_Onvista);
            this.group3.Items.Add(this.CB_Quelle_Yahoo);
            this.group3.Label = "Datenquelle";
            this.group3.Name = "group3";
            // 
            // CB_Quelle_Lokal
            // 
            this.CB_Quelle_Lokal.Label = "Lokal";
            this.CB_Quelle_Lokal.Name = "CB_Quelle_Lokal";
            this.CB_Quelle_Lokal.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Quelle_Lokal_Click);
            // 
            // CB_Quelle_Onvista
            // 
            this.CB_Quelle_Onvista.Label = "Onvista";
            this.CB_Quelle_Onvista.Name = "CB_Quelle_Onvista";
            this.CB_Quelle_Onvista.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Quelle_Onvista_Click);
            // 
            // CB_Quelle_Yahoo
            // 
            this.CB_Quelle_Yahoo.Label = "YahooFinance";
            this.CB_Quelle_Yahoo.Name = "CB_Quelle_Yahoo";
            this.CB_Quelle_Yahoo.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Quelle_Yahoo_Click);
            // 
            // group4
            // 
            this.group4.Items.Add(this.CB_Ziel_NeuesTB);
            this.group4.Items.Add(this.CB_Ziel_AktuellesTB);
            this.group4.Items.Add(this.CB_Ziel_Cursor);
            this.group4.Label = "Datenziel";
            this.group4.Name = "group4";
            // 
            // CB_Ziel_NeuesTB
            // 
            this.CB_Ziel_NeuesTB.Label = "Neues Tabellenblatt";
            this.CB_Ziel_NeuesTB.Name = "CB_Ziel_NeuesTB";
            this.CB_Ziel_NeuesTB.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Ziel_NeuesTB_Click);
            // 
            // CB_Ziel_AktuellesTB
            // 
            this.CB_Ziel_AktuellesTB.Label = "Aktuelles Tabellenblatt";
            this.CB_Ziel_AktuellesTB.Name = "CB_Ziel_AktuellesTB";
            this.CB_Ziel_AktuellesTB.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Ziel_AktuellesTB_Click);
            // 
            // CB_Ziel_Cursor
            // 
            this.CB_Ziel_Cursor.Label = "Cursorposition";
            this.CB_Ziel_Cursor.Name = "CB_Ziel_Cursor";
            this.CB_Ziel_Cursor.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Ziel_Cursor_Click);
            // 
            // group5
            // 
            this.group5.Items.Add(this.CB_Visualisierung_Diagramm);
            this.group5.Items.Add(this.CB_Visualisierung_Tabelle);
            this.group5.Label = "Visualisierung";
            this.group5.Name = "group5";
            // 
            // CB_Visualisierung_Diagramm
            // 
            this.CB_Visualisierung_Diagramm.Label = "Diagramm";
            this.CB_Visualisierung_Diagramm.Name = "CB_Visualisierung_Diagramm";
            this.CB_Visualisierung_Diagramm.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Visualisierung_Diagramm_Click);
            // 
            // CB_Visualisierung_Tabelle
            // 
            this.CB_Visualisierung_Tabelle.Label = "Tabelle";
            this.CB_Visualisierung_Tabelle.Name = "CB_Visualisierung_Tabelle";
            this.CB_Visualisierung_Tabelle.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Visualisierung_Tabelle_Click);
            // 
            // group6
            // 
            this.group6.Items.Add(this.CB_Algo_Trend_Kurs);
            this.group6.Items.Add(this.CB_Algo_Trend_Volumen);
            this.group6.Label = "Algorithmus";
            this.group6.Name = "group6";
            // 
            // CB_Algo_Trend_Kurs
            // 
            this.CB_Algo_Trend_Kurs.Label = "Trend (Kurs)";
            this.CB_Algo_Trend_Kurs.Name = "CB_Algo_Trend_Kurs";
            this.CB_Algo_Trend_Kurs.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Algo_Trend_Kurs_Click);
            // 
            // CB_Algo_Trend_Volumen
            // 
            this.CB_Algo_Trend_Volumen.Label = "Trend (Volumen)";
            this.CB_Algo_Trend_Volumen.Name = "CB_Algo_Trend_Volumen";
            this.CB_Algo_Trend_Volumen.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_Algo_Trend_Volumen_Click);
            // 
            // group7
            // 
            this.group7.Items.Add(this.BTN_Aktionen_Ausfuehren);
            this.group7.Label = "Aktionen";
            this.group7.Name = "group7";
            // 
            // BTN_Aktionen_Ausfuehren
            // 
            this.BTN_Aktionen_Ausfuehren.Label = "Ausführen";
            this.BTN_Aktionen_Ausfuehren.Name = "BTN_Aktionen_Ausfuehren";
            this.BTN_Aktionen_Ausfuehren.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // group8
            // 
            this.group8.Items.Add(this.BTN_1);
            this.group8.Items.Add(this.BTN_Test2);
            this.group8.Items.Add(this.BTN_Test3);
            this.group8.Items.Add(this.TableObjektTest);
            this.group8.Items.Add(this.button3);
            this.group8.Items.Add(this.button4);
            this.group8.Items.Add(this.button5);
            this.group8.Label = "Test";
            this.group8.Name = "group8";
            // 
            // BTN_1
            // 
            this.BTN_1.Label = "Tabelle bei aktiver Zelle einfügen";
            this.BTN_1.Name = "BTN_1";
            this.BTN_1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BTN_Test_Click);
            // 
            // BTN_Test2
            // 
            this.BTN_Test2.Label = "Tabelle auf aktuellem Tabllenblatt einfügen";
            this.BTN_Test2.Name = "BTN_Test2";
            this.BTN_Test2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BTN_Test2_Click);
            // 
            // BTN_Test3
            // 
            this.BTN_Test3.Label = "Tabelle auf neuem Tabllenblatt einfügen";
            this.BTN_Test3.Name = "BTN_Test3";
            this.BTN_Test3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BTN_Test3_Click);
            // 
            // TableObjektTest
            // 
            this.TableObjektTest.Label = "TableObject anlegen (Cursorposition)";
            this.TableObjektTest.Name = "TableObjektTest";
            this.TableObjektTest.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.TableObjektTest_Click);
            // 
            // button3
            // 
            this.button3.Label = "TableObject verschieben (Cursorposition)";
            this.button3.Name = "button3";
            this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Label = "OnVista Test (BMW)";
            this.button4.Name = "button4";
            this.button4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button4_Click);
            // 
            // group2
            // 
            this.group2.Label = "group2";
            this.group2.Name = "group2";
            // 
            // checkBox3
            // 
            this.checkBox3.Label = "checkBox3";
            this.checkBox3.Name = "checkBox3";
            // 
            // BTN_Test1
            // 
            this.BTN_Test1.Label = "Tabelle bei aktiver Zelle einfügen";
            this.BTN_Test1.Name = "BTN_Test1";
            this.BTN_Test1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BTN_Test_Click);
            // 
            // button5
            // 
            this.button5.Label = "YahooFinanceAPI Test (BMW)";
            this.button5.Name = "button5";
            this.button5.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button5_Click);
            // 
            // AlgoTradingRibbon
            // 
            this.Name = "AlgoTradingRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.AlgoTradingRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.group5.ResumeLayout(false);
            this.group5.PerformLayout();
            this.group6.ResumeLayout(false);
            this.group6.PerformLayout();
            this.group7.ResumeLayout(false);
            this.group7.PerformLayout();
            this.group8.ResumeLayout(false);
            this.group8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BTN_Aktionen_Ausfuehren;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Typ_Historisch;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Typ_Live;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Quelle_Lokal;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Quelle_Onvista;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Quelle_Yahoo;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group5;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox3;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Ziel_NeuesTB;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Ziel_AktuellesTB;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Ziel_Cursor;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Visualisierung_Diagramm;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Visualisierung_Tabelle;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group6;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Algo_Trend_Kurs;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_Algo_Trend_Volumen;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group7;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group8;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BTN_1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BTN_Test2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BTN_Test1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BTN_Test3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton TableObjektTest;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button5;
    }

    partial class ThisRibbonCollection
    {
        internal AlgoTradingRibbon AlgoTradingRibbon
        {
            get { return this.GetRibbon<AlgoTradingRibbon>(); }
        }
    }
}
