using Helen.App.Models;
using SFML.Audio;

namespace Helen.App.Services
{
    public static class Tracks
    {
        public static string Silence => "res/sfx/silence.ogg";
        public static string Title => "res/sfx/Kevin_MacLeod/Moorland.ogg";
    }

    public class MusicService
    {
        #region Properties

        public static Music Music { get; set; }

        public static string CurrentFile { get; set; }

        #endregion

        #region Methods

        public static void Init()
        {
            CurrentFile = Tracks.Silence;
            Music = new Music(Tracks.Silence);
        }

        public static void Close()
        {
            Music?.Stop();
        }

        public static void Swap(string filename)
        {
            if (CurrentFile != filename)
            {
                CurrentFile = filename;
                Music.Stop();
                Music = new Music(filename) { Volume = Settings.Instance.MusicVolumeSafe };
                Music.Play();
            }
        }

        #endregion
    }
}
