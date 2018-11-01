using System;
using System.Collections.Generic;
using System.Text;
using AVFoundation;
using QuizApp.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(TextSpeecher))]
namespace QuizApp.iOS
{
    public class TextSpeecher : Interfaces.ITextSpeecher
    {
        public TextSpeecher() { }

        public void Speak(string text)
        {
            var speechSynthesizer = new AVSpeechSynthesizer();
            var speechUtterance = new AVSpeechUtterance(text)
            {
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-GB"),
                Volume = 0.5f,
                PitchMultiplier = 1.0f
            };

            speechSynthesizer.SpeakUtterance(speechUtterance);
        }
    }
}
