using Helen.App.Extensions;
using Helen.App.Models;
using Helen.App.Scenes;
using Helen.App.Services;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Helen.App
{
    public class Program
    {
        #region Properties

        public static Scene CurrentScene { get; set; }

        private static RenderWindow Window;

        private static object _lock = new object();

        #endregion

        static void Main(string[] args)
        {
            Options.Init();

            var mode = new VideoMode(800, 600);
            Window = new RenderWindow(mode, "YeomanSaga");
            Window.Closed += (x, y) => Close();
            Window.SetVerticalSyncEnabled(Options.Instance.Vsync);

            // Services.
            SceneService.Init(Window);

            //var texture = new Texture("res/gfx/title.png");
            //var sprite = new Sprite(texture);

            Fonts.Init();
            Navigate(SceneService.TitleScene);

            while (Window.IsOpen)
            {
                lock (_lock)
                {
                    CurrentScene.Progress();
                    Window.DispatchEvents();
                    Window.Clear(Color.Black);
                    Window.Draw(CurrentScene);
                    Window.Display();
                }
            }
        }

        public static void Navigate(Scene scene)
        {
            lock (_lock)
            {
                CurrentScene?.Close();
                CurrentScene = scene.Open();
            }
        }

        public static void Close()
        {
            CurrentScene?.Music?.Stop();
            Window.Close();
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
