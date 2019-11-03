using System;

namespace Helen.Core
{
    public class Battle
    {
        #region Static Members

        public const int MaxPartySize = 4;

        #endregion

        #region Properties

        public Actor[] PartyA;
        public Actor[] PartyB;

        public BattleActor[] BattlePartyA;
        public BattleActor[] BattlePartyB;

        #endregion

        #region Constructors

        public Battle()
        {
            BattlePartyA = new BattleActor[MaxPartySize];
            BattlePartyB = new BattleActor[MaxPartySize];
        }

        public Battle(Actor[] partyA, Actor[] partyB) : this()
        {
            PartyA = partyA;
            PartyB = partyB;
            Init();
        }

        #endregion

        #region Methods

        public void Init()
        {
            for (int i = 0; i < MaxPartySize; ++i)
            {
                BattlePartyA[i] = PartyA[i]?.Battle();
                BattlePartyB[i] = PartyB[i]?.Battle();
            }
        }

        #endregion
    }
}
