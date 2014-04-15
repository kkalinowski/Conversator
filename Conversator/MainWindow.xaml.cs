using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace Conversator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            tUserText.Focus();
        }

        private void TUserText_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            tUserText.Focus();
        }
    }
}
