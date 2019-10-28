using System;

namespace Helen.Core
{
    public class Actor
    {
        #region Static Members

        public const int MaxWeaponCount = 8;

        #endregion

        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Weapon[] Weapons { get; set; }

        public int Health { get; set; }

        public int HealthMax { get; set; }

        #endregion

        #region Constructors

        public Actor()
        {
            Weapons = new Weapon[MaxWeaponCount];
        }

        public Actor(Guid id, string name, int healthMax) : this()
        {
            Id        = id;
            Name      = name;
            HealthMax = healthMax;
            Health    = healthMax;
        }

        #endregion

        #region Methods

        public bool Add(Weapon weapon)
        {
            bool result = false;

            for (int i = 0; i < MaxWeaponCount; ++i)
            {
                if (result = (Weapons[i] == null))
                {
                    Weapons[i] = weapon;
                    break;
                }
            }

            return result;
        }

        public BattleActor Battle()
        {
            return new BattleActor(Name, Health, HealthMax);
        }

        #endregion
    }
}
