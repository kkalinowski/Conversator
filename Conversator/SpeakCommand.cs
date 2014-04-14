using System;
using System.Windows.Input;
using lib12.Collections;
using lib12.DependencyInjection;

namespace Conversator
{
    [Singleton]
    public class SpeakCommand : ICommand
    {
        #region CanExecute
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual bool CanExecute(object parameter = null)
        {
            return !MainViewModel.ConversationEngine.IsWaitingForAnswer;
        }
        #endregion

        #region Props
        [WireUp]
        public MainViewModel MainViewModel { get; set; }
        [WireUp]
        public SpeechRecognizer SpeechRecognizer { get; set; }
        #endregion

        #region Execute
        public void Execute(object parameter = null)
        {
            SpeechRecognizer.Recognize();
        }
        #endregion
    }
}