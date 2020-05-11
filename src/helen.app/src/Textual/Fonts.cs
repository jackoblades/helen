using SFML.Graphics;

namespace Helen.App.Textual
{
    public static class Fonts
    {
        #region Properties

        public static Font FontTitle;
        public static Font FontBody;
        public static Font FontCredit;
        public static Font FontUnicode;

        #endregion

        #region Methods

        public static void Init()
        {
            FontTitle   = new Font("res/ttf/Ruritania/Ruritania.ttf");
            FontBody    = new Font("res/ttf/Penshurst/penshurs.ttf");
            FontCredit  = new Font("res/ttf/SourceSerifPro/SourceSerifPro-Regular.ttf");
            FontUnicode = new Font("res/ttf/NewAthenaUnicode/new_athena_unicode.ttf");
        }

        #endregion
    }
}
