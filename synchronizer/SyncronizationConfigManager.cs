using System;
using System.Configuration;

namespace synchronizer
{
    public static class SyncronizationConfigManager
    {
        private static int _synchronizationIntervalInDays;
        private static bool _autosyncronizationMode;
        private static int _autosyncIntervalInSeconds;
        private static string _outlookCategoryForImported;
        private static string _googleCategoryColorIDForImported;

        public static int SynchronizationIntervalInDays
        {
            get { return _synchronizationIntervalInDays; }
            set
            {
                _synchronizationIntervalInDays = value;
                ChangeConfigValue("syncIntervalInDays", value.ToString());
            }
        }

        public static bool AutosyncronizationMode
        {
            get { return _autosyncronizationMode; }
            set
            {
                _autosyncronizationMode = value;
                ChangeConfigValue("autosync", value.ToString());
            }
        }

        public static int AutosyncIntervalInSeconds
        {
            get { return _autosyncIntervalInSeconds; }
            set
            {
                _autosyncIntervalInSeconds = value;
                ChangeConfigValue("autosyncIntervalSec", value.ToString());
            }
        }

        public static string OutlookCategoryForImported
        {
            get { return _outlookCategoryForImported; }
            set
            {
                _outlookCategoryForImported = value;
                ChangeConfigValue("outlookCategoryForImported", value);
            }
        }

        public static string GoogleCategoryColorIDForImported
        {
            get { return _googleCategoryColorIDForImported; }
            set
            {
                _googleCategoryColorIDForImported = value;
                ChangeConfigValue("googleColorIDForImported", value);
            }
        }

        static SyncronizationConfigManager()
        {
            LoadConfigKeys();
        }

        private static void LoadConfigKeys()
        {
            SynchronizationIntervalInDays = int.Parse(ConfigurationManager.AppSettings["syncIntervalInDays"]);
            switch (ConfigurationManager.AppSettings["autosync"])
            {
                case "False":
                    AutosyncronizationMode = false;
                    break;
                case "True":
                    AutosyncronizationMode = true;
                    break;
            }
            AutosyncIntervalInSeconds = int.Parse(ConfigurationManager.AppSettings["autosyncIntervalSec"]);
            OutlookCategoryForImported = ConfigurationManager.AppSettings["outlookCategoryForImported"];
            GoogleCategoryColorIDForImported = ConfigurationManager.AppSettings["googleColorIDForImported"];
        }

        private static void ChangeConfigValue(string configKey, string newConfigValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[configKey].Value = newConfigValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
