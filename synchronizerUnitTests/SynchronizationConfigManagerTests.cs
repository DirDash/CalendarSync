using System;
using Xunit;
using Assert = Xunit.Assert;
using System.Configuration;
using SynchronizerLib;

namespace SynchronizerLibUnitTests
{
    public class SynchronizationConfigManagerTests
    {
        [Fact]
        public void SetIsCorrect_ValueChanged()
        {
            string newGoogleColorID = SynchronizationConfigManager.GoogleCategoryColorIDForImported + "0";
            SynchronizationConfigManager.GoogleCategoryColorIDForImported = newGoogleColorID;
            Assert.Equal(ConfigurationManager.AppSettings["googleColorIDForImported"], SynchronizationConfigManager.GoogleCategoryColorIDForImported);
        }
    }
}
