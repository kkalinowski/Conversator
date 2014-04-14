using System.Linq;
using lib12.DependencyInjection;
using lib12.Extensions;
using lib12.WPF.Core;
using mshtml;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Timers = System.Timers;

namespace Conversator
{
    [Singleton]
    public class ConversationEngine : NotifyingObject
    {
        #region Const
        private const string ConversatorAddress = "http://www.cleverbot.com/";
        private const string SayItButtonId = "sayit";
        private const string SayItTextBoxId = "stimulus";
        private const int WaitTime = 100000;
        private const int TimerInterval = 500;
        #endregion

        #region Fields
        private readonly List<string> conversation;
        private readonly Timers.Timer timer;
        #endregion

        #region Properties
        public WebBrowser Browser { get; set; }
        [WireUp]
        public TextSynthesizer Synthesizer { get; set; }

        private string conversationText;
        public string ConversationText
        {
            get { return conversationText; }
            set
            {
                conversationText = value;
                OnPropertyChanged("ConversationText");
            }
        }

        private bool isWaitingForAnswer;
        public bool IsWaitingForAnswer
        {
            get { return isWaitingForAnswer; }
            set
            {
                isWaitingForAnswer = value;
                OnPropertyChanged("IsWaitingForAnswer");
            }
        }
        #endregion

        #region Init
        public ConversationEngine()
        {
            ConversationText = string.Empty;
            conversation = new List<string>();
            InitBrowser(ConversatorAddress);
            timer = new Timers.Timer(TimerInterval);
            timer.Elapsed += timer_Elapsed;
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
        #endregion

        public void Say(string text)
        {
            var document = (IHTMLDocument2)Browser.Document.DomDocument;
            var sayItButton = document.all.item(SayItButtonId);
            var sayItTextBox = document.all.item(SayItTextBoxId);

            sayItTextBox.Value = text;
            sayItButton.Click();

            AddTextToConversation(text);
            Synthesizer.Speak(text);

            IsWaitingForAnswer = true;
            timer.Enabled = true;
        }

        void timer_Elapsed(object sender, Timers.ElapsedEventArgs e)
        {
            timer.Enabled = false;
            InvokeOnFormBrowserThread(ParseAnswer);
        }

        private void InvokeOnFormBrowserThread(Action action)
        {
            if (Browser.IsHandleCreated && Browser.InvokeRequired)
            {
                Browser.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void ParseAnswer()
        {
            var document = (IHTMLDocument2)Browser.Document.DomDocument;
            dynamic form = document.forms.item();
            var agilityDocument = new HtmlAgilityPack.HtmlDocument();
            agilityDocument.LoadHtml(form.innerHTML);

            var shareIcon = agilityDocument.DocumentNode.SelectSingleNode("//span[@id='snipTextIcon']");
            var bots = agilityDocument.DocumentNode.SelectNodes("//tr[@class='bot']");
            if (shareIcon.NotNull() && bots.NotNull())
            {
                var lastText = bots.LastOrDefault().InnerText.Trim();
                AddTextToConversation(lastText);
                Synthesizer.Speak(lastText);
                IsWaitingForAnswer = false;
            }
            else
            {
                timer.Enabled = true;
            }
        }

        private void AddTextToConversation(string text)
        {
            conversation.Add(text);
            ConversationText += string.Format("- {0}\n", text);
        }
    }
}