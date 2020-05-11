using Helen.App.Extensions;
using Helen.App.Models;
using Helen.App.Services;
using Helen.App.Textual;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Helen.App.Scenes
{
    public class SettingsScene : Scene
    {
        #region Private Members

        #region Entities

        protected Text Title;

        protected Text VsyncIndicator;
        protected Text VsyncDescription;

        protected Text Back;
        protected Text BackDot;

        protected Text Credit;

        #endregion

        #region Progression Fields

        #endregion

        #endregion

        #region Properties

        public override Drawable[] Drawables => _drawables;
        private readonly Drawable[] _drawables;

        #endregion

        #region Constructors

        public SettingsScene(RenderWindow window) : base(window)
        {
            _drawables = new Drawable[]
            {
                Title,
                VsyncIndicator,
                VsyncDescription,
                Back,
                BackDot,
                Credit,
            };
        }

        #endregion

        #region Methods

        public override void Init()
        {
            Title = new Text("Settings", Fonts.FontTitle, 60);
            Title.Position = new Vector2f(100f, 50f);

            VsyncIndicator = new Text((Settings.Instance.Vsync) ? Glyphs.ToggleOn : Glyphs.ToggleOff, Fonts.FontUnicode, 40);
            VsyncIndicator.Position = new Vector2f(250f, 300f);
            VsyncDescription = new Text("VSync", Fonts.FontBody, 40);
            VsyncDescription.Position = new Vector2f(300f, 300f);

            Back = new Text("Back", Fonts.FontBody, 40);
            Back.Position = new Vector2f(300f, 450f);
            BackDot = new Text("", Fonts.FontTitle, 40);
            BackDot.Position = new Vector2f(250f, 450f);

            Credit = new Text($"{Program.Version} - {Program.Footer}", Fonts.FontCredit, 18);
            Credit.Position = new Vector2f(20f, 575f);
        }

        public override Scene Open()
        {
            return base.Open();
        }

        public override void Close()
        {
            base.Close();
        }

        public override void Progress()
        {
            var mousePosition = Mouse.GetPosition(_window);
            BackDot.Indicate(Back, mousePosition);
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
            if (VsyncDescription.Contains(e.X, e.Y)
            ||  VsyncIndicator.Contains(e.X, e.Y))
            {
                VsyncDescription.Style = Text.Styles.Underlined;
            }
            if (Back.Contains(e.X, e.Y))
            {
                Back.Style = Text.Styles.Underlined;
                BackDot.DisplayedString = "x";
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
            VsyncDescription.Style = Text.Styles.Regular;
            Back.Style             = Text.Styles.Regular;
            BackDot.DisplayedString = "";

            if (VsyncDescription.Contains(e.X, e.Y)
            ||  VsyncIndicator.Contains(e.X, e.Y))
            {
                Settings.Instance.Toggle(Preferences.Vsync);
                _window.SetVerticalSyncEnabled(Settings.Instance.Vsync);
                VsyncIndicator.DisplayedString = (Settings.Instance.Vsync) ? Glyphs.ToggleOn : Glyphs.ToggleOff;
            }
            if (Back.Contains(e.X, e.Y))
            {
                Program.Navigate(SceneService.TitleScene);
            }
        }

        #endregion
    }
}
