namespace helen.app.src
{
    public class Options
    {
        #region Properties

        public static Options Instance { get; set; }

        public bool Vsync { get; set; }

        #endregion

        #region Constructors

        public Options()
        {
        }

        #endregion

        #region Methods

        public void Init()
        {
            Instance = Load() ?? Generate();
        }

        public Options Load()
        {
            return new Options();
        }

        public Options Generate()
        {
            return new Options()
            {
                Vsync = true,
            };
        }

        #endregion
    }
}