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

        public void Receive(Weapon weapon)
        {
            int damage = Math.Max(0, weapon.Power - CurrentWeapon.Defense);
            Health = Health - damage;
        }

        #endregion
    }
}