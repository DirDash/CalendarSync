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
            int autosyncIntervalSec = SynchronizationConfigManager.AutosyncIntervalInMinutes * 10;
            SynchronizationConfigManager.AutosyncIntervalInMinutes = autosyncIntervalSec;
            Assert.Equal(ConfigurationManager.AppSettings["autosyncIntervalSec"], SynchronizationConfigManager.AutosyncIntervalInMinutes.ToString());
        }
    }
}
