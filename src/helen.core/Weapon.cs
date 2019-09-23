using System;

namespace Helen.Core
{
    public class Weapon
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Power { get; set; }

        public int Defense { get; set; }

        public int Speed { get; set; }

        #endregion

        #region Constructors

        public Weapon()
        {
        }

        public Weapon(Guid id, string name, int power, int defense, int speed)
        {
            Id      = id;
            Name    = name;
            Power   = power;
            Defense = defense;
            Speed   = speed;
        }

        #endregion
    }
}
