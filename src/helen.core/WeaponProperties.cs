using System.Text;
using System;

namespace Helen.Core
{
    [Flags]
    public enum WeaponProperties
    {
        None     = 0x0000,
        Piercing = 0x0001,
    }

    public static class WeaponPropertiesExtensions
    {
        public static string Name(this WeaponProperties property)
        {
            var sb = new StringBuilder();

            if (property.HasFlag(WeaponProperties.Piercing))
                sb.Append($"{Enum.GetName(typeof(WeaponProperties), WeaponProperties.Piercing)}, ");

            return sb.ToString().TrimEnd(new char[2] { ' ', ','});
        }
    }
}
