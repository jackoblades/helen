namespace Helen.App
{
    public static class Fonts
    {
        #region Properties

        public static Font FontTitle;
        public static Font FontBody;
        public static Font FontCredit;

        #endregion

        #region Methods

        public static void Init()
        {
            FontTitle  = new Font("res/ttf/Ruritania/Ruritania.ttf");
            FontBody   = new Font("res/ttf/Penshurst/penshurs.ttf");
            FontCredit = new Font("res/ttf/SourceSerifPro/SourceSerifPro-Regular.ttf");
        }

        #endregion
    }
}
