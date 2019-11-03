using System;
using System.Linq;

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

        public BattleState State()
        {
            var state = BattleState.None;

            if (BattlePartyA != null && BattlePartyB != null)
            {
                bool partyA = BattlePartyA.Any(x => x.IsAlive);
                bool partyB = BattlePartyB.Any(x => x.IsAlive);
                state = ( partyA &&  partyB) ? BattleState.Ongoing
                      : ( partyA && !partyB) ? BattleState.Victory
                      : (!partyA &&  partyB) ? BattleState.Defeat
                      :                        BattleState.Draw;
            }

            return state;
        }

        #endregion
    }
}
