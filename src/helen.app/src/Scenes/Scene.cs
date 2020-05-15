using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

namespace Helen.App.Scenes
{
    public abstract class Scene
    {
        #region Private Members

        protected RenderWindow _window;

        #endregion

        #region Properties

        public abstract Drawable[] Drawables { get; }

        #endregion

        #region Constructors

        public Scene(RenderWindow window)
        {
            _window = window;
            Init();
        }

        #endregion

        #region Methods

        public virtual void Init()
        {
        }

        public virtual Scene Open()
        {
            _window.KeyPressed          += OnKeyPressed;
            _window.MouseButtonPressed  += OnMouseButtonPressed;
            _window.MouseButtonReleased += OnMouseButtonReleased;
            return this;
        }

        public virtual void Close()
        {
            _window.KeyPressed          -= OnKeyPressed;
            _window.MouseButtonPressed  -= OnMouseButtonPressed;
            _window.MouseButtonReleased -= OnMouseButtonReleased;
        }

        public virtual void Progress()
        {
        }

        #endregion

        #region Event Handlers

        protected abstract void OnKeyPressed(object sender, KeyEventArgs e);
        protected abstract void OnMouseButtonPressed(object sender, MouseButtonEventArgs e);
        protected abstract void OnMouseButtonReleased(object sender, MouseButtonEventArgs e);

        #endregion
    }
}
