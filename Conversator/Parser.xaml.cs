using lib12.DependencyInjection;
using System.Windows.Controls;

namespace Conversator
{
    /// <summary>
    /// Interaction logic for Parser.xaml
    /// </summary>
    public partial class Parser : UserControl
    {
        private ConversationEngine engine;

        public Parser()
        {
            InitializeComponent();
            engine = Instances.Get<ConversationEngine>();
            host.Child = engine.Browser;
        }
    }
}
