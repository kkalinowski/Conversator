using System.Speech.Synthesis;
using lib12.DependencyInjection;

namespace Conversator.Logic
{
    [Singleton]
    public class TextSynthesizer
    {
        private readonly SpeechSynthesizer synthesizer;

        public TextSynthesizer()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
        }

        public void Speak(string text)
        {
            synthesizer.SpeakAsync(text);
        }
    }
}