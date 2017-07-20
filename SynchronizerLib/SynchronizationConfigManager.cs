using System;
using System.Configuration;

namespace SynchronizerLib
{
    public static class SynchronizationConfigManager
    {
        private static readonly string _settingSection = "appSettings";
        private static readonly string _syncIntervalInDaysKey = "syncIntervalInDays";
        private static readonly string _autosyncModeKey = "autosyncs";
        private static readonly string _autosyncIntervalInSecKey = "autosyncIntervalSec";        

        private static int _synchronizationIntervalInDays;
        private static bool _autosyncronizationMode;
        private static int _autosyncIntervalInSeconds;

        static SynchronizationConfigManager()
        {
            LoadConfigKeys();
        }

        public static int SynchronizationIntervalInDays
        {
            get { return _synchronizationIntervalInDays; }
            set
            {
                _synchronizationIntervalInDays = value;
                ChangeConfigValue(_syncIntervalInDaysKey, value.ToString());
            }
        }

        public static bool AutosyncronizationMode
        {
            get { return _autosyncronizationMode; }
            set
            {
                _autosyncronizationMode = value;
                ChangeConfigValue(_autosyncModeKey, value.ToString());
            }
        }

        public static int AutosyncIntervalInSeconds
        {
            get { return _autosyncIntervalInSeconds; }
            set
            {
                _autosyncIntervalInSeconds = value;
                ChangeConfigValue(_autosyncIntervalInSecKey, value.ToString());
            }
        }

        private static void LoadConfigKeys()
        {
            _synchronizationIntervalInDays = int.Parse(ConfigurationManager.AppSettings[_syncIntervalInDaysKey]);
            switch (ConfigurationManager.AppSettings[_autosyncModeKey])
            {
                case "False":
                    _autosyncronizationMode = false;
                    break;
                case "True":
                    _autosyncronizationMode = true;
                    break;
            }
            _autosyncIntervalInSeconds = int.Parse(ConfigurationManager.AppSettings[_autosyncIntervalInSecKey]);
        }

        private static void ChangeConfigValue(string configKey, string newConfigValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[configKey].Value = newConfigValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_settingSection);
        }
    }
}
