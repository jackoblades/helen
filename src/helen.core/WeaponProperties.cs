using System.Text;
using System;

namespace Helen.Core
{
    [Flags]
    public enum WeaponProperties
    {
        None         = 0x0000,
        Piercing     = 0x0001,
        Healing      = 0x0002,
        TrueHealing  = 0x0003,
        Stun         = 0x0004,
    }

    public static class WeaponPropertiesExtensions
    {
        public static string Name(this WeaponProperties property, Weapon weapon)
        {
            var sb = new StringBuilder();
            var prop = property;

            // Composite flags.
            if (prop.HasFlag(WeaponProperties.TrueHealing))
            {
                sb.Append($"{Enum.GetName(typeof(WeaponProperties), WeaponProperties.TrueHealing)}, ");
                prop &= ~(WeaponProperties.Piercing | WeaponProperties.Healing);
            }

            // Independent flags.
            if (prop.HasFlag(WeaponProperties.Piercing))
                sb.Append($"{Enum.GetName(typeof(WeaponProperties), WeaponProperties.Piercing)}, ");
            if (prop.HasFlag(WeaponProperties.Healing))
                sb.Append($"{Enum.GetName(typeof(WeaponProperties), WeaponProperties.Healing)}, ");
            if (prop.HasFlag(WeaponProperties.Stun))
                sb.Append($"{Enum.GetName(typeof(WeaponProperties), WeaponProperties.Stun)} {weapon.Stun}, ");

            return sb.ToString().TrimEnd(new char[2] { ' ', ',' });
        }
    }
}
