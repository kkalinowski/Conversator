using lib12.DependencyInjection;
using lib12.WPF.Core;

namespace Conversator
{
    [Singleton]
    public class MainViewModel : NotifyingObject
    {
        #region Properties
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        private string userText;
        public string UserText
        {
            get { return userText; }
            set
            {
                userText = value;
                OnPropertyChanged("UserText");
            }
        }
        #endregion

        public MainViewModel()
        {
            Text = "asdf";
            UserText = "asdfasdf";
        }
    }
}