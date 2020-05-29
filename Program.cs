using System;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace SpeechSynth
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the file as one string.
            string text = File.ReadAllText(@"Please input the location of the text file");

            // Initialize a new instance of the SpeechSynthesizer.  
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SelectVoiceByHints(VoiceGender.Female);
                Console.WriteLine("Creating voice file...");
                SaveTextToSpeech("title", synth, text);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void SaveTextToSpeech(string name, SpeechSynthesizer synth, string content)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string savePath = $@"{desktop}\{name}.wav";
            // Configure the audio output.   
            synth.SetOutputToWaveFile(savePath,
              new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));

            // Create a SoundPlayer instance to play output audio file.  
            System.Media.SoundPlayer m_SoundPlayer =
              new System.Media.SoundPlayer(savePath);

            // Build a prompt.  
            PromptBuilder builder = new PromptBuilder();
            builder.AppendText(content);

            // Speak the prompt.  
            synth.Speak(builder);
            m_SoundPlayer.Play();
        }

    }
}
