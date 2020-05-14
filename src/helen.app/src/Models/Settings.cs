using Helen.App.Extensions;
using Helen.App.Repository.Charters;
using System;
using System.Threading.Tasks;

namespace Helen.App.Models
{
    public class Settings
    {
        #region Properties

        #region Static

        /// <summary>
        /// Static instance.
        /// </summary>
        public static Settings Instance { get; set; }

        /// <summary>
        /// Static backup.
        /// </summary>
        public static Settings Backup { get; set; }

        #endregion

        #region Member Variables

        /// <summary>
        /// Unique ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Vertical Synchronization.
        /// </summary>
        public Preferences Preferences { get; set; }

        /// <summary>
        /// Volume at which the music plays.
        /// </summary>
        public int MusicVolume { get; set; }

        #endregion

        #region Helpers

        /// <summary>
        /// True if Vsync is active; False otherwise.
        /// </summary>
        public bool Vsync => Preferences.HasFlag(Preferences.Vsync);

        /// <summary>
        /// Volume at which the music plays.
        /// 0 <= <see cref=MusicVolumeSafe/> <= 100.
        /// </summary>
        public int MusicVolumeSafe => MusicVolume.WithBounds(0, 100);

        #endregion

        #endregion

        #region Constructors

        public Settings()
        {
        }

        public Settings(Settings settings)
        {
            Id          = settings.Id;
            Preferences = settings.Preferences;
            MusicVolume = settings.MusicVolume;
        }

        #endregion

        #region Methods

        public static void Init()
        {
            Instance = Load().Result ?? Generate();
        }

        public void Toggle(Preferences pref)
        {
            Preferences = Preferences.Toggle(pref);
        }

        private static async Task<Settings> Load()
        {
            return await SettingsCharter.ReadAsync();
        }

        public static async Task Save()
        {
            await SettingsCharter.UpsertAsync(Instance);
        }

        private static Settings Generate()
        {
            return new Settings()
            {
                Id = Guid.Empty, // Enforce a single persistent instance.
                Preferences = Preferences.Vsync,
            };
        }

        #endregion
    }
}
