using Helen.App.Extensions;
using Helen.App.Scenes;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Helen.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var mode = new VideoMode(800, 600);
            var window = new RenderWindow(mode, "YeomanSaga");
            window.Closed += (x, y) => window.Close();
            window.SetVerticalSyncEnabled(true);

            //var texture = new Texture("res/gfx/title.png");
            //var sprite = new Sprite(texture);

            Fonts.Init();
            Scene currentScene = new TitleScene(window).Open();

            while (window.IsOpen)
            {
                currentScene.Progress();
                window.DispatchEvents();
                window.Clear(Color.Black);
                window.Draw(currentScene);
                window.Display();
            }
        }

        private static SoundBuffer GenerateSineWave(double frequency, double volume, int seconds)
        {
            uint sampleRate = 44100;
            var samples = new short[seconds * sampleRate];

            for (int i = 0; i < samples.Length; i++)
                samples[i] = (short)(Math.Sin(frequency * (2 * Math.PI) * i / sampleRate) * volume * short.MaxValue);

            return new SoundBuffer(samples, 1, sampleRate);
        }
    }
}
