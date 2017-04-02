using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Tools;

namespace AQM_Algo_Trading_Addin_CGR
{
    public partial class ThisAddIn
    {

        public CustomTaskPane SharePane { get; private set; }
        public AlgoControl ac = new AlgoControl();
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            new AlgoTradingRibbon();
            SharePane = this.SharePane = this.CustomTaskPanes.Add(ac, "Algorithmus");
            SharePane.Width = 380;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Von VSTO generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
