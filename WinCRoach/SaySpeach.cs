using System;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinCRoach
{
    internal static class SaySpeach
    {
        internal static void Say(string say)
        {
            var synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
            if (string.IsNullOrWhiteSpace(say))
            {
                say = "All we need to do is to make sure we keep talking";
            }
            synthesizer.Speak(say);

            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                var info = voice.VoiceInfo;
                Console.WriteLine($"Id: {info.Id} | Name: {info.Name} |Age: {info.Age} | Gender: {info.Gender} | Culture: {info.Culture}");
            }
            // Console.ReadKey();
        }
    }
}
