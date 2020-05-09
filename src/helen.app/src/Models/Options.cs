using Helen.App.Extensions;
using System;

namespace Helen.App.Models
{
    public class Options
    {
        #region Properties

        /// <summary>
        /// Static instance.
        /// </summary>
        public static Options Instance { get; set; }

        /// <summary>
        /// Volume at which the music plays.
        /// </summary>
        public int MusicVolume { get; set; }

        /// <summary>
        /// Volume at which the music plays.
        /// 0 <= <see cref=MusicVolumeSafe/> <= 1.
        /// </summary>
        public int MusicVolumeSafe => MusicVolume.WithBounds(0, 100);

        /// <summary>
        /// Vertical Synchronization.
        /// </summary>
        /// <value></value>
        public bool Vsync { get; set; }

        #endregion

        #region Constructors

        public Options()
        {
        }

        #endregion

        #region Methods

        public static void Init()
        {
            Instance = Load() ?? Generate();
        }

        private static Options Load()
        {
            return null;
        }

        private static Options Generate()
        {
            return new Options()
            {
                Vsync = true,
            };
        }

        #endregion
    }
}
