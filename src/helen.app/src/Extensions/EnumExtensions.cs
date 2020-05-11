using System;

namespace Helen.App.Extensions
{
    public static class FlagExtensions
    {
        public static T TurnOn<T>(this Enum type, T value)
        {
            return (T)(object)(((int)(object)type | (int)(object)value));
        }

        public static T TurnOff<T>(this Enum type, T value)
        {
            return (T)(object)(((int)(object)type & ~(int)(object)value));
        }

        public static T Toggle<T>(this Enum type, T value)
        {
            return (((int)(object)type & (int)(object)value) == (int)(object)value)
                 ? type.TurnOff(value)
                 : type.TurnOn(value);
        }
    }
}
