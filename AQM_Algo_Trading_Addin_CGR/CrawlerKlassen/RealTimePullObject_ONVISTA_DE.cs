using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Onvista
{
    class RealTimePullObject_ONVISTA_DE
    {
        WebClient webClient;
        private string aktienSymbol;
        private string stockid;
        //private string url = "http://www.onvista.de/aktien/";
        private string url;
        private string sourceHTML;
        private DateTime timestamp_geladen;

        public RealTimePullObject_ONVISTA_DE(string url)
        {
            this.aktienSymbol   = aktienSymbol;
            this.webClient      = new WebClient();
            this.url            = url;
            
        }

        public string getStockID(bool updateRelevant)
        {
            string startTag = "<meta name=\"og:image\" content=\"http://chartdata.onvista.de/image?granularity=year&type=Stock&id=";
            string endTag = "&";

            string tmp = getItemAusSourceCode(startTag, endTag, updateRelevant);
            tmp = tmp.Replace('\n', ' ');
            tmp = tmp.Replace(" ", "");
            stockid = tmp;

            return stockid;
        }

        public string getAktienName(bool updateRelevant)
        {
            string startTag = "<h1 property=\"v:title\">";
            string endTag   = "</h1>";

            string tmp = getItemAusSourceCode(startTag, endTag, updateRelevant);
            tmp = tmp.Replace('\n', ' ');
            //tmp = tmp.Replace(" ", "");

            return tmp;
        }

        public string getAktienKurs(bool updateRelevant)
        {
            getStockID(true);
            //string startTag = "<span data-push=\"" + stockid + "\:last:1:1:Stock\">";
            string startTag = "<span data-push="+stockid+":last:1:1:Stock>";
            string endTag   = "</span>";
            string tmp = getItemAusSourceCode(startTag, endTag, updateRelevant);
            tmp = tmp.Replace('\n', ' ');
            tmp = tmp.Replace(" ", "");

            //return getItemAusSourceCode(startTag, endTag, updateRelevant);
            return tmp;
        }

        public string getAktienVolumen(bool updateRelevant)
        {
            getStockID(false);
            string startTag = "<span data-push=" + stockid + ":totalVolume:1:1:Stock>";
            string endTag = " Stk.</span>";

            string tmp = getItemAusSourceCode(startTag, endTag, updateRelevant);
            tmp = tmp.Replace('\n', ' ');
            tmp = tmp.Replace(" ", "");

            /*startTag = "<td>";
            endTag = "</td>";
            string tmp2 = getItemBetweenTags(startTag, endTag, tmp);
            tmp2 = tmp2.Replace('\n', ' ');
            tmp2 = tmp2.Replace(" ", "");
            */
            return tmp;
        }
    

        public string getHandelsPlatz(bool updateRelevant)
        {
            string startTag = "<span class=\"ICON icon-realtime\" title=\"Realtime\"><span>Realtime:</span></span>";
            string endTag   = "<span class=\"SELEKTOR\">Börsenplatz auswählen</span>";
            

            return getItemAusSourceCode(startTag, endTag, false); ;
        }

        public string getProvider(bool updateRelevant)
        {
            return "onvista.de";
        }

        public string getTimestampGeladen()
        {
            return timestamp_geladen.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public string getUhrzeitGehandelt()
        {
            string startTag = "<time datetime=\"" + getDatumGehandelt() + "T";
            string endTag   = "+";

            return getItemAusSourceCode(startTag, endTag, false);
        }

        public string getUhrzeitVolumen()
        {/*
            string startTag = "<td>Zeit<br>Kursdaten</td>";
            string endTag = "<br>";
            string uhrzeit = getItemAusSourceCode(startTag, endTag, false);

            return uhrzeit.Substring(uhrzeit.IndexOf("<td>") + 4);*/
            return "";
        }

        public string getDatumGehandelt()
        {
            string startTag = "<time datetime=\"";
            string endTag   = "T";
            string datum    = getItemAusSourceCode(startTag, endTag, false);

            return datum;
        }

        public string getTimestampGehandelt()
        {
            string timestamp = getDatumGehandelt() + " " + getUhrzeitGehandelt();

            return DateTime.ParseExact(
                timestamp, 
                "dd.MM.yy HH:mm:ss", 
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"
                );
        }

        public string getTimestampVolumen()
        {/*
            //   string timestamp = getDatumGehandelt() + " " + getUhrzeitVolumen();

            string timestamp = null;
            return DateTime.ParseExact(
                timestamp,
                "dd.MM.yy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"
                );*/
            return "";
        }

        private string getItemAusSourceCode(string startTag, string endTag, bool updateRelevant)
        {
            int start   = 0;
            int end     = 0;

            updateSourceHTML(true);
            start = sourceHTML.IndexOf(startTag, 0);

            if (start < 0)
                throw new Exception("Wert nicht gefunden: \"" + startTag);

            start   += startTag.Length;
            end     =  sourceHTML.IndexOf(endTag, start);

            return sourceHTML.Substring(start, end - start);
        }

        private string getItemBetweenTags(string startTag, string endTag, string quelltext)
        {
            int start = 0;
            int end = 0;

            start = quelltext.IndexOf(startTag, 0);

            if (start < 0)
                throw new Exception("Wert nicht gefunden: \"" + startTag);

            start += startTag.Length;
            end = quelltext.IndexOf(endTag, start);

            return quelltext.Substring(start, end - start);
        }
        private void updateSourceHTML(bool updateRelevant)
        {
            if (updateRelevant)
            {
                webClient.Headers.Add("user-agent", "AGENT");
                sourceHTML = webClient.DownloadString(url);
                sourceHTML = HttpUtility.HtmlDecode(sourceHTML);

                timestamp_geladen = DateTime.Now;
            }
        }

        public String getHTMLSource()
        {
            updateSourceHTML(true);
            return sourceHTML;
        }
    }
}
