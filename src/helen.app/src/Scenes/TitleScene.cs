using Helen.App.Extensions;
using Helen.App.Services;
using Helen.App.Textual;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Helen.App.Scenes
{
    public class TitleScene : Scene
    {
        #region Private Members

        #region Entities

        protected Text Title;

        protected Text Option1;
        protected Text Option2;
        protected Text Option3;
        protected Text Option4;

        protected Text OptionDot1;
        protected Text OptionDot2;
        protected Text OptionDot3;
        protected Text OptionDot4;

        protected Text Credit;

        #endregion

        #region Progression Fields

        protected byte a = 0;
        protected byte b = 0;
        protected byte c = 0;

        #endregion

        #endregion

        #region Properties

        public override Drawable[] Drawables => _drawables;
        private readonly Drawable[] _drawables;

        #endregion

        #region Constructors

        public TitleScene(RenderWindow window) : base(window)
        {
            _drawables = new Drawable[]
            {
                Title,
                Option1,
                Option2,
                Option3,
                Option4,
                OptionDot1,
                OptionDot2,
                OptionDot3,
                OptionDot4,
                Credit,
            };
        }

        #endregion

        #region Methods

        public override void Init()
        {
            Title = new Text("Helen", Fonts.FontTitle, 120);
            Title.Position = new Vector2f(200f, 100f);

            Option1 = new Text("New",      Fonts.FontBody, 40);
            Option2 = new Text("Load",     Fonts.FontBody, 40);
            Option3 = new Text("Settings", Fonts.FontBody, 40);
            Option4 = new Text("Quit",     Fonts.FontBody, 40);
            Option1.Position = new Vector2f(300f, 300f);
            Option2.Position = new Vector2f(300f, 350f);
            Option3.Position = new Vector2f(300f, 400f);
            Option4.Position = new Vector2f(300f, 450f);
            OptionDot1 = new Text("", Fonts.FontTitle, 40);
            OptionDot2 = new Text("", Fonts.FontTitle, 40);
            OptionDot3 = new Text("", Fonts.FontTitle, 40);
            OptionDot4 = new Text("", Fonts.FontTitle, 40);
            OptionDot1.Position = new Vector2f(250f, 300f);
            OptionDot2.Position = new Vector2f(250f, 350f);
            OptionDot3.Position = new Vector2f(250f, 400f);
            OptionDot4.Position = new Vector2f(250f, 450f);

            Credit = new Text($"{Program.Version} - {Program.Footer}", Fonts.FontCredit, 18);
            Credit.Position = new Vector2f(20f, 575f);
        }

        public override Scene Open()
        {
            base.Open();
            MusicService.Swap(Tracks.Title);
            return this;
        }

        public override void Close()
        {
            base.Close();
        }

        public override void Progress()
        {
            if (a < 255) a++;
            if (a >= 100 && b < 255) b++;
            if (b >= 100 && c < 255) c++;
            Title.FillColor = new Color(255, 255, 255, a);
            Option1.FillColor = new Color(255, 255, 255, b);
            Option2.FillColor = new Color(255, 255, 255, b);
            Option3.FillColor = new Color(255, 255, 255, b);
            Option4.FillColor = new Color(255, 255, 255, b);
            OptionDot1.FillColor = new Color(255, 255, 255, b);
            OptionDot2.FillColor = new Color(255, 255, 255, b);
            OptionDot3.FillColor = new Color(255, 255, 255, b);
            OptionDot4.FillColor = new Color(255, 255, 255, b);
            Credit.FillColor = new Color(255, 255, 255, c);

            var mousePosition = Mouse.GetPosition(_window);
            OptionDot1.Indicate(Option1, mousePosition);
            OptionDot2.Indicate(Option2, mousePosition);
            OptionDot3.Indicate(Option3, mousePosition);
            OptionDot4.Indicate(Option4, mousePosition);
        }

        #endregion

        #region Event Handlers

        protected override void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = sender as Window;
            switch (e.Code)
            {
                case Keyboard.Key.Escape: Program.Close();
                    break;
            }
        }

        protected override void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:   OnMouseButtonPressed(e); break;
                case Mouse.Button.Right:  OnMouseButtonPressed(e); break;
                case Mouse.Button.Middle: OnMouseButtonPressed(e); break;
            }
        }

        private void OnMouseButtonPressed(MouseButtonEventArgs e)
        {
            if      (Option1.Contains(e.X, e.Y))
            {
                Option1.Style = Text.Styles.Underlined;
                OptionDot1.DisplayedString = "x";
            }
            else if (Option2.Contains(e.X, e.Y))
            {
                Option2.Style = Text.Styles.Underlined;
                OptionDot2.DisplayedString = "x";
            }
            else if (Option3.Contains(e.X, e.Y))
            {
                Option3.Style = Text.Styles.Underlined;
                OptionDot3.DisplayedString = "x";
            }
            else if (Option4.Contains(e.X, e.Y))
            {
                Option4.Style = Text.Styles.Underlined;
                OptionDot4.DisplayedString = "x";
            }
        }

        protected override void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:   OnMouseButtonReleased(e); break;
                case Mouse.Button.Right:  OnMouseButtonReleased(e); break;
                case Mouse.Button.Middle: OnMouseButtonReleased(e); break;
            }
        }

        private void OnMouseButtonReleased(MouseButtonEventArgs e)
        {
            Option1.Style = Text.Styles.Regular;
            Option2.Style = Text.Styles.Regular;
            Option3.Style = Text.Styles.Regular;
            Option4.Style = Text.Styles.Regular;
            OptionDot1.DisplayedString = "";
            OptionDot2.DisplayedString = "";
            OptionDot3.DisplayedString = "";
            OptionDot4.DisplayedString = "";

            if      (Option1.Contains(e.X, e.Y))
            {
            }
            else if (Option2.Contains(e.X, e.Y))
            {
            }
            else if (Option3.Contains(e.X, e.Y))
            {
                Program.Navigate(SceneService.SettingsScene);
            }
            else if (Option4.Contains(e.X, e.Y))
            {
                Program.Close();
            }
        }

        #endregion
    }
}
