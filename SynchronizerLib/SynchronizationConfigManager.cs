using System;
using System.Configuration;

namespace SynchronizerLib
{
    public static class SynchronizationConfigManager
    {
        private static readonly string _settingSection = "appSettings";
        private static readonly string _syncIntervalInDaysKey = "syncIntervalInDays";
        private static readonly string _autosyncModeKey = "autosync";
        private static readonly string _autosyncIntervalInMinKey = "autosyncIntervalMin";        

        private static int _synchronizationIntervalInDays;
        private static bool _autosyncronizationMode;
        private static int _autosyncIntervalInMinutes;

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

        public static int AutosyncIntervalInMinutes
        {
            get { return _autosyncIntervalInMinutes; }
            set
            {
                _autosyncIntervalInMinutes = value;
                ChangeConfigValue(_autosyncIntervalInMinKey, value.ToString());
            }
        }

        private static void LoadConfigKeys()
        {
            _synchronizationIntervalInDays = int.Parse(ConfigurationManager.AppSettings[_syncIntervalInDaysKey]);
            switch (ConfigurationManager.AppSettings[_autosyncModeKey].ToLower())
            {
                case "false":
                    _autosyncronizationMode = false;
                    break;
                case "true":
                    _autosyncronizationMode = true;
                    break;
            }
            _autosyncIntervalInMinutes = int.Parse(ConfigurationManager.AppSettings[_autosyncIntervalInMinKey]);
        }

        private static void ChangeConfigValue(string configKey, string newConfigValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[configKey].Value = newConfigValue;
            currentConfig.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_settingSection);
            LoadConfigKeys();
        }
    }
}
