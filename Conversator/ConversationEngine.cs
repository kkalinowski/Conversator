using System.Linq;
using lib12.DependencyInjection;
using lib12.Extensions;
using mshtml;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Conversator
{
    [Singleton]
    public class ConversationEngine
    {
        private const string ConversatorAddress = "http://www.cleverbot.com/";
        private const string SayItButtonId = "sayit";
        private const string SayItTextBoxId = "stimulus";
        private const string BotSayId = "bot";
        private const int WaitTime = 100000;
        private const int TimerInterval = 500;

        private readonly List<string> conversation;
        private string conversationText;

        private readonly Timers.Timer timer;

        public WebBrowser Browser { get; set; }

        public ConversationEngine()
        {
            conversationText = string.Empty;
            conversation = new List<string>();
            InitBrowser(ConversatorAddress);
            timer = new Timers.Timer(TimerInterval);
            timer.Elapsed += timer_Elapsed;
        }

        public string Say(string text)
        {
            var document = (IHTMLDocument2)Browser.Document.DomDocument;
            var sayItButton = document.all.item(SayItButtonId);
            var sayItTextBox = document.all.item(SayItTextBoxId);

            sayItTextBox.Value = text;
            sayItButton.Click();

            conversation.Add(text);
            conversationText = conversationText + text + Environment.NewLine;

            document = (IHTMLDocument2)Browser.Document.DomDocument;
            //var botText = document.all.item("bot");
            var agilityDocument = new HtmlAgilityPack.HtmlDocument();
            agilityDocument.LoadHtml(document.activeElement.innerHTML);
            var bots = agilityDocument.DocumentNode.SelectNodes("//tr[@class='bot']");
            if (bots.NotNull())
            {
                var lastText = bots.LastOrDefault().InnerText.Trim() + Environment.NewLine;
                conversation.Add(lastText);
                conversationText += lastText;
            }

            //timer.Enabled = true;
            return conversationText;
        }

        private void InitBrowser(string url)
        {
            Browser = new WebBrowser { ScriptErrorsSuppressed = true };
            Browser.Navigate(url);

            WaitTillLoad();
        }

        private void WaitTillLoad()
        {
            WebBrowserReadyState loadStatus;
            var counter = 0;
            while (true)
            {
                loadStatus = Browser.ReadyState;
                Application.DoEvents();
                if ((counter > WaitTime) || (loadStatus == WebBrowserReadyState.Uninitialized)
                    || (loadStatus == WebBrowserReadyState.Loading) || (loadStatus == WebBrowserReadyState.Interactive))
                    break;

                counter++;
            }

            counter = 0;
            while (true)
            {
                loadStatus = Browser.ReadyState;
                Application.DoEvents();
                if (loadStatus == WebBrowserReadyState.Complete && Browser.IsBusy != true)
                    break;

                counter++;
            }
        }

        void timer_Elapsed(object sender, Timers.ElapsedEventArgs e)
        {
            timer.Enabled = false;
            //var document = (IHTMLDocument2)Browser.Document.DomDocument;
            //var botText = document.all.item("bot");
        }
    }
}