using System;
using System.ComponentModel;

namespace Helen.Core
{
    public class Weapon
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Effectiveness { get; set; }

        public int Defense { get; set; }

        public int Speed { get; set; }

        public int Stun { get; set; }

        public WeaponProperties Properties { get; set; }

        public string PropertySummary => Properties.Name(this);

        public bool IsPiercing => Properties.HasFlag(WeaponProperties.Piercing);

        public bool IsHealing => Properties.HasFlag(WeaponProperties.Healing);

        #endregion

        #region Constructors

        public Weapon()
        {
        }

        public Weapon(Guid id, string name, int effectiveness, int defense, int speed)
            : this()
        {
            Id            = id;
            Name          = name;
            Effectiveness = effectiveness;
            Defense       = defense;
            Speed         = speed;
        }

        public Weapon(Guid id, string name, int effectiveness, int defense, int speed, int stun)
            : this(id, name, effectiveness, defense, speed)
        {
            Stun = stun;
            Properties |= WeaponProperties.Stun;
        }

        public Weapon(Guid id, string name, int effectiveness, int defense, int speed, WeaponProperties properties)
            : this(id, name, effectiveness, defense, speed)
        {
            Properties = properties;
        }

        public Weapon(Guid id, string name, int effectiveness, int defense, int speed, int stun, WeaponProperties properties)
            : this(id, name, effectiveness, defense, speed, stun)
        {
            Properties = properties;
        }

        #endregion
    }
}
