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
            int autosyncIntervalSec = SynchronizationConfigManager.AutosyncIntervalInSeconds * 10;
            SynchronizationConfigManager.AutosyncIntervalInSeconds = autosyncIntervalSec;
            Assert.Equal(ConfigurationManager.AppSettings["autosyncIntervalSec"], SynchronizationConfigManager.AutosyncIntervalInSeconds.ToString());
        }
    }
}
