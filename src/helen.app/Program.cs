using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace helen.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var mode = new VideoMode(800, 600);
            var window = new RenderWindow(mode, "Helen");
            window.Closed += (x, y) => window.Close();
            window.KeyPressed += OnKeyPressed;

            var texture = new Texture("res/gfx/title.png");
            var sprite = new Sprite(texture);

            Music music = new Music("res/sfx/fts8b.wav");
            music.Play();

            while (window.IsOpen)
            {
                //while (window.Po)
                // Process events.
                window.DispatchEvents();
                window.Clear(Color.Black);
                window.Draw(sprite);
                window.Display();
            }
        }

        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = sender as Window;
            switch (e.Code)
            {
                case Keyboard.Key.Escape: window.Close();
                    break;
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
