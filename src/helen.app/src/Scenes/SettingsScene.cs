using Helen.App.Extensions;
using Helen.App.Models;
using Helen.App.Repository.Charters;
using Helen.App.Services;
using Helen.App.Textual;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Threading.Tasks;

namespace Helen.App.Scenes
{
    public class SettingsScene : Scene
    {
        #region Private Members

        #region Entities

        protected Text Title;

        protected Text MusicVolume;
        private string MusicVolumeText => $"Music Volume:  {Settings.Instance.MusicVolumeSafe}";
        protected Text MusicDecrease;
        protected Text MusicIncrease;

        protected Text Vsync;
        private string VsyncText => (Settings.Instance.Vsync) ? "VSync:  On" : "VSync:  Off";

        protected Text Save;
        protected Text SaveDot;

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
                MusicVolume,
                MusicDecrease,
                MusicIncrease,
                Vsync,
                Save,
                SaveDot,
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

            MusicVolume = new Text(MusicVolumeText, Fonts.FontBody, 40);
            MusicVolume.Position = new Vector2f(150f, 250f);

            MusicDecrease = new Text("<", Fonts.FontUnicode, 40);
            MusicDecrease.Position = new Vector2f(480f, 250f);
            MusicIncrease = new Text(">", Fonts.FontUnicode, 40);
            MusicIncrease.Position = new Vector2f(520f, 250f);

            Vsync = new Text(VsyncText, Fonts.FontBody, 40);
            Vsync.Position = new Vector2f(150f, 300f);

            Save = new Text("Save & Exit", Fonts.FontBody, 40);
            Save.Position = new Vector2f(400f, 450f);
            SaveDot = new Text("", Fonts.FontTitle, 40);
            SaveDot.Position = new Vector2f(350f, 450f);

            Back = new Text("Cancel", Fonts.FontBody, 40);
            Back.Position = new Vector2f(100f, 450f);
            BackDot = new Text("", Fonts.FontTitle, 40);
            BackDot.Position = new Vector2f(50f, 450f);

            Credit = new Text($"{Program.Version} - {Program.Footer}", Fonts.FontCredit, 18);
            Credit.Position = new Vector2f(20f, 575f);
        }

        public override Scene Open()
        {
            base.Open();
            Settings.Backup = new Settings(Settings.Instance);
            MusicVolume.DisplayedString = MusicVolumeText;
            Vsync.DisplayedString = VsyncText;
            return this;
        }

        public override void Close()
        {
            base.Close();
        }

        public override void Progress()
        {
            var mousePosition = Mouse.GetPosition(_window);
            SaveDot.Indicate(Save, mousePosition);
            BackDot.Indicate(Back, mousePosition);
            Vsync.Indicate(mousePosition, Text.Styles.Underlined);
            MusicDecrease.Indicate(mousePosition, Text.Styles.Bold);
            MusicIncrease.Indicate(mousePosition, Text.Styles.Bold);
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
            if (MusicDecrease.Contains(e.X, e.Y))
            {
                MusicDecrease.Style = MusicDecrease.Style.TurnOn(Text.Styles.Underlined);
            }
            if (MusicIncrease.Contains(e.X, e.Y))
            {
                MusicIncrease.Style = MusicIncrease.Style.TurnOn(Text.Styles.Underlined);
            }
            if (Save.Contains(e.X, e.Y))
            {
                Save.Style = Text.Styles.Underlined;
                SaveDot.DisplayedString = "x";
            }
            if (Back.Contains(e.X, e.Y))
            {
                Back.Style = Text.Styles.Underlined;
                BackDot.DisplayedString = "x";
            }
        }

        protected override async void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:   await OnMouseButtonReleased(e); break;
                case Mouse.Button.Right:  await OnMouseButtonReleased(e); break;
                case Mouse.Button.Middle: await OnMouseButtonReleased(e); break;
            }
        }

        private async Task OnMouseButtonReleased(MouseButtonEventArgs e)
        {
            MusicDecrease.Style = MusicDecrease.Style.TurnOff(Text.Styles.Underlined);
            MusicIncrease.Style = MusicIncrease.Style.TurnOff(Text.Styles.Underlined);
            Save.Style = Text.Styles.Regular;
            Back.Style = Text.Styles.Regular;
            SaveDot.DisplayedString = "";
            BackDot.DisplayedString = "";

            if (MusicDecrease.Contains(e.X, e.Y))
            {
                Settings.Instance.MusicVolumeSafe -= 5;
                MusicVolume.DisplayedString = MusicVolumeText;
                MusicService.Music.Volume = Settings.Instance.MusicVolumeSafe;
            }
            if (MusicIncrease.Contains(e.X, e.Y))
            {
                Settings.Instance.MusicVolumeSafe += 5;
                MusicVolume.DisplayedString = MusicVolumeText;
                MusicService.Music.Volume = Settings.Instance.MusicVolumeSafe;
            }
            if (Vsync.Contains(e.X, e.Y))
            {
                Settings.Instance.Toggle(Preferences.Vsync);
                Vsync.DisplayedString = VsyncText;
                _window.SetVerticalSyncEnabled(Settings.Instance.Vsync);
            }
            if (Save.Contains(e.X, e.Y))
            {
                await Settings.SaveAsync();
                Program.Navigate(SceneService.TitleScene);
            }
            if (Back.Contains(e.X, e.Y))
            {
                ResetSettings();
                Program.Navigate(SceneService.TitleScene);
            }
        }

        private void ResetSettings()
        {
            Settings.Instance = Settings.Backup;
            _window.SetVerticalSyncEnabled(Settings.Instance.Vsync);
        }

        #endregion
    }
}
