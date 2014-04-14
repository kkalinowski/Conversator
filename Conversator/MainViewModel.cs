using System.Windows.Input;
using lib12.DependencyInjection;
using lib12.WPF.Core;
using lib12.WPF.EventTranscriptions;

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

        public ICommand KeyboardCommand { get; private set; }

        [WireUp]
        public SayCommand SayCommand { get; set; }
        #endregion

        #region Init
        public MainViewModel()
        {
            KeyboardCommand = new DelegateCommand<EventTranscriptionParameter<KeyEventArgs>>(ExecuteKeyboard);
        }

        private void ExecuteKeyboard(EventTranscriptionParameter<KeyEventArgs> parameter)
        {
            if (parameter.EventArgs.Key == Key.Enter && SayCommand.CanExecute())
                SayCommand.Execute();
        }
        #endregion

    }
}