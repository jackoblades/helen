using Helen.App.Extensions;
using Helen.App.Models;
using Helen.App.Repository;
using Helen.App.Scenes;
using Helen.App.Services;
using Helen.App.Textual;
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

        public static readonly string Version = "alpha";

        public static readonly string Footer = @"https://github.com/jackoblades 🄯 2019 - 2020 Anno Domini";

        public static Scene CurrentScene { get; set; }

        private static RenderWindow Window;

        private static object _lock = new object();

        #endregion

        static void Main(string[] args)
        {
            Orm.Instance.InitAsync().Wait();
            Settings.Init();
            MusicService.Init();

            var mode = new VideoMode(800, 600);
            Window = new RenderWindow(mode, "Helen");
            Window.Closed += (x, y) => Close();
            Window.SetVerticalSyncEnabled(Settings.Instance.Vsync);

            // Services.
            SceneService.Init(Window);

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
            MusicService.Close();
            Window.Close();
        }
    }
}
