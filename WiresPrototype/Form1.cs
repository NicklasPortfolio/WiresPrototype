using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace WiresPrototype
{
    public partial class Form1 : Form
    {
        static string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
        static string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

        static void SpeechResult(SpeechRecognitionResult result)
        {
            switch (result.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    break;
                case ResultReason.NoMatch:
                    SpeechOut("Speech could not be recognized");
                    break;
            }
        }

        static async void SpeechOut(string line)
        {
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            speechConfig.SpeechSynthesisVoiceName = "en-US-JasonNeural";

            using (var speechSynthezier = new SpeechSynthesizer(speechConfig))
            {
                await speechSynthezier.SpeakTextAsync(line);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            SpeechOut("What are the colors of the wires and the last digit of the serial number?");

            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            SpeechResult(speechRecognitionResult);
            WiresLogic(speechRecognitionResult.Text);
        }

        private void WiresLogic(string speech)
        {
            Wires wires = new Wires(speech);

            switch (wires.Colors.Count)
            {
                case 3:
                    if (!wires.Colors.Any(color => color.Equals("red"))) {
                        SpeechOut("Cut the second wire.");
                    }

                    else if (wires.Colors.Last() == "white")
                    {
                        SpeechOut("Cut the last wire.");
                    }

                    else if (wires.Colors.Count(color => color.Equals("blue")) > 1)
                    {
                        SpeechOut("Cut the last blue wire.");
                    }

                    else { SpeechOut("Cut the last wire."); }
                    break;

                case 4:
                    if (wires.Colors.Count(color => color.Equals("red")) > 1 && wires.Digit % 2 == 1) {
                        SpeechOut("Cut the last red wire.");
                    }

                    else if (wires.Colors.Last() == "yellow" && !wires.Colors.Any(color => color.Equals("red")))
                    {
                        SpeechOut("Cut the last wire.");
                    }

                    else if (wires.Colors.Count(color => color.Equals("blue")) == 1)
                    {
                        SpeechOut("Cut the first wire.");
                    }

                    else if (wires.Colors.Count(color => color.Equals("yellow")) > 1)
                    {
                        SpeechOut("Cut the last wire.");
                    }

                    else { SpeechOut("Cut the second wire."); }
                    break;
            }
        }
    }
}
