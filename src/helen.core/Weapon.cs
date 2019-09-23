﻿using System.ComponentModel;
using System;

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

        public WeaponProperties Properties { get; set; }

        public bool IsPiercing => Properties.HasFlag(WeaponProperties.Piercing);

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

        public Weapon(Guid id, string name, int effectiveness, int defense, int speed, WeaponProperties properties)
            : this(id, name, effectiveness, defense, speed)
        {
            Properties = properties;
        }

        #endregion
    }
}