using System.Threading;
using lib12.DependencyInjection;
using lib12.Extensions;
using mshtml;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ThreadingTimer = System.Threading.Timer;

namespace Conversator
{
    [Singleton]
    public class ConversationEngine
    {
        private const string ConversatorAddress = "http://www.cleverbot.com/";
        private const string SayItButtonId = "sayit";
        private const string SayItTextBoxId = "stimulus";
        private const int WaitTime = 100000;
        private const int TimerSpan = 500;

        private WebBrowser browser;
        private IHTMLDocument2 document;
        private dynamic sayItButton;
        private dynamic sayItTextBox;

        private List<string> conversation;
        private string conversationText;

        private ThreadingTimer timer;

        public ConversationEngine()
        {
            conversationText = string.Empty;
            conversation = new List<string>();
            InitBrowser(ConversatorAddress);
            timer = new ThreadingTimer(TimerTick, null, TimerSpan, Timeout.Infinite);
        }

        public string Say(string text)
        {
            sayItTextBox.Value = text;
            sayItButton.Click();
            conversation.Add(text);
            conversationText = conversationText + text + Environment.NewLine;
            timer.Change(0, TimerSpan);
            return conversationText;
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
                    break;

                counter++;
            }
        }

        private void TimerTick(object arg)
        {
            dynamic botText = document.all.item("bot");
            //timer.Change(0, TimerSpan);
        }
    }
}