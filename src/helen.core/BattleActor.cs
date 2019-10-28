using System;
namespace Helen.Core
{
    public class BattleActor
    {
        #region Properties

        public string Name { get; set; }

        public int Health { get; set; }

        public int HealthMax { get; set; }

        public int Wait { get; set; }

        public Weapon CurrentWeapon { get; set; }

        public BattleActor CurrentTarget { get; set; }

        public bool IsAlive => Health > 0;

        #endregion

        #region Constructors

        public BattleActor()
        {
        }

        public BattleActor(string name, int health, int healthMax)
        {
            Name      = name;
            Health    = health;
            HealthMax = healthMax;
        }

        #endregion

        #region Methods

        public void Select(Weapon weapon)
        {
            CurrentWeapon = weapon;
            Wait          = weapon.Speed;
        }

        public void Select(BattleActor target)
        {
            CurrentTarget = target;
        }

        public void Act()
        {
            if (--Wait == 0)
            {
                CurrentTarget.Receive(CurrentWeapon);
                CurrentTarget = null;
                CurrentWeapon = null;
            }
        }

        public void Receive(Weapon weapon)
        {
            int defense = (weapon.IsPiercing) ? 0 : CurrentWeapon.Defense;
            int damage = Math.Max(0, weapon.Effectiveness - defense);
            Health = (weapon.IsHealing) ? Math.Min(HealthMax, Health + damage)
                     /*   IsDamage   */ : Math.Max(0,         Health - damage);
        }

        #endregion
    }
}
