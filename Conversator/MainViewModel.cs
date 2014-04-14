using lib12.DependencyInjection;
using lib12.WPF.Core;

namespace Conversator
{
    [Singleton]
    public class MainViewModel : NotifyingObject
    {
        #region Properties
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

        [WireUp]
        public ConversationEngine ConversationEngine { get; set; }
        #endregion
    }
}