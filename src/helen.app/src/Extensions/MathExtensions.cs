using System;

namespace Helen.App.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// With a specified <see cref=int/> x:
        /// Return the greater of x or its lower bound, or
        /// Return the lesser  of x or its upper bound.
        /// </summary>
        public static int WithBounds(this int x, int lower, int upper)
        {
            return Math.Min(Math.Max(x, lower), upper);
        }
    }
}
