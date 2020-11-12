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
    public class BattleScene : Scene
    {
        #region Private Members

        #region Entities

        protected RectangleShape TopInfoArea;
        protected RectangleShape BotInfoArea;

        protected RectangleShape TopInfoBorder;
        protected RectangleShape BotInfoBorder;

        protected Text EnemyActorStatus1;
        protected Text EnemyActorStatus2;
        protected Text AlliedActorStatus1;
        protected Text AlliedActorStatus2;

        #endregion

        #region Progression Fields

        #endregion

        #endregion

        #region Properties

        public override Drawable[] Drawables => _drawables;
        private readonly Drawable[] _drawables;

        #endregion

        #region Constructors

        public BattleScene(RenderWindow window) : base(window)
        {
            _drawables = new Drawable[]
            {
                TopInfoArea,
                BotInfoArea,
                TopInfoBorder,
                BotInfoBorder,
                //EnemyActorStatus1,
                //EnemyActorStatus2,
                //AlliedActorStatus1,
                //AlliedActorStatus2,
            };
        }

        #endregion

        #region Methods

        public override void Init()
        {
            TopInfoArea = new RectangleShape(new Vector2f(2000f, 100f));
            TopInfoArea.Position = new Vector2f(0f, 0f);
            TopInfoArea.FillColor = new Color(50, 50, 50);
            BotInfoArea = new RectangleShape(new Vector2f(2000f, 200f));
            BotInfoArea.Position = new Vector2f(0f, 600f);
            BotInfoArea.FillColor = new Color(50, 50, 50);

            TopInfoBorder = new RectangleShape(new Vector2f(2000f, 2f));
            TopInfoBorder.Position = new Vector2f(0f, 98f);
            TopInfoBorder.FillColor = new Color(250, 250, 250);
            BotInfoBorder = new RectangleShape(new Vector2f(2000f, 2f));
            BotInfoBorder.Position = new Vector2f(0f, 600f);
            BotInfoBorder.FillColor = new Color(250, 250, 250);
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
        }

        protected override async void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
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
        }

        #endregion
    }
}
