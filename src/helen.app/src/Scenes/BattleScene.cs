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

        protected Texture BorderTexture;
        protected Texture PortraitTexture;
        protected Texture EnemyTexture;
        protected Texture AlliedTexture;
        protected Texture BgTexture;
        protected Texture CharacterTexture;

        protected Sprite Background;
        protected RectangleShape TopInfoArea;
        protected RectangleShape BotInfoArea;
        protected Sprite TopBorder;
        protected Sprite BotBorder;
        protected Sprite EnemyPortrait;
        protected Sprite AlliedPortrait;
        protected Sprite EnemyBorder;
        protected Sprite AlliedBorder;
        protected Sprite EnemyCharacter;
        protected Sprite AlliedCharacter;

        protected Text EnemyActorName;
        protected Text EnemyActorStatus;
        protected Text EnemyActorAction;
        protected Text EnemyActorEffects;
        protected Text AlliedActorName;
        protected Text AlliedActorStatus;
        protected Text AlliedActorAction;
        protected Text AlliedActorEffects;

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
                Background,
                TopInfoArea,
                TopBorder,
                BotInfoArea,
                BotBorder,
                EnemyBorder,
                AlliedBorder,
                EnemyPortrait,
                AlliedPortrait,
                EnemyCharacter,
                AlliedCharacter,
                EnemyActorName,
                EnemyActorStatus,
                EnemyActorAction,
                EnemyActorEffects,
                AlliedActorName,
                AlliedActorStatus,
                AlliedActorAction,
                AlliedActorEffects,
            };
        }

        #endregion

        #region Methods

        public override void Init()
        {
            BorderTexture = new Texture("res/gfx/self/border.png") { Repeated = true };
            PortraitTexture = new Texture("res/gfx/self/portrait_border.png");
            EnemyTexture  = new Texture("res/gfx/wl/antagonist.png");
            AlliedTexture = new Texture("res/gfx/wl/protagonist.png");
            BgTexture = new Texture("res/gfx/parallax/forest.png");
            CharacterTexture = new Texture("res/gfx/josephseraph/heroes.png");

            Background = new Sprite(BgTexture);
            Background.Scale = new Vector2f(3f, 3f);
            Background.Position = new Vector2f(0f, 10f);

            TopInfoArea = new RectangleShape(new Vector2f(2000f, 100f));
            TopInfoArea.Position = new Vector2f(0f, 0f);
            TopInfoArea.FillColor = new Color(50, 50, 50);
            BotInfoArea = new RectangleShape(new Vector2f(2000f, 110f));
            BotInfoArea.Position = new Vector2f(0f, SceneService.WindowHeight - 110);
            BotInfoArea.FillColor = new Color(50, 50, 50);

            TopBorder = new Sprite(BorderTexture, new IntRect(0, 0, 2000, 11));
            TopBorder.Position = new Vector2f(0f, 100f);
            BotBorder = new Sprite(BorderTexture, new IntRect(0, 0, 2000, 11));
            BotBorder.Position = new Vector2f(0f, SceneService.WindowHeight - 110);

            EnemyBorder = new Sprite(PortraitTexture);
            EnemyBorder.Scale = new Vector2f(.25f, .25f);
            EnemyBorder.Position = new Vector2f(7f, 7f);
            AlliedBorder = new Sprite(PortraitTexture);
            AlliedBorder.Scale = new Vector2f(.25f, .25f);
            AlliedBorder.Position = new Vector2f(7f, SceneService.WindowHeight - 87);

            EnemyPortrait = new Sprite(EnemyTexture);
            EnemyPortrait.Scale = new Vector2f(.25f, .25f);
            EnemyPortrait.Position = new Vector2f(9.5f, 9f);
            AlliedPortrait = new Sprite(AlliedTexture);
            AlliedPortrait.Scale = new Vector2f(.25f, .25f);
            AlliedPortrait.Position = new Vector2f(10.5f, SceneService.WindowHeight - 85);

            EnemyCharacter = new Sprite(CharacterTexture, new IntRect(23, 33, 22, 32));
            EnemyCharacter.Scale = new Vector2f(3f, 3f);
            EnemyCharacter.Position = new Vector2f(150f, 390f);
            AlliedCharacter = new Sprite(CharacterTexture, new IntRect(67, 97, 22, 32));
            AlliedCharacter.Scale = new Vector2f(3f, 3f);
            AlliedCharacter.Position = new Vector2f(550f, 390f);

            EnemyActorName = new Text("Antagonist", Fonts.FontBody, 24);
            EnemyActorName.Position = new Vector2f(100f, 30f);
            EnemyActorName.FillColor = new Color(255, 255, 255);
            EnemyActorStatus = new Text("HP: 100 of 100", Fonts.FontBody, 24);
            EnemyActorStatus.Position = new Vector2f(300f, 30f);
            EnemyActorStatus.FillColor = new Color(255, 255, 255);
            EnemyActorAction = new Text("Great Swing", Fonts.FontBody, 24);
            EnemyActorAction.Position = new Vector2f(100f, 60f);
            EnemyActorAction.FillColor = new Color(255, 255, 255);
            EnemyActorEffects = new Text("Eff: 34   Def:  5   Wait: 10", Fonts.FontBody, 24);
            EnemyActorEffects.Position = new Vector2f(300f, 60f);
            EnemyActorEffects.FillColor = new Color(255, 255, 255);

            AlliedActorName = new Text("Protagonist", Fonts.FontBody, 24);
            AlliedActorName.Position = new Vector2f(100f, SceneService.WindowHeight - 65);
            AlliedActorName.FillColor = new Color(255, 255, 255);
            AlliedActorStatus = new Text("HP:  80 of 100", Fonts.FontBody, 24);
            AlliedActorStatus.Position = new Vector2f(300f, SceneService.WindowHeight - 65);
            AlliedActorStatus.FillColor = new Color(255, 255, 255);
            AlliedActorAction = new Text("Flamberge", Fonts.FontBody, 24);
            AlliedActorAction.Position = new Vector2f(100f, SceneService.WindowHeight - 35);
            AlliedActorAction.FillColor = new Color(255, 255, 255);
            AlliedActorEffects = new Text("Eff: 56   Def: 15   Wait:  5", Fonts.FontBody, 24);
            AlliedActorEffects.Position = new Vector2f(300f, SceneService.WindowHeight - 35);
            AlliedActorEffects.FillColor = new Color(255, 255, 255);
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
        }

        #endregion
    }
}
