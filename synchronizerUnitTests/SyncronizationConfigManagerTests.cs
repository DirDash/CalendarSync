using System;
using Xunit;
using Assert = Xunit.Assert;
using System.Configuration;
using SynchronizerLib;

namespace SynchronizerLibUnitTests
{
    public class SyncronizationConfigManagerTests
    {
        [Fact]
        public void SetIsCorrect_ValueChanged()
        {
            string newGoogleColorID = SyncronizationConfigManager.GoogleCategoryColorIDForImported + "0";
            SyncronizationConfigManager.GoogleCategoryColorIDForImported = newGoogleColorID;
            Assert.Equal(ConfigurationManager.AppSettings["googleColorIDForImported"], SyncronizationConfigManager.GoogleCategoryColorIDForImported);
        }
    }
}
