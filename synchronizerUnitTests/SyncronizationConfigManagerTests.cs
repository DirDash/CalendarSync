using System;
using Xunit;
using Assert = Xunit.Assert;
using System.Configuration;

namespace synchronizerUnitTests
{
    public class SyncronizationConfigManagerTests
    {
        [Fact]
        public void SetIsCorrect_ValueChanged()
        {
            string newGoogleColorID = synchronizer.SyncronizationConfigManager.GoogleCategoryColorIDForImported + "0";
            synchronizer.SyncronizationConfigManager.GoogleCategoryColorIDForImported = newGoogleColorID;
            Assert.Equal(ConfigurationManager.AppSettings["googleColorIDForImported"], synchronizer.SyncronizationConfigManager.GoogleCategoryColorIDForImported);
        }
    }
}
