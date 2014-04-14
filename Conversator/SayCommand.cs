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

        public virtual bool CanExecute(object parameter)
        {
            return MainViewModel.UserText.IsNotNullAndNotEmpty();
        }
        #endregion

        #region Props
        [WireUp]
        public MainViewModel MainViewModel { get; set; }
        #endregion

        #region Execute
        public void Execute(object parameter)
        {
            MainViewModel.Text = MainViewModel.ConversationEngine.Say(MainViewModel.UserText);
            MainViewModel.UserText = string.Empty;
        }
        #endregion
    }
}