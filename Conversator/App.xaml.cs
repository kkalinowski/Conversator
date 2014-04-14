using System;
using System.Windows;
using System.Windows.Threading;

namespace Conversator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            DealWithUnhadledException(e.ExceptionObject as Exception);
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DealWithUnhadledException(e.Exception);
        }

        private static void DealWithUnhadledException(Exception ex)
        {
            MessageBox.Show("Something very bad happend... Please send screenshot from this error to kkalinowski@outlook.com " + ex.Message, "Error");
        }
    }
}
