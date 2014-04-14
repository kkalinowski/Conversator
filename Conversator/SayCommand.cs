using lib12.Collections;
using lib12.DependencyInjection;
using System;
using System.Windows.Input;

namespace Conversator
{
    [Singleton]
    public class SayCommand : ICommand
    {
        #region CanExecute
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual bool CanExecute(object parameter = null)
        {
            return MainViewModel.UserText.IsNotNullAndNotEmpty()
                && !MainViewModel.ConversationEngine.IsWaitingForAnswer;
        }
        #endregion

        #region Props
        [WireUp]
        public MainViewModel MainViewModel { get; set; }
        #endregion

        #region Execute
        public void Execute(object parameter = null)
        {
            MainViewModel.ConversationEngine.Say(MainViewModel.UserText);
            MainViewModel.UserText = string.Empty;
        }
        #endregion
    }
}