using lib12.DependencyInjection;

namespace Conversator
{
    [Singleton]
    public class ConversationEngine
    {
        public string Say(string text)
        {
            return text + text;
        }
    }
}