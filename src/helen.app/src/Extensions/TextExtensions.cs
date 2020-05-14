using SFML.Graphics;
using SFML.System;

namespace Helen.App.Extensions
{
    /// <summary>
    /// Quality of life extensions for <see cref="Text"/>.
    /// </summary>
    public static class TextExtensions
    {
        /// <summary>
        /// True if the specified point lies within this <see cref="Text"/> bounds;
        /// False otherwise.
        /// </summary>
        public static bool Contains(this Text text, Vector2i position)
        {
            return text.GetGlobalBounds().Contains(position.X, position.Y);
        }

        /// <summary>
        /// True if the specified point lies within this <see cref="Text"/> bounds;
        /// False otherwise.
        /// </summary>
        public static bool Contains(this Text text, int x, int y)
        {
            return text.GetGlobalBounds().Contains(x, y);
        }

        /// <summary>
        /// If the specified position lies within this <see cref="Text"/> space, underline it.
        /// </summary>
        public static void Indicate(this Text text, Vector2i position)
        {
            text.Style = (text.Contains(position.X, position.Y))
                       ? Text.Styles.Underlined
                       : Text.Styles.Regular;
        }

        /// <summary>
        /// If the specified position lies within this <see cref="Text"/> space,
        /// show its indicator, unless it is under a click.
        /// </summary>
        public static void Indicate(this Text indicator, Text text, Vector2i position)
        {
            indicator.DisplayedString = text.Contains(position)
                                      ? (indicator.DisplayedString == "x")
                                      ? "x" : "o" : string.Empty;
        }
    }
}
