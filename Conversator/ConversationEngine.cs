using System.Windows.Controls;
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

        private readonly WebBrowser browser;
        private readonly IHTMLDocument2 document;
        private readonly dynamic sayItButton;
        private readonly dynamic sayItTextBox;

        public ConversationEngine()
        {
            browser = new WebBrowser();
            browser.Navigate(ConversatorAddress);
            document = (IHTMLDocument2)browser.Document;
            sayItButton = document.all.item(SayItButtonId);
            sayItTextBox = document.all.item(SayItTextBoxId);
        }

        public string Say(string text)
        {
            sayItTextBox.value = text;
            sayItButton.click();
            return text + text;
        }
    }
}