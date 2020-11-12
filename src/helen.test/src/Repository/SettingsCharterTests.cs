using Helen.App.Repository;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Helen.Test.Repository
{
    public class SettingsCharterTests
    {
        #region Methods

        private async Task<Orm> InitAsync(string testname)
        {
            Directory.CreateDirectory($"./{nameof(SettingsCharterTests)}/");
            var filepath = $"./{nameof(SettingsCharterTests)}/{testname}.sqlite3";
            File.Delete(filepath);
            return await new Orm(filepath).InitAsync();
        }

        #endregion

        #region Tests

        [Fact]
        public async Task Test1()
        {
            var orm = await InitAsync(nameof(Test1));
        }

        #endregion
    }
}
