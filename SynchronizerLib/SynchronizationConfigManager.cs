using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchronizerLib
{
    public static class SynchronizationConfigManager
    {
        private static int _synchronizationIntervalInDays;
        private static bool _autosyncronizationMode;
        private static int _autosyncIntervalInSeconds;
        
        private static string _outlookFilter;
        private static EventTransformation _outlookOutTransformation;
        private static EventTransformation _outlookInTransformation;
        
        private static string _googleFilter;
        private static EventTransformation _googleOutTransformation;
        private static EventTransformation _googleInTransformation;

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

        public static string OutlookFilter
        {
            get { return _outlookFilter; }
        }

        public static EventTransformation OutlookOutTransformation
        {
            get { return _outlookOutTransformation; }
        }

        public static EventTransformation OutlookInTransformation
        {
            get { return _outlookInTransformation; }
        }

        public static string GoogleFilter
        {
            get { return _googleFilter; }
        }

        public static EventTransformation GoogleOutTransformation
        {
            get { return _googleOutTransformation; }
        }

        public static EventTransformation GoogleInTransformation
        {
            get { return _googleInTransformation; }
        }

        static SynchronizationConfigManager()
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
            _outlookFilter = ConfigurationManager.AppSettings["outlookFilter"];
            _outlookOutTransformation = new EventTransformation(ConfigurationManager.AppSettings["outlookOutTransformationCondition"],
                                                                ConfigurationManager.AppSettings["outlookOutTransformation"]);
            _outlookInTransformation = new EventTransformation(ConfigurationManager.AppSettings["outlookInTransformationCondition"],
                                                                ConfigurationManager.AppSettings["outlookInTransformation"]);
            _googleFilter = ConfigurationManager.AppSettings["googleFilter"];
            _googleOutTransformation = new EventTransformation(ConfigurationManager.AppSettings["googleOutTransformationCondition"],
                                                                ConfigurationManager.AppSettings["googleOutTransformation"]);
            _googleInTransformation =  new EventTransformation(ConfigurationManager.AppSettings["googleInTransformationCondition"],
                                                                ConfigurationManager.AppSettings["googleInTransformation"]);
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
