using System;
using System.Globalization;
using System.Speech.Recognition;
using lib12.DependencyInjection;

namespace Conversator.Logic
{
    [Singleton]
    public class SpeechRecognizer
    {
        private readonly SpeechRecognitionEngine recognitionEngine;

        public SpeechRecognizer()
        {
            recognitionEngine = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            recognitionEngine.SetInputToDefaultAudioDevice();
            LoadGrammars();
        }

        private void LoadGrammars()
        {
            var dictation = new DictationGrammar { Name = "Dictation Grammar" };
            recognitionEngine.LoadGrammar(dictation);
        }

        public void Recognize()
        {
            try
            {
                recognitionEngine.RecognizeAsync();
            }
            catch
            {

            }
        }

        public void SubscribeForRecognizedText(EventHandler<RecognizeCompletedEventArgs> action)
        {
            recognitionEngine.RecognizeCompleted += action;
        }
    }
}