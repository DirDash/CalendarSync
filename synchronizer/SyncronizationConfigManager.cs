using System;
using System.Configuration;
using System.Collections.Generic;

namespace synchronizer
{
    public static class SyncronizationConfigManager
    {
        public static bool Autosyncronization { get; private set; }
        public static int AutosyncIntervalInSeconds { get; private set; }
        public static string OutlookCategoryForImported { get; private set; }
        public static string GoogleCategoryColorIDForImported { get; private set; }

        static SyncronizationConfigManager()
        {
            RefreshConfigKeys();
        }

        private static void RefreshConfigKeys()
        {
            switch (ConfigurationManager.AppSettings["autosync"])
            {
                case "false":
                    Autosyncronization = false;
                    break;
                case "true":
                    Autosyncronization = true;
                    break;
            }
            AutosyncIntervalInSeconds = int.Parse(ConfigurationManager.AppSettings["autosyncIntervalSec"]);
            OutlookCategoryForImported = ConfigurationManager.AppSettings["outlookCategoryForImported"];
            GoogleCategoryColorIDForImported = ConfigurationManager.AppSettings["googleColorIDForImported"];
        }

        public static void ChangeConfigValue(string configKey, string newConfigValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[configKey].Value = newConfigValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            RefreshConfigKeys();
        }
    }
}
