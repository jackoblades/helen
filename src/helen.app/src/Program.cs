using Helen.App.Extensions;
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
            window.KeyPressed += OnKeyPressed;
            window.SetVerticalSyncEnabled(true);

            //var texture = new Texture("res/gfx/title.png");
            //var sprite = new Sprite(texture);

            Fonts.Init();

            var title = new Text("YeomanSaga", Fonts.FontTitle, 100);
            title.Position = new Vector2f(40f, 100f);

            var option1 = new Text("New", Fonts.FontBody, 40);
            var option2 = new Text("Load", Fonts.FontBody, 40);
            var option3 = new Text("Options", Fonts.FontBody, 40);
            var option4 = new Text("Quit", Fonts.FontBody, 40);
            option1.Position = new Vector2f(300f, 300f);
            option2.Position = new Vector2f(300f, 350f);
            option3.Position = new Vector2f(300f, 400f);
            option4.Position = new Vector2f(300f, 450f);
            var optionDot1 = new Text("", Fonts.FontTitle, 40);
            var optionDot2 = new Text("", Fonts.FontTitle, 40);
            var optionDot3 = new Text("", Fonts.FontTitle, 40);
            var optionDot4 = new Text("", Fonts.FontTitle, 40);
            optionDot1.Position = new Vector2f(250f, 300f);
            optionDot2.Position = new Vector2f(250f, 350f);
            optionDot3.Position = new Vector2f(250f, 400f);
            optionDot4.Position = new Vector2f(250f, 450f);

            var credit = new Text(@"https://github.com/jackoblades 🄯 2019 - 2020 Anno Domini", Fonts.FontCredit, 18);
            credit.Position = new Vector2f(20f, 700f);

            byte a = 0;
            byte b = 0;
            byte c = 0;

            Music music = new Music("res/sfx/fts8b.wav");
            music.Play();

            while (window.IsOpen)
            {
                if (a < 255) a++;
                if (a >= 100 && b < 255) b++;
                if (b >= 100 && c < 255) c++;
                title.FillColor = new Color(255, 255, 255, a);
                option1.FillColor = new Color(255, 255, 255, b);
                option2.FillColor = new Color(255, 255, 255, b);
                option3.FillColor = new Color(255, 255, 255, b);
                option4.FillColor = new Color(255, 255, 255, b);
                optionDot1.FillColor = new Color(255, 255, 255, b);
                optionDot2.FillColor = new Color(255, 255, 255, b);
                optionDot3.FillColor = new Color(255, 255, 255, b);
                optionDot4.FillColor = new Color(255, 255, 255, b);
                credit.FillColor = new Color(255, 255, 255, c);

                var mousePosition = Mouse.GetPosition(window);
                optionDot1.DisplayedString = option1.Contains(mousePosition) ? "o" : "";
                optionDot2.DisplayedString = option2.Contains(mousePosition) ? "o" : "";
                optionDot3.DisplayedString = option3.Contains(mousePosition) ? "o" : "";
                optionDot4.DisplayedString = option4.Contains(mousePosition) ? "o" : "";

                window.DispatchEvents();
                window.Clear(Color.Black);
                window.Draw(title);
                window.Draw(option1);
                window.Draw(option2);
                window.Draw(option3);
                window.Draw(option4);
                window.Draw(optionDot1);
                window.Draw(optionDot2);
                window.Draw(optionDot3);
                window.Draw(optionDot4);
                window.Draw(credit);
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
