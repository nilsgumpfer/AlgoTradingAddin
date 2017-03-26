using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AQM_Algo_Trading_Addin_CGR
{
    class RealTimePullObject_BOERSE_DE : RealTimePullObject
    {
        WebClient webClient;
        private string aktienSymbol;
        private string url = "http://www.boerse.de/wertpapier/";
        private string sourceHTML;
        private DateTime timestamp_geladen;

        public RealTimePullObject_BOERSE_DE(string aktienSymbol)
        {
            this.aktienSymbol   = aktienSymbol;
            this.webClient      = new WebClient();
            this.url            += aktienSymbol;
        }

        public string getAktienName(bool updateRelevant)
        {
            string startTag = "<h1 class=\"toolHeadHeadline\" id=\"toolHeadline\">";
            string endTag   = "</h1>";

            string tmp = getItemAusSourceCode(startTag, endTag, updateRelevant);
            tmp = tmp.Replace('\n', ' ');
            tmp = tmp.Replace(" ", "");

            return tmp;
        }

        public string getAktienKurs(bool updateRelevant)
        {
            string startTag = "id=\"" + aktienSymbol + "_p_16\">";
            string endTag   = "</div>";

            return getItemAusSourceCode(startTag, endTag, updateRelevant);
        }

        public string getAktienVolumen(bool updateRelevant)
        {
            string startTag = "<td>Volumen</td>";
            string endTag = "<td>Tagesvolumen</td>";

            string tmp = getItemAusSourceCode(startTag, endTag, updateRelevant);
            tmp = tmp.Replace('\n', ' ');
            tmp = tmp.Replace(" ", "");

            startTag = "<td>";
            endTag = "</td>";
            string tmp2 = getItemBetweenTags(startTag, endTag, tmp);
            tmp2 = tmp2.Replace('\n', ' ');
            tmp2 = tmp2.Replace(" ", "");

            return tmp2;
        }
    

        public string getHandelsPlatz(bool updateRelevant)
        {
            string startTag = "class=\"pushDiv\" id=\"" + aktienSymbol + "_t_16\">" + getUhrzeitGehandelt() + "</div><br>";
            string endTag   = "</td>";

            return getItemAusSourceCode(startTag, endTag, false);
        }

        public string getProvider(bool updateRelevant)
        {
            return "boerse.de";
        }

        public string getTimestampGeladen()
        {
            return timestamp_geladen.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public string getUhrzeitGehandelt()
        {
            string startTag = "class=\"pushDiv\" id=\"" + aktienSymbol + "_t_16\">";
            string endTag   = "</div>";

            return getItemAusSourceCode(startTag, endTag, false);
        }

        public string getUhrzeitVolumen()
        {
            string startTag = "<td>Zeit<br>Kursdaten</td>";
            string endTag = "<br>";
            string uhrzeit = getItemAusSourceCode(startTag, endTag, false);

            return uhrzeit.Substring(uhrzeit.IndexOf("<td>") + 4);
        }

        public string getDatumGehandelt()
        {
            string startTag = "<td>Zeit<br>Kursdaten</td>";
            string endTag   = "</td>";
            string datum    = getItemAusSourceCode(startTag, endTag, false);

            return datum.Substring(datum.IndexOf("<br>") + 4);
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
        {
            string timestamp = getDatumGehandelt() + " " + getUhrzeitVolumen();

            return DateTime.ParseExact(
                timestamp,
                "dd.MM.yy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss"
                );
        }

        private string getItemAusSourceCode(string startTag, string endTag, bool updateRelevant)
        {
            int start   = 0;
            int end     = 0;

            updateSourceHTML(updateRelevant);
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
                sourceHTML = webClient.DownloadString(url);
                sourceHTML = HttpUtility.HtmlDecode(sourceHTML);

                timestamp_geladen = DateTime.Now;
            }
        }
    }
}
