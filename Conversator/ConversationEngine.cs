using System.IO;
using System.Net.Mime;
using System.Windows.Forms;
using lib12.DependencyInjection;
using mshtml;

namespace Conversator
{
    [Singleton]
    public class ConversationEngine
    {
        private const string ConversatorAddress = "http://www.cleverbot.com/";
        private const string SayItButtonId = "sayit";
        private const string SayItTextBoxId = "stimulus";
        private const int WaitTime = 100000;

        private WebBrowser browser;
        private IHTMLDocument2 document;
        private dynamic sayItButton;
        private dynamic sayItTextBox;

        public ConversationEngine()
        {
            InitBrowser(ConversatorAddress);
        }

        public string Say(string text)
        {
            sayItTextBox.Value = text;
            sayItButton.Click();
            return text + text;
        }

        private void InitBrowser(string url)
        {
            browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.Navigate(url);

            WaitTillLoad();

            document = (IHTMLDocument2)browser.Document.DomDocument;
            sayItButton = document.all.item(SayItButtonId);
            sayItTextBox = document.all.item(SayItTextBoxId);
        }

        private void WaitTillLoad()
        {
            WebBrowserReadyState loadStatus;
            var counter = 0;
            while (true)
            {
                loadStatus = browser.ReadyState;
                Application.DoEvents();
                if ((counter > WaitTime) || (loadStatus == WebBrowserReadyState.Uninitialized)
                    || (loadStatus == WebBrowserReadyState.Loading) || (loadStatus == WebBrowserReadyState.Interactive))
                    break;

                counter++;
            }

            counter = 0;
            while (true)
            {
                loadStatus = browser.ReadyState;
                Application.DoEvents();
                if (loadStatus == WebBrowserReadyState.Complete && browser.IsBusy != true)
                {
                    break;
                }
                counter++;
            }
        }
    }
}