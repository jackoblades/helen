using Helen.App.Models;
using Helen.App.Repository;
using Helen.App.Repository.Charters;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Helen.Test.Repository
{
    public class SettingsCharterTests
    {
        #region Methods

        private async Task<SettingsCharter> InitAsync(string testname)
        {
            Directory.CreateDirectory($"./{nameof(SettingsCharterTests)}/");
            var filepath = $"./{nameof(SettingsCharterTests)}/{testname}.sqlite3";
            File.Delete(filepath);
            return new SettingsCharter(await new Orm(filepath).InitAsync());
        }

        #endregion

        #region Tests

        [Fact]
        public async Task Settings_UpsertRead()
        {
            // Arrange.
            var charter = await InitAsync(nameof(Settings_UpsertRead));

            var expected = new Settings()
            {
                Id = Guid.NewGuid(),
                Preferences = Preferences.Vsync,
                MusicVolume = 50,
            };

            // Act.
            await charter.UpsertAsync(expected);
            var actual = await charter.ReadAsync();

            // Assert.
            Assert.Equal(expected.Id,          actual.Id);
            Assert.Equal(expected.Preferences, actual.Preferences);
            Assert.Equal(expected.MusicVolume, actual.MusicVolume);
        }

        #endregion
    }
}
