namespace Helen.App.Repository.Charters
{
    public abstract class Charter
    {
        #region Properties
        
        protected Orm _orm;

        #endregion

        #region Constructors

        public Charter(Orm orm)
        {
            _orm = orm;
        }

        #endregion
    }
}
