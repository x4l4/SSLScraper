using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SSLScraper
{
    public partial class Form3 : Form
    {
        String result;
        public Form3()
        {
            InitializeComponent();
            webBrowser3.Navigate("https://www.sslproxies.org/");
        }

        private void webBrowser3_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlElementCollection collection = webBrowser3.Document.GetElementsByTagName("td");
            foreach(HtmlElement element in collection)
            {
                //Console.WriteLine(element.InnerText);
                try
                {
                    if (!(element.InnerText.Contains("ago")))
                    {
                        Match ipMatch = Regex.Match(element.InnerText, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                        Match portMatch = Regex.Match(element.InnerText, @"\d{1,8}\b");
                        if (ipMatch.Success)
                            result = result + ipMatch.Value + ":";
                        if (portMatch.Success && portMatch.Value.Contains(ipMatch.Value))
                            result = result + portMatch.Value + "\n";
                    }
                }catch { }
            }
            Console.WriteLine(result);
        }
    }
}
