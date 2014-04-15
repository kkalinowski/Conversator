using System;
using System.Windows;
using System.Windows.Input;
using lib12.DependencyInjection;

namespace Conversator.Commands
{
    [Singleton]
    public class InfoCommand : ICommand
    {
        #region CanExecute
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual bool CanExecute(object parameter = null)
        {
            return true;
        }
        #endregion

        #region Execute
        public void Execute(object parameter = null)
        {
            MessageBox.Show("Application developed by Krzysztof Kalinowski - kkalinowski.net. Logic by www.cleverbot.com, speech by Microsoft.Speech library, look by MahApps", "Info");
        }
        #endregion
    }
}