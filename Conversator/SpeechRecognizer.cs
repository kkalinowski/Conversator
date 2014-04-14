using System;
using System.Globalization;
using System.Speech.Recognition;
using System.Windows;
using lib12.DependencyInjection;

namespace Conversator
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
            var dictation = new DictationGrammar();
            dictation.Name = "Dictation Grammar";
            recognitionEngine.LoadGrammar(dictation);
        }

        public void Recognize()
        {
            recognitionEngine.RecognizeAsync();
        }

        public void SubscribeForRecognizedText(EventHandler<RecognizeCompletedEventArgs> action)
        {
            recognitionEngine.RecognizeCompleted += action;
        }
    }
}