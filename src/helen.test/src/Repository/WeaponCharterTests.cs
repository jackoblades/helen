using Helen.App.Repository;
using Helen.App.Repository.Charters;
using Helen.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Helen.Test.Repository
{
    public class WeaponCharterTests
    {
        #region Methods

        private async Task<WeaponCharter> InitAsync(string testname)
        {
            Directory.CreateDirectory($"./{nameof(WeaponCharterTests)}/");
            var filepath = $"./{nameof(WeaponCharterTests)}/{testname}.sqlite3";
            File.Delete(filepath);
            return new WeaponCharter(await new Orm(filepath).InitAsync());
        }

        #endregion

        #region Tests

        [Fact]
        public async Task Weapon_UpsertRead()
        {
            // Arrange.
            var charter = await InitAsync(nameof(Weapon_UpsertRead));

            var expected = new Weapon()
            {
                Id = Guid.NewGuid(),
                Name = "TestWeapon",
                Effectiveness = 10,
                Defense       = 50,
                Speed         = 50000,
                Stun          = 3001,
                Properties = WeaponProperties.TrueHealing,
            };

            // Act.
            await charter.UpsertAsync(expected);
            var actual = await charter.ReadAsync();

            // Assert.
            Assert.Equal(expected.Id,            actual.Id);
            Assert.Equal(expected.Name,          actual.Name);
            Assert.Equal(expected.Effectiveness, actual.Effectiveness);
            Assert.Equal(expected.Defense,       actual.Defense);
            Assert.Equal(expected.Speed,         actual.Speed);
            Assert.Equal(expected.Stun,          actual.Stun);
            Assert.Equal(expected.Properties,    actual.Properties);
        }

        #endregion
    }
}
